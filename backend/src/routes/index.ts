import { Router } from "express";
import medicoRoutes from "./medicoRoutes";
import authRoutes from "./authRoutes";
import questionarioRoutes from "./questionarioRoutes";
import historicoRoutes from "./historicoRoutes";

const routes = Router();

routes.use("/medicos", medicoRoutes);
routes.use("/auth", authRoutes);
routes.use("/questionarios", questionarioRoutes);
routes.use("/historico", historicoRoutes);

export default routes;
