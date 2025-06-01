### Comandos de instalção
# Criar a pasta do projeto
mkdir cardiocheck-api

# Entrar na pasta
cd cardiocheck-api

# Inicializar o projeto Node.js (isso criará o package.json)
npm init -y
# Instalar as dependências básicas
npm install express cors mongoose dotenv bcryptjs jsonwebtoken validator swagger-jsdoc swagger-ui-express
npm install --save-dev nodemon


## Estrutura do Projeto
cardiocheck-api/
├── config/
│   └── database.js
├── controllers/
│   ├── authController.js
│   ├── historicoController.js
│   ├── questionarioController.js
│   └── usuarioController.js
├── middlewares/
│   └── authMiddleware.js
├── models/
│   ├── Avaliacao.js
│   ├── Questionario.js
│   └── Usuario.js
├── routes/
│   ├── authRoutes.js
│   ├── historicoRoutes.js
│   ├── questionarioRoutes.js
│   └── usuarioRoutes.js
├── services/
│   ├── authService.js
│   ├── historicoService.js
│   ├── questionarioService.js
│   └── usuarioService.js
├── app.js
├── package.json
└── .env