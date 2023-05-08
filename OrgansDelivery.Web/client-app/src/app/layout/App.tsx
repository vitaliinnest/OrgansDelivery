import React, { useEffect } from "react";
import NavBar from "./NavBar";
import { observer } from "mobx-react-lite";
import { Outlet, ScrollRestoration, useLocation } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { useStore } from "../stores/store";
import LoadingBackdrop from "./LoadingBackdrop";

export default observer(function App() {
    const location = useLocation();
    const { commonStore, userStore } = useStore();

    useEffect(() => {
        if (commonStore.token) {
            userStore.getUser().finally(() => commonStore.setAppLoaded());
        } else {
            commonStore.setAppLoaded();
        }
    }, [commonStore, userStore]);

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
});
