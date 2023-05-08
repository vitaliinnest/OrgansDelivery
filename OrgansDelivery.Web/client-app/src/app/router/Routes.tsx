import { createBrowserRouter, Navigate, RouteObject } from "react-router-dom";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import App from "../layout/App";
import RequireAuth from "./RequireAuth";
import ConfirmEmail from "../../features/users/ConfirmEmail";

export const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            {
                element: <RequireAuth />,
                children: [
                ],
            },
            { path: "/confirmEmail", element: <ConfirmEmail /> },
            { path: "not-found", element: <NotFound /> },
            { path: "server-error", element: <ServerError /> },
            { path: "*", element: <Navigate replace to="/not-found" /> },
        ],
    },
];

export const router = createBrowserRouter(routes);
