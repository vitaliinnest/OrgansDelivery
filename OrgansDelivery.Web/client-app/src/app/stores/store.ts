import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import UserStore from "./userStore";
import TenantStore from "./tenantStore";
import InviteStore from "./inviteStore";
import EmployeeStore from "./employeeStore";
import RecordStore from "./recordStore";
import ConditionsStore from "./conditionsStore";

interface Store {
    commonStore: CommonStore;
    userStore: UserStore;
    tenantStore: TenantStore;
    employeeStore: EmployeeStore;
    inviteStore: InviteStore;
    recordStore: RecordStore;
    conditionsStore: ConditionsStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    tenantStore: new TenantStore(),
    employeeStore: new EmployeeStore(),
    inviteStore: new InviteStore(),
    recordStore: new RecordStore(),
    conditionsStore: new ConditionsStore(),
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
