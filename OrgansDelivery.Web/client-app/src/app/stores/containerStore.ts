import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { Container, ContainerFormValues } from "../models/container";
import { store } from "./store";

export default class ContainerStore {
    containers: Container[] = [];
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadContainers = async () => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            const containers = await agent.ContainerActions.getContainers();
            runInAction(() => (this.containers = containers));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    };

    createContainer = async (container: ContainerFormValues) => {
        try {
            runInAction(() => this.isLoading = true);
            store.modalStore.closeModal();
            const created = await agent.ContainerActions.createContainer(container);
            runInAction(() => this.containers.push(created));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    };

    updateContainer = async (containerId: string, update: ContainerFormValues) => {
        try {
            runInAction(() => this.isLoading = true);
            store.modalStore.closeModal();
            const updated = await agent.ContainerActions.updateContainer(containerId, update);
            runInAction(() => {
                this.containers = this.containers.map((c) =>
                    c.id === containerId ? updated : c
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

    deleteContainer = async (containerId: string) => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            await agent.ConditionsActions.deleteConditions(containerId);
            runInAction(() => {
                this.containers = this.containers.filter(
                    (c) => c.id !== containerId
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
