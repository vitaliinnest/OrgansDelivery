import React, { useMemo, useState } from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import { observer } from "mobx-react-lite";
import ConditionsLineChart, { ConditionsRecordUnit } from "../../app/layout/charts/LineChart";
import { grey } from "@mui/material/colors";
import { Organ } from "../../app/models/organ";
import { ConditionsRecord, ConditionsViolation } from "../../app/models/conditionsRecord";
import { Button, Divider, capitalize } from "@mui/material";
import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import RecordsList from "../records/RecordsList";
import ViolationsList from "../records/ViolationsList";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";

type Props = {
    organ: Organ;
    records: ConditionsRecord[];
    violations: ConditionsViolation[];
    chartOption: keyof ConditionsRecord;
    chartOptions: (keyof ConditionsRecord)[];
    onChangeChartOption: (name: keyof ConditionsRecord) => void;
};

const OrganDashboard = (props: Props) => {
    const { organ, chartOption, chartOptions, onChangeChartOption } = props;

    const [range, setRange] = useState<DateRange>({
        start: getDate(7),
        end: new Date(),
    });

    const rangeRecords = useMemo(
        () => props.records.filter(r => range.start <= r.dateTime && r.dateTime <= range.end),
        [props.records, range.end, range.start]
    );

    const rangeViolations = useMemo(
        () => props.violations.filter(v => range.start <= v.record.dateTime && v.record.dateTime <= range.end),
        [props.violations, range.end, range.start]
    );

    const chartData = useMemo(
        () => rangeRecords
            .map<ConditionsRecordUnit>(r => ({
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
                    height: "100vh",
                    overflow: "auto",
                    // pb: 5
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
                                <Typography align="center" variant="h5">{capitalize(chartOption)} Chart</Typography>
                                <ConditionsLineChart
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
                                    onChange={range => setRange(range)}
                                />
                            </Paper>
                        </Grid>
                        <Grid item md={5}>
                            <Paper
                                sx={{
                                    p: 2,
                                    display: "flex",
                                    flexDirection: "column",
                                }}
                            >
                                <Typography variant="h5" mb={2}>Organ Details</Typography>
                                <Divider/>
                                <Typography variant="h6" mt={1} mb={1}>Name: {organ.name}</Typography>
                                <Divider/>
                                <Typography variant="h6" mt={1} mb={1}>Description: {organ.description}</Typography>
                                <Divider/>
                                <Typography variant="h6" mt={1} mb={1}>Creation Date: {organ.organCreationDate.toString()}</Typography>
                                <Divider/>
                                <Typography variant="h6" mt={1} mb={1}>Container Name: {organ.container.name}</Typography>
                                <Divider/>
                                <Typography variant="h6" mt={1} mb={1}>Conditions Name: {organ.conditions.name}</Typography>
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
                    </Grid>
                </Container>
            </Box>
        </Box>
    );
};

export default observer(OrganDashboard);

type ChartOptionsSelectorProps = {
    option: string;
    options: string[]
    onChange: (name: keyof ConditionsRecord) => void;
}

const ChartOptionsSelector = (props: ChartOptionsSelectorProps) => {
    const { option, onChange } = props;
    
    const options = useMemo(
        () => props.options.map((o) => capitalize(o)).sort(),
        [props.options]
    );

    return <FormControl
        required
    >
        <InputLabel>
            Condition
        </InputLabel>
        <Select
            label="Condition"
            value={option}
            onChange={(e) => onChange(e.target.value as keyof ConditionsRecord)}
        >
            {options.map(o => (
                <MenuItem value={o.toLowerCase()}>{o}</MenuItem>
            ))}
        </Select>
    </FormControl>;
}

type DateRange = {
    start: Date;
    end: Date;
}

type DateRangePickerProps = {
    range: DateRange;
    onChange: (range: DateRange) => void;
}

const DateRangePicker = (props: DateRangePickerProps) => {
    const { range, onChange } = props;
    return (
        <Grid mt={1} container spacing={2}>
            <Grid item>
                <DatePicker
                    label="Start"
                    value={dayjs(range.start)}
                    onChange={val => val && onChange({ ...range, start: val.toDate() })}
                />
            </Grid>
            <Grid item>
                <DatePicker
                    label="End"
                    value={dayjs(range.end)}
                    onChange={val => val && onChange({ ...range, end: val.toDate() })}
                />
            </Grid>
        </Grid>
    );
}

function getDate(daysBefore: number): Date {
    const currentDate = new Date();
    currentDate.setDate(currentDate.getDate() - daysBefore);
    return currentDate;
}

