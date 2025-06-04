import { Router } from "express";
import { MedicoController } from "../controllers/MedicoController";
import { body } from "express-validator";

const medicoRoutes = Router();
const medicoController = new MedicoController();

medicoRoutes.post(
    "/",
    [
        body("nome").notEmpty().withMessage("Nome é obrigatório"),
        body("crm").notEmpty().withMessage("CRM é obrigatório"),
        body("email").isEmail().withMessage("Email inválido"),
        body("senha")
            .isLength({ min: 6 })
            .withMessage("Senha deve ter no mínimo 6 caracteres"),
        body("especialidade")
            .notEmpty()
            .withMessage("Especialidade é obrigatória"),
    ],
    medicoController.create.bind(medicoController)
);

export default medicoRoutes;
