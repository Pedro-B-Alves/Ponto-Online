import os
from flask import Flask, request, jsonify
import face_recognition
import requests
import shutil
import numpy as np
from PIL import Image
from io import BytesIO
from waitress import serve

app = Flask(__name__)
dataset_path = "dataset"


def load_known_faces():
    known_faces = []
    known_names = []

    for name in os.listdir(dataset_path):
        person_dir = os.path.join(dataset_path, name)
        if os.path.isdir(person_dir):
            for filename in os.listdir(person_dir):
                image_path = os.path.join(person_dir, filename)
                image = face_recognition.load_image_file(image_path)
                encoding = face_recognition.face_encodings(image)[0]
                known_faces.append(encoding)
                known_names.append(name)

    return known_faces, known_names


def recognize_face(image_url, known_faces, known_names):
    response = requests.get(image_url)
    image = Image.open(BytesIO(response.content))
    unknown_image = np.array(image)

    face_locations = face_recognition.face_locations(unknown_image)
    if not face_locations:
        return [], "Nenhum rosto detectado"

    unknown_encodings = face_recognition.face_encodings(unknown_image, face_locations)
    unknown_encoding = unknown_encodings[0]

    results = face_recognition.compare_faces(known_faces, unknown_encoding)
    matched_indexes = [i for i, result in enumerate(results) if result]

    if matched_indexes:
        names = [known_names[i] for i in matched_indexes]
        return names, "Cadastrado"
    else:
        return [], "Desconhecido"


@app.route('/recognize', methods=['POST'])
def recognize():
    data = request.get_json()
    if not data or 'image_url' not in data:
        return jsonify(error='Invalid request. Image URL is missing.'), 400

    known_faces, known_names = load_known_faces()
    names, message = recognize_face(data['image_url'], known_faces, known_names)

    return jsonify(names=names, message=message)


@app.route('/register', methods=['POST'])
def register():
    data = request.get_json()
    if not data or 'name' not in data or 'image_urls' not in data:
        return jsonify(error='Invalid request. Name or image URLs are missing.'), 400

    name = data['name']
    image_urls = data['image_urls']
    person_dir = os.path.join(dataset_path, name)
    os.makedirs(person_dir, exist_ok=True)

    for i, image_url in enumerate(image_urls):
        image_path = os.path.join(person_dir, f"{i}.jpg")
        response = requests.get(image_url, stream=True)
        with open(image_path, 'wb') as out_file:
            shutil.copyfileobj(response.raw, out_file)
        del response

    return jsonify(message='Pessoa cadastrada com sucesso.')


if __name__ == '__main__':
    serve(app, host="127.0.0.1", port=5000)