import { IEntity } from "./common";
import { Role } from "./role";

export interface Employee extends IEntity {
    name: string;
    surname: string;
    email: string;
    language: string;
    role: Role;
}
