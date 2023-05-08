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
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { useNavigate } from "react-router-dom";
import { observer } from "mobx-react-lite";

type NavigationOption = {
    title: string;
    path: string;
};

const mainOptions: NavigationOption[] = [
    {
        title: "Organs",
        path: "/organs",
    },
    {
        title: "Containers",
        path: "/containers",
    },
    {
        title: "Conditions",
        path: "/conditions",
    },
];

const profileOptions: NavigationOption[] = [
    {
        title: "Profile",
        path: "/profile",
    },
    {
        title: "Logout",
        path: "/logout"
    },
];

const NavBar = () => {
    const navigate = useNavigate();
    const [accountMenuAnchorEl, setAccountMenuAnchorEl] = useState<null | HTMLElement>(null);
    
    const onAccountMenuClick = (event: React.MouseEvent<HTMLElement>) => {
        setAccountMenuAnchorEl(event.currentTarget);
    };
    const onAccountMenuClose = () => {
        setAccountMenuAnchorEl(null);
    };

    return (
        <AppBar position="static">
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
                        onClick={() => navigate('/')}
                    >
                        Organs Delivery
                    </Typography>
                    <Box
                        sx={{
                            flexGrow: 1,
                            display: "flex",
                        }}
                    >
                        {mainOptions.map((page) => (
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
                    <ThreeDotsMenu />
                    <Box sx={{ flexGrow: 0 }}>
                        <Tooltip title="Profile">
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
                            {profileOptions.map((setting) => (
                                <MenuItem
                                    key={setting.title}
                                    onClick={() => undefined}
                                >
                                    <Typography textAlign="center">
                                        {setting.title}
                                    </Typography>
                                </MenuItem>
                            ))}
                        </Menu>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
};

export default observer(NavBar);


const threeDotsMenuOptions: NavigationOption[] = [
    {
        title: "Invites",
        path: "/invites"
    },
    {
        title: "Users",
        path: "/users",
    },
];

const ITEM_HEIGHT = 48;

const ThreeDotsMenu = () => {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const navigate = useNavigate();

    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <Box
            sx={{
                mr: 1
            }}
        >
            <Tooltip
                title="Tenant Settings"
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
                MenuListProps={{
                    "aria-labelledby": "long-button",
                }}
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                PaperProps={{
                    style: {
                        maxHeight: ITEM_HEIGHT * 4.5,
                        width: "20ch",
                    },
                }}
            >
                {threeDotsMenuOptions.map((option) => (
                    <MenuItem
                        key={option.title}
                        onClick={() => navigate(option.path)}
                    >
                        {option.title}
                    </MenuItem>
                ))}
            </Menu>
        </Box>
    );
}
