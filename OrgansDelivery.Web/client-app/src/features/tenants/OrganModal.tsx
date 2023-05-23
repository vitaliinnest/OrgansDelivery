import React from "react";
import EntityFormModal, { ActionType } from "../../app/modals/EntityFormModal";
import { useFormik } from "formik";
import { Grid, TextField } from "@mui/material";
import * as Yup from "yup";
import { TenantFormValues } from "../../app/models/tenant";

const validationSchema = Yup.object({
    name: Yup.string().required(),
});

type Props = {
    initialValues: TenantFormValues;
    action: ActionType;
    onSubmit: (tenant: TenantFormValues) => void;
};

const TenantModal = (props: Props) => {
    const { initialValues, action, onSubmit } = props;

    const formik = useFormik<TenantFormValues>({
        initialValues,
        onSubmit,
        validationSchema,
    });

    return (
        <EntityFormModal
            entityName="Tenant"
            action={action}
            onSubmit={formik.handleSubmit}
        >
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        name="name"
                        label="Tenant Name"
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
            </Grid>
        </EntityFormModal>
    );
};

export default TenantModal;
