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

type NavBarPage = {
    title: string;
    path?: string;
    onClick?: () => void;
};

const pages: NavBarPage[] = [
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

// todo: move to a separate menu "Invites", "Users"
const settings: NavBarPage[] = [
    {
        title: "Profile",
        path: "/profile",
    },
    {
        title: "Logout",
    },
];

const NavBar = () => {
    const navigate = useNavigate();
    const [openAccountMenu, setOpenAccountMenu] = useState(false);
    const [openThreeDotsMenu, setOpenThreeDotsMenu] = useState(false);

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
                        {pages.map((page) => (
                            <Button
                                key={page.title}
                                sx={{ my: 2, mr: 2, color: "white", display: "block" }}
                                onClick={() => page.path && navigate(page.path)}
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
                    <Box sx={{ flexGrow: 0 }}>
                        <Tooltip title="Open settings">
                            <IconButton onClick={() => setOpenAccountMenu(true)} sx={{ p: 0 }}>
                                <Avatar />
                            </IconButton>
                        </Tooltip>
                        <Menu
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
                            open={openAccountMenu}
                            onClose={() => setOpenAccountMenu(false)}
                        >
                            {settings.map((setting) => (
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
