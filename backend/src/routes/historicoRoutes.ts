import { Router } from "express";
import { HistoricoController } from "../controllers/HistoricoController";
import authMiddleware from "../middlewares/authMiddleware";

const historicoRoutes = Router();
const historicoController = new HistoricoController();

historicoRoutes.get(
    "/",
    authMiddleware,
    historicoController.getHistorico.bind(historicoController)
);

export default historicoRoutes;
