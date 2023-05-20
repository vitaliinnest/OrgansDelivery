import React from "react";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";
import { ComparedResult, ConditionsViolation } from "../../app/models/conditionsRecord";
import { Orientation } from "../../app/models/conditions";
import { orientationToString } from "./RecordsList";

type Props = {
    violations: ConditionsViolation[];
}

const ViolationsList = (props: Props) => {
    const { violations } = props;
    
    return (
        <EntitiesTable
            tableTitle="Violations"
            headCells={[
                {
                    id: "name",
                    disablePadding: true,
                    label: "Record Date",
                },
                {
                    id: "temperature",
                    disablePadding: false,
                    label: "Temperature",
                },
                {
                    id: "humidity",
                    disablePadding: false,
                    label: "Humidity",
                },
                {
                    id: "light",
                    disablePadding: false,
                    label: "Light",
                },
                {
                    id: "orientation",
                    disablePadding: false,
                    label: "Orientation",
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
        />
    );
};

export default observer(ViolationsList);

// todo: add background color to violated cells
const numberComparedResultToString = (result: ComparedResult<number>) => {
    return `${result.actual} / ${result.expectedValue} / ${result.isViolated}`;
}

// todo: add background color to violated cells
const orientationComparedResultToString = (result: ComparedResult<Orientation>) => {
    const { actual, expectedValue } = result;
    return `{ ${orientationToString(actual)} } / { ${orientationToString(expectedValue)} } / ${result.isViolated}`;
}
