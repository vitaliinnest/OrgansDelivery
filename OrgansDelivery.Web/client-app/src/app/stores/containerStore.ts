import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { Container, CreateContainer } from "../models/container";

export default class ContainerStore {
    containers: Container[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    loadContainers = async () => {
        try {
            const containers = await agent.ContainerActions.getContainers();
            runInAction(() => (this.containers = containers));
        } catch (error) {
            console.log(error);
        }
    };

    createContainer = async (container: CreateContainer) => {
        try {
            const created = await agent.ContainerActions.createContainer(
                container
            );
            runInAction(() => this.containers.push(created));
        } catch (error) {
            console.log(error);
        }
    };

    deleteContainer = async (containerId: string) => {
        try {
            await agent.ConditionsActions.deleteConditions(containerId);
            runInAction(() => {
                this.containers = this.containers.filter(
                    (c) => c.id !== containerId
                );
            });
        } catch (error) {
            console.log(error);
        }
    };
}
