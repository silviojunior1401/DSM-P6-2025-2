import { Response } from "express";

export class AppError extends Error {
    public readonly statusCode: number;

    constructor(message: string, statusCode = 400) {
        super(message);
        this.statusCode = statusCode;
    }
}

export function handleError(error: Error | AppError, res: Response) {
    if (error instanceof AppError) {
        return res.status(error.statusCode).json({
            status: "error",
            message: error.message,
        });
    }

    console.error(error);

    return res.status(500).json({
        status: "error",
        message: "Erro interno do servidor",
    });
}
