import { Request, Response, NextFunction } from "express";

export default function errorMiddleware(
    err: Error,
    req: Request,
    res: Response,
    next: NextFunction
) {
    console.error(err.stack);

    return res.status(500).json({
        status: "error",
        message: "Erro interno do servidor",
    });
}
