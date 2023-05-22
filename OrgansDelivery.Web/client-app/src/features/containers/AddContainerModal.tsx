import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import ContainerModal from "./ContainerModal";
import { Device } from "../../app/models/device";

type Props = {
    devices: Device[];
}

const AddContainerModal = (props: Props) => {
    const { devices } = props;
    const { containerStore, modalStore } = useStore();

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
                containerStore.createContainer(container)
                    .then(modalStore.closeModal);
            }}            
        />
    );
};

export default observer(AddContainerModal);
