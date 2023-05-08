import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { useNavigate } from 'react-router';
import { toast } from 'react-toastify';
import agent from '../../app/api/agent';
import useQuery from '../../app/util/hooks';
import LoadingBackdrop from '../../app/layout/LoadingBackdrop';

const ConfirmEmail = () => {
    const navigate = useNavigate();
    
    const userId = useQuery().get('userId') as string;
    const token = useQuery().get('token') as string;
    
    useEffect(() => {
        agent.UserActions.confirmEmail(userId, token).then(() => {
            navigate('/login');
            toast.success('Email confirmed. You can login now');
        });
    }, [navigate, token, userId]);
    
    return <LoadingBackdrop />;
}

export default observer(ConfirmEmail);
