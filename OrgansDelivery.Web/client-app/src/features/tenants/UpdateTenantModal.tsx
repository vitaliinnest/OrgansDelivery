import { observer } from 'mobx-react-lite';
import React from 'react';
import { useStore } from '../../app/stores/store';
import TenantModal from './OrganModal';
import { toast } from 'react-toastify';
import { useTranslation } from "react-i18next";

const UpdateTenantModal = () => {
    const { modalStore, tenantStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "navbar" });

    if (!tenantStore.tenant) {
        return null;
    }

    return (
        <TenantModal
            initialValues={tenantStore.tenant}
            action="Update"
            onSubmit={(values) => {
                tenantStore.updateTenant(values).then(() => {
                    modalStore.closeModal();
                    toast.success(t('tenantUpdated'));
                })
            }}
        />
    );
}

export default observer(UpdateTenantModal);
