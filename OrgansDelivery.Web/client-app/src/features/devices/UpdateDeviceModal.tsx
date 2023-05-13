import React from "react";
import { observer } from "mobx-react-lite";
import DeviceModal from "./DeviceModal";
import { useStore } from "../../app/stores/store";
import { Device } from "../../app/models/device";

type Props = {
    device: Device;
};

const UpdateDeviceModal = (props: Props) => {
    const { device } = props;
    const { deviceStore } = useStore();

    return (
        <DeviceModal
            initialValues={{
                id: device.id,
                name: device.name,
                conditionsIntervalCheckInMs: device.conditionsIntervalCheckInMs,
            }}
            actionName="Update"
            onSubmit={(values) => {
                deviceStore.updateDevice(device.id, values);
            }}
        />
    );
};

export default observer(UpdateDeviceModal);
