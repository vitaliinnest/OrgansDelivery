import React from "react";
import { observer } from "mobx-react-lite";
import EntityFormModal from "../../app/modals/EntityFormModal";
import { useFormik } from "formik";
import { FormControl, FormHelperText, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import * as Yup from "yup";
import { ContainerFormValues } from "../../app/models/container";
import { Device } from "../../app/models/device";

const validationSchema = Yup.object({
    name: Yup.string().required(),
    deviceId: Yup.string().required(),
});

type Props = {
    initialValues: ContainerFormValues;
    actionName: string;
    devices: Device[];
    onSubmit: (organ: ContainerFormValues) => void;
};

const ContainerModal = (props: Props) => {
    const { initialValues, actionName, devices, onSubmit } = props;

    const formik = useFormik<ContainerFormValues>({
        initialValues,
        onSubmit,
        validationSchema
    });

    const noDevices = devices.length === 0;

    return (
        <EntityFormModal
            entityName="Container"
            actionName={actionName}
            onSubmit={formik.handleSubmit}
        >
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        name="name"
                        label="Container Name"
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
                            formik.touched.description && Boolean(formik.errors.description)
                        }
                        helperText={formik.touched.description && formik.errors.description}
                    />
                </Grid>
                <Grid item xs={12}>
                    <FormControl
                        required
                        fullWidth
                        disabled={noDevices}
                        error={noDevices}
                    >
                        <InputLabel>
                            Device
                        </InputLabel>
                        <Select
                            name="deviceId"
                            value={formik.values.deviceId}
                            label="Device *"
                            fullWidth
                            onChange={(e) => {
                                formik.handleChange(e);
                            }}
                            error={
                                formik.touched.deviceId &&
                                Boolean(formik.errors.deviceId)
                            }
                        >
                            {devices.map(d => (
                                <MenuItem value={d.id}>{d.name}</MenuItem>
                            ))}
                        </Select>
                        <FormHelperText>
                            {(noDevices && "Add a device first")
                            || (formik.touched.deviceId && formik.errors.deviceId)}
                        </FormHelperText>
                    </FormControl>
                </Grid>
            </Grid>
        </EntityFormModal>
    );
};

export default observer(ContainerModal);
