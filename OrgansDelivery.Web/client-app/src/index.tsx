import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import { router } from './app/router/Routes';
import './app/layout/styles.css';
import { CssBaseline, ThemeProvider } from "@mui/material";
import theme from "./app/layout/theme";
import 'react-toastify/dist/ReactToastify.min.css';

const root = ReactDOM.createRoot(
    document.getElementById("root") as HTMLElement
);
root.render(
    <React.StrictMode>
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <RouterProvider router={router} />
        </ThemeProvider>
    </React.StrictMode>
);
