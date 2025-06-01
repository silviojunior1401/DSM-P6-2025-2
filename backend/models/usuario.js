const mongoose = require('mongoose');
const validator = require('validator');
const bcrypt = require('bcryptjs');

const usuarioSchema = new mongoose.Schema({
  nome: {
    type: String,
    required: [true, 'Nome é obrigatório'],
    trim: true
  },
  idade: {
    type: Number,
    required: [true, 'Idade é obrigatória'],
    min: [18, 'Idade mínima é 18 anos']
  },
  sexo: {
    type: String,
    required: [true, 'Sexo é obrigatório'],
    enum: ['Masculino', 'Feminino']
  },
  email: {
    type: String,
    required: [true, 'Email é obrigatório'],
    unique: true,
    lowercase: true,
    validate: [validator.isEmail, 'Email inválido']
  },
  senha: {
    type: String,
    required: [true, 'Senha é obrigatória'],
    minlength: [6, 'Senha deve ter no mínimo 6 caracteres'],
    select: false
  },
  endereco: String,
  telefone: String,
  cpf: {
    type: String,
    required: [true, 'CPF é obrigatório'],
    unique: true,
    validate: {
      validator: function(v) {
        return /^\d{11}$/.test(v);
      },
      message: 'CPF inválido'
    }
  },
  createdAt: {
    type: Date,
    default: Date.now
  }
});

// Hash da senha antes de salvar
usuarioSchema.pre('save', async function(next) {
  if (!this.isModified('senha')) return next();
  this.senha = await bcrypt.hash(this.senha, 12);
  next();
});

// Método para comparar senhas
usuarioSchema.methods.correctPassword = async function(candidatePassword, userPassword) {
  return await bcrypt.compare(candidatePassword, userPassword);
};

const Usuario = mongoose.model('Usuario', usuarioSchema);

module.exports = Usuario;