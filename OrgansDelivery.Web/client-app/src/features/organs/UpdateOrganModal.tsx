import React from "react";
import OrganModal from "./OrganModal";
import { useStore } from "../../app/stores/store";
import { Organ } from "../../app/models/organ";
import { observer } from "mobx-react-lite";
import { Container } from "../../app/models/container";
import { Conditions } from "../../app/models/conditions";
import { toast } from "react-toastify";
import { useTranslation } from "react-i18next";

type Props = {
    organ: Organ;
    containers: Container[];
    conditions: Conditions[];
};

const UpdateOrganModal = (props: Props) => {
    const { organ, containers, conditions } = props;
    const { organStore, modalStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "toast" });

    return (
        <OrganModal
            initialValues={{
                name: organ.name,
                description: organ.description,
                organCreationDate: organ.organCreationDate,
                containerId: organ.container.id,
                conditionsId: organ.conditions.id,
            }}
            action="Update"
            containers={containers}
            conditions={conditions}
            onSubmit={(values) => {
                organStore.updateOrgan(organ.id, values).then(() => {
                    modalStore.closeModal();
                    toast.success(t('updated'));
                });
            }}
        />
    );
};

export default observer(UpdateOrganModal);
