const Usuario = require('../models/Usuario');
const AppError = require('../utils/appError');
const catchAsync = require('../utils/catchAsync');
const jwt = require('jsonwebtoken');

exports.criarUsuario = catchAsync(async (req, res, next) => {
  const { nome, idade, sexo, email, senha, endereco, telefone, cpf } = req.body;
  
  const novoUsuario = await Usuario.create({
    nome,
    idade,
    sexo,
    email,
    senha,
    endereco,
    telefone,
    cpf
  });
  
  // Remover senha do output
  novoUsuario.senha = undefined;
  
  res.status(201).json({
    status: 'success',
    data: {
      usuario: novoUsuario
    }
  });
});