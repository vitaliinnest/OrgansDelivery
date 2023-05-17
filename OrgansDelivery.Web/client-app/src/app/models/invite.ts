import { IEntity } from "./common";

export interface Invite extends IEntity {
    email: string;
    inviteCode: string;
    roleId: string;
    language: string;
}

export interface CreateInvite {
    email: string;
    roleId: string;
    language: string;
}
