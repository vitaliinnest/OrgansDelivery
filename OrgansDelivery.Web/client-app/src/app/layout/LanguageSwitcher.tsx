import React, { useState } from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import { useTranslation } from "react-i18next";

type LngOpt = {
    nativeName: string;
};

const lngs: Record<string, LngOpt> = {
    en: { nativeName: 'English' },
    ua: { nativeName: 'Українська' },
};

const LanguageSwitcher = () => {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const { t, i18n } = useTranslation();

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
                title={i18n.resolvedLanguage && lngs[i18n.resolvedLanguage].nativeName}
            >
                <Button
                    id="lng-button"
                    variant="contained"
                    disableElevation
                    onClick={handleClick}
                    sx={{
                        color: "inherit"
                    }}
                >
                    <Typography>
                        {i18n.resolvedLanguage}
                    </Typography>
                </Button>
            </Tooltip>
            <Menu
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
            >
                {Object.keys(lngs).map((lng) => (
                    <MenuItem
                        key={lng}
                        disabled={i18n.resolvedLanguage === lng}
                        onClick={() => {
                            i18n.changeLanguage(lng);
                            handleClose();
                        }}
                    >
                        {lngs[lng]?.nativeName}
                    </MenuItem>
                ))}
            </Menu>
        </Box>
    );
};

export default LanguageSwitcher;
