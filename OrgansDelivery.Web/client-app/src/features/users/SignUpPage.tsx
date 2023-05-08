import React from "react";
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
    Container,
    Grid,
    Link,
    TextField,
    Typography,
} from "@mui/material";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import { validate } from "uuid";
YupPassword(Yup);

const validationSchema = Yup.object({
    name: Yup.string().required(),
    surname: Yup.string().required(),
    email: Yup.string().email(),
    password: Yup.string().required().min(8).minLowercase(1),
    repeatPassword: Yup.string().oneOf(
        [Yup.ref("password"), undefined],
        "Passwords must match"
    ),
    inviteCode: Yup.string().test("is-uuid", "Invalid invide code", (value) => {
        return value === undefined || validate(value);
    }),
});

const initialValues: Register = {
    name: "",
    surname: "",
    email: "",
    password: "",
    inviteCode: "",
};

const SignUpPage = () => {
    const { userStore } = useStore();

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
                Sign Up
            </Typography>
            <Formik
                initialValues={initialValues}
                onSubmit={(creds) => userStore.register(creds)}
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
                                    label="First Name"
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
                            <Grid item sm={6}>
                                <TextField
                                    name="surname"
                                    label="Last Name"
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
                                    label="Email Address"
                                    margin="normal"
                                    required
                                    fullWidth
                                    onChange={handleChange}
                                    value={values.email}
                                    error={
                                        touched.email &&
                                        Boolean(errors.email)
                                    }
                                    helperText={
                                        touched.email && errors.email
                                    }
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    name="password"
                                    label="Password"
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
                                    name="inviteCode"
                                    label="Invite Code"
                                    margin="normal"
                                    fullWidth
                                    onChange={handleChange}
                                    value={values.inviteCode}
                                    error={
                                        touched.inviteCode &&
                                        Boolean(errors.inviteCode)
                                    }
                                    helperText={
                                        touched.inviteCode &&
                                        errors.inviteCode
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
                            Sign Up
                        </Button>
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                <Link href="#" variant="body2">
                                    Already have an account? Sign in
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
