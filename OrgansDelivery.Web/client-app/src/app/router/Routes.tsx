import { createBrowserRouter, Navigate, RouteObject } from "react-router-dom";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import App from "../layout/App";
import RequireAuth from "./RequireAuth";
import ConfirmEmail from "../../features/users/ConfirmEmail";
import SignUpPage from "../../features/users/SignUpPage";
import SignInPage from "../../features/users/SignInPage";
import CreateTenantPage from "../../features/tenants/CreateTenantPage";

export const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            {
                element: <RequireAuth />,
                children: [
                    { path: "create-tenant", element: <CreateTenantPage /> },
                    // { path: "organs", element: <OrgansList /> },
                    // { path: "organs/:organId", element: <OrganDetails /> },
                    // { path: "containers", element: <ContainersList /> },
                    // { path: "containers/:containerId", element: <ContainerDetails /> },
                    // { path: "conditions", element: <ConditionsList /> },
                    // { path: "conditions/:conditionId", element: <ConditionsDetails /> },
                    // { path: "invites", element: <InvitesList /> },
                    // { path: "users", element: <UsersList /> },
                ],
            },
            { path: "/confirmEmail", element: <ConfirmEmail /> },
            { path: "not-found", element: <NotFound /> },
            { path: "server-error", element: <ServerError /> },
            { path: "*", element: <Navigate replace to="/organs" /> },
        ],
    },
    {
        path: "/sign-in",
        element: <SignInPage />,
    },
    {
        path: "/sign-up",
        element: <SignUpPage />,
    },
];

export const router = createBrowserRouter(routes);
