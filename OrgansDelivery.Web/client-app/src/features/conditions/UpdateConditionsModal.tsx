import { observer } from "mobx-react-lite";
import ConditionsModal from "./ConditionsModal";
import { useStore } from "../../app/stores/store";
import { Conditions } from "../../app/models/conditions";
import { toast } from "react-toastify";
import { useTranslation } from "react-i18next";

type Props = {
    conditions: Conditions;
}

const UpdateConditionsModal = (props: Props) => {
    const { conditions } = props;
    const { conditionsStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "toast" });

    return (
        <ConditionsModal
            initialValues={conditions}
            action="Update"
            onSubmit={(values) => {
                conditionsStore.updateConditions(conditions.id, values)
                    .then(() => {
                        toast.success(t('updated'));
                    });
            }}
        />
    );
}

export default observer(UpdateConditionsModal);
