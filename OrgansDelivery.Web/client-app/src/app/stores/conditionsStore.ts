import { makeAutoObservable, runInAction } from "mobx";
import { Conditions, ConditionsFormValues } from "../models/conditions";
import agent from "../api/agent";
import { store } from "./store";

export default class ConditionsStore {
    conditions: Conditions[] = [];
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadConditions = async () => {
        try {
            runInAction(() => this.isLoading = true);
            const conditions = await agent.ConditionsActions.getConditions();
            runInAction(() => (this.conditions = conditions));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };

    createConditions = async (conditions: ConditionsFormValues) => {
        try {
            runInAction(() => this.isLoading = true);
            store.modalStore.closeModal();
            const created = await agent.ConditionsActions.createConditions(
                conditions
            );
            runInAction(() => this.conditions.push(created));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };

    updateConditions = async (conditionsId: string, update: ConditionsFormValues) => {
        try {
            runInAction(() => this.isLoading = true);
            store.modalStore.closeModal();
            const updated = await agent.ConditionsActions.updateConditions(conditionsId, update);
            runInAction(() => {
                this.conditions = this.conditions.map((c) =>
                    c.id === conditionsId ? updated : c
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
    }

    deleteConditions = async (conditionsId: string) => {
        try {
            runInAction(() => this.isLoading = true);
            store.modalStore.closeModal();
            await agent.ConditionsActions.deleteConditions(conditionsId);
            runInAction(() => {
                this.conditions = this.conditions.filter(
                    (c) => c.id !== conditionsId
                );
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
