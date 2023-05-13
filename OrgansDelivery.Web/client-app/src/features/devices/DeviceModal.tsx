import React from "react";
import EntityFormModal from "../../app/modals/EntityFormModal";
import { useFormik } from "formik";
import { Grid, TextField } from "@mui/material";
import * as Yup from "yup";
import { DeviceFormValues } from "../../app/models/device";
import { guid } from "../../app/util/validation";

const validationSchema = Yup.object({
    id: guid(),
    name: Yup.string().required(),
    conditionsIntervalCheckInMs: Yup.number().required(),
});

type Props = {
    initialValues: DeviceFormValues;
    actionName: string;
    onSubmit: (device: DeviceFormValues) => void;
};

const DeviceModal = (props: Props) => {
    const { initialValues, actionName, onSubmit } = props;

    const formik = useFormik<DeviceFormValues>({
        initialValues,
        onSubmit,
        validationSchema,
    });

    return (
        <EntityFormModal
            entityName="Organ"
            actionName={actionName}
            onSubmit={formik.handleSubmit}
        >
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        name="id"
                        label="Device Id"
                        required
                        fullWidth
                        autoFocus
                        onChange={formik.handleChange}
                        value={formik.values.id}
                        error={
                            formik.touched.id && Boolean(formik.errors.id)
                        }
                        helperText={formik.touched.id && formik.errors.id}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="name"
                        label="Device Name"
                        required
                        fullWidth
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
                        name="conditionsIntervalCheckInMs"
                        label="Interval (ms)"
                        type="number"
                        required
                        fullWidth
                        onChange={formik.handleChange}
                        value={formik.values.conditionsIntervalCheckInMs}
                        error={
                            formik.touched.conditionsIntervalCheckInMs
                            && Boolean(formik.errors.conditionsIntervalCheckInMs)
                        }
                        helperText={formik.touched.conditionsIntervalCheckInMs
                            && formik.errors.conditionsIntervalCheckInMs}
                    />
                </Grid>
            </Grid>
        </EntityFormModal>
    );
};

export default DeviceModal;
