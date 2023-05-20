import { format } from "date-fns";
import React, { useMemo } from "react";
import {
    LineChart,
    Line,
    CartesianGrid,
    YAxis,
    XAxis,
    Tooltip,
    Label,
} from "recharts";
import ConditionsTooltip, { RawConditionsRecordUnit } from "./ConditionsTooltip";
import { AxisDomain } from "recharts/types/util/types";

type ConditionsRecordUnit = {
    value: number;
    dateTime: Date;
};

export type DateRange = {
    start: Date;
    end: Date;
}

const dateFormatter = (date: number) => {
    return format(new Date(date), "MM/dd/yyyy");
};

type Props = {
    valueName: string;
    unitName: string;
    data: ConditionsRecordUnit[];
};

const ConditionsLineChart = (props: Props) => {
    const { valueName, unitName } = props;
    const data = useMemo(
        () => props.data.map<RawConditionsRecordUnit>(d => ({...d, dateTimeTimestamp: d.dateTime.getTime() })),
        [props.data]);
    
    const domain: AxisDomain = [(dataMin: number) => dataMin, () => new Date(2022, 10, 25, 11).getTime()];

    return (
        <LineChart
            width={800}
            height={350}
            data={data}
            margin={{ top: 40, right: 20, bottom: 30, left: 25 }}
        >
            <Line type="monotone" dataKey="value" stroke="#8884d8" />
            <CartesianGrid stroke="#ccc" strokeDasharray="5 5" />
            <XAxis
                dataKey="dateTimeTimestamp"
                scale="time"
                tickFormatter={dateFormatter}
                domain={domain}
                type="number"
            >
                <Label
                    value="Date"
                    style={{ textAnchor: "middle" }}
                    position="insideBottom"
                    offset={-20}
                />
            </XAxis>
            <YAxis name="y axis">
                <Label
                    value={`${valueName}, ${unitName}`}
                    style={{ textAnchor: "middle" }}
                    angle={-90}
                    position="insideLeft"
                    offset={0}
                />
            </YAxis>
            <Tooltip
                content={
                    <ConditionsTooltip
                        valueName={valueName}
                        unitName={unitName}
                    />
                }
            />
        </LineChart>
    );
};

export default ConditionsLineChart;
