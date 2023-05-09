import { makeAutoObservable, runInAction } from "mobx";
import { Conditions, CreateConditions } from "../models/conditions";
import agent from "../api/agent";

export default class ConditionsStore {
    conditions: Conditions[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    loadConditions = async () => {
        try {
            const conditions = await agent.ConditionsActions.getConditions();
            runInAction(() => (this.conditions = conditions));
        } catch (error) {
            console.log(error);
        }
    };

    createCondition = async (conditions: CreateConditions) => {
        try {
            const created = await agent.ConditionsActions.createConditions(
                conditions
            );
            runInAction(() => this.conditions.push(created));
        } catch (error) {
            console.log(error);
        }
    };

    deleteConditions = async (conditionsId: string) => {
        try {
            await agent.ConditionsActions.deleteConditions(conditionsId);
            runInAction(() => {
                this.conditions = this.conditions.filter(
                    (c) => c.id !== conditionsId
                );
            });
        } catch (error) {
            console.log(error);
        }
    };
}
