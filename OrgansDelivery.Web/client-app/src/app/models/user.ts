import { IEntity, IWithTenant } from "./common";

export interface User extends IEntity, IWithTenant {
    name: string;
    surname: string;
    language: string; // todo: enum?
    token: string;
}

export interface Login {
    email: string;
    password: string;
}

export interface Register {
    name: string;
    surname: string;
    email: string;
    password: string;
    inviteCode?: string;
}

export interface UpdateUser {
    name: string
    surname: string
    language: string
}
