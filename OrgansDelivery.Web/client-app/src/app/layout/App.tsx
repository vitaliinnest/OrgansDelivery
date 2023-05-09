import React, { useEffect } from "react";
import NavBar from "./NavBar";
import { observer } from "mobx-react-lite";
import { Outlet, ScrollRestoration } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { useStore } from "../stores/store";
import LoadingBackdrop from "./LoadingBackdrop";

const App = () => {
    const { commonStore, userStore, tenantStore } = useStore();

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

    if (!commonStore.appLoaded) {
        return <LoadingBackdrop />;
    }

    return (
        <>
            <ScrollRestoration />
            <ToastContainer
                position="bottom-right"
                hideProgressBar
                theme="colored"
            />
            <>
                <NavBar />
                <Outlet />
            </>
        </>
    );
}

export default observer(App);
