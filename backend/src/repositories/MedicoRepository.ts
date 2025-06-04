import { AppDataSource } from "../config/database";
import { Medico } from "../models/entities/Medico";
import { INovoMedico } from "../models/interfaces/IMedico";

export class MedicoRepository {
    private repository = AppDataSource.getRepository(Medico);

    async create(medicoData: INovoMedico): Promise<Medico> {
        const medico = this.repository.create(medicoData);
        return await this.repository.save(medico);
    }

    async findByEmail(email: string): Promise<Medico | null> {
        return await this.repository.findOne({ where: { email } });
    }

    async findByCRM(crm: string): Promise<Medico | null> {
        return await this.repository.findOne({ where: { crm } });
    }

    async findById(id: string): Promise<Medico | null> {
        return await this.repository.findOne({ where: { id } });
    }
}
