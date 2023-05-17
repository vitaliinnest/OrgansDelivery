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

const validationSchema = Yup.object({
    name: Yup.string().required(),
    organCreationDate: Yup.date().required(),
    containerId: Yup.string().required(),
    conditionsId: Yup.string().required(),
});

type Props = {
    initialValues: OrganFormValues;
    actionName: string;
    containers: Container[];
    conditions: Conditions[];
    onSubmit: (organ: OrganFormValues) => void;
};

const OrganModal = (props: Props) => {
    const { initialValues, actionName, containers, conditions, onSubmit } = props;

    const formik = useFormik<OrganFormValues>({
        initialValues,
        onSubmit,
        validationSchema,
    });

    const noContainers = containers.length === 0;
    const noConditions = conditions.length === 0;

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
                            formik.touched.description &&
                            Boolean(formik.errors.description)
                        }
                        helperText={
                            formik.touched.description &&
                            formik.errors.description
                        }
                    />
                </Grid>
                <Grid item xs={12}>
                    <DateTimePicker
                        sx={{
                            width: '100%'
                        }}
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
                <Grid item xs={12}>
                    <FormControl
                        required
                        fullWidth
                        disabled={noContainers}
                        error={noContainers}
                    >
                        <InputLabel>
                            Container
                        </InputLabel>
                        <Select
                            name="containerId"
                            value={formik.values.containerId}
                            label="Container *"
                            fullWidth
                            onChange={formik.handleChange}
                            error={
                                formik.touched.containerId &&
                                Boolean(formik.errors.containerId)
                            }
                        >
                            {containers.map(c => (
                                <MenuItem value={c.id}>{c.name}</MenuItem>
                            ))}
                        </Select>
                        <FormHelperText>
                            {(noContainers && "Create a container first")
                            || (formik.touched.containerId && formik.errors.containerId)}
                        </FormHelperText>
                    </FormControl>
                </Grid>
                <Grid item xs={12}>
                    <FormControl
                        required
                        fullWidth
                        disabled={noConditions}
                        error={noConditions}
                    >
                        <InputLabel>
                            Conditions
                        </InputLabel>
                        <Select
                            name="conditionsId"
                            value={formik.values.conditionsId}
                            label="Conditions *"
                            fullWidth
                            onChange={formik.handleChange}
                            error={
                                formik.touched.conditionsId &&
                                Boolean(formik.errors.conditionsId)
                            }
                        >
                            {conditions.map(c => (
                                <MenuItem value={c.id}>{c.name}</MenuItem>
                            ))}
                        </Select>
                        <FormHelperText>
                            {(noConditions && "Create conditions first")
                            || (formik.touched.conditionsId && formik.errors.conditionsId)}
                        </FormHelperText>
                    </FormControl>
                </Grid>
            </Grid>
        </EntityFormModal>
    );
};

export default OrganModal;
