import { IEntity, IWithTenant } from "./base";

export interface Invite extends IEntity, IWithTenant {
    email: string;
    inviteCode: string;
    roleId: string;
    language: string;
}
