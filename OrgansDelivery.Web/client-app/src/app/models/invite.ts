import { IEntity, IWithTenant } from "./common";

export interface Invite extends IEntity, IWithTenant {
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
