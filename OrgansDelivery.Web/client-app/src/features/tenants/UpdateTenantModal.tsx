import { observer } from 'mobx-react-lite';
import React from 'react';
import { useStore } from '../../app/stores/store';
import TenantModal from './TenantModal';
import { toast } from 'react-toastify';
import { useTranslation } from "react-i18next";

const UpdateTenantModal = () => {
    const { tenantStore } = useStore();
    const { t } = useTranslation('translation', { keyPrefix: "toast" });

    if (!tenantStore.tenant) {
        return null;
    }

    return (
        <TenantModal
            initialValues={tenantStore.tenant}
            action="Update"
            onSubmit={(values) => {
                tenantStore.updateTenant(values).then(() => {
                    toast.success(t('updated'));
                })
            }}
        />
    );
}

export default observer(UpdateTenantModal);
