import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";

const RequireUnauthozed = () => {
    const { userStore: { isLoggedIn } } = useStore();
    const location = useLocation();

    if (isLoggedIn) {
        return <Navigate to="/organs" state={{ from: location }} />;
    }

    return <Outlet />;
}

export default observer(RequireUnauthozed);
