import { IEntity } from "./common";
import { ConditionsRef } from "./conditions";
import { ContainerRef } from "./container";

export interface Organ extends IEntity {
    name: string;
    description: string;
    organCreationDate: Date;
    container: ContainerRef;
    conditions: ConditionsRef;
}

export interface OrganRef extends IEntity {
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
