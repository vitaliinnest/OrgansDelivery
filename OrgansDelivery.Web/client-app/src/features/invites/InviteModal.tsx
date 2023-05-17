import React from "react";
import EntityFormModal from "../../app/modals/EntityFormModal";
import { useFormik } from "formik";
import { OrganFormValues } from "../../app/models/organ";
import { FormControl, FormHelperText, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import * as Yup from "yup";
import { DateTimePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";
import { Container } from "../../app/models/container";
import { Conditions } from "../../app/models/conditions";
import { InviteFormValues } from "../../app/models/invite";

const validationSchema = Yup.object({
    email: Yup.string().email().required(),
});

type Props = {
    initialValues: InviteFormValues;
    actionName: string;
    onSubmit: (invite: InviteFormValues) => void;
};

const InviteModal = (props: Props) => {
    const { initialValues, actionName, onSubmit } = props;

    const formik = useFormik<InviteFormValues>({
        initialValues,
        onSubmit,
        validationSchema,
    });

    return (
        <EntityFormModal
            entityName="Invite"
            actionName={actionName}
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
