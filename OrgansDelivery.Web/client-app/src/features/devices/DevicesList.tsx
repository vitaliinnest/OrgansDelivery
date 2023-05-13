import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import EntitiesTable from "../../app/layout/EntitiesTable";
import AddDeviceModal from "./AddDeviceModal";
import UpdateDeviceModal from "./UpdateDeviceModal";

const DevicesList = () => {
    const { modalStore, deviceStore } = useStore();

    useEffect(() => {
        deviceStore.loadDevices();
    }, [deviceStore]);

    const onDeviceCreate = () => {
        modalStore.openModal(
            <AddDeviceModal />
        );
    };

    const onDeviceUpdate = (deviceId: string) => {
        const device = deviceStore.devices.find(
            (d) => d.id === deviceId
        );
        if (!device) {
            return;
        }

        modalStore.openModal(
            <UpdateDeviceModal
                device={device}
            />
        );
    };

    const onDeviceDeleteConfirmation = (deviceId: string) => {
        deviceStore.deleteDevice(deviceId);
    }

    if (deviceStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <EntitiesTable
            tableTitle="Devices"
            headCells={[
                {
                    id: "id",
                    disablePadding: true,
                    label: "Device Id",
                },
                {
                    id: "description",
                    disablePadding: false,
                    label: "Device Name",
                },
                {
                    id: "interval",
                    disablePadding: false,
                    label: "Interval (ms)",
                }
            ]}
            rows={deviceStore.devices.map((d) => [
                d.id,
                d.id,
                d.name,
                d.conditionsIntervalCheckInMs,
            ])}
            onCreate={onDeviceCreate}
            onUpdate={onDeviceUpdate}
            onDeleteConfirmation={onDeviceDeleteConfirmation}
        />
    );
};

export default observer(DevicesList);
