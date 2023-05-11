import { IEntity, IWithTenant } from "./common";

export interface Organ extends IEntity, IWithTenant {
    name: string;
    description: string;
    organCreationDate: Date;
    containerId: string;
    containerName: string;
}

export interface OrganFormValues {
    name: string;
    description: string;
    organCreationDate: Date;
}
