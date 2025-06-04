import { Request, Response } from "express";
import { MedicoService } from "../services/MedicoService";
import { validationResult } from "express-validator";

export class MedicoController {
    private medicoService: MedicoService;

    constructor() {
        this.medicoService = new MedicoService();
    }

    public async create(req: Request, res: Response): Promise<Response> {
        try {
            // Validar entrada
            const errors = validationResult(req);
            if (!errors.isEmpty()) {
                return res.status(400).json({ errors: errors.array() });
            }

            const { nome, crm, email, senha, especialidade } = req.body;

            const medico = await this.medicoService.createMedico({
                nome,
                crm,
                email,
                senha,
                especialidade,
            });

            // Remover senha da resposta
            const { senha: _, ...medicoSemSenha } = medico;

            return res.status(201).json(medicoSemSenha);
        } catch (error) {
            if (error instanceof Error) {
                return res.status(400).json({ error: error.message });
            }
            return res.status(500).json({ error: "Erro ao criar médico" });
        }
    }
}
