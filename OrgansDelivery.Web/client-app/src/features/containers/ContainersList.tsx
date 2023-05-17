import React, { useMemo } from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import EntitiesTable from "../../app/layout/EntitiesTable";
import AddContainerModal from "./AddContainerModal";
import UpdateContainerModal from "./UpdateContainerModal";

const ContainersList = () => {
    const { modalStore, containerStore, deviceStore } = useStore();

    useEffect(() => {
        containerStore.loadContainers();
        deviceStore.loadDevices();
    }, [containerStore, deviceStore]);

    const unusedDevices = useMemo(
        () => deviceStore.devices.filter(d => d.container === undefined),
        [deviceStore.devices]);

    const onContainerCreate = () => {
        modalStore.openModal(
            <AddContainerModal
                devices={unusedDevices}
            />
        );
    };

    const onContainerUpdate = (containerId: string) => {
        const container = containerStore.containers.find(
            (c) => c.id === containerId
        );
        if (!container) {
            return;
        }

        const device = deviceStore.devices.find(d => d.container?.id === containerId);

        modalStore.openModal(
            <UpdateContainerModal
                container={container}
                devices={[
                    ...(device ? [device] : []),
                    ...unusedDevices
                ]}
            />
        );
    };

    const onContainerDeleteConfirmation = (containerId: string) => {
        containerStore.deleteContainer(containerId);
    }

    if (containerStore.isLoading || deviceStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <EntitiesTable
            tableTitle="Containers"
            headCells={[
                {
                    id: "name",
                    disablePadding: true,
                    label: "Container Name",
                },
                {
                    id: "description",
                    disablePadding: false,
                    label: "Description",
                },
                {
                    id: "device-name",
                    disablePadding: false,
                    label: "Device name",
                }
            ]}
            rows={containerStore.containers.map((c) => [
                c.id,
                c.name,
                c.description,
                c.device.name,
            ])}
            onCreate={onContainerCreate}
            onUpdate={onContainerUpdate}
            onDeleteConfirmation={onContainerDeleteConfirmation}
        />
    );
};

export default observer(ContainersList);
