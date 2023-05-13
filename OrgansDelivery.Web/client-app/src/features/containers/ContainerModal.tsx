import React from "react";
import { observer } from "mobx-react-lite";
import EntityFormModal from "../../app/modals/EntityFormModal";
import { useFormik } from "formik";
import { Grid, TextField } from "@mui/material";
import * as Yup from "yup";
import { ContainerFormValues } from "../../app/models/container";
import { Device } from "../../app/models/device";

const validationSchema = Yup.object({
    name: Yup.string().required(),
    organCreationDate: Yup.date().required(),
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
                            formik.touched.description && Boolean(formik.errors.description)
                        }
                        helperText={formik.touched.description && formik.errors.description}
                    />
                </Grid>
            </Grid>
        </EntityFormModal>
    );
};

export default observer(ContainerModal);
