import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import EntitiesTable from "../../app/layout/EntitiesTable";
import AddDeviceModal from "./AddDeviceModal";
import UpdateDeviceModal from "./UpdateDeviceModal";
import { useTranslation } from "react-i18next";

const DevicesList = () => {
    const { modalStore, deviceStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "lists.devices" });

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
            tableTitle={t('entitiesName')}
            headCells={[
                {
                    id: "id",
                    disablePadding: true,
                    label: t("deviceId"),
                },
                {
                    id: "description",
                    disablePadding: false,
                    label: t("deviceName"),
                },
                {
                    id: "interval",
                    disablePadding: false,
                    label: t("interval"),
                }
            ]}
            rows={deviceStore.devices.map((d) => [
                d.id,
                d.id,
                d.name,
                d.conditionsIntervalCheckInMs,
            ])}
            onClick={onDeviceUpdate}
            onCreate={onDeviceCreate}
            onUpdate={onDeviceUpdate}
            onDeleteConfirmation={onDeviceDeleteConfirmation}
        />
    );
};

export default observer(DevicesList);
