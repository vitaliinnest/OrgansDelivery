import { makeAutoObservable, runInAction } from "mobx";
import {
    ConditionsRecord,
    ConditionsViolation,
} from "../models/conditionsRecord";
import agent from "../api/agent";

export default class RecordStore {
    records: ConditionsRecord[] = [];
    violations: ConditionsViolation[] = [];
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadRecords = async (organId: string) => {
        try {
            runInAction(() => this.isLoading = true);
            const records = await agent.ConditionsRecordActions.getConditionsRecords(organId);
            runInAction(() => this.records = records);
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };

    loadViolations = async (organId: string) => {
        try {
            runInAction(() => this.isLoading = true);
            const violations = await agent.ConditionsRecordActions.getViolations(organId);
            runInAction(() => this.violations = violations);
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };
}
