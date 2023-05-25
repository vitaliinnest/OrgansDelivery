import React from "react";
import { observer } from "mobx-react-lite";
import OrganModal from "./OrganModal";
import { useStore } from "../../app/stores/store";
import { Conditions } from "../../app/models/conditions";
import { Container } from "../../app/models/container";
import { toast } from "react-toastify";
import { useTranslation } from "react-i18next";

type Props = {
    containers: Container[];
    conditions: Conditions[];
};

const AddOrganModal = (props: Props) => {
    const { containers, conditions } = props;
    const { organStore, containerStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "toast" });

    return (
        <OrganModal
            initialValues={{
                name: "",
                description: "",
                organCreationDate: new Date(),
                conditionsId: "",
                containerId: "",
            }}
            action="Add"
            conditions={conditions}
            containers={containers}
            onSubmit={(organ) => {
                organStore.createOrgan(organ).then(() => {
                    containerStore.loadContainers();
                    toast.success(t('added'));
                });
            }}            
        />
    );
};

export default observer(AddOrganModal);
