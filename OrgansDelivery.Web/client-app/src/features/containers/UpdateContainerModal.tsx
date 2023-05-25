import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import ContainerModal from "./ContainerModal";
import { Device } from "../../app/models/device";
import { Container } from "../../app/models/container";
import { toast } from "react-toastify";
import { useTranslation } from "react-i18next";

type Props = {
    container: Container;
    devices: Device[];
}

const UpdateContainerModal = (props: Props) => {
    const { container, devices } = props;
    const { containerStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "toast" });

    return (
        <ContainerModal
            initialValues={{
                ...container,
                deviceId: container.device.id,
            }}
            action="Update"
            devices={devices}
            onSubmit={(values) => {
                containerStore.updateContainer(container.id, values).then(() => {
                    toast.success(t('updated'));
                });
            }}            
        />
    );
};

export default observer(UpdateContainerModal);
