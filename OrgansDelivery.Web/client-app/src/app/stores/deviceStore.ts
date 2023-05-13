import { makeAutoObservable, runInAction } from "mobx";
import { Device, DeviceFormValues } from "../models/device";
import agent from "../api/agent";

export default class DeviceStore {
    devices: Device[] = [];
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadDevices = async () => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            const devices = await agent.DeviceActions.getDevices();
            runInAction(() => {
                this.devices = devices;
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    };

    addDevice = async (device: DeviceFormValues) => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            const created = await agent.DeviceActions.addDevice(device);
            runInAction(() => this.devices.push(created));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    };

    updateDevice = async (deviceId: string, update: DeviceFormValues) => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            const updated = await agent.DeviceActions.updateDevice(
                deviceId,
                update
            );
            runInAction(() => {
                this.devices = this.devices.map((d) =>
                    d.id === deviceId ? updated : d
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

    deleteDevice = async (deviceId: string) => {
        try {
            runInAction(() => {
                this.isLoading = true;
            });
            await agent.ConditionsActions.deleteConditions(deviceId);
            runInAction(() => {
                this.devices = this.devices.filter((d) => d.id !== deviceId);
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
