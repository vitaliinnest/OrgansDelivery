import React from "react";
import { Grid, TextField } from "@mui/material";
import { Condition } from "../models/conditions";

type Props = {
    conditionName: string;
    condition: Condition<number>;
    onChange: (condition: Condition<number>) => void;
};

const DecimalConditionField = (props: Props) => {
    const { conditionName, condition, onChange } = props;

    const onChangeExpectedValue = (e: React.ChangeEvent<HTMLInputElement>) => {
        onChange({
            ...condition,
            expectedValue: Number(e.target.value),
        });
    };

    const onChangeAllowedDeviation = (e: React.ChangeEvent<HTMLInputElement>) => {
        onChange({
            ...condition,
            allowedDeviation: Number(e.target.value),
        });
    };

    return (
        <>
            <Grid item sm={6}>
                <TextField
                    name="name"
                    label="Expected Value"
                    required
                    fullWidth
                    type="number"
                    onChange={onChangeExpectedValue}
                    value={condition.expectedValue}
                    helperText={conditionName}
                />
            </Grid>
            <Grid item sm={6}>
                <TextField
                    name="name"
                    label="Allowed Deviation"
                    required
                    fullWidth
                    type="number"
                    onChange={onChangeAllowedDeviation}
                    value={condition.allowedDeviation}
                />
            </Grid>
        </>
    )
}

export default DecimalConditionField;