import { makeAutoObservable, runInAction } from "mobx";
import { OrganFormValues, Organ } from "../models/organ";
import agent from "../api/agent";

export default class OrganStore {
    organs: Organ[] = [];
    selectedOrgan?: Organ = undefined;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadOrgans = async () => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            const organs = await agent.OrganActions.getOrgans();
            runInAction(() => {
                this.organs = organs;
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    };

    loadOrgan = async (organId: string) => {
        const organ = this.organs.find((o) => o.id === organId);
        if (organ) {
            runInAction(() => {
                this.selectedOrgan = organ;
            })
            return;
        }
        await this.loadOrgans();
        await this.loadOrgan(organId);
    };

    createOrgan = async (organ: OrganFormValues) => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            const created = await agent.OrganActions.createOrgan(organ);
            runInAction(() => this.organs.push(created));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    };

    updateOrgan = async (organId: string, update: OrganFormValues) => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            const updated = await agent.OrganActions.updateOrgan(organId, update);
            runInAction(() => {
                this.organs = this.organs.map((o) =>
                    o.id === organId ? updated : o
                );
            });
        } catch (error) {
            console.log(error);
            throw error;
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    };

    deleteOrgan = async (organId: string) => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            await agent.OrganActions.deleteOrgan(organId);
            runInAction(() => {
                this.organs = this.organs.filter((o) => o.id !== organId);
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    };
}