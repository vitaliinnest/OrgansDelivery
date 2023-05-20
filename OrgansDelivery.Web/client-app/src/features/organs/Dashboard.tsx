import React from "react";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import List from "@mui/material/List";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import { observer } from "mobx-react-lite";
import ConditionsLineChart from "../../app/layout/charts/LineChart";
import { grey } from "@mui/material/colors";
import { Organ } from "../../app/models/organ";

type Props = {
    organ: Organ;
};

const OrganDashboard = () => {
    return (
        <Box sx={{ display: "flex" }}>
            <Box
                component="main"
                sx={{
                    backgroundColor: grey[100],
                    flexGrow: 1,
                    height: "100vh",
                    overflow: "auto",
                }}
            >
                <Toolbar />
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
                                <ConditionsLineChart
                                    valueName="Temperature"
                                    unitName="°C"
                                    data={[
                                        {
                                            value: 10,
                                            dateTime: new Date(2022, 10, 21, 8),
                                        },
                                        {
                                            value: 30,
                                            dateTime: new Date(2022, 10, 22, 9),
                                        },
                                        {
                                            value: 10,
                                            dateTime: new Date(2022, 10, 23, 10),
                                        },
                                        {
                                            value: 15,
                                            dateTime: new Date(2022, 10, 25, 11),
                                        },
                                    ]}
                                />
                            </Paper>
                        </Grid>
                        <Grid item md={5}>
                            <Paper
                                sx={{
                                    p: 2,
                                    display: "flex",
                                    flexDirection: "column",
                                    height: 240,
                                }}
                            >
                                Container details here
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
                                List of records here with the ability to choose date range
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
                                List of violations here (use date range from the above)
                            </Paper>
                        </Grid>
                    </Grid>
                </Container>
            </Box>
        </Box>
    );
};

export default observer(OrganDashboard);
