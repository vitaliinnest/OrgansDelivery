import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import InviteModal from "./InviteModal";

const InviteUserModal = () => {
    const { inviteStore, modalStore } = useStore();

    return (
        <InviteModal
            initialValues={{
                email: "",
            }}
            action="Create"
            onSubmit={(invite) => {
                inviteStore.createInvite(invite)
                    .then(modalStore.closeModal);
            }}            
        />
    );
};

export default observer(InviteUserModal);
