import { IEntity } from "./common";

export interface Condition<T> {
    expectedValue: T;
    allowedDeviation: T;
}

export interface Orientation {
    x: number;
    y: number;
}

export interface Conditions extends IEntity {
    name: string;
    description: string;
    humidity: Condition<number>;
    light: Condition<number>;
    temperature: Condition<number>;
    orientation: Condition<Orientation>;
}

export interface ConditionsRef extends IEntity {
    name: string;
    description: string;
    humidity: Condition<number>;
    light: Condition<number>;
    temperature: Condition<number>;
    orientation: Condition<Orientation>;
}

export interface ConditionsFormValues {
    name: string;
    description: string;
    humidity: Condition<number>;
    light: Condition<number>;
    temperature: Condition<number>;
    orientation: Condition<Orientation>;
}
