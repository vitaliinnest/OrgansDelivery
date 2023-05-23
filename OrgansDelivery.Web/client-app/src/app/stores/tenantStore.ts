import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { TenantFormValues, Tenant } from "../models/tenant";

export default class TenantStore {
    tenant: Tenant | null = null;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    get hasTenant() {
        return !!this.tenant;
    }

    loadTenant = async () => {
        try {
            runInAction(() => this.isLoading = true);
            const tenant = await agent.TenantActions.getTenant();
            runInAction(() => this.tenant = tenant);
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    }
    
    createTenant = async (tenant: TenantFormValues) => {
        try {
            runInAction(() => this.isLoading = true);
            const created = await agent.TenantActions.createTenant(tenant);
            runInAction(() => (this.tenant = created));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };

    updateTenant = async (tenant: TenantFormValues) => {
        try {
            runInAction(() => this.isLoading = true);
            const updated = await agent.TenantActions.updateTenant(tenant);
            runInAction(() => (this.tenant = updated));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    }
}
