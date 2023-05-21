import { IEntity } from "./common";
import { Condition, ConditionsRef, Orientation } from "./conditions";
import { OrganRef } from "./organ";

export interface ConditionsRecord extends IEntity {
    dateTime: Date;
    temperature: number;
    humidity: number;
    light: number;
    orientation: Orientation;
    conditions: ConditionsRef;
    organ: OrganRef;
}

export interface ConditionsRecordRef extends IEntity {
    dateTime: Date;
    temperature: number;
    humidity: number;
    light: number;
    orientation: Orientation;
    conditionsId: string;   
}

export interface ComparedResult<T> extends Condition<T> {
    actual: T;
    isViolated: boolean;
}

export interface ConditionsViolation {
    record: ConditionsRecordRef;
    temperature: ComparedResult<number>;
    humidity: ComparedResult<number>;
    light: ComparedResult<number>;
    orientation: ComparedResult<Orientation>;
}
