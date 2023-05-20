import React from "react";
import { alpha } from "@mui/material/styles";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import DeleteIcon from "@mui/icons-material/Delete";
import AddCircleIcon from "@mui/icons-material/AddCircle";
import EditIcon from "@mui/icons-material/Edit";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import DeleteDialog from "../modals/DeleteDialog";

type Props = {
    tableTitle: string;
    selected: string[];
    onCreate?: () => void;
    onUpdate?: (entityId: string) => void;
    onDelete?: (entityId: string) => void;
};

const EnhancedTableToolbar = (props: Props) => {
    const { selected, tableTitle, onCreate, onUpdate, onDelete } = props;
    const { modalStore } = useStore();

    const onUpdateSelected = () => {
        onUpdate?.(selected[0]);
    };

    const onDeleteSelected = () => {
        modalStore.openModal(
            <DeleteDialog
                onConfirm={(confirmed) => {
                    if (confirmed) {
                        onDelete?.(selected[0]);
                    }
                }}
            />
        );
    };

    return (
        <Toolbar
            sx={{
                pl: { sm: 2 },
                pr: { xs: 1, sm: 1 },
                ...(selected.length > 0 && {
                    bgcolor: (theme) =>
                        alpha(
                            theme.palette.primary.main,
                            theme.palette.action.activatedOpacity
                        ),
                }),
            }}
        >
            {selected.length > 0 ? (
                <Typography
                    sx={{ flex: "1 1 100%" }}
                    color="inherit"
                    variant="subtitle1"
                    component="div"
                >
                    {selected.length} item(s) selected
                </Typography>
            ) : (
                <Typography
                    sx={{ flex: "1 1 100%" }}
                    variant="h6"
                    id="tableTitle"
                    component="div"
                >
                    {tableTitle}
                </Typography>
            )}
            {selected.length === 1 && (
                <>
                    {onUpdateSelected && <Tooltip title="Update">
                        <IconButton onClick={onUpdateSelected}>
                            <EditIcon />
                        </IconButton>
                    </Tooltip>}
                    {onDeleteSelected && <Tooltip title="Delete">
                        <IconButton onClick={onDeleteSelected}>
                            <DeleteIcon />
                        </IconButton>
                    </Tooltip>}
                </>
            )}
            {selected.length === 0 && onCreate && (
                <Tooltip title="Add">
                    <IconButton onClick={onCreate}>
                        <AddCircleIcon color="inherit" />
                    </IconButton>
                </Tooltip>
            )}
        </Toolbar>
    );
};

export default observer(EnhancedTableToolbar);
