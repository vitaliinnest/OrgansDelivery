import { IEntity } from "./common";

export interface Tenant extends IEntity {
    url: string;
    name: string;
    description: string;
}

export interface CreateTenant {
    name: string;
}
