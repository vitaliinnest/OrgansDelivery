import React, { useEffect } from "react";
import { useStore } from "../../app/stores/store";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import { observer } from "mobx-react-lite";
import EntitiesTable from "../../app/layout/EntitiesTable";

const UsersList = () => {
    const { employeeStore, userStore } = useStore();

    useEffect(() => {
        employeeStore.loadEmployees();
    }, [employeeStore]);

    const onUserDeleteConfirmation = (employeeId: string) => {
        employeeStore.deleteEmployee(employeeId);
    }

    if (employeeStore.isLoading) {
        return <LoadingBackdrop />;
    }

    return (
        <EntitiesTable
            tableTitle="Users"
            headCells={[
                {
                    id: "full-name",
                    disablePadding: true,
                    label: "Full Name",
                },
                {
                    id: "email",
                    disablePadding: false,
                    label: "Email"
                },
            ]}
            rows={employeeStore.employees.map(e => [
                e.id,
                `${e.name} ${e.surname} ${e.id === userStore.user?.id ? '(You)' : ''}`,
                e.email,
            ])}
            onDeleteConfirmation={onUserDeleteConfirmation}
        />
    );
};

export default observer(UsersList);
