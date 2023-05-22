import { observer } from "mobx-react-lite";
import ConditionsModal from "./ConditionsModal";
import { useStore } from "../../app/stores/store";
import { Conditions } from "../../app/models/conditions";

type Props = {
    conditions: Conditions;
}

const UpdateConditionsModal = (props: Props) => {
    const { conditions } = props;
    const { conditionsStore, modalStore } = useStore();

    return (
        <ConditionsModal
            initialValues={conditions}
            action="Update"
            onSubmit={(values) => {
                conditionsStore.updateConditions(conditions.id, values)
                    .then(modalStore.closeModal);
            }}
        />
    );
}

export default observer(UpdateConditionsModal);
