import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import * as yup from 'yup';

// const validationSchema = yup.object({
//     email: yup
//         .string().email().required(),
//     password: yup
//         .string("Enter your password")
//         .min(8, "Password should be of minimum 8 characters length")
//         .required("Password is required"),
// });
  

const SignInPage = () => {
    const { userStore } = useStore();
    return (
        <Formik
            initialValues={{ email: "", password: "", error: null }}
            onSubmit={(values, { setErrors }) =>
                userStore
                    .login(values)
                    .catch((error) =>
                        setErrors({ error: "Invalid email or password" })
                    )
            }
        >
            {({ handleSubmit, isSubmitting, errors }) => (
                <Form
                    className="ui form"
                    onSubmit={handleSubmit}
                    autoComplete="off"
                >
                    {/* <Header
                        as="h2"
                        content="Login to Reactivities"
                        color="teal"
                        textAlign="center"
                    />
                    <MyTextInput placeholder="Email" name="email" />
                    <MyTextInput
                        placeholder="Password"
                        name="password"
                        type="password"
                    />
                    <ErrorMessage
                        name="error"
                        render={() => (
                            <Label
                                style={{ marginBottom: 10 }}
                                basic
                                color="red"
                                content={errors.error}
                            />
                        )}
                    />
                    <Button
                        loading={isSubmitting}
                        positive
                        content="Login"
                        type="submit"
                        fluid
                    /> */}
                </Form>
            )}
        </Formik>
    );
}

export default observer(SignInPage);
