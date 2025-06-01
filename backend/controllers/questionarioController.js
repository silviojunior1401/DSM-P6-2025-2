const Questionario = require('../models/Questionario');
const Avaliacao = require('../models/Avaliacao');
const catchAsync = require('../utils/catchAsync');

// Função para calcular o risco cardíaco (simplificada)
const calcularRiscoCardiaco = (questionario) => {
  // Esta é uma implementação simplificada. Em produção, use um modelo mais preciso.
  let score = 0;
  
  // Chest pain type (1-4)
  score += questionario.chestPainType * 2;
  
  // Resting blood pressure (normal < 120, high > 140)
  if (questionario.restingBloodPressure > 140) score += 3;
  else if (questionario.restingBloodPressure > 120) score += 1;
  
  // Serum cholesterol (normal < 200, high > 240)
  if (questionario.serumCholesterol > 240) score += 3;
  else if (questionario.serumCholesterol > 200) score += 1;
  
  // Fasting blood sugar (> 120 mg/dl)
  if (questionario.fastingBloodSugar === 1) score += 2;
  
  // Max heart rate (normal 60-100)
  if (questionario.maxHeartRate < 60 || questionario.maxHeartRate > 100) score += 2;
  
  // Exercise angina
  if (questionario.exerciseAngina === 1) score += 3;
  
  // Oldpeak (ST depression)
  score += questionario.oldpeak * 2;
  
  // ST slope
  score += questionario.stSlope * 2;
  
  // Determinar resultado (0 = baixo risco, 1 = alto risco)
  const resultado = score > 15 ? 1 : 0;
  
  // Gerar recomendação
  let recomendacao;
  if (resultado === 1) {
    recomendacao = 'Recomendamos que você consulte um cardiologista o mais breve possível.';
  } else {
    recomendacao = 'Seu risco cardíaco parece estar dentro da normalidade. Mantenha hábitos saudáveis.';
  }
  
  return { resultado, recomendacao };
};

exports.enviarQuestionario = catchAsync(async (req, res, next) => {
  const dadosQuestionario = req.body;
  dadosQuestionario.usuario = req.user.id;
  
  // Calcular resultado
  const { resultado, recomendacao } = calcularRiscoCardiaco(dadosQuestionario);
  dadosQuestionario.resultado = resultado;
  dadosQuestionario.recomendacao = recomendacao;
  
  // Salvar questionário
  const questionario = await Questionario.create(dadosQuestionario);
  
  // Adicionar ao histórico
  await Avaliacao.create({
    usuario: req.user.id,
    resultado,
    data: questionario.createdAt
  });
  
  res.status(200).json({
    status: 'success',
    data: {
      resultado,
      recomendacao
    }
  });
});