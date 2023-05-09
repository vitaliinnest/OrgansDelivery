import React, { useEffect } from "react";
import { useStore } from "../../app/stores/store";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";

const OrgansList = () => {
    const { organStore } = useStore();

    useEffect(() => {
        organStore.loadOrgans();
    }, [organStore]);

    if (organStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <EntitiesTable
            tableTitle="Organs"
            headCells={[
                {
                    id: "name",
                    numeric: false,
                    disablePadding: true,
                    label: "Organ Name",
                },
                {
                    id: "calories",
                    numeric: true,
                    disablePadding: false,
                    label: "Description",
                },
                {
                    id: "fat",
                    numeric: true,
                    disablePadding: false,
                    label: "Container Name",
                },
                {
                    id: "carbs",
                    numeric: true,
                    disablePadding: false,
                    label: "Creation Date",
                },
            ]}
            
            rows={organStore.organs.map(o => [
                o.name,
                o.description,
                o.containerName ?? "-",
                o.organCreationDate.toString(),
            ])}
        />
    );
};

export default observer(OrgansList);
