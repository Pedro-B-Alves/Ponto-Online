CREATE DATABASE IF NOT EXISTS ponto;

USE ponto;

CREATE TABLE IF NOT EXISTS horarioTrabalho (
    idHorario INT AUTO_INCREMENT PRIMARY KEY,
    horarioEntrada TIME,
    horarioPausa TIME,
    horarioRetorno TIME,
    horarioSaida TIME
);

CREATE TABLE IF NOT EXISTS equipe (
    idEquipe INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR (100)
);

CREATE TABLE IF NOT EXISTS usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    idHorario INT,
    idEquipe INT,
    nome VARCHAR (255) NOT NULL,
    foto VARCHAR (255),
    cargo VARCHAR (255),
    email VARCHAR (255) NOT NULL,
    senha VARCHAR (255) NOT NULL,
    quantidadeFerias INT,
    FOREIGN KEY (idHorario) REFERENCES horarioTrabalho(idHorario),
	FOREIGN KEY (idEquipe) REFERENCES equipe(idEquipe)
);

CREATE TABLE IF NOT EXISTS solicitacaoFerias (
    idSolicitacao INT AUTO_INCREMENT PRIMARY KEY,
    idUsuario INT,
    mesDesejado VARCHAR (50),
    aprovado INT,
    FOREIGN KEY (idUsuario) REFERENCES usuario(idUsuario)
);

CREATE TABLE IF NOT EXISTS notificacao (
    idNotificacao INT AUTO_INCREMENT PRIMARY KEY,
    idUsuario INT,
    mensagem VARCHAR (100),
    mes DATE,
    FOREIGN KEY (idUsuario) REFERENCES usuario(idUsuario)
);

CREATE TABLE IF NOT EXISTS registroPonto (
    idRegistro INT AUTO_INCREMENT PRIMARY KEY,
    idUsuario INT,
    mes DATE,
    horarioEntrada TIME,
    metodoEntrada VARCHAR (255),
    detalheEntrada VARCHAR (255),
    horarioPausa TIME,
    metodoPausa VARCHAR (255),
    detalhePausa VARCHAR(255),
    horarioRetorno TIME,
    metodoRetorno VARCHAR (255),
    detalheRetorno VARCHAR (255),
    horarioSaida TIME,
    metodoSaida VARCHAR (255),
    detalheSaida VARCHAR (255),
    horaExtra TIME,
    falta INT,
    atraso INT,
    FOREIGN KEY (idUsuario) REFERENCES usuario(idUsuario)
);
