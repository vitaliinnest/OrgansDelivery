import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { Employee } from "../models/employee";

export default class EmployeeStore {
    employees: Employee[] = [];
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadEmployees = async () => {
        try {
            runInAction(() => this.isLoading = true);
            const employees = await agent.EmployeeActions.getEmployees();
            runInAction(() => (this.employees = employees));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };

    deleteEmployee = async (employeeId: string) => {
        try {
            runInAction(() => this.isLoading = true);
            await agent.EmployeeActions.deleteEmployee(employeeId);
            runInAction(() => {
                this.employees = this.employees.filter(
                    (e) => e.id !== employeeId
                );
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };
}
