export interface IMedico {
    id?: string;
    nome: string;
    crm: string;
    email: string;
    senha: string;
    especialidade: string;
}

export interface INovoMedico {
    nome: string;
    crm: string;
    email: string;
    senha: string;
    especialidade: string;
}

export interface ILogin {
    crm?: string;
    email?: string;
    senha: string;
}
