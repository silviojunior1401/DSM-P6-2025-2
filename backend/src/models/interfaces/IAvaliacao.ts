export interface IAvaliacao {
    id?: string;
    data: Date;
    resultado: number;
    recomendacao?: string;
    medicoId: string;
    questionarioId: string;
}
