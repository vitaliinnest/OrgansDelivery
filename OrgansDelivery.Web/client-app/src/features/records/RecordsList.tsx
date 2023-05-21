import React from "react";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";
import { ConditionsRecord } from "../../app/models/conditionsRecord";
import { Orientation } from "../../app/models/conditions";

type Props = {
    records: ConditionsRecord[];
}

const RecordsList = (props: Props) => {
    const { records } = props;

    return (
        <EntitiesTable
            tableTitle="Records"
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