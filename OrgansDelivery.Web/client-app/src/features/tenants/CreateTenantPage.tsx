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
    Link,
    TextField,
    Typography,
} from "@mui/material";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import { Link as RouterLink } from "react-router-dom";
import { CreateTenant } from "../../app/models/tenant";

const validationSchema = Yup.object({
    url: Yup.string().required(),
    name: Yup.string().required(),
    description: Yup.string(),
});

const initialValues: CreateTenant = {
    url: "",
    name: "",
    description: "",
};

const CreateTenantPage = () => {
    return (
        <>Create tenant page</>
    );
}

export default observer(CreateTenantPage);
