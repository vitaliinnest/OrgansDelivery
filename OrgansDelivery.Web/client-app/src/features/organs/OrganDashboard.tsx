import React, { useMemo, useState } from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import { observer } from "mobx-react-lite";
import ConditionsLineChart, {
    ConditionsRecordUnit,
} from "../../app/layout/charts/ConditionsLineChart";
import { grey } from "@mui/material/colors";
import { Organ } from "../../app/models/organ";
import {
    ConditionsRecord,
    ConditionsViolation,
} from "../../app/models/conditionsRecord";
import { Divider } from "@mui/material";
import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import RecordsList from "../records/RecordsList";
import ViolationsList from "../records/ViolationsList";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";
import { useTranslation } from "react-i18next";
import { numberConditionToString, orientationConditionToString } from "../conditions/ConditionsList";
import { unitByValueNameMap } from "../../app/util/common";

type Props = {
    organ: Organ;
    records: ConditionsRecord[];
    violations: ConditionsViolation[];
    chartOption: keyof ConditionsRecord;
    optionTitle: string;
    chartOptions: ChartOptions;
    onChangeChartOption: (name: keyof ConditionsRecord) => void;
};

const OrganDashboard = (props: Props) => {
    const {
        organ,
        optionTitle,
        chartOption,
        chartOptions,
        onChangeChartOption,
    } = props;
    const { t } = useTranslation("translation", { keyPrefix: "details" });

    const [range, setRange] = useState<DateRange>({
        start: getDate(7),
        end: new Date(),
    });

    const rangeRecords = useMemo(
        () =>
            props.records.filter(
                (r) => range.start <= r.dateTime && r.dateTime <= range.end
            ),
        [props.records, range.end, range.start]
    );

    const rangeViolations = useMemo(
        () =>
            props.violations.filter(
                (v) =>
                    range.start <= v.record.dateTime &&
                    v.record.dateTime <= range.end
            ),
        [props.violations, range.end, range.start]
    );

    const chartData = useMemo(
        () =>
            rangeRecords.map<ConditionsRecordUnit>((r) => ({
                dateTime: r.dateTime,
                value: Number(r[chartOption]),
            })),
        [chartOption, rangeRecords]
    );

    return (
        <Box sx={{ display: "flex" }}>
            <Box
                component="main"
                sx={{
                    backgroundColor: grey[100],
                    flexGrow: 1,
                    height: "93vh",
                    overflow: "auto",
                }}
            >
                <Container maxWidth="xl" sx={{ mt: 4, mb: 4 }}>
                    <Grid container spacing={3}>
                        <Grid item md={7}>
                            <Paper
                                sx={{
                                    p: 2,
                                    display: "flex",
                                    flexDirection: "column",
                                }}
                            >
                                <Typography align="center" variant="h5">
                                    {optionTitle} {t("chartName")}
                                </Typography>
                                <ConditionsLineChart
                                    valueTitle={optionTitle}
                                    valueName={chartOption}
                                    data={chartData}
                                />
                                <ChartOptionsSelector
                                    option={chartOption}
                                    options={chartOptions}
                                    onChange={onChangeChartOption}
                                />
                                <DateRangePicker
                                    range={range}
                                    onChange={(range) => setRange(range)}
                                />
                            </Paper>
                        </Grid>
                        <Grid item md={5}>
                            <OrganInfoCard organ={organ} />
                        </Grid>
                        <Grid item md={12}>
                            <Paper
                                sx={{
                                    p: 2,
                                    display: "flex",
                                    flexDirection: "column",
                                }}
                            >
                                <ViolationsList violations={rangeViolations} />
                            </Paper>
                        </Grid>
                        <Grid item md={12}>
                            <Paper
                                sx={{
                                    p: 2,
                                    display: "flex",
                                    flexDirection: "column",
                                }}
                            >
                                <RecordsList records={rangeRecords} />
                            </Paper>
                        </Grid>
                    </Grid>
                </Container>
            </Box>
        </Box>
    );
};

export default observer(OrganDashboard);

type OrganInfoCardProps = {
    organ: Organ;
}

const OrganInfoCard = (props: OrganInfoCardProps) => {
    const { organ } = props;
    const { t } = useTranslation("translation", { keyPrefix: "details.infoSection" });

    return (
        <Paper
            sx={{
                p: 2,
                display: "flex",
                flexDirection: "column",
            }}
        >
            <Typography variant="h5" mb={2}>
                {t('organDetails')}
            </Typography>
            <Divider />
            <Typography variant="h6" mt={1} mb={1}>
                {t('name')}: {organ.name}
            </Typography>
            <Divider />
            <Typography variant="h6" mt={1} mb={1}>
                {t('description')}: {organ.description}
            </Typography>
            <Divider />
            <Typography variant="h6" mt={1} mb={1}>
                {t('creationDate')}: {organ.organCreationDate.toLocaleString()}
            </Typography>
            <Divider />
            <Typography variant="h6" mt={1} mb={1}>
                {t('containerName')}: {organ.container.name}
            </Typography>
            <Divider textAlign="left">
            <Typography variant="h6">{t('conditions')}</Typography>
            </Divider>
            <Typography variant="h6" mt={1} mb={1}>
                {t('conditionsName')}: {organ.conditions.name}
            </Typography>
            <Divider />
            <Typography variant="h6" mt={1} mb={1}>
                {t('conditionsDescription')}: {organ.conditions.description}
            </Typography>
            <Divider />
            <Typography variant="h6" mt={1} mb={1}>
                {t('conditionsHumidity')}: {numberConditionToString(organ.conditions.humidity)} {unitByValueNameMap['humidity']}
            </Typography>
            <Divider />
            <Typography variant="h6" mt={1} mb={1}>
                {t('conditionsTemperature')}: {numberConditionToString(organ.conditions.temperature)} {unitByValueNameMap['temperature']}
            </Typography>
            <Divider />
            <Typography variant="h6" mt={1} mb={1}>
                {t('conditionsLight')}: {numberConditionToString(organ.conditions.light)} {unitByValueNameMap['light']}
            </Typography>
            <Divider />
            <Typography variant="h6" mt={1} mb={1}>
                {t('conditionsOrientation')}: {orientationConditionToString(organ.conditions.orientation)} {unitByValueNameMap['orientation']}
            </Typography>
        </Paper>
    );
}

export type ChartOptions = {
    opt: keyof ConditionsRecord;
    title: string;
}[];

type ChartOptionsSelectorProps = {
    option: keyof ConditionsRecord;
    options: ChartOptions;
    onChange: (name: keyof ConditionsRecord) => void;
};

const ChartOptionsSelector = (props: ChartOptionsSelectorProps) => {
    const { option, onChange } = props;
    const { t } = useTranslation("translation", { keyPrefix: "details" });

    const options = useMemo(() => props.options.sort(), [props.options]);

    return (
        <FormControl required>
            <InputLabel>{t("condition")}</InputLabel>
            <Select
                label={t("condition")}
                value={option}
                onChange={(e) =>
                    onChange(e.target.value as keyof ConditionsRecord)
                }
            >
                {options.map((o) => (
                    <MenuItem value={o.opt}>{o.title}</MenuItem>
                ))}
            </Select>
        </FormControl>
    );
};

type DateRange = {
    start: Date;
    end: Date;
};

type DateRangePickerProps = {
    range: DateRange;
    onChange: (range: DateRange) => void;
};

const DateRangePicker = (props: DateRangePickerProps) => {
    const { range, onChange } = props;
    const { t } = useTranslation("translation", { keyPrefix: "details" });

    return (
        <Grid mt={1} container spacing={2}>
            <Grid item>
                <DatePicker
                    label={t("start")}
                    value={dayjs(range.start)}
                    onChange={(val) =>
                        val && onChange({ ...range, start: val.toDate() })
                    }
                />
            </Grid>
            <Grid item>
                <DatePicker
                    label={t("end")}
                    value={dayjs(range.end)}
                    onChange={(val) =>
                        val && onChange({ ...range, end: val.toDate() })
                    }
                />
            </Grid>
        </Grid>
    );
};

function getDate(daysBefore: number): Date {
    const currentDate = new Date();
    currentDate.setDate(currentDate.getDate() - daysBefore);
    return currentDate;
}
