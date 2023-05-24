import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import InviteModal from "./InviteModal";
import { toast } from "react-toastify";
import { useTranslation } from "react-i18next";

const InviteUserModal = () => {
    const { inviteStore, modalStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "toast" });

    return (
        <InviteModal
            initialValues={{
                email: "",
            }}
            action="Create"
            onSubmit={(invite) => {
                inviteStore.createInvite(invite).then(() => {
                    modalStore.closeModal();
                    toast.success(t('invited'));
                });
            }}            
        />
    );
};

export default observer(InviteUserModal);
