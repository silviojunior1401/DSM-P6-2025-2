import { MedicoRepository } from "../repositories/MedicoRepository";
import { INovoMedico } from "../models/interfaces/IMedico";
import bcrypt from "bcryptjs";

export class MedicoService {
    private medicoRepository: MedicoRepository;

    constructor() {
        this.medicoRepository = new MedicoRepository();
    }

    async createMedico(medicoData: INovoMedico) {
        // Verificar se o médico já existe
        const existingMedicoCRM = await this.medicoRepository.findByCRM(
            medicoData.crm
        );
        if (existingMedicoCRM) {
            throw new Error("CRM já cadastrado");
        }

        const existingMedicoEmail = await this.medicoRepository.findByEmail(
            medicoData.email
        );
        if (existingMedicoEmail) {
            throw new Error("Email já cadastrado");
        }

        // Hash da senha
        const hashedPassword = await bcrypt.hash(medicoData.senha, 10);

        // Criar médico com senha hasheada
        const newMedico = {
            ...medicoData,
            senha: hashedPassword,
        };

        return await this.medicoRepository.create(newMedico);
    }
}
