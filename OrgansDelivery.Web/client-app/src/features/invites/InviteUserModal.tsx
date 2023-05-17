import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";

const InviteUserModal = () => {
    const { devices } = props;
    const { containerStore, modalStore } = useStore();

    return (
        <ContainerModal
            initialValues={{
                name: "",
                description: "",
                deviceId: ""
            }}
            actionName="Add"
            devices={devices}
            onSubmit={(container) => {
                containerStore.createContainer(container)
                    .then(modalStore.closeModal);
            }}            
        />
    );
};

export default observer(InviteUserModal);
