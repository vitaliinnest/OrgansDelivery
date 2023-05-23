import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useTranslation } from "react-i18next";
import { toast } from "react-toastify";
import ProfileModal from "./ProfileModal";

const UpdateProfileModal = () => {
    const { modalStore, userStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "navbar" });

    if (!userStore.user) {
        return null;
    }

    return (
        <ProfileModal
            initialValues={userStore.user}
            action="Update"
            onSubmit={(values) => {
                userStore.updateUser(values).then(() => {
                    modalStore.closeModal();
                    toast.success(t('profileUpdated'));
                })
            }}
        />
    );
}

export default observer(UpdateProfileModal);
