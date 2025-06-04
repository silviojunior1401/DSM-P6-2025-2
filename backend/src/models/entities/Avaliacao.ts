import {
    Entity,
    PrimaryGeneratedColumn,
    Column,
    ManyToOne,
    OneToOne,
    JoinColumn,
    CreateDateColumn,
} from "typeorm";
import { Medico } from "./Medico";
import { Questionario } from "./Questionario";

@Entity("avaliacoes")
export class Avaliacao {
    @PrimaryGeneratedColumn("uuid")
    id: string;

    @Column({ type: "tinyint" })
    resultado: number;

    @Column({ type: "text" })
    recomendacao: string;

    @CreateDateColumn({ type: "timestamp" })
    data: Date;

    @ManyToOne(() => Medico, (medico) => medico.avaliacoes)
    @JoinColumn({ name: "medico_id" })
    medico: Medico;

    @Column({ name: "medico_id" })
    medicoId: string;

    @OneToOne(() => Questionario, (questionario) => questionario.avaliacao)
    @JoinColumn({ name: "questionario_id" })
    questionario: Questionario;

    @Column({ name: "questionario_id" })
    questionarioId: string;
}
