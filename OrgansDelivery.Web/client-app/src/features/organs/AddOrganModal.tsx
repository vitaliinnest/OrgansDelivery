import React from "react";
import { observer } from "mobx-react-lite";
import OrganModal from "./OrganModal";
import { useStore } from "../../app/stores/store";
import { Conditions } from "../../app/models/conditions";
import { Container } from "../../app/models/container";

type Props = {
    containers: Container[];
    conditions: Conditions[];
};

const AddOrganModal = (props: Props) => {
    const { containers, conditions } = props;
    const { organStore, modalStore } = useStore();

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
                organStore.createOrgan(organ)
                    .then(modalStore.closeModal)
            }}            
        />
    );
};

export default observer(AddOrganModal);
