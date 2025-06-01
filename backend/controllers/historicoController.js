const Avaliacao = require('../models/Avaliacao');
const catchAsync = require('../utils/catchAsync');

exports.getHistorico = catchAsync(async (req, res, next) => {
  const historico = await Avaliacao.find({ usuario: req.user.id })
    .sort('-data')
    .select('-usuario -_id -__v');
  
  res.status(200).json({
    status: 'success',
    results: historico.length,
    data: {
      historico
    }
  });
});