import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { Employee } from "../models/employee";

export default class EmployeeStore {
    employees: Employee[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    loadEmployees = async () => {
        try {
            const employees = await agent.EmployeeActions.getEmployees();
            runInAction(() => (this.employees = employees));
        } catch (error) {
            console.log(error);
        }
    };

    deleteEmployee = async (employeeId: string) => {
        try {
            await agent.EmployeeActions.deleteEmployee(employeeId);
            runInAction(() => {
                this.employees = this.employees.filter(
                    (e) => e.id !== employeeId
                );
            });
        } catch (error) {
            console.log(error);
        }
    };
}
