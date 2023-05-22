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
import { capitalize } from "@mui/material";
import { ConditionsRecord } from "../../models/conditionsRecord";
import { unitByValueNameMap } from "../../util/common";
import { useTranslation } from "react-i18next";

export type ConditionsRecordUnit = {
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
    valueTitle: string;
    valueName: keyof ConditionsRecord;
    data: ConditionsRecordUnit[];
    range?: DateRange;
};

const ConditionsLineChart = (props: Props) => {
    const { valueName, valueTitle } = props;
    const data = useMemo(
        () => props.data.map<RawConditionsRecordUnit>(d => ({...d, dateTimeTimestamp: d.dateTime.getTime() })),
        [props.data]);

    const { t } = useTranslation('translation', { keyPrefix: 'details' });

    const domain: AxisDomain = ['dataMin', 'dataMax'];

    const unit = unitByValueNameMap[valueName] ?? "";

    return (
        <LineChart
            width={800}
            height={380}
            data={data}
            margin={{ top: 40, right: 20, bottom: 50, left: 40 }}
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
                    value={t('chartDate').toString()}
                    style={{ textAnchor: "middle" }}
                    position="insideBottom"
                    offset={-20}
                />
            </XAxis>
            <YAxis name="y axis">
                <Label
                    value={`${valueTitle}, ${unit}`}
                    style={{ textAnchor: "middle" }}
                    angle={-90}
                    position="insideLeft"
                    offset={-15}
                />
            </YAxis>
            <Tooltip
                content={
                    <ConditionsTooltip
                        valueTitle={valueTitle}
                        unitName={unit}
                    />
                }
            />
        </LineChart>
    );
};

export default ConditionsLineChart;
