import React from "react";
import OrganModal from "./OrganModal";
import { useStore } from "../../app/stores/store";
import { Organ } from "../../app/models/organ";
import { observer } from "mobx-react-lite";
import { Container } from "../../app/models/container";
import { Conditions } from "../../app/models/conditions";

type Props = {
    organ: Organ;
    containers: Container[];
    conditions: Conditions[];
};

const UpdateOrganModal = (props: Props) => {
    const { organ, containers, conditions } = props;
    const { organStore, containerStore } = useStore();

    return (
        <OrganModal
            initialValues={{
                name: organ.name,
                description: organ.description,
                organCreationDate: organ.organCreationDate,
                containerId: organ.container.id,
                conditionsId: organ.conditions.id,
            }}
            actionName="Update"
            containers={containers}
            conditions={conditions}
            onSubmit={(values) => {
                organStore.updateOrgan(organ.id, values)
                    .then(containerStore.loadContainers)
            }}
        />
    );
};

export default observer(UpdateOrganModal);
