export interface IQuestionario {
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

export interface IResultado {
    resultado: number;
    recomendacao: string;
}
