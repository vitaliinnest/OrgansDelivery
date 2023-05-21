import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import { router } from './app/router/Routes';
import './app/layout/styles.css';
import { CssBaseline, ThemeProvider } from "@mui/material";
import theme from "./app/layout/theme";
import 'react-toastify/dist/ReactToastify.min.css';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import './i18n';

const root = ReactDOM.createRoot(
    document.getElementById("root") as HTMLElement
);
root.render(
    <React.StrictMode>
        <React.Suspense fallback="loading">
            <LocalizationProvider dateAdapter={AdapterDayjs}>
                {/* // todo: pass language locale to mui
                    need to map i18n locale to mui's one
                */}
                <ThemeProvider theme={theme}>
                    <CssBaseline />
                    <RouterProvider router={router} />
                </ThemeProvider>
            </LocalizationProvider>
        </React.Suspense>
    </React.StrictMode>
);
