import React from "react";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";
import { ConditionsRecord } from "../../app/models/conditionsRecord";
import { Orientation } from "../../app/models/conditions";
import { useTranslation } from "react-i18next";
import { unitByValueNameMap } from "../../app/util/common";

type Props = {
    records: ConditionsRecord[];
}

const RecordsList = (props: Props) => {
    const { records } = props;
    const { t } = useTranslation('translation', { keyPrefix: 'details' });

    return (
        <EntitiesTable
            tableTitle={t("records")}
            headCells={[
                {
                    id: "name",
                    disablePadding: true,
                    label: t("recordDate"),
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
            rows={records.map(r => [
                r.id,
                r.dateTime.toLocaleString(),
                r.temperature,
                r.humidity,
                r.light,
                orientationToString(r.orientation),
            ])}
        />
    );
};

export default observer(RecordsList);

export const orientationToString = (orientation: Orientation) => {
    return `{${orientation.x}, ${orientation.y}}`;
}