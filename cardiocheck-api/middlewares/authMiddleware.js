const jwt = require('jsonwebtoken');
const Usuario = require('../models/Usuario');

// Proteger rotas - verificar token JWT
exports.protect = async (req, res, next) => {
  let token;
  
  if (req.headers.authorization && req.headers.authorization.startsWith('Bearer')) {
    token = req.headers.authorization.split(' ')[1];
  }
  
  if (!token) {
    return res.status(401).json({
      status: 'fail',
      message: 'Você não está logado. Por favor, faça login para acessar.'
    });
  }
  
  try {
    // Verificar token
    const decoded = jwt.verify(token, process.env.JWT_SECRET);
    
    // Verificar se usuário ainda existe
    const currentUser = await Usuario.findById(decoded.id);
    if (!currentUser) {
      return res.status(401).json({
        status: 'fail',
        message: 'O usuário pertencente a este token não existe mais.'
      });
    }
    
    // Garantir acesso às rotas protegidas
    req.user = currentUser;
    next();
  } catch (err) {
    return res.status(401).json({
      status: 'fail',
      message: 'Token inválido ou expirado. Por favor, faça login novamente.'
    });
  }
};