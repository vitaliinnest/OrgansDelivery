import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { router } from "../router/Routes";
import { CreateTenant, Tenant } from "../models/tenant";

export default class TenantStore {
    tenant: Tenant | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    get hasTenant() {
        return !!this.tenant;
    }

    loadTenant = async () => {
        try {
            const tenant = await agent.TenantActions.loadTenant();
            runInAction(() => this.tenant = tenant);
        } catch (error) {
            console.log(error);
        }
    }
    
    createTenant = async (tenant: CreateTenant) => {
        try {
            const created = await agent.TenantActions.createTenant(tenant);
            runInAction(() => (this.tenant = created));
            router.navigate('/organs');
        } catch (error) {
            throw error;
        }
    };
}
