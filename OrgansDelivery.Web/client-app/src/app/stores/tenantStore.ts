import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { TenantFormValues, Tenant } from "../models/tenant";
import { store } from "./store";

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
            const { tenant: created, token } = await agent.TenantActions.createTenant(tenant);
            store.commonStore.setToken(token);
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
            store.modalStore.closeModal();
            const updated = await agent.TenantActions.updateTenant(tenant);
            runInAction(() => (this.tenant = updated));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    }
}
