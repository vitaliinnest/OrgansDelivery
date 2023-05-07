import { Typography } from "@mui/material";
import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <Typography>Not Found</Typography>
        // <Segment placeholder>
        //     <Header icon>
        //         <Icon name='search' />
        //         Oops - we've looked everywhere but could not find what you are looking for!
        //     </Header>
        //     <Segment.Inline>
        //         <Button as={Link} to='/activities'>
        //             Return to activities page
        //         </Button>
        //     </Segment.Inline>
        // </Segment>
    )
}