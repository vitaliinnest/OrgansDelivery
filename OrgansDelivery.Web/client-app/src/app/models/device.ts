import { IEntity, IWithTenant } from "./common";

export interface Device extends IEntity, IWithTenant {
    name: string;
    conditionsIntervalCheckInMs: number;
}

export interface DeviceFormValues {
    id: string;
    name: string;
    conditionsIntervalCheckInMs: number;
}
