const express = require('express');
const router = express.Router();
const questionarioController = require('../controllers/questionarioController');
const authMiddleware = require('../middlewares/authMiddleware');

/**
 * @swagger
 * tags:
 *   name: Questionários
 *   description: Gerenciamento de questionários de saúde
 */

/**
 * @swagger
 * /questionarios:
 *   post:
 *     summary: Envio de questionário de saúde
 *     tags: [Questionários]
 *     security:
 *       - bearerAuth: []
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/Questionario'
 *     responses:
 *       200:
 *         description: Questionário processado com sucesso
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/Resultado'
 *       400:
 *         description: Dados do questionário inválidos
 */
router.post('/', authMiddleware.protect, questionarioController.enviarQuestionario);

module.exports = router;