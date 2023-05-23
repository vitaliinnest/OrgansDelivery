import React from "react";
import EntityFormModal, { ActionType } from "../../app/modals/EntityFormModal";
import { useFormik } from "formik";
import { Grid, TextField } from "@mui/material";
import * as Yup from "yup";
import { UpdateUser } from "../../app/models/user";

const validationSchema = Yup.object({
    name: Yup.string().required(),
    surname: Yup.string().required(),
});

type Props = {
    initialValues: UpdateUser;
    action: ActionType;
    onSubmit: (user: UpdateUser) => void;
};

const ProfileModal = (props: Props) => {
    const { initialValues, action, onSubmit } = props;

    const formik = useFormik<UpdateUser>({
        initialValues,
        onSubmit,
        validationSchema,
    });

    return (
        <EntityFormModal
            entityName="Profile"
            action={action}
            onSubmit={formik.handleSubmit}
        >
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        name="name"
                        label="First Name"
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
                        name="surname"
                        label="Last Name"
                        required
                        fullWidth
                        onChange={formik.handleChange}
                        value={formik.values.surname}
                        error={
                            formik.touched.surname && Boolean(formik.errors.surname)
                        }
                        helperText={formik.touched.surname && formik.errors.surname}
                    />
                </Grid>
            </Grid>
        </EntityFormModal>
    );
};

export default ProfileModal;
