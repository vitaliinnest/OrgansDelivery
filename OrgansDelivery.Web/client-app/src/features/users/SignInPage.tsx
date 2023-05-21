import React from "react";
import { Formik } from "formik";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import * as Yup from 'yup';
import YupPassword from "yup-password";
import { Login } from "../../app/models/user";
import {
    Avatar,
    Box,
    Button,
    Container,
    Grid,
    Link,
    TextField,
    Typography,
} from "@mui/material";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import { Link as RouterLink } from "react-router-dom";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import { router } from "../../app/router/Routes";
import { useTranslation } from "react-i18next";
YupPassword(Yup);

const validationSchema = Yup.object({
    email: Yup.string().email(),
    password: Yup.string().required().min(8).minLowercase(1),
});

const initialValues: Login = {
    email: "vitalii.nesterenko@nure.ua",
    password: "asdfljksa234234",
};

const SignInPage = () => {
    const { userStore, tenantStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: 'auth' });

    if (userStore.isLoading || tenantStore.isLoading) {
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
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                {t('signIn')}
            </Typography>
            <Formik
                initialValues={initialValues}
                onSubmit={(creds) => {
                    userStore.login(creds)
                        .then(() => tenantStore.loadTenant())
                        .then(() => router.navigate('/organs'));
                }}
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
                                    name="email"
                                    label={t('email')}
                                    margin="normal"
                                    required
                                    fullWidth
                                    autoFocus
                                    onChange={handleChange}
                                    value={values.email}
                                    error={
                                        touched.email && Boolean(errors.email)
                                    }
                                    helperText={touched.email && errors.email}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    name="password"
                                    label={t('password')}
                                    type="password"
                                    margin="normal"
                                    required
                                    fullWidth
                                    onChange={handleChange}
                                    value={values.password}
                                    error={
                                        touched.password &&
                                        Boolean(errors.password)
                                    }
                                    helperText={
                                        touched.password && errors.password
                                    }
                                />
                            </Grid>
                        </Grid>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            {t('signIn')}
                        </Button>
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                <Link variant="body2" component={RouterLink} to="/sign-up">
                                    {t('signUpQuestion')}
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                )}
            </Formik>
        </Container>
    );
}

export default observer(SignInPage);
