import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import EntitiesTable from "../../app/layout/EntitiesTable";
import AddConditionsModal from "./AddConditionsModal";
import UpdateConditionsModal from "./UpdateConditionsModal";
import { useTranslation } from "react-i18next";

const ConditionsList = () => {
    const { modalStore, conditionsStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "lists.conditions" });

    useEffect(() => {
        conditionsStore.loadConditions();
    }, [conditionsStore]);

    const onConditionsCreate = () => {
        modalStore.openModal(
            <AddConditionsModal />
        );
    };

    const onConditionsUpdate = (conditionsId: string) => {
        const conditions = conditionsStore.conditions.find(
            (c) => c.id === conditionsId
        );
        if (!conditions) {
            return;
        }

        modalStore.openModal(
            <UpdateConditionsModal
                conditions={conditions}
            />
        );
    };

    const onConditionsDeleteConfirmation = (conditionsId: string) => {
        conditionsStore.deleteConditions(conditionsId);
    }

    if (conditionsStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <EntitiesTable
            tableTitle={t('entitiesName')}
            headCells={[
                {
                    id: "name",
                    disablePadding: true,
                    label: t("conditionsName"),
                },
                {
                    id: "description",
                    disablePadding: false,
                    label: t("description"),
                },
            ]}
            rows={conditionsStore.conditions.map((c) => [
                c.id,
                c.name,
                c.description,
            ])}
            onClick={onConditionsUpdate}
            onCreate={onConditionsCreate}
            onUpdate={onConditionsUpdate}
            onDeleteConfirmation={onConditionsDeleteConfirmation}
        />
    );
};

export default observer(ConditionsList);
