import React from "react";
import EntityFormModal from "../../app/modals/EntityFormModal";
import { useFormik } from "formik";
import { Grid, TextField } from "@mui/material";
import * as Yup from "yup";
import { ConditionsFormValues } from "../../app/models/conditions";
import DecimalConditionField from "../../app/layout/DecimalConditionField";
import OrientationConditionField from "../../app/layout/OrientationConditionField";

const conditionValidationSchema = Yup.object({
    expectedValue: Yup.number().required(),
    allowedDeviation: Yup.number().required(),
});

const validationSchema = Yup.object({
    name: Yup.string().required(),
    humidity: conditionValidationSchema,
    light: conditionValidationSchema,
    temperature: conditionValidationSchema,
    orientation: conditionValidationSchema,
});

type Props = {
    initialValues: ConditionsFormValues;
    actionName: string;
    onSubmit: (conditions: ConditionsFormValues) => void;
};

const ConditionsModal = (props: Props) => {
    const { initialValues, actionName, onSubmit } = props;

    const formik = useFormik<ConditionsFormValues>({
        initialValues,
        onSubmit,
        validationSchema,
    });

    return (
        <EntityFormModal
            maxWidth="md"
            entityName="Organ"
            actionName={actionName}
            onSubmit={formik.handleSubmit}
        >
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        name="name"
                        label="Organ Name"
                        required
                        fullWidth
                        autoFocus
                        onChange={formik.handleChange}
                        value={formik.values.name}
                        error={
                            formik.touched.name && Boolean(formik.errors.name)
                        }
                        helperText={formik.touched.name && formik.errors.name}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="description"
                        label="Description"
                        required
                        fullWidth
                        onChange={formik.handleChange}
                        value={formik.values.description}
                        error={
                            formik.touched.description &&
                            Boolean(formik.errors.description)
                        }
                        helperText={
                            formik.touched.description &&
                            formik.errors.description
                        }
                    />
                </Grid>
                <DecimalConditionField
                    conditionName="Humidity"
                    condition={formik.values.humidity}
                    onChange={(humidity) => {
                        formik.setFieldValue('humidity', humidity, true)
                    }}
                />
                <DecimalConditionField
                    conditionName="Light"
                    condition={formik.values.light}
                    onChange={(light) => {
                        formik.setFieldValue('light', light, true)
                    }}
                />
                <DecimalConditionField
                    conditionName="Temperature"
                    condition={formik.values.temperature}
                    onChange={(temperature) => {
                        formik.setFieldValue('temperature', temperature, true)
                    }}
                />
                <OrientationConditionField
                    conditionName="Orientation"
                    condition={formik.values.orientation}
                    onChange={(orientation) => {
                        formik.setFieldValue('orientation', orientation, true)
                    }}
                />
            </Grid>
        </EntityFormModal>
    );
};

export default ConditionsModal;
