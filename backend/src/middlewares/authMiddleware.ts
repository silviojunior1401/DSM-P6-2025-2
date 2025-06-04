import { Request, Response, NextFunction } from "express";
import jwt from "jsonwebtoken";
import authConfig from "../config/auth";

interface TokenPayload {
    id: string;
    iat: number;
    exp: number;
}

export default function authMiddleware(
    req: Request,
    res: Response,
    next: NextFunction
) {
    const authHeader = req.headers.authorization;

    if (!authHeader) {
        return res.status(401).json({ error: "Token não fornecido" });
    }

    const parts = authHeader.split(" ");

    if (parts.length !== 2) {
        return res.status(401).json({ error: "Token mal formatado" });
    }

    const [scheme, token] = parts;

    if (!/^Bearer$/i.test(scheme)) {
        return res.status(401).json({ error: "Token mal formatado" });
    }

    try {
        const decoded = jwt.verify(
            token,
            authConfig.jwt.secret
        ) as TokenPayload;

        req.userId = decoded.id;

        return next();
    } catch (err) {
        return res.status(401).json({ error: "Token inválido" });
    }
}
