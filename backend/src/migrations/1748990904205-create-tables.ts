import { MigrationInterface, QueryRunner } from "typeorm";

export class CreateTables1748990904205 implements MigrationInterface {
    public async up(queryRunner: QueryRunner): Promise<void> {
        // Criar tabela de médicos
        await queryRunner.query(`
      CREATE TABLE medicos (
        id VARCHAR(36) NOT NULL PRIMARY KEY,
        nome VARCHAR(100) NOT NULL,
        crm VARCHAR(20) NOT NULL UNIQUE,
        email VARCHAR(100) NOT NULL UNIQUE,
        senha VARCHAR(255) NOT NULL,
        especialidade VARCHAR(100) NOT NULL,
        createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
      ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
    `);

        // Criar tabela de questionários
        await queryRunner.query(`
      CREATE TABLE questionarios (
        id VARCHAR(36) NOT NULL PRIMARY KEY,
        nome VARCHAR(100) NOT NULL,
        age INT NOT NULL,
        sex TINYINT NOT NULL,
        chestPainType TINYINT NOT NULL,
        restingBloodPressure FLOAT NOT NULL,
        serumCholesterol FLOAT NOT NULL,
        fastingBloodSugar TINYINT NOT NULL,
        restingECG TINYINT NOT NULL,
        maxHeartRate FLOAT NOT NULL,
        exerciseAngina TINYINT NOT NULL,
        oldpeak FLOAT NOT NULL,
        stSlope TINYINT NOT NULL,
        createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
      ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
    `);

        // Criar tabela de avaliações
        await queryRunner.query(`
      CREATE TABLE avaliacoes (
        id VARCHAR(36) NOT NULL PRIMARY KEY,
        resultado TINYINT NOT NULL,
        recomendacao TEXT NOT NULL,
        data TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        medico_id VARCHAR(36) NOT NULL,
        questionario_id VARCHAR(36) NOT NULL,
        FOREIGN KEY (medico_id) REFERENCES medicos(id) ON DELETE CASCADE,
        FOREIGN KEY (questionario_id) REFERENCES questionarios(id) ON DELETE CASCADE,
        UNIQUE KEY (questionario_id)
      ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
    `);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`DROP TABLE avaliacoes`);
        await queryRunner.query(`DROP TABLE questionarios`);
        await queryRunner.query(`DROP TABLE medicos`);
    }
}
