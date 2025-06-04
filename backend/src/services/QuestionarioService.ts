import { QuestionarioRepository } from "../repositories/QuestionarioRepository";
import { AvaliacaoRepository } from "../repositories/AvaliacaoRepository";
import { IQuestionario, IResultado } from "../models/interfaces/IQuestionario";
import { MedicoRepository } from "../repositories/MedicoRepository";

export class QuestionarioService {
    private questionarioRepository: QuestionarioRepository;
    private avaliacaoRepository: AvaliacaoRepository;
    private medicoRepository: MedicoRepository;

    constructor() {
        this.questionarioRepository = new QuestionarioRepository();
        this.avaliacaoRepository = new AvaliacaoRepository();
        this.medicoRepository = new MedicoRepository();
    }

    async processQuestionario(
        questionarioData: IQuestionario,
        medicoId: string
    ): Promise<IResultado> {
        // Validar se o médico existe
        const medico = await this.medicoRepository.findById(medicoId);
        if (!medico) {
            throw new Error("Médico não encontrado");
        }

        // Salvar questionário
        const questionario = await this.questionarioRepository.create(
            questionarioData
        );

        // Algoritmo simplificado para avaliação de risco cardíaco
        // Em um cenário real, este seria um algoritmo mais complexo baseado em evidências médicas
        const riskScore = this.calculateRiskScore(questionarioData); /// ALTERAR PARA IA

        // Determinar resultado (0 = baixo risco, 1 = alto risco)
        const resultado = riskScore > 15 ? 1 : 0;

        // Gerar recomendação baseada no resultado
        const recomendacao = this.generateRecommendation(
            resultado,
            questionarioData
        );

        // Salvar avaliação
        await this.avaliacaoRepository.create({
            resultado,
            recomendacao,
            medicoId,
            questionarioId: questionario.id,
        });

        return { resultado, recomendacao };
    }

    private calculateRiskScore(data: IQuestionario): number {
        let score = 0;

        // Idade: quanto maior, maior o risco
        score += data.age / 10;

        // Sexo: homens têm maior risco (1 = masculino)
        if (data.sex === 1) score += 2;

        // Tipo de dor no peito (4 = mais grave)
        score += data.chestPainType;

        // Pressão arterial: acima de 130 é preocupante
        if (data.restingBloodPressure > 130) score += 3;

        // Colesterol: acima de 200 é preocupante
        if (data.serumCholesterol > 200) score += 3;

        // Açúcar no sangue em jejum (1 = alto)
        if (data.fastingBloodSugar === 1) score += 2;

        // ECG em repouso (2 = mais grave)
        score += data.restingECG;

        // Frequência cardíaca máxima: quanto menor, pior
        if (data.maxHeartRate < 150) score += 2;

        // Angina induzida por exercício (1 = sim)
        if (data.exerciseAngina === 1) score += 3;

        // Depressão ST (oldpeak): quanto maior, pior
        score += data.oldpeak * 2;

        // Inclinação ST (2 = mais grave)
        score += data.stSlope;

        return score;
    }

    private generateRecommendation(
        resultado: number,
        data: IQuestionario
    ): string {
        if (resultado === 1) {
            return "Paciente apresenta alto risco de doença cardíaca. Recomenda-se avaliação cardiológica completa, incluindo exames complementares como ecocardiograma e teste ergométrico. Considerar ajustes no estilo de vida e medicação preventiva.";
        } else {
            let recomendacao =
                "Paciente apresenta baixo risco de doença cardíaca. ";

            // Adicionar recomendações específicas baseadas nos dados
            if (data.restingBloodPressure > 120) {
                recomendacao += "Monitorar pressão arterial regularmente. ";
            }

            if (data.serumCholesterol > 180) {
                recomendacao +=
                    "Considerar ajustes na dieta para controle do colesterol. ";
            }

            if (data.age > 50) {
                recomendacao +=
                    "Realizar check-up cardiológico anual devido à idade. ";
            }

            return (
                recomendacao +
                "Manter hábitos saudáveis e atividade física regular."
            );
        }
    }
}
