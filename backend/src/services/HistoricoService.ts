import { AvaliacaoRepository } from "../repositories/AvaliacaoRepository";
import { MedicoRepository } from "../repositories/MedicoRepository";

export class HistoricoService {
    private avaliacaoRepository: AvaliacaoRepository;
    private medicoRepository: MedicoRepository;

    constructor() {
        this.avaliacaoRepository = new AvaliacaoRepository();
        this.medicoRepository = new MedicoRepository();
    }

    async getHistoricoByMedicoId(medicoId: string) {
        // Verificar se o médico existe
        const medico = await this.medicoRepository.findById(medicoId);
        if (!medico) {
            throw new Error("Médico não encontrado");
        }

        // Buscar avaliações do médico
        const avaliacoes = await this.avaliacaoRepository.findByMedicoId(
            medicoId
        );

        // Formatar para o formato esperado pela API
        return avaliacoes.map((avaliacao) => ({
            data: avaliacao.data,
            resultado: avaliacao.resultado,
        }));
    }
}
