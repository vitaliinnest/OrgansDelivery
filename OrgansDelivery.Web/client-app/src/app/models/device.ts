import { IEntity, IWithTenant } from "./common";

export interface Device extends IEntity, IWithTenant {
    name: string
}
