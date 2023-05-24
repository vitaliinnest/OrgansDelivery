import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { useTranslation } from "react-i18next";
import { toast } from "react-toastify";

type Props = {
    onConfirm: (confirmed: boolean) => void;
};

const AlertDialog = (props: Props) => {
    const { onConfirm } = props;
    const { modalStore } = useStore();
    const { t } = useTranslation('translation');

    return (
        <div>
            <Dialog
                open={modalStore.modal.open}
                onClose={modalStore.closeModal}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">{t('deleteDialog.title')}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {t('deleteDialog.content')}
                    </DialogContentText>
                </DialogContent>
                <DialogActions sx={{ pr: 3, pb: 2 }} >
                    <Button
                        onClick={() => {
                            onConfirm(false);
                            modalStore.closeModal();
                        }}
                    >
                        {t('deleteDialog.cancel')}
                    </Button>
                    <Button
                        color="error"
                        onClick={() => {
                            onConfirm(true);
                            modalStore.closeModal();
                            toast.success(t('toast.deleted'));
                        }}
                        variant="contained"
                    >
                        {t('deleteDialog.delete')}
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
};

export default observer(AlertDialog);
