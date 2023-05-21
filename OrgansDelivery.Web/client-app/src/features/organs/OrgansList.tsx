import React, { useEffect, useMemo } from "react";
import { useStore } from "../../app/stores/store";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";
import { router } from "../../app/router/Routes";
import AddOrganModal from "./AddOrganModal";
import UpdateOrganModal from "./UpdateOrganModal";
import { useTranslation } from "react-i18next";

const OrgansList = () => {
    const { organStore, containerStore, conditionsStore, modalStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "lists.organs" });

    useEffect(() => {
        organStore.loadOrgans();
        containerStore.loadContainers();
        conditionsStore.loadConditions();
    }, [organStore, containerStore, conditionsStore]);
    
    const onOrganClick = (organId: string) => {
        router.navigate(`/organs/${organId}`);
    }

    const unusedContainers = useMemo(
        () => containerStore.containers.filter(c => c.organ === undefined),
        [containerStore.containers]);

    const onOrganCreate = () => {
        modalStore.openModal(
            <AddOrganModal
                conditions={conditionsStore.conditions}
                containers={unusedContainers}
            />
        );
    };

    const onOrganUpdate = (organId: string) => {
        const organ = organStore.organs.find((o) => o.id === organId);
        if (!organ) {
            return;
        }

        const container = containerStore.containers.find(c => c.organ?.id === organId);

        modalStore.openModal(
            <UpdateOrganModal
                conditions={conditionsStore.conditions}
                containers={[
                    ...(container ? [container] : []),
                    ...unusedContainers,
                ]}
                organ={organ}
            />
        );
    };

    const onOrganDeleteConfirmation = (organId: string) => {
        organStore.deleteOrgan(organId);
    }

    if (
        organStore.isLoading ||
        containerStore.isLoading ||
        conditionsStore.isLoading
    ) {
        return <LoadingBackdrop />;
    }

    return (
        <EntitiesTable
            tableTitle={t('entityName')}
            headCells={[
                {
                    id: "name",
                    disablePadding: true,
                    label: t("organName"),
                },
                {
                    id: "description",
                    disablePadding: false,
                    label: t("description"),
                },
                {
                    id: "creation-date",
                    disablePadding: false,
                    label: t("creationDate"),
                },
                {
                    id: "container-name",
                    disablePadding: false,
                    label: t("containerName"),
                },
                {
                    id: "container-name",
                    disablePadding: false,
                    label: t("conditionsName"),
                }
            ]}
            rows={organStore.organs.map(o => [
                o.id,
                o.name,
                o.description,
                o.organCreationDate.toString(),
                o.container.name,
                o.conditions.name,
            ])}
            onClick={onOrganClick}
            onCreate={onOrganCreate}
            onUpdate={onOrganUpdate}
            onDeleteConfirmation={onOrganDeleteConfirmation}
        />
    );
};

export default observer(OrgansList);
