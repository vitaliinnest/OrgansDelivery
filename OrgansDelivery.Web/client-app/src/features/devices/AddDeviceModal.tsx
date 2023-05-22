import React from "react";
import { observer } from "mobx-react-lite";
import DeviceModal from "./DeviceModal";
import { useStore } from "../../app/stores/store";

const AddOrganModal = () => {
    const { deviceStore, modalStore } = useStore();

    return (
        <DeviceModal
            initialValues={{
                id: "",
                name: "",
                conditionsIntervalCheckInMs: 30000,
            }}
            action="Add"
            onSubmit={(device) => {
                deviceStore.addDevice(device)
                    .then(modalStore.closeModal);
            }}
        />
    );
};

export default observer(AddOrganModal);
