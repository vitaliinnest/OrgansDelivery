import React, { useMemo } from "react";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";
import { ComparedResult, ConditionsViolation } from "../../app/models/conditionsRecord";
import { Orientation } from "../../app/models/conditions";
import { orientationToString } from "./RecordsList";
import { unitByValueNameMap } from "../../app/util/common";

type Props = {
    violations: ConditionsViolation[];
}

const ViolationsList = (props: Props) => {
    const { violations } = props;
    const violationByRecordIdMap = useMemo(
        () => violations.reduce((map, obj) => {
            map.set(obj.record.id, obj);
            return map;
        }, new Map<string, ConditionsViolation>()),
        [violations]
    );

    return (
        <EntitiesTable
            tableTitle="Violations (Actual / Expected / Allowed Deviation)"
            headCells={[
                {
                    id: "record-date",
                    disablePadding: true,
                    label: "Record Date",
                },
                {
                    id: "temperature",
                    disablePadding: false,
                    label: `Temperature, ${unitByValueNameMap['temperature']}`,
                },
                {
                    id: "humidity",
                    disablePadding: false,
                    label: `Humidity, ${unitByValueNameMap['humidity']}`,
                },
                {
                    id: "light",
                    disablePadding: false,
                    label: `Light, ${unitByValueNameMap['light']}`,
                },
                {
                    id: "orientation",
                    disablePadding: false,
                    label: `Orientation, ${unitByValueNameMap['orientation']}`,
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
                console.log(recordId, columnId);
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

// todo: add background color to violated cells
const numberComparedResultToString = (result: ComparedResult<number>) => {
    const { actual, expectedValue, allowedDeviation } = result;
    return `${actual} / ${expectedValue} / ${allowedDeviation}`;
}

// todo: add background color to violated cells
const orientationComparedResultToString = (result: ComparedResult<Orientation>) => {
    const { actual, expectedValue, allowedDeviation } = result;
    return `${orientationToString(actual)} / ${orientationToString(expectedValue)} / ${orientationToString(allowedDeviation)}`;
}
