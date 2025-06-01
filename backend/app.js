require('dotenv').config();
const express = require('express');
const cors = require('cors');
const mongoose = require('mongoose');
const swaggerJsdoc = require('swagger-jsdoc');
const swaggerUi = require('swagger-ui-express');

// Importar rotas
const usuarioRoutes = require('./routes/usuarioRoutes');
const authRoutes = require('./routes/authRoutes');
const questionarioRoutes = require('./routes/questionarioRoutes');
const historicoRoutes = require('./routes/historicoRoutes');

// Configuração do Swagger
const swaggerOptions = {
  definition: {
    openapi: '3.0.3',
    info: {
      title: 'CardioCheck API',
      description: 'API para o aplicativo móvel CardioCheck, que avalia o risco de doenças cardíacas.',
      version: '1.0.0',
    },
    servers: [
      {
        url: 'http://localhost:3000/v1',
        description: 'Servidor Local',
      },
    ],
  },
  apis: ['./routes/*.js'],
};

const swaggerSpec = swaggerJsdoc(swaggerOptions);

// Configuração do Express
const app = express();
app.use(cors());
app.use(express.json());

// Conexão com o MongoDB
mongoose.connect(process.env.MONGODB_URI)
  .then(() => console.log('Conectado ao MongoDB'))
  .catch(err => console.error('Erro ao conectar ao MongoDB:', err));

// Rotas
app.use('/v1/usuarios', usuarioRoutes);
app.use('/v1/auth', authRoutes);
app.use('/v1/questionarios', questionarioRoutes);
app.use('/v1/historico', historicoRoutes);

// Swagger UI
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec));

// Iniciar servidor
const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Servidor rodando na porta ${PORT}`);
});