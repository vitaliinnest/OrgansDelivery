import React from "react";
import { Formik } from "formik";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import * as Yup from 'yup';
import {
    Avatar,
    Box,
    Button,
    Container,
    Grid,
    TextField,
    Typography,
} from "@mui/material";
import { TenantFormValues } from "../../app/models/tenant";
import ApartmentOutlinedIcon from '@mui/icons-material/ApartmentOutlined';
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import { useTranslation } from "react-i18next";

const validationSchema = Yup.object({
    name: Yup.string().required(),
});

const initialValues: TenantFormValues = {
    name: "",
};

const CreateTenantPage = () => {
    const { tenantStore } = useStore();
    const { t } = useTranslation("translation", { keyPrefix: "tenant" });

    if (tenantStore.isLoading) {
        return <LoadingBackdrop />;
    }
    
    return (
        <Container
            sx={{
                marginTop: 12,
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
            }}
            maxWidth="sm"
        >
            <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
                <ApartmentOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                {t('heading')}
            </Typography>
            <Formik
                initialValues={initialValues}
                onSubmit={(tenant) => tenantStore.createTenant(tenant)}
                validationSchema={validationSchema}
            >
                {({
                    values,
                    isSubmitting,
                    errors,
                    touched,
                    isValid,
                    handleSubmit,
                    handleChange,
                }) => (
                    <Box
                        component="form"
                        noValidate
                        onSubmit={handleSubmit}
                        sx={{ mt: 3 }}
                    >
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextField
                                    name="name"
                                    label={t('tenantName')}
                                    margin="normal"
                                    required
                                    fullWidth
                                    autoFocus
                                    onChange={handleChange}
                                    value={values.name}
                                    error={
                                        touched.name && Boolean(errors.name)
                                    }
                                    helperText={touched.name && errors.name}
                                />
                            </Grid>
                        </Grid>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            {t('createBtn')}
                        </Button>
                    </Box>
                )}
            </Formik>
        </Container>
    );
}

export default observer(CreateTenantPage);
