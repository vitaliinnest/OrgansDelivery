import React, { useState } from "react";
import { observer } from "mobx-react-lite";
import EntityFormModal from "../../app/modals/EntityFormModal";
import { useStore } from "../../app/stores/store";
import { useFormik } from "formik";
import { OrganFormValues } from "../../app/models/organ";
import { Grid, TextField } from "@mui/material";
import * as Yup from "yup";
import { DateTimePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";

const validationSchema = Yup.object({
    name: Yup.string().required(),
    organCreationDate: Yup.date().required(),
});

type Props = {
    initialValues: OrganFormValues;
    actionName: string;
    onSubmit: (organ: OrganFormValues) => void;
};

const OrganModal = (props: Props) => {
    const { initialValues, actionName, onSubmit } = props;

    const formik = useFormik<OrganFormValues>({
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
                <Grid item xs={12}>
                    <DateTimePicker
                        label="Creation Date"
                        onChange={(value) =>
                            formik.setFieldValue(
                                "organCreationDate",
                                value?.toDate(),
                                true
                            )
                        }
                        defaultValue={dayjs(
                            formik.initialValues.organCreationDate
                        )}
                    />
                </Grid>
            </Grid>
        </EntityFormModal>
    );
};

export default observer(OrganModal);
