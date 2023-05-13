import { IEntity, IWithTenant } from "./common";

export interface Container extends IEntity, IWithTenant {
    name: string;
    description: string;
    deviceId: string;
}

export interface ContainerFormValues {
    name: string;
    description: string;
    deviceId: string;
}
