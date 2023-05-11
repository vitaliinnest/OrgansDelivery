import React from "react";
import OrganModal from "./OrganModal";
import { useStore } from "../../app/stores/store";
import { Organ } from "../../app/models/organ";
import { observer } from "mobx-react-lite";

type Props = {
    organ: Organ;
};

const UpdateOrganModal = (props: Props) => {
    const { organ } = props;
    const { organStore } = useStore();

    return (
        <OrganModal
            initialValues={{
                name: organ.name,
                description: organ.description,
                organCreationDate: organ.organCreationDate,
            }}
            actionName="Update"
            onSubmit={(values) => {
                organStore.updateOrgan(organ.id, values);
            }} />
    );
};

export default observer(UpdateOrganModal);
