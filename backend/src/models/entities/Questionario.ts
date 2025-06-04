import {
    Entity,
    PrimaryGeneratedColumn,
    Column,
    OneToOne,
    CreateDateColumn,
} from "typeorm";
import { Avaliacao } from "./Avaliacao";

@Entity("questionarios")
export class Questionario {
    @PrimaryGeneratedColumn("uuid")
    id: string;

    @Column({ type: "int" })
    age: number;

    @Column({ type: "tinyint" })
    sex: number;

    @Column({ type: "tinyint" })
    chestPainType: number;

    @Column({ type: "float" })
    restingBloodPressure: number;

    @Column({ type: "float" })
    serumCholesterol: number;

    @Column({ type: "tinyint" })
    fastingBloodSugar: number;

    @Column({ type: "tinyint" })
    restingECG: number;

    @Column({ type: "float" })
    maxHeartRate: number;

    @Column({ type: "tinyint" })
    exerciseAngina: number;

    @Column({ type: "float" })
    oldpeak: number;

    @Column({ type: "tinyint" })
    stSlope: number;

    @CreateDateColumn({ type: "timestamp" })
    createdAt: Date;

    @OneToOne(() => Avaliacao, (avaliacao) => avaliacao.questionario)
    avaliacao: Avaliacao;
}
