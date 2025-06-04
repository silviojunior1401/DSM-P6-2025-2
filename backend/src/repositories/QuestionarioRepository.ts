import { AppDataSource } from "../config/database";
import { Questionario } from "../models/entities/Questionario";
import { IQuestionario } from "../models/interfaces/IQuestionario";

export class QuestionarioRepository {
    private repository = AppDataSource.getRepository(Questionario);

    async create(questionarioData: IQuestionario): Promise<Questionario> {
        const questionario = this.repository.create(questionarioData);
        return await this.repository.save(questionario);
    }

    async findById(id: string): Promise<Questionario | null> {
        return await this.repository.findOne({ where: { id } });
    }
}
