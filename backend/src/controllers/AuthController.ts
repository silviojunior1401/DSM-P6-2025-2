import { Request, Response } from "express";
import { AuthService } from "../services/AuthService";
import { validationResult } from "express-validator";

export class AuthController {
    private authService: AuthService;

    constructor() {
        this.authService = new AuthService();
    }

    async login(req: Request, res: Response) {
        try {
            // Validar entrada
            const errors = validationResult(req);
            if (!errors.isEmpty()) {
                return res.status(400).json({ errors: errors.array() });
            }

            const { crm, email, senha } = req.body;

            // Verificar se pelo menos um dos campos (crm ou email) foi fornecido
            if (!crm && !email) {
                return res
                    .status(400)
                    .json({ error: "CRM ou email são necessários para login" });
            }

            const token = await this.authService.login({ crm, email, senha });

            return res.json(token);
        } catch (error) {
            if (error instanceof Error) {
                return res.status(401).json({ error: error.message });
            }
            return res.status(500).json({ error: "Erro ao realizar login" });
        }
    }
}
