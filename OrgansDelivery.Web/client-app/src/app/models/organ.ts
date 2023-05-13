import { IEntity, IWithTenant } from "./common";

export interface Organ extends IEntity, IWithTenant {
    name: string;
    description: string;
    organCreationDate: Date;
    containerId: string;
    conditionsId: string;
}

export interface OrganFormValues {
    name: string;
    description: string;
    organCreationDate: Date;
    containerId: string;
    conditionsId: string;
}
