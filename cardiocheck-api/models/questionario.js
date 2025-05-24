const mongoose = require('mongoose');

const questionarioSchema = new mongoose.Schema({
  usuario: {
    type: mongoose.Schema.ObjectId,
    ref: 'Usuario',
    required: [true, 'Questionário deve pertencer a um usuário']
  },
  chestPainType: {
    type: Number,
    required: [true, 'Tipo de dor no peito é obrigatório'],
    enum: [1, 2, 3, 4]
  },
  restingBloodPressure: {
    type: Number,
    required: [true, 'Pressão sanguínea em repouso é obrigatória'],
    min: [0, 'Pressão sanguínea não pode ser negativa']
  },
  serumCholesterol: {
    type: Number,
    required: [true, 'Colesterol sérico é obrigatório'],
    min: [0, 'Colesterol sérico não pode ser negativo']
  },
  fastingBloodSugar: {
    type: Number,
    required: [true, 'Açúcar no sangue em jejum é obrigatório'],
    enum: [0, 1]
  },
  restingECG: {
    type: Number,
    required: [true, 'Eletrocardiograma em repouso é obrigatório'],
    enum: [0, 1, 2]
  },
  maxHeartRate: {
    type: Number,
    required: [true, 'Frequência cardíaca máxima é obrigatória'],
    min: [0, 'Frequência cardíaca não pode ser negativa']
  },
  exerciseAngina: {
    type: Number,
    required: [true, 'Angina induzida por exercício é obrigatória'],
    enum: [0, 1]
  },
  oldpeak: {
    type: Number,
    required: [true, 'Oldpeak é obrigatório'],
    min: [0, 'Oldpeak não pode ser negativo']
  },
  stSlope: {
    type: Number,
    required: [true, 'Inclinação do segmento ST é obrigatória'],
    enum: [0, 1, 2]
  },
  resultado: {
    type: Number,
    enum: [0, 1],
    default: 0
  },
  recomendacao: String,
  createdAt: {
    type: Date,
    default: Date.now
  }
});

const Questionario = mongoose.model('Questionario', questionarioSchema);

module.exports = Questionario;