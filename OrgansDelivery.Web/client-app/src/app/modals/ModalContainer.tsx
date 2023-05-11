import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";

const ModalContainer = () => {
    const { modalStore } = useStore();
    return (
        <div>
            {modalStore.modal.body}
        </div>
    )
}

export default observer(ModalContainer)
