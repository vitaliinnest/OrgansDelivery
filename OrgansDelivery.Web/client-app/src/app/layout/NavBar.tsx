import React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import Button from "@mui/material/Button";
import SelfImprovementIcon from "@mui/icons-material/SelfImprovement";
import { useNavigate } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import LanguageSwitcher from "./LanguageSwitcher";
import ThreeDotsMenu from "./ThreeDotsMenu";
import { useTranslation } from "react-i18next";
import ProfileMenu from "./ProfileMenu";

type MenuOption = {
    title: string;
};

export type NavigationMenuOption = MenuOption & {
    path?: string;
    onClick?: () => void;
};

export type ActionMenuOption = MenuOption & {
    onClick: () => void;
};

const NavBar = () => {
    const { t } = useTranslation("translation", { keyPrefix: "navbar" });
    const { userStore, tenantStore } = useStore();
    const navigate = useNavigate();

    const mainOptions: NavigationMenuOption[] = [
        {
            title: t("organs"),
            path: "/organs",
        },
        {
            title: t("containers"),
            path: "/containers",
        },
        {
            title: t("conditions"),
            path: "/conditions",
        },
        {
            title: t("devices"),
            path: "/devices",
        },
    ];

    return (
        <AppBar position="sticky">
            <Container maxWidth={false}>
                <Toolbar disableGutters>
                    <SelfImprovementIcon
                        sx={{
                            mr: 1,
                            fontSize: 45,
                        }}
                    />
                    <Typography
                        variant="h5"
                        noWrap
                        component="a"
                        sx={{
                            mr: 10,
                            display: "flex",
                            fontWeight: 700,
                            color: "inherit",
                            textDecoration: "none",
                            cursor: "pointer",
                        }}
                        onClick={() => navigate("/organs")}
                    >
                        Organs Storage
                    </Typography>
                    <Box
                        sx={{
                            flexGrow: 1,
                            display: "flex",
                        }}
                    >
                        {userStore.isLoggedIn &&
                            tenantStore.hasTenant &&
                            mainOptions.map((page) => (
                                <Button
                                    key={page.title}
                                    sx={{
                                        my: 2,
                                        mr: 2,
                                        color: "white",
                                        display: "block",
                                    }}
                                    onClick={() => {
                                        if (page.path) {
                                            navigate(page.path);
                                        } else {
                                            page.onClick?.();
                                        }
                                    }}
                                >
                                    <Typography
                                        variant="h6"
                                        component="div"
                                        textTransform="capitalize"
                                    >
                                        {page.title}
                                    </Typography>
                                </Button>
                            ))}
                    </Box>
                    <LanguageSwitcher />
                    {userStore.isLoggedIn && tenantStore.hasTenant && (
                        <ThreeDotsMenu />
                    )}
                    {userStore.isLoggedIn && (
                        <ProfileMenu />
                    )}
                </Toolbar>
            </Container>
        </AppBar>
    );
};

export default observer(NavBar);
