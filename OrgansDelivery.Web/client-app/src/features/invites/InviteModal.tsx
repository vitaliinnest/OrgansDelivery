import React from "react";
import EntityFormModal, { ActionType } from "../../app/modals/EntityFormModal";
import { useFormik } from "formik";
import { Grid, TextField } from "@mui/material";
import * as Yup from "yup";
import { InviteFormValues } from "../../app/models/invite";

const validationSchema = Yup.object({
    email: Yup.string().email().required(),
});

type Props = {
    initialValues: InviteFormValues;
    action: ActionType;
    onSubmit: (invite: InviteFormValues) => void;
};

const InviteModal = (props: Props) => {
    const { initialValues, action, onSubmit } = props;

    const formik = useFormik<InviteFormValues>({
        initialValues,
        onSubmit,
        validationSchema,
    });

    return (
        <EntityFormModal
            entityName="Invite"
            action={action}
            onSubmit={formik.handleSubmit}
        >
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        name="email"
                        label="Email"
                        required
                        fullWidth
                        autoFocus
                        onChange={formik.handleChange}
                        value={formik.values.email}
                        error={
                            formik.touched.email && Boolean(formik.errors.email)
                        }
                        helperText={formik.touched.email && formik.errors.email}
                    />
                </Grid>
            </Grid>
        </EntityFormModal>
    );
};

export default InviteModal;
