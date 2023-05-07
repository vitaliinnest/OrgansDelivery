import { IEntity, IWithTenant } from "./base";

export interface Organ extends IEntity, IWithTenant {
    name: string;
    description: string;
    organCreationDate: string;
    containerId: string;
}
