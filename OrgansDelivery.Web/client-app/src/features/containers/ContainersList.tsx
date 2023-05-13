import React from "react";
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

    const onContainerCreate = () => {
        modalStore.openModal(
            <AddContainerModal devices={deviceStore.devices} />
        );
    };

    const onContainerUpdate = (containerId: string) => {
        const container = containerStore.containers.find(
            (c) => c.id === containerId
        );
        if (!container) {
            return;
        }

        modalStore.openModal(
            <UpdateContainerModal
                container={container}
                devices={deviceStore.devices}
            />
        );
    };

    const onContainerDeleteConfirmation = (containerId: string) => {
        containerStore.deleteContainer(containerId);
    }

    if (containerStore.isLoading || containerStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <EntitiesTable
            tableTitle="Containers"
            headCells={[
                {
                    id: "name",
                    numeric: false,
                    disablePadding: true,
                    label: "Container Name",
                },
                {
                    id: "description",
                    numeric: true,
                    disablePadding: false,
                    label: "Description",
                },
            ]}
            rows={containerStore.containers.map((c) => [
                c.id,
                c.name,
                c.description,
            ])}
            onCreate={onContainerCreate}
            onUpdate={onContainerUpdate}
            onDeleteConfirmation={onContainerDeleteConfirmation}
        />
    );
};

export default observer(ContainersList);
