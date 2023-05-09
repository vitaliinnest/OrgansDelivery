import { IEntity, IWithTenant } from "./common";
import { Role } from "./role";

export interface Employee extends IEntity, IWithTenant {
    name: string;
    surname: string;
    language: string;
    role: Role;
}
