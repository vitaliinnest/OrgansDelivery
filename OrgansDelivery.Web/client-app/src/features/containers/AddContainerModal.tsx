import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import ContainerModal from "./ContainerModal";
import { Device } from "../../app/models/device";
import { toast } from "react-toastify";
import { useTranslation } from "react-i18next";

type Props = {
    devices: Device[];
}

const AddContainerModal = (props: Props) => {
    const { devices } = props;
    const { containerStore, modalStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "toast" });

    return (
        <ContainerModal
            initialValues={{
                name: "",
                description: "",
                deviceId: ""
            }}
            action="Add"
            devices={devices}
            onSubmit={(container) => {
                containerStore.createContainer(container).then(() => {
                    modalStore.closeModal();
                    toast.success(t('added'));
                });
            }}            
        />
    );
};

export default observer(AddContainerModal);
