import React from "react";
import { observer } from "mobx-react-lite";
import OrganModal from "./OrganModal";
import { useStore } from "../../app/stores/store";

const AddOrganModal = () => {
    const { organStore } = useStore();

    return (
        <OrganModal
            initialValues={{
                name: "",
                description: "",
                organCreationDate: new Date(),
            }}
            actionName="Add"
            onSubmit={(organ) => {
                organStore.createOrgan(organ);
            }}            
        />
    );
};

export default observer(AddOrganModal);
