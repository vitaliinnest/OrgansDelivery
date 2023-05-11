import React, { useEffect } from "react";
import { useStore } from "../../app/stores/store";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";
import { router } from "../../app/router/Routes";
import { OrganFormValues  } from "../../app/models/organ";
import CreateOrganModal from "./CreateOrganModal";
import UpdateOrganModal from "./UpdateOrganModal";

const OrgansList = () => {
    const { organStore, modalStore } = useStore();

    useEffect(() => {
        organStore.loadOrgans();
    }, [organStore]);
    
    const onOrganClick = (organId: string) => {
        router.navigate(`/organs/${organId}`);
    }

    const onOrganCreate = () => {
        modalStore.openModal(<CreateOrganModal />);
    }

    const onOrganUpdate = (organId: string) => {
        console.log('fire')
        const organ = organStore.organs.find(o => o.id === organId);
        if (!organ) {
            return;
        }
        modalStore.openModal(<UpdateOrganModal organ={organ} />);
    }

    const onOrganDelete = (organId: string) => {
        organStore.deleteOrgan(organId);
    }

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
                o.id,
                o.name,
                o.description,
                o.containerName ?? "-",
                o.organCreationDate.toString(),
            ])}
            onClick={onOrganClick}
            onCreate={onOrganCreate}
            onUpdate={onOrganUpdate}
            onDelete={onOrganDelete}
        />
    );
};

export default observer(OrgansList);
