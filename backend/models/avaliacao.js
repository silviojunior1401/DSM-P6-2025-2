const mongoose = require('mongoose');

const avaliacaoSchema = new mongoose.Schema({
  usuario: {
    type: mongoose.Schema.ObjectId,
    ref: 'Usuario',
    required: [true, 'Avaliação deve pertencer a um usuário']
  },
  data: {
    type: Date,
    default: Date.now
  },
  resultado: {
    type: Number,
    enum: [0, 1],
    required: [true, 'Resultado é obrigatório']
  }
});

const Avaliacao = mongoose.model('Avaliacao', avaliacaoSchema);

module.exports = Avaliacao;