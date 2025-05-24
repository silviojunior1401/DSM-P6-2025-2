const express = require('express');
const router = express.Router();
const historicoController = require('../controllers/historicoController');
const authMiddleware = require('../middlewares/authMiddleware');

/**
 * @swagger
 * tags:
 *   name: Histórico
 *   description: Gerenciamento do histórico de avaliações
 */

/**
 * @swagger
 * /historico:
 *   get:
 *     summary: Recupera o histórico de avaliações do usuário
 *     tags: [Histórico]
 *     security:
 *       - bearerAuth: []
 *     responses:
 *       200:
 *         description: Histórico recuperado com sucesso
 *         content:
 *           application/json:
 *             schema:
 *               type: array
 *               items:
 *                 $ref: '#/components/schemas/Avaliacao'
 *       401:
 *         description: Não autorizado
 */
router.get('/', authMiddleware.protect, historicoController.getHistorico);

module.exports = router;