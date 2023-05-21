import React, { useState } from "react";
import Box from "@mui/material/Box";
import IconButton from "@mui/material/IconButton";
import Menu from "@mui/material/Menu";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { useNavigate } from "react-router-dom";
import { NavigationMenuOption } from "./NavBar";
import { useTranslation } from "react-i18next";

const ThreeDotsMenu = () => {
    const { t } = useTranslation('translation', { keyPrefix: 'navbar' });

    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const navigate = useNavigate();

    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };
    
    const handleClose = () => {
        setAnchorEl(null);
    };
    
    const threeDotsMenuOptions: NavigationMenuOption[] = [
        {
            title: t("users"),
            path: "/users",
        },
        {
            title: t("invites"),
            path: "/invites"
        },
    ];
    
    return (
        <Box
            sx={{
                mr: 1
            }}
        >
            <Tooltip
                title={t("threeDotsMenu")}
            >
                <IconButton
                    aria-label="more"
                    id="long-button"
                    aria-controls={open ? "long-menu" : undefined}
                    aria-expanded={open ? "true" : undefined}
                    aria-haspopup="true"
                    onClick={handleClick}
                    sx={{
                        color: "inherit"
                    }}
                >
                    <MoreVertIcon />
                </IconButton>
            </Tooltip>
            <Menu
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
            >
                {threeDotsMenuOptions.map((option) => (
                    <MenuItem
                        key={option.title}
                        onClick={() => {
                            navigate(option.path);
                            handleClose();
                        }}
                    >
                        {option.title}
                    </MenuItem>
                ))}
            </Menu>
        </Box>
    );
};

export default ThreeDotsMenu;
