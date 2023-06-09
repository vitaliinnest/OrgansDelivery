import { makeAutoObservable, runInAction } from "mobx";
import {
    ConditionsRecord,
    ConditionsViolation,
} from "../models/conditionsRecord";
import agent from "../api/agent";
import { parseDateString } from "../util/common";

export default class RecordStore {
    records: ConditionsRecord[] = [];
    violations: ConditionsViolation[] = [];
    areRecordsLoading = false;
    areViolationsLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadRecords = async (organId: string) => {
        try {
            runInAction(() => this.areRecordsLoading = true);
            const records = await agent.ConditionsRecordActions.getConditionsRecords(organId);
            for (const record of records) {
                record.dateTime = parseDateString(record.dateTime.toString());
            }
            runInAction(() => this.records = records);
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.areRecordsLoading = false);
        }
    };

    loadViolations = async (organId: string) => {
        try {
            runInAction(() => this.areViolationsLoading = true);
            const violations = await agent.ConditionsRecordActions.getViolations(organId);
            for (const violation of violations) {
                violation.record.dateTime = parseDateString(violation.record.dateTime.toString());
            }
            runInAction(() => this.violations = violations);
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.areViolationsLoading = false);
        }
    };
}
