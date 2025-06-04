import { MedicoRepository } from "../repositories/MedicoRepository";
import { ILogin } from "../models/interfaces/IMedico";
import bcrypt from "bcryptjs";
import jwt from "jsonwebtoken";
import authConfig from "../config/auth";

export class AuthService {
    private medicoRepository: MedicoRepository;

    constructor() {
        this.medicoRepository = new MedicoRepository();
    }

    async login(loginData: ILogin) {
        let medico;

        // Verificar se o login é por CRM ou email
        if (loginData.crm) {
            medico = await this.medicoRepository.findByCRM(loginData.crm);
        } else if (loginData.email) {
            medico = await this.medicoRepository.findByEmail(loginData.email);
        } else {
            throw new Error("CRM ou email são necessários para login");
        }

        if (!medico) {
            throw new Error("Médico não encontrado");
        }

        // Verificar senha
        const passwordMatch = await bcrypt.compare(
            loginData.senha,
            medico.senha
        );
        if (!passwordMatch) {
            throw new Error("Senha incorreta");
        }

        // Gerar token JWT
        const token = jwt.sign({ id: medico.id }, authConfig.jwt.secret, {
            expiresIn: authConfig.jwt.expiresIn as any,
        });

        return {
            accessToken: token,
            tokenType: "Bearer",
        };
    }
}
