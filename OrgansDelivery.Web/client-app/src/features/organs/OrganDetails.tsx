import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import { useStore } from "../../app/stores/store";
import { useEffect, useState } from "react";
import LoadingBackdrop from "../../app/layout/LoadingBackdrop";
import OrganDashboard, { ChartOptions } from "./OrganDashboard";
import { ConditionsRecord } from "../../app/models/conditionsRecord";
import { useTranslation } from "react-i18next";

const OrganDetails = () => {
    const { organId } = useParams();
    const { organStore, recordStore } = useStore();
    const { t } = useTranslation("translation", { keyPrefix: "details" });

    const chartConditionsOptions: ChartOptions = [
        {
            opt: 'humidity',
            title: t('chartOptions.humidity')
        },
        {
            opt: 'light',
            title: t('chartOptions.light')
        },
        {
            opt: 'temperature',
            title: t('chartOptions.temperature')
        },
    ];    

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
                optionTitle={t(`chartOptions.${chartOption}`)}
                chartOptions={chartConditionsOptions}
                onChangeChartOption={name => setChartOption(name)}
            />
        </>
    );
}

export default observer(OrganDetails);
