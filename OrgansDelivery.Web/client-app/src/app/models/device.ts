import { IEntity } from "./common";
import { ContainerRef } from "./container";

export interface Device extends IEntity {
    name: string;
    conditionsIntervalCheckInMs: number;
    container?: ContainerRef;
}

export interface DeviceRef extends IEntity {
    name: string;
    conditionsIntervalCheckInMs: number;
    containerId: string;
}

export interface DeviceFormValues {
    id: string;
    name: string;
    conditionsIntervalCheckInMs: number;
}
