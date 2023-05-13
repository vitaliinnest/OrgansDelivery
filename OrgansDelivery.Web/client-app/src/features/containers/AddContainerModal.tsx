import React from "react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import ContainerModal from "./ContainerModal";

const AddOrganModal = () => {
    const { containerStore } = useStore();

    return (
        <ContainerModal
            initialValues={{
                name: "",
                description: "",
                deviceId: ""
            }}
            actionName="Add"
            onSubmit={(container) => {
                containerStore.createContainer(container);
            }}            
        />
    );
};

export default observer(AddOrganModal);
