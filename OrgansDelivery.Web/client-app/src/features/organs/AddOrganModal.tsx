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
    const { organStore } = useStore();

    return (
        <OrganModal
            initialValues={{
                name: "",
                description: "",
                organCreationDate: new Date(),
                conditionsId: "",
                containerId: "",
            }}
            actionName="Add"
            conditions={conditions}
            containers={containers}
            onSubmit={(organ) => {
                organStore.createOrgan(organ);
            }}            
        />
    );
};

export default observer(AddOrganModal);
