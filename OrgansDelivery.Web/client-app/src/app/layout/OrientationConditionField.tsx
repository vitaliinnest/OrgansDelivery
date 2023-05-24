import React from "react";
import { Grid, TextField, Typography } from "@mui/material";
import { Condition, Orientation } from "../models/conditions";

type Props = {
    conditionName: string;
    condition: Condition<Orientation>;
    onChange: (condition: Condition<Orientation>) => void;
};

const OrientationConditionField = (props: Props) => {
    const { conditionName, condition, onChange } = props;

    const onChangeExpectedValueX = (e: React.ChangeEvent<HTMLInputElement>) => {
        onChange({
            ...condition,
            expectedValue: {
                ...condition.expectedValue,
                x: Number(e.target.value)
            },
        });
    };

    const onChangeExpectedValueY = (e: React.ChangeEvent<HTMLInputElement>) => {
        onChange({
            ...condition,
            expectedValue: {
                ...condition.expectedValue,
                y: Number(e.target.value)
            },
        });
    };

    const onChangeAllowedDeviationX = (e: React.ChangeEvent<HTMLInputElement>) => {
        onChange({
            ...condition,
            allowedDeviation: {
                ...condition.allowedDeviation,
                x: Number(e.target.value)
            },
        });
    };

    const onChangeAllowedDeviationY = (e: React.ChangeEvent<HTMLInputElement>) => {
        onChange({
            ...condition,
            allowedDeviation: {
                ...condition.allowedDeviation,
                y: Number(e.target.value)
            },
        });
    };

    return (
        <>
            <Grid item sm={3}>
                <TextField
                    label="X Expected Value"
                    required
                    fullWidth
                    type="number"
                    onChange={onChangeExpectedValueX}
                    value={condition.expectedValue.x}
                    helperText={<Typography fontWeight="bold" variant="subtitle2" >{conditionName}</Typography>}
                />
            </Grid>
            <Grid item sm={3}>
                <TextField
                    label="Y Expected Value"
                    required
                    fullWidth
                    type="number"
                    onChange={onChangeExpectedValueY}
                    value={condition.expectedValue.y}
                />
            </Grid>
            <Grid item sm={3}>
                <TextField
                    label="X Allowed Deviation"
                    required
                    fullWidth
                    type="number"
                    onChange={onChangeAllowedDeviationX}
                    value={condition.allowedDeviation.x}
                />
            </Grid>
            <Grid item sm={3}>
                <TextField
                    label="Y Allowed Deviation"
                    required
                    fullWidth
                    type="number"
                    onChange={onChangeAllowedDeviationY}
                    value={condition.allowedDeviation.y}
                />
            </Grid>
        </>
    )
}

export default OrientationConditionField;