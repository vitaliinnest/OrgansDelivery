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
    const { containerStore, deviceStore } = useStore();

    return (
        <ContainerModal
            initialValues={{
                name: container.name,
                description: container.description,
                deviceId: container.device.id,
            }}
            actionName="Add"
            devices={devices}
            onSubmit={(values) => {
                containerStore.updateContainer(container.id, values)
                    .then(deviceStore.loadDevices);
            }}            
        />
    );
};

export default observer(UpdateContainerModal);
