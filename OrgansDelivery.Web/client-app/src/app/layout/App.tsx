import React, { useEffect, useState } from "react";
import NavBar from "./NavBar";
import { observer } from "mobx-react-lite";
import { Outlet, ScrollRestoration } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { useStore } from "../stores/store";
import LoadingBackdrop from "./LoadingBackdrop";
import ModalContainer from "../modals/ModalContainer";
import { ThemeProvider } from "@emotion/react";
import { createThemeWithLng } from "./theme";
import { I18nextProvider, useTranslation } from "react-i18next";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import i18next from "i18next";

const App = () => {
    const { commonStore, userStore, tenantStore } = useStore();
    const { t, i18n } = useTranslation();
    const [theme, setTheme] = useState(createThemeWithLng(i18n.resolvedLanguage));

    useEffect(() => {
        if (commonStore.token) {
            Promise.all([
                userStore.getUser(),
                tenantStore.loadTenant(),
            ])
            .finally(() => commonStore.setAppLoaded());
        } else {
            commonStore.setAppLoaded();
        }
    }, [commonStore, tenantStore, userStore]);

    useEffect(() => {
        setTheme(createThemeWithLng(i18n.resolvedLanguage));
    }, [i18n.resolvedLanguage]);

    if (!commonStore.appLoaded) {
        return <LoadingBackdrop />;
    }

    return (
        <ThemeProvider theme={theme}>
            <LocalizationProvider
                dateAdapter={AdapterDayjs}
            >
                <I18nextProvider i18n={i18next}>    
                    <ScrollRestoration />
                    <ModalContainer />
                    <ToastContainer
                        position="bottom-right"
                        hideProgressBar
                        theme="colored"
                    />
                    <>
                        <NavBar />
                        <Outlet />
                    </>
                </I18nextProvider>
            </LocalizationProvider>
        </ThemeProvider>
    );
}

export default observer(App);
