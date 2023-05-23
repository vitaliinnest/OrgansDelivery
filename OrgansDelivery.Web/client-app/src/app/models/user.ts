import { IEntity } from "./common";

export interface User extends IEntity {
    name: string;
    surname: string;
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
    name: string;
    surname: string;
}
