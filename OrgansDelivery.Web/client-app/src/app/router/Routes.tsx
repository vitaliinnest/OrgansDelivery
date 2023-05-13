import { createBrowserRouter, Navigate, RouteObject } from "react-router-dom";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import App from "../layout/App";
import RequireAuth from "./RequireAuth";
import ConfirmEmail from "../../features/users/ConfirmEmail";
import SignUpPage from "../../features/users/SignUpPage";
import SignInPage from "../../features/users/SignInPage";
import CreateTenantPage from "../../features/tenants/CreateTenantPage";
import RequireTenant from "./RequireTenant";
import OrgansList from "../../features/organs/OrgansList";
import OrganDetails from "../../features/organs/OrganDetails";
import ContainersList from "../../features/containers/ContainersList";
import DevicesList from "../../features/devices/DevicesList";

export const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            {
                path: "/sign-in",
                element: <SignInPage />,
            },
            {
                path: "/sign-up",
                element: <SignUpPage />,
            },
            {
                element: <RequireAuth />,
                children: [
                    { path: "create-tenant", element: <CreateTenantPage /> },
                    {
                        // todo: test
                        element: <RequireTenant />,
                        children: [
                            { path: "organs", element: <OrgansList /> },
                            { path: "organs/:organId", element: <OrganDetails /> },
                            { path: "containers", element: <ContainersList /> },
                            // { path: "conditions", element: <ConditionsList /> },
                            { path: "devices", element: <DevicesList /> },
                            // { path: "invites", element: <InvitesList /> },
                            // { path: "users", element: <UsersList /> },
                        ],
                    },
                ],
            },
            { path: "/confirm-email", element: <ConfirmEmail /> },
            { path: "not-found", element: <NotFound /> },
            { path: "server-error", element: <ServerError /> },
            { path: "*", element: <Navigate replace to="/organs" /> },
        ],
    },
];

export const router = createBrowserRouter(routes);
