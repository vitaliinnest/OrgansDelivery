import { IEntity, IWithTenant } from "./common";
import { Condition, Orientation } from "./conditions";

export interface ConditionsRecord extends IEntity, IWithTenant {
    containerId: string;
    dateTime: Date;
    temperature: number;
    humidity: number;
    light: number;
    orientation: Orientation;
}

export interface ComparedResult<T> extends Condition<T> {
    actual: T;
    isViolated: boolean;
}

export interface ConditionsViolation {
    recordId: string;
    containerId: string;
    deviceId: string;
    temperature: ComparedResult<number>;
    humidity: ComparedResult<number>;
    light: ComparedResult<number>;
    orientation: ComparedResult<Orientation>;
}
