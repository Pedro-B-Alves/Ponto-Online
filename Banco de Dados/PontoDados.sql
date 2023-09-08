USE ponto;

/*Tabela horarioTrabalho*/
INSERT INTO horarioTrabalho (horarioEntrada, horarioPausa, horarioRetorno, horarioSaida) VALUES ('09:00:00','14:30:00','15:30:00','18:00:00');
INSERT INTO horarioTrabalho (horarioEntrada, horarioPausa, horarioRetorno, horarioSaida) VALUES ('08:00:00','14:30:00','15:30:00','17:00:00');

/*Tabela equipe*/
INSERT INTO equipe (nome) VALUES ('Desenvolvimento');
INSERT INTO equipe (nome) VALUES ('RH');

/*Tabela usuario*/
INSERT INTO usuario (idHorario, idEquipe, nome, foto, cargo, email, senha, quantidadeFerias) VALUES (1, 1, 'pedro', 'pedro.png', 'Desenvolvimento', 'pedro@gmail.com', '$pedro123', 1);
INSERT INTO usuario (idHorario, idEquipe, nome, foto, cargo, email, senha, quantidadeFerias) VALUES (2,2, 'luis', 'luis.png', 'RH', 'luis@gmail.com', '#luis987', 1);

/*Tabela solicitacaoFerias*/
INSERT INTO solicitacaoFerias (idUsuario, mesDesejado, aprovado) VALUES (1, 'Agosto', 1);
INSERT INTO solicitacaoFerias (idUsuario, mesDesejado, aprovado) VALUES (2, 'Dezembro', 0);

/*Tabela notificacao*/
INSERT INTO notificacao (idUsuario, mensagem, mes) VALUES (2, 'Está próximo do horário de entrada', '2023-07-10');
INSERT INTO notificacao (idUsuario, mensagem, mes) VALUES (1, 'Está próximo do horário de entrada', '2023-07-10');

/*Tabela registroPonto*/
INSERT INTO registroPonto (idUsuario, mes, horarioEntrada, metodoEntrada, detalheEntrada, horarioPausa, metodoPausa, detalhePausa, horarioRetorno, metodoRetorno, detalheRetorno, horarioSaida, metodoSaida, detalheSaida, horaExtra, falta, atraso) VALUES (2, '2023-07-10', '08:00:00', 'QR Code', 'www.ponto.com/qrcode', '14:30:00', 'QR Code', 'www.ponto.com/qrcode', '15:30:00', 'Reconhecimento Facial', 'Reconhecimento.png', '17:00:00', 'QR Code', 'www.ponto.com/qrcode', '00:00:00', 0, 0);
INSERT INTO registroPonto (idUsuario, mes, horarioEntrada, metodoEntrada, detalheEntrada, horarioPausa, metodoPausa, detalhePausa, horarioRetorno, metodoRetorno, detalheRetorno, horarioSaida, metodoSaida, detalheSaida, horaExtra, falta, atraso) VALUES (1, '2023-07-10', '09:00:00', 'Geolocalização', 'Avenida Paulista', '14:30:00', 'QR Code', 'www.ponto.com/qrcode', '15:30:00', 'Reconhecimento Facial', 'Reconhecimento2.png', '18:00:00', 'QR Code', 'www.ponto.com/qrcode', '00:00:00', 0, 0);