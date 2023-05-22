import React from "react";
import { TooltipProps } from 'recharts';
import { Typography } from "@mui/material";
import { useTranslation } from "react-i18next";

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
    valueTitle: string;
    unitName: string;
};

const ConditionsTooltip = (props: Props) => {
    const { active, payload, valueTitle, unitName } = props;
    const { t } = useTranslation("translation", { keyPrefix: "details" });

    if (!active) {
        return null;
    }        

    const data = !!payload?.length
        ? (payload[0].payload as RawConditionsRecordUnit)
        : undefined;
    
    const dateTime = data
        ? new Date(data.dateTimeTimestamp).toLocaleString()
        : " -- ";

    return (
        <div className="area-chart-tooltip" style={style}>
            <Typography variant="body1" >{t('chartDate')}: {dateTime}</Typography>
            <Typography variant="body1" >{valueTitle}: {data?.value} {unitName}</Typography>
        </div>
    );
};

export default ConditionsTooltip;
