import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { Typography } from "@mui/material";

export default observer(function ServerError() {
    const {commonStore} = useStore();
    return (
        <Typography>Server Error</Typography>
        // <Container>
        //     <Header as='h1' content='Server Error' />
        //     <Header sub as='h5' color="red" content={commonStore.error?.message} />
        //     {commonStore.error?.details && (
        //         <Segment>
        //             <Header as='h4' content='Stack trace' color="teal" />
        //             <code style={{marginTop: '10px'}}>{commonStore.error.details}</code>
        //         </Segment>
        //     )}
        // </Container>
    )
})