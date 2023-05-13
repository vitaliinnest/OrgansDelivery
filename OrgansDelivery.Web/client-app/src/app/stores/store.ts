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
import ContainerStore from "./containerStore";
import DeviceStore from "./deviceStore";

interface Store {
    modalStore: ModalStore;
    commonStore: CommonStore;
    userStore: UserStore;
    tenantStore: TenantStore;
    
    employeeStore: EmployeeStore;
    inviteStore: InviteStore;

    organStore: OrganStore;
    containerStore: ContainerStore;
    conditionsStore: ConditionsStore;
    deviceStore: DeviceStore;
    recordStore: RecordStore;
}

export const store: Store = {
    modalStore: new ModalStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    tenantStore: new TenantStore(),
    
    employeeStore: new EmployeeStore(),
    inviteStore: new InviteStore(),
    
    organStore: new OrganStore(),
    containerStore: new ContainerStore(),
    conditionsStore: new ConditionsStore(),
    deviceStore: new DeviceStore(),
    recordStore: new RecordStore(),
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
