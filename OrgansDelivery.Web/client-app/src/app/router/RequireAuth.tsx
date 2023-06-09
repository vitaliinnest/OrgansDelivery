import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";

const RequireAuth = () => {
    const { userStore: { isLoggedIn } } = useStore();
    const location = useLocation();

    if (!isLoggedIn) {
        return <Navigate to="/sign-in" state={{ from: location }} />;
    }

    return <Outlet />;
}

export default observer(RequireAuth);
