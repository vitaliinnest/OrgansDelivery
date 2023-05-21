import { createTheme } from "@mui/material/styles";
import { red } from "@mui/material/colors";
import { ukUA as coreUkUA, enUS as coreEnUS, Localization } from "@mui/material/locale";
import { ukUA as pickerUkUA, enUS as pickerEnUS } from '@mui/x-date-pickers/locales';

const themeOptions = {
    palette: {
        primary: {
            main: "#556cd6",
        },
        secondary: {
            main: "#19857b",
        },
        error: {
            main: red.A400,
        },
    },
};

export const createThemeWithLng = (lng: string | undefined) => {
    const loc = getLocalizationObject(lng);
    return createTheme(
        themeOptions,
        loc.picker,
        loc.core,
    );
}

const getLocalizationObject = (lng: string | undefined) => {
    switch (lng) {
        case "en": return { core: coreEnUS, picker: pickerEnUS };
        case "ua": return { core: coreUkUA, picker: pickerUkUA };
        default: return { core: coreEnUS, picker: pickerEnUS };
    }
}
