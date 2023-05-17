import { IEntity } from "./common";

export interface Invite extends IEntity {
    email: string;
    inviteCode: string;
}

export interface InviteFormValues {
    email: string;
}
