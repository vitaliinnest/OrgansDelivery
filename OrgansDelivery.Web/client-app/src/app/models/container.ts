import { IEntity, IWithTenant } from "./common";

export interface Container extends IEntity, IWithTenant {
    name: string;
    description: string;
}

export interface ContainerFormValues {
    name: string;
    description: string;
    conditionsId: string;
    organId?: string;
}
