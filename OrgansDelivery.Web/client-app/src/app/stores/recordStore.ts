import { makeAutoObservable, runInAction } from "mobx";
import {
    ConditionsRecord,
    ConditionsViolation,
} from "../models/conditionsRecord";
import agent from "../api/agent";

type AllRecords = {
    records: ConditionsRecord[];
    violations: ConditionsViolation[];
};

export default class RecordStore {
    conditionsRecordsByOrganIdMap = new Map<string, ConditionsRecord[]>();
    violationsByOrganIdMap = new Map<string, ConditionsViolation[]>();

    constructor() {
        makeAutoObservable(this);
    }

    loadOrganRecords = async (organId: string): Promise<AllRecords> => {
        const records = this.conditionsRecordsByOrganIdMap.get(organId);
        const violations = this.violationsByOrganIdMap.get(organId);

        if (!records || !violations) {
            await this.loadRecords();
            return await this.loadOrganRecords(organId);
        }

        return {
            records,
            violations,
        };
    };

    loadRecords = async () => {
        try {
            const conditionsRecords =
                await agent.ConditionsRecordActions.getConditionsRecords();
            const violations =
                await agent.ConditionsRecordActions.getViolations();
            runInAction(() => {
                this.conditionsRecordsByOrganIdMap = conditionsRecords;
                this.violationsByOrganIdMap = violations;
            });
        } catch (error) {
            console.log(error);
        }
    };
}
