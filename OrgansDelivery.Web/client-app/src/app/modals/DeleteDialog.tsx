import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";

type Props = {
    onConfirm: (confirmed: boolean) => void;
};

const AlertDialog = (props: Props) => {
    const { onConfirm } = props;
    const { modalStore } = useStore();

    return (
        <div>
            <Dialog
                open={modalStore.modal.open}
                onClose={modalStore.closeModal}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">Deletion</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        Are you sure you want to delete this item?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button
                        onClick={() => {
                            onConfirm(false);
                            modalStore.closeModal();
                        }}
                    >
                        Cancel
                    </Button>
                    <Button
                        onClick={() => {
                            onConfirm(true);
                            modalStore.closeModal();
                        }}
                        autoFocus
                    >
                        Delete
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
};

export default observer(AlertDialog);
