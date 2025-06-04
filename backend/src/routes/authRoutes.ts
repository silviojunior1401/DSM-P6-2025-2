import { Router } from "express";
import { AuthController } from "../controllers/AuthController";
import { body } from "express-validator";

const authRoutes = Router();
const authController = new AuthController();

authRoutes.post(
    "/login",
    [body("senha").notEmpty().withMessage("Senha é obrigatória")],
    authController.login.bind(authController)
);

export default authRoutes;
