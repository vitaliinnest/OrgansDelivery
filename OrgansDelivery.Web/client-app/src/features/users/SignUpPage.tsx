import React, { useState } from "react";
import { Formik } from "formik";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { Register } from "../../app/models/user";
import * as Yup from "yup";
import YupPassword from "yup-password";
import {
    Avatar,
    Box,
    Button,
    Checkbox,
    Container,
    FormControlLabel,
    Grid,
    Link,
    TextField,
    Typography,
} from "@mui/material";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import { validate } from "uuid";
import { Link as RouterLink } from "react-router-dom";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import { guid } from "../../app/util/validation";
import { useTranslation } from "react-i18next";
import { router } from "../../app/router/Routes";
import { toast } from "react-toastify";
YupPassword(Yup);

const validationSchema = Yup.object({
    name: Yup.string().required(),
    surname: Yup.string().required(),
    email: Yup.string().email().required(),
    password: Yup.string().required().min(8).minLowercase(1).required(),
    repeatPassword: Yup.string()
        .oneOf([Yup.ref("password"), undefined], "Passwords must match")
        .required(),
    inviteCode: guid(),
});

const initialValues: Register = {
    name: "",
    surname: "",
    email: "",
    password: "",
    repeatPassword: "",
    inviteCode: "",
};

const SignUpPage = () => {
    const { userStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: 'auth' });
    const [useInviteCode, setUseInviteCode] = useState(false);

    if (userStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <Container
            sx={{
                marginTop: 4,
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
                {t("signUp")}
            </Typography>
            <Formik
                initialValues={initialValues}
                onSubmit={(creds) => {
                    userStore.register(creds).then(() => {
                        router.navigate("/sign-in");
                        toast.success(t("signUpSuccess"));
                    });
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
                            <Grid item sm={6}>
                                <TextField
                                    name="name"
                                    label={t("firstName")}
                                    margin="normal"
                                    required
                                    fullWidth
                                    autoFocus
                                    onChange={handleChange}
                                    value={values.name}
                                    error={touched.name && Boolean(errors.name)}
                                    helperText={touched.name && errors.name}
                                />
                            </Grid>
                            <Grid item sm={6}>
                                <TextField
                                    name="surname"
                                    label={t("lastName")}
                                    margin="normal"
                                    required
                                    fullWidth
                                    onChange={handleChange}
                                    value={values.surname}
                                    error={
                                        touched.surname &&
                                        Boolean(errors.surname)
                                    }
                                    helperText={
                                        touched.surname && errors.surname
                                    }
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    name="email"
                                    label={t("email")}
                                    margin="normal"
                                    required
                                    fullWidth
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
                                    label={t("password")}
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
                            <Grid item xs={12}>
                                <TextField
                                    name="repeatPassword"
                                    label={t("repeatPassword")}
                                    type="password"
                                    margin="normal"
                                    required
                                    fullWidth
                                    onChange={handleChange}
                                    value={values.repeatPassword}
                                    error={
                                        touched.repeatPassword &&
                                        Boolean(errors.repeatPassword)
                                    }
                                    helperText={
                                        touched.repeatPassword && errors.repeatPassword
                                    }
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <FormControlLabel
                                    control={
                                        <Checkbox
                                            checked={useInviteCode}
                                            onChange={(e) => {
                                                setUseInviteCode(e.target.checked);
                                            }}
                                        />
                                    }
                                    label={t('useInviteCode')}
                                />
                                {useInviteCode && (
                                    <TextField
                                        name="inviteCode"
                                        label={t("inviteCode")}
                                        margin="normal"
                                        fullWidth
                                        onChange={handleChange}
                                        value={values.inviteCode}
                                        error={
                                            touched.inviteCode &&
                                            Boolean(errors.inviteCode)
                                        }
                                        helperText={
                                            touched.inviteCode && errors.inviteCode
                                        }
                                    />
                                )}
                            </Grid>
                        </Grid>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            {t("signUp")}
                        </Button>
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                <Link
                                    variant="body2"
                                    component={RouterLink}
                                    to="/sign-in"
                                >
                                    {t("signUpQuestion")}
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                )}
            </Formik>
        </Container>
    );
};

export default observer(SignUpPage);
