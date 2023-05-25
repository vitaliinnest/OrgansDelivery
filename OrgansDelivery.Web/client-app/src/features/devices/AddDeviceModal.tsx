import React from "react";
import { observer } from "mobx-react-lite";
import DeviceModal from "./DeviceModal";
import { useStore } from "../../app/stores/store";
import { toast } from "react-toastify";
import { useTranslation } from "react-i18next";

const AddOrganModal = () => {
    const { deviceStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "toast" });
    
    return (
        <DeviceModal
            initialValues={{
                id: "",
                name: "",
                conditionsIntervalCheckInMs: 30000,
            }}
            action="Add"
            onSubmit={(device) => {
                deviceStore.addDevice(device).then(() => {
                    toast.success(t('added'));
                });
            }}
        />
    );
};

export default observer(AddOrganModal);
