import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import UserStore from "./userStore";
import TenantStore from "./tenantStore";
import InviteStore from "./inviteStore";
import EmployeeStore from "./employeeStore";

interface Store {
    commonStore: CommonStore;
    userStore: UserStore;
    tenantStore: TenantStore;
    employeeStore: EmployeeStore;
    inviteStore: InviteStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    tenantStore: new TenantStore(),
    employeeStore: new EmployeeStore(),
    inviteStore: new InviteStore(),
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
