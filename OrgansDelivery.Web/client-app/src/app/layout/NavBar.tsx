import React, { useState } from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import Container from "@mui/material/Container";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import SelfImprovementIcon from "@mui/icons-material/SelfImprovement";
import { useNavigate } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import LanguageSwitcher from "./LanguageSwitcher";
import ThreeDotsMenu from "./ThreeDotsMenu";
import { useTranslation } from "react-i18next";

type MenuOption = {
    title: string;
};

export type NavigationMenuOption = MenuOption & {
    path: string;
};

type ActionMenuOption = MenuOption & {
    onClick: () => void;
}

const NavBar = () => {
    const { t } = useTranslation('translation', { keyPrefix: 'navbar' });
    
    const mainOptions: NavigationMenuOption[] = [
        {
            title: t('organs'),
            path: "/organs",
        },
        {
            title: t('containers'),
            path: "/containers",
        },
        {
            title: t('conditions'),
            path: "/conditions",
        },
        {
            title: t('devices'),
            path: "/devices",
        }
    ];

    const [accountMenuAnchorEl, setAccountMenuAnchorEl] = useState<null | HTMLElement>(null);
    const { userStore, tenantStore } = useStore();
    
    const onAccountMenuClick = (event: React.MouseEvent<HTMLElement>) => {
        setAccountMenuAnchorEl(event.currentTarget);
    };
    
    const onAccountMenuClose = () => {
        setAccountMenuAnchorEl(null);
    };
    
    const navigate = useNavigate();

    const profileOptions: ActionMenuOption[] = [
        {
            // todo: add profile editing
            title: t("profile"),
            onClick: () => navigate("/profile"),
        },
        {
            title: t("logout"),
            onClick: () => userStore.logout()
        },
    ];

    return (
        <AppBar position="sticky">
            <Container maxWidth={false}>
                <Toolbar disableGutters>
                    <SelfImprovementIcon
                        sx={{
                            mr: 1,
                            fontSize: 45
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
                            cursor: "pointer"
                        }}
                        onClick={() => navigate('/organs')}
                    >
                        Organs Storage
                    </Typography>
                    <Box
                        sx={{
                            flexGrow: 1,
                            display: "flex",
                        }}
                    >
                        {tenantStore.hasTenant && mainOptions.map((page) => (
                            <Button
                                key={page.title}
                                sx={{ my: 2, mr: 2, color: "white", display: "block" }}
                                onClick={() => navigate(page.path)}
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
                    {userStore.isLoggedIn && tenantStore.hasTenant && <ThreeDotsMenu />}
                    {userStore.isLoggedIn && <Box sx={{ flexGrow: 0 }}>
                        <Tooltip title={t("profile")}>
                            <IconButton
                                onClick={onAccountMenuClick}
                            >
                                <Avatar />
                            </IconButton>
                        </Tooltip>
                        <Menu
                            anchorEl={accountMenuAnchorEl}
                            sx={{ mt: "45px" }}
                            id="menu-appbar"
                            anchorOrigin={{
                                vertical: "top",
                                horizontal: "right",
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: "top",
                                horizontal: "right",
                            }}
                            open={Boolean(accountMenuAnchorEl)}
                            onClose={onAccountMenuClose}
                        >
                            {profileOptions.map((option) => (
                                <MenuItem
                                    key={option.title}
                                    onClick={() => {
                                        option.onClick();
                                        onAccountMenuClose();
                                    }}
                                >
                                    <Typography textAlign="center">
                                        {option.title}
                                    </Typography>
                                </MenuItem>
                            ))}
                        </Menu>
                    </Box>}
                </Toolbar>
            </Container>
        </AppBar>
    );
};

export default observer(NavBar);
