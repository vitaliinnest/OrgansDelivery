import { IEntity } from "./base";

export interface Tenant extends IEntity {
    url: string;
    name: string;
    description: string;
}
