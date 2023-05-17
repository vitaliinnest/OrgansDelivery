import React from "react";
import { observer } from "mobx-react-lite";
import DeviceModal from "./DeviceModal";
import { useStore } from "../../app/stores/store";

const AddOrganModal = () => {
    const { deviceStore } = useStore();

    return (
        <DeviceModal
            initialValues={{
                id: "",
                name: "",
                conditionsIntervalCheckInMs: 30000,
            }}
            actionName="Add"
            onSubmit={(device) => {
                deviceStore.addDevice(device);
            }}
        />
    );
};

export default observer(AddOrganModal);
