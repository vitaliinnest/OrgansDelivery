import { makeAutoObservable, runInAction } from "mobx";
import { CreateOrgan, Organ } from "../models/organ";
import agent from "../api/agent";

export default class OrganStore {
    organs: Organ[] = [];
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadOrgans = async () => {
        try {
            this.isLoading = true;
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

    loadOrgan = async (organId: string): Promise<Organ> => {
        const organ = this.organs.find((o) => o.id === organId);
        if (organ) {
            return organ;
        }

        await this.loadOrgans();
        return await this.loadOrgan(organId);
    };

    createOrgan = async (organ: CreateOrgan) => {
        try {
            const created = await agent.OrganActions.createOrgan(organ);
            runInAction(() => this.organs.push(created));
        } catch (error) {
            console.log(error);
        }
    };

    deleteOrgan = async (organId: string) => {
        try {
            await agent.OrganActions.deleteOrgan(organId);
            runInAction(() => {
                this.organs = this.organs.filter((o) => o.id !== organId);
            });
        } catch (error) {
            console.log(error);
        }
    };
}
