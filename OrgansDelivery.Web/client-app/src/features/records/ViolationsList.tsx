import React, { useMemo } from "react";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";
import { ComparedResult, ConditionsViolation } from "../../app/models/conditionsRecord";
import { Orientation } from "../../app/models/conditions";
import { orientationToString } from "./RecordsList";
import { unitByValueNameMap } from "../../app/util/common";
import { useTranslation } from "react-i18next";

type Props = {
    violations: ConditionsViolation[];
}

const ViolationsList = (props: Props) => {
    const { violations } = props;
    const { t } = useTranslation('translation', { keyPrefix: 'details' });

    const violationByRecordIdMap = useMemo(
        () => violations.reduce((map, obj) => {
            map.set(obj.record.id, obj);
            return map;
        }, new Map<string, ConditionsViolation>()),
        [violations]
    );

    return (
        <EntitiesTable
            tableTitle={`${t("violations")} (${t('actual')} / ${t("expected")} / ${t("allowedDeviation")})`}
            headCells={[
                {
                    id: "record-date",
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
            rows={violations.map(v => [
                v.record.id,
                v.record.dateTime.toLocaleString(),
                numberComparedResultToString(v.temperature),
                numberComparedResultToString(v.humidity),
                numberComparedResultToString(v.light),
                orientationComparedResultToString(v.orientation),
            ])}
            cellSx={(recordId, columnId) => {
                if (!columnId) return;

                const violation = violationByRecordIdMap.get(recordId);
                if (!violation) return;
                
                const comparedResult = (violation as any)[columnId.toLowerCase()] as any as ComparedResult<any>;
                if (!comparedResult) return;

                if (!comparedResult.isViolated) return;

                return { pl: 1, backgroundColor: 'red', color: 'white' };
            }}
        />
    );
};

export default observer(ViolationsList);

const numberComparedResultToString = (result: ComparedResult<number>) => {
    const { actual, expectedValue, allowedDeviation } = result;
    return `${actual} / ${expectedValue} / ${allowedDeviation}`;
}

const orientationComparedResultToString = (result: ComparedResult<Orientation>) => {
    const { actual, expectedValue, allowedDeviation } = result;
    return `${orientationToString(actual)} / ${orientationToString(expectedValue)} / ${orientationToString(allowedDeviation)}`;
}
