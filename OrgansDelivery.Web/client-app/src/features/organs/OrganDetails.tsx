import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import { Typography } from "@mui/material";
import OrganDashboard from "./Dashboard";

const OrganDetails = () => {
    const { organStore } = useStore();
    const { organId } = useParams();
    
    useEffect(() => {
        if (organId) {
            organStore.loadOrgan(organId);
        }
    }, [organId, organStore]);

    if (organStore.isLoading || !organStore.selectedOrgan) {
        return <LoadingBackdrop />;
    }

    return (
        <>
            <OrganDashboard />
        </>
    );
}

export default observer(OrganDetails);
