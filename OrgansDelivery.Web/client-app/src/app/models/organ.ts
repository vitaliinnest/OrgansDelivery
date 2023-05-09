import { IEntity, IWithTenant } from "./common";

export interface Organ extends IEntity, IWithTenant {
    name: string;
    description: string;
    organCreationDate: string;
    containerId: string;
}

export interface CreateOrgan {
    name: string;
    description: string;
    organCreationDate: Date;
}
