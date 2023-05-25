import React, { useState } from "react";
import Box from "@mui/material/Box";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import Avatar from "@mui/material/Avatar";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import { useStore } from "../stores/store";
import { useTranslation } from "react-i18next";
import { ActionMenuOption } from "./NavBar";
import { observer } from "mobx-react-lite";
import UpdateProfileModal from "../../features/users/UpdateProfileModal";
import { router } from "../router/Routes";
import LoadingBackdrop from "./LoadingBackdrop";

const ProfileMenu = () => {
    const { t } = useTranslation("translation", { keyPrefix: "navbar" });
    const { userStore, modalStore } = useStore();

    const onProfileClick = () => {
        modalStore.openModal(
            <UpdateProfileModal />
        );
    };

    const profileOptions: ActionMenuOption[] = [
        {
            title: t("profile"),
            onClick: onProfileClick,
        },
        {
            title: t("logout"),
            onClick: () => {
                userStore.logout();
                router.navigate("/sign-in");
            },
        },
    ];

    const [accountMenuAnchorEl, setAccountMenuAnchorEl] = useState<null | HTMLElement>(null);

    const onAccountMenuClick = (event: React.MouseEvent<HTMLElement>) => {
        setAccountMenuAnchorEl(event.currentTarget);
    };

    const onAccountMenuClose = () => {
        setAccountMenuAnchorEl(null);
    };

    if (!userStore.user) {
        return null;
    }

    if (userStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <Box
            sx={{
                flexGrow: 0,
            }}
        >
            <Tooltip title={t("profile")}>
                <IconButton onClick={onAccountMenuClick}>
                    <Avatar />
                </IconButton>
            </Tooltip>
            <Menu
                anchorEl={accountMenuAnchorEl}
                sx={{
                    mt: "45px",
                }}
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
        </Box>
    );
};

export default observer(ProfileMenu);
