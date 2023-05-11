import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import EntitiesTable from "../../app/layout/EntitiesTable";
import AddContainerModal from "./AddContainerModal";

const ContainersList = () => {
    const { containerStore, modalStore } = useStore();

    useEffect(() => {
        containerStore.loadContainers();
    }, [containerStore]);

    const onContainerCreate = () => {
        modalStore.openModal(<AddContainerModal />);
    }

    if (containerStore.isLoading) {
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
            rows={containerStore.containers.map(c => [
                c.id,
                c.name,
                c.description,
            ])}
            // onClick={onContainerClick}
            // onCreate={onContainerCreate}
            // onUpdate={onContainerUpdate}
            // onDelete={onContainerDelete}
        />
    );
}

export default observer(ContainersList);
