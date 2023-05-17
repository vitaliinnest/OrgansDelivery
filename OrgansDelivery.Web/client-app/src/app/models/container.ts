import { IEntity } from "./common";
import { DeviceRef } from "./device";
import { OrganRef } from "./organ";

export interface Container extends IEntity {
    name: string;
    description: string;
    device: DeviceRef;
    organ?: OrganRef;
}

export interface ContainerRef extends IEntity {
    name: string;
    description: string;
    deviceId: string;
}

export interface ContainerFormValues {
    name: string;
    description: string;
    deviceId: string;
}
