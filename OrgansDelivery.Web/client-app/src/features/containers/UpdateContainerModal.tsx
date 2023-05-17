import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import ContainerModal from "./ContainerModal";
import { Device } from "../../app/models/device";
import { Container } from "../../app/models/container";

type Props = {
    container: Container;
    devices: Device[];
}

const UpdateContainerModal = (props: Props) => {
    const { container, devices } = props;
    const { containerStore,  modalStore } = useStore();

    return (
        <ContainerModal
            initialValues={{
                ...container,
                deviceId: container.device.id,
            }}
            actionName="Update"
            devices={devices}
            onSubmit={(values) => {
                containerStore.updateContainer(container.id, values)
                    .then(modalStore.closeModal);
            }}            
        />
    );
};

export default observer(UpdateContainerModal);
