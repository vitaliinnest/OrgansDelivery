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

type Props = {
    onConfirm: (confirmed: boolean) => void;
};

const AlertDialog = (props: Props) => {
    const { onConfirm } = props;
    const { modalStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: 'deleteDialog' });

    return (
        <div>
            <Dialog
                open={modalStore.modal.open}
                onClose={modalStore.closeModal}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">{t('title')}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {t('content')}
                    </DialogContentText>
                </DialogContent>
                <DialogActions sx={{ pr: 3, pb: 2 }} >
                    <Button
                        onClick={() => {
                            onConfirm(false);
                            modalStore.closeModal();
                        }}
                    >
                        {t('cancel')}
                    </Button>
                    <Button
                        color="error"
                        onClick={() => {
                            onConfirm(true);
                            modalStore.closeModal();
                        }}
                        variant="contained"
                    >
                        {t('delete')}
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
};

export default observer(AlertDialog);
