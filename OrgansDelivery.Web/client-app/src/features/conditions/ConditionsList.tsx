import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import EntitiesTable from "../../app/layout/EntitiesTable";
import AddConditionsModal from "./AddConditionsModal";
import UpdateConditionsModal from "./UpdateConditionsModal";
import { useTranslation } from "react-i18next";
import { orientationToString } from "../records/RecordsList";
import { Condition, Orientation } from "../../app/models/conditions";
import { unitByValueNameMap } from "../../app/util/common";

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
            tableTitle={`${t('entitiesName')} (${t('expected')} / ${t('allowedDeviation')})`}
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
                {
                    id: "temperature",
                    disablePadding: false,
                    label: `${t('temperature')}, ${unitByValueNameMap['temperature']}`,
                },
                {
                    id: "humidity",
                    disablePadding: false,
                    label: `${t("humidity")}, ${unitByValueNameMap['humidity']}`,
                },
                {
                    id: "light",
                    disablePadding: false,
                    label: `${t("light")}, ${unitByValueNameMap['light']}`,
                },
                {
                    id: "orientation",
                    disablePadding: false,
                    label: `${t("orientation")}, ${unitByValueNameMap['orientation']}`,
                }
            ]}
            rows={conditionsStore.conditions.map((c) => [
                c.id,
                c.name,
                c.description,
                numberConditionToString(c.temperature),
                numberConditionToString(c.humidity),
                numberConditionToString(c.light),
                orientationConditionToString(c.orientation),
            ])}
            onClick={onConditionsUpdate}
            onCreate={onConditionsCreate}
            onUpdate={onConditionsUpdate}
            onDeleteConfirmation={onConditionsDeleteConfirmation}
        />
    );
};

export default observer(ConditionsList);

export const numberConditionToString = (result: Condition<number>) => {
    const { expectedValue, allowedDeviation } = result;
    return `${expectedValue} / ${allowedDeviation}`;
}

export const orientationConditionToString = (result: Condition<Orientation>) => {
    const { expectedValue, allowedDeviation } = result;
    return `${orientationToString(expectedValue)} / ${orientationToString(allowedDeviation)}`;
}
