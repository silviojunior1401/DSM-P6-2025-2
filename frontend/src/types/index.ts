// Baseado em mobile/CardioCheck/Model/Questionario.cs
export interface IQuestionario {
    nome: string;
    age: number;
    sex: number;
    chestPainType: number;
    restingBloodPressure: number;
    serumCholesterol: number;
    fastingBloodSugar: number;
    restingECG: number;
    maxHeartRate: number;
    exerciseAngina: number;
    oldpeak: number;
    stSlope: number;
}

// Baseado em mobile/CardioCheck/Model/Resultado.cs
export interface IResultado {
    predicao: number;
    recomendacao: string;
}

// Baseado em mobile/CardioCheck/Model/Avaliacao.cs
export interface IAvaliacao {
    data: string;
    resultado: number;
    questionario: IQuestionario;
}
