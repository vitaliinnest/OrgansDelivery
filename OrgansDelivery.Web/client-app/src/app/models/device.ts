import { IEntity, IWithTenant } from "./base";

export interface Device extends IEntity, IWithTenant {
    name: string
}
