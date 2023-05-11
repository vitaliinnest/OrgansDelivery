import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import UserStore from "./userStore";
import TenantStore from "./tenantStore";
import InviteStore from "./inviteStore";
import EmployeeStore from "./employeeStore";
import RecordStore from "./recordStore";
import ConditionsStore from "./conditionsStore";
import OrganStore from "./organStore";
import ModalStore from "./modalStore";

interface Store {
    modalStore: ModalStore;
    commonStore: CommonStore;
    userStore: UserStore;
    tenantStore: TenantStore;
    employeeStore: EmployeeStore;
    inviteStore: InviteStore;
    recordStore: RecordStore;
    conditionsStore: ConditionsStore;
    organStore: OrganStore;
}

export const store: Store = {
    modalStore: new ModalStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    tenantStore: new TenantStore(),
    employeeStore: new EmployeeStore(),
    inviteStore: new InviteStore(),
    recordStore: new RecordStore(),
    conditionsStore: new ConditionsStore(),
    organStore: new OrganStore(),
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
