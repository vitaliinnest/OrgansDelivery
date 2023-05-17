import React, { useEffect, useMemo } from "react";
import { useStore } from "../../app/stores/store";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";
import { router } from "../../app/router/Routes";

const OrgansList = () => {
    const { inviteStore, modalStore } = useStore();

    useEffect(() => {
        inviteStore.loadInvites();
    }, [inviteStore]);
    
    const onInviteUser = () => {
        modalStore.openModal(
            <InviteUserModal />
        );
    };

    const onInviteDeleteConfirmation = (inviteId: string) => {
        inviteStore.deleteInvite(inviteId);
    }

    if (inviteStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <EntitiesTable
            tableTitle="Invites"
            headCells={[
                {
                    id: "email",
                    disablePadding: true,
                    label: "Invitee  Email",
                },
            ]}
            rows={inviteStore.invites.map(i => [
                i.id,
                i.email,
            ])}
            onCreate={onInviteUser}
            onDeleteConfirmation={onInviteDeleteConfirmation}
        />
    );
};

export default observer(OrgansList);
