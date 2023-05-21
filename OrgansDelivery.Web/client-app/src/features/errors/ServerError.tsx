import React from "react";
import { Box, Button, Container, Typography } from "@mui/material";
import Grid from "@mui/material/Grid";
import { router } from "../../app/router/Routes";

const ServerError = () => {
    return (
        <Box
            sx={{
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                minHeight: "100vh",
            }}
        >
            <Container maxWidth="md">
                <Grid container spacing={2}>
                    <Grid xs={6}>
                        <Typography variant="h1">500</Typography>
                        <Typography variant="h6">
                            Server Error.
                        </Typography>
                        <Button
                            variant="contained"
                            onClick={() => router.navigate('/organs')}
                        >
                            Back Home
                        </Button>
                    </Grid>
                    <Grid xs={6}>
                        <img
                            src="https://www.hostpapa.com/blog/app/uploads/2022/12/What-Is-the-Mysterious-500-Internal-Server-Error-Header-1-1568x882.jpg"
                            alt=""
                            width={500}
                            height={250}
                        />
                    </Grid>
                </Grid>
            </Container>
        </Box>
    );
}

export default ServerError;
