import React from "react";
import { format } from "date-fns";
import { TooltipProps } from 'recharts';
import { Typography } from "@mui/material";

const style = {
    padding: 6,
    backgroundColor: "#fff",
    border: "1px solid #ccc",
};

export type RawConditionsRecordUnit = {
    value: number;
    dateTimeTimestamp: number;
};

type Props = TooltipProps<any, any> & {
    valueName: string
    unitName: string;
};

const ConditionsTooltip = (props: Props) => {
    const { active, payload, valueName, unitName } = props;

    if (!active) {
        return null;
    }
    
    const data = !!payload?.length
        ? (payload[0].payload as RawConditionsRecordUnit)
        : undefined;
    
    const dateTime = data
        ? format(new Date(data.dateTimeTimestamp), "MM/dd/yyyy")
        : " -- ";

    return (
        <div className="area-chart-tooltip" style={style}>
            <Typography variant="body1" >Date: {dateTime}</Typography>
            <Typography variant="body1" >{valueName}: {data?.value} {unitName}</Typography>
        </div>
    );
};

export default ConditionsTooltip;
