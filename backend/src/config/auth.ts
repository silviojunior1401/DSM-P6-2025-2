import dotenv from "dotenv";

dotenv.config();

export default {
    jwt: {
        secret: process.env.JWT_SECRET || "default_secret",
        expiresIn: process.env.JWT_EXPIRES_IN || "1d",
    },
};
