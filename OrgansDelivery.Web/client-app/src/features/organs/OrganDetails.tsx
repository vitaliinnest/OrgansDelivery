import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import { useStore } from "../../app/stores/store";
import { useEffect, useState } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import OrganDashboard from "./OrganDashboard";
import { ConditionsRecord } from "../../app/models/conditionsRecord";

const chartConditionsOptions: (keyof ConditionsRecord)[] = [
    'humidity',
    'light',
    'temperature',
];

const OrganDetails = () => {
    const { organId } = useParams();
    const { organStore, recordStore } = useStore();
    
    const [chartOption, setChartOption] = useState<keyof ConditionsRecord>('temperature');

    useEffect(() => {
        if (!organId) {
            return;
        }
        organStore.loadOrgan(organId);
        recordStore.loadRecords(organId);
        recordStore.loadViolations(organId);
    }, [organId, organStore, recordStore]);

    if (
        organStore.isLoading ||
        !organStore.selectedOrgan ||
        recordStore.areRecordsLoading ||
        recordStore.areViolationsLoading
    ) {
        return <LoadingBackdrop />;
    }

    return (
        <>
            <OrganDashboard
                organ={organStore.selectedOrgan}
                records={recordStore.records}
                violations={recordStore.violations}
                chartOption={chartOption}
                chartOptions={chartConditionsOptions}
                onChangeChartOption={name => setChartOption(name)}
            />
        </>
    );
}

export default observer(OrganDetails);
