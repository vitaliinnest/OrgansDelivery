import React, { PropsWithChildren } from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { observer } from "mobx-react-lite";
import { Box, Breakpoint } from "@mui/material";
import { useStore } from "../stores/store";

type Props = {
    entityName: string;
    description?: string;
    actionName: string;
    maxWidth?: Breakpoint | false;
    onSubmit: () => void;
};

const EntityFormModal = (props: PropsWithChildren<Props>) => {
    const {
        entityName,
        description,
        children,
        actionName,
        maxWidth,
        onSubmit,
    } = props;

    const { modalStore } = useStore();

    return (
        <Dialog
            maxWidth={maxWidth}
            open={modalStore.modal.open}
            onClose={modalStore.closeModal}
        >
            <DialogTitle>{actionName} {entityName}</DialogTitle>
            <Box
                component="form"
                noValidate
                onSubmit={onSubmit}
            >
                <DialogContent sx={{ pt: 1 }}>
                    <DialogContentText>{description}</DialogContentText>
                    {children}
                </DialogContent>
                <DialogActions>
                    <Button onClick={modalStore.closeModal}>Close</Button>
                    <Button type="submit">{actionName}</Button>
                </DialogActions>
            </Box>
        </Dialog>
    );
};

export default observer(EntityFormModal);
