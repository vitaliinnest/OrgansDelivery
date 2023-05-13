import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import EntitiesTable from "../../app/layout/EntitiesTable";
import AddConditionsModal from "./AddConditionsModal";

const ConditionsList = () => {
    const { modalStore, conditionsStore } = useStore();

    useEffect(() => {
        conditionsStore.loadConditions();
    }, [conditionsStore]);

    const onConditionsCreate = () => {
        modalStore.openModal(
            <AddConditionsModal />
        );
    };

    const onConditionsUpdate = (containerId: string) => {
        // const container = containerStore.containers.find(
        //     (c) => c.id === containerId
        // );
        // if (!container) {
        //     return;
        // }

        // modalStore.openModal(
        //     <UpdateContainerModal
        //         container={container}
        //         devices={deviceStore.devices}
        //     />
        // );
    };

    const onConditionsDeleteConfirmation = (conditionsId: string) => {
        conditionsStore.deleteConditions(conditionsId);
    }

    if (conditionsStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <EntitiesTable
            tableTitle="Conditions"
            headCells={[
                {
                    id: "name",
                    disablePadding: true,
                    label: "Conditions Name",
                },
                {
                    id: "description",
                    disablePadding: false,
                    label: "Description",
                },
            ]}
            rows={conditionsStore.conditions.map((c) => [
                c.id,
                c.name,
                c.description,
            ])}
            onCreate={onConditionsCreate}
            onUpdate={onConditionsUpdate}
            onDeleteConfirmation={onConditionsDeleteConfirmation}
        />
    );
};

export default observer(ConditionsList);
