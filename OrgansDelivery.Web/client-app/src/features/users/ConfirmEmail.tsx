import React, { useEffect } from 'react';
import { useNavigate } from 'react-router';
import { toast } from 'react-toastify';
import agent from '../../app/api/agent';
import useQuery from '../../app/util/hooks';
import LoadingBackdrop from '../../app/layout/LoadingBackdrop';
import { useTranslation } from 'react-i18next';

const ConfirmEmail = () => {
    const navigate = useNavigate();
    const { t } = useTranslation('translation', { keyPrefix: "auth" });

    const userId = useQuery().get('userId') as string;
    const token = useQuery().get('token') as string;
    
    useEffect(() => {
        agent.UserActions.confirmEmail(userId, token).then(() => {
            navigate('/sign-in');
            toast.success(t('emailConfirmed'));
        });
    }, [navigate, t, token, userId]);
    
    return <LoadingBackdrop />;
}

export default ConfirmEmail;
