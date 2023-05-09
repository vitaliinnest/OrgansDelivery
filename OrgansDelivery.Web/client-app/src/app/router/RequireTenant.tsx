import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";

const RequireTenant = () => {
    const { tenantStore } = useStore();
    const location = useLocation();

    if (!tenantStore.hasTenant) {
        console.log("no tenant!");
        return <Navigate to="/create-tenant" state={{ from: location }} />;
    }
    console.log("tenant exists");

    return <Outlet />;
}

export default observer(RequireTenant);
