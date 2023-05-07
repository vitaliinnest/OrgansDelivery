import { IEntity, IWithTenant } from "./base";

export interface Container extends IEntity, IWithTenant {
    name: string;
    description: string;
}
