import { IEntity } from "./common";

export interface Tenant extends IEntity {
    name: string;
}

export interface TenantFormValues {
    name: string;
}
