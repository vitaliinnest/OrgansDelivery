import React from "react";
import { observer } from "mobx-react-lite";
import OrganModal from "./OrganModal";
import { useStore } from "../../app/stores/store";

const CreateOrganModal = () => {
    const { organStore } = useStore();

    return (
        <OrganModal
            initialValues={{
                name: "",
                description: "",
                organCreationDate: new Date(),
            }}
            actionName="Create"
            onSubmit={(organ) => {
                organStore.createOrgan(organ);
            }}            
        />
    );
};

export default observer(CreateOrganModal);
