import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import { router } from './app/router/Routes';
import './app/layout/styles.css';
import { CssBaseline } from "@mui/material";
import 'react-toastify/dist/ReactToastify.min.css';
import './i18n';
import { I18nextProvider } from "react-i18next";
import i18next from "./i18n";

const root = ReactDOM.createRoot(
    document.getElementById("root") as HTMLElement
);
root.render(
    <React.StrictMode>
        <React.Suspense fallback="loading">
            <I18nextProvider i18n={i18next}>
                <CssBaseline />
                <RouterProvider router={router} />
            </I18nextProvider>
        </React.Suspense>
    </React.StrictMode>
);
