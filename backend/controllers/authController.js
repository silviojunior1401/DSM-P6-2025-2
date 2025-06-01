const Usuario = require('../models/Usuario');
const AppError = require('../utils/appError');
const catchAsync = require('../utils/catchAsync');
const jwt = require('jsonwebtoken');

const signToken = id => {
  return jwt.sign({ id }, process.env.JWT_SECRET, {
    expiresIn: process.env.JWT_EXPIRES_IN
  });
};

exports.login = catchAsync(async (req, res, next) => {
  const { email, cpf, senha } = req.body;
  
  // 1) Verificar se email ou CPF e senha existem
  if (!senha || (!email && !cpf)) {
    return next(new AppError('Por favor, forneça email/CPF e senha!', 400));
  }
  
  // 2) Verificar se usuário existe e senha está correta
  let usuario;
  if (email) {
    usuario = await Usuario.findOne({ email }).select('+senha');
  } else {
    usuario = await Usuario.findOne({ cpf }).select('+senha');
  }
  
  if (!usuario || !(await usuario.correctPassword(senha, usuario.senha))) {
    return next(new AppError('Email/CPF ou senha incorretos', 401));
  }
  
  // 3) Se tudo ok, enviar token para cliente
  const token = signToken(usuario._id);
  
  res.status(200).json({
    status: 'success',
    token,
    tokenType: 'Bearer'
  });
});