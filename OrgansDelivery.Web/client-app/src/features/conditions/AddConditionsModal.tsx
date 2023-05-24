import { observer } from "mobx-react-lite";
import ConditionsModal from "./ConditionsModal";
import { useStore } from "../../app/stores/store";
import { useTranslation } from "react-i18next";
import { toast } from "react-toastify";

const AddConditionsModal = () => {
    const { conditionsStore, modalStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "toast" });

    return (
        <ConditionsModal
            initialValues={{
                name: "",
                description: "",
                humidity: {
                    allowedDeviation: 10,
                    expectedValue: 10,
                },
                light: {
                    allowedDeviation: 10,
                    expectedValue: 10,
                },
                temperature: {
                    allowedDeviation: 10,
                    expectedValue: 10,
                },
                orientation: {
                    allowedDeviation: { x: 10, y: 10 },
                    expectedValue: { x: 10, y: 10 },
                },
            }}
            action="Add"
            onSubmit={(conditions) => {
                conditionsStore.createCondition(conditions).then(() => {
                    modalStore.closeModal();
                    toast.success(t('added'));
                });
            }}
        />
    );
}

export default observer(AddConditionsModal);
