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
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    };

    addDevice = async (device: DeviceFormValues) => {

    }

    updateDevice = async (deviceId: string, device: DeviceFormValues) => {

    }

    deleteDevice = async (deviceId: string) => {
        
    }
}
