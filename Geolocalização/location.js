// PositionStack API access key
const apiKey = '5abcb0b627b6519cbbe47c0d7feb8809';

// Coordinates (latitude and longitude)
const latitude = -23.62427221; // Substitua com sua latitude
const longitude = -46.70136611; // Substitua com sua longitude

// Construa a URL para a requisição à API PositionStack
const apiUrl = `http://api.positionstack.com/v1/reverse?access_key=${apiKey}&query=${latitude},${longitude}`;

// Faça a requisição à API
fetch(apiUrl)
  .then(response => {
    if (!response.ok) {
      throw new Error(`Erro de requisição: ${response.statusText}`);
    }
    return response.json();
  })
  .then(data => {
    // Extraia as informações de endereço da resposta da API
    const address = data.data[1];
    if (address) {
      console.log(address);
    } else {
      console.error('Nenhum endereço encontrado para as coordenadas fornecidas.');
    }
  })
  .catch(error => {
    console.error('Erro:', error.message);
  });
