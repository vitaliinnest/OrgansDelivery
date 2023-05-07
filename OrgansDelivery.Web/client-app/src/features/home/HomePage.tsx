import { observer } from "mobx-react-lite";
import React from "react";
import { Link } from "react-router-dom";
import { useStore } from "../../app/stores/store";
import LoginForm from "../users/LoginForm";
import RegsiterForm from "../users/RegisterForm";
import { Typography } from "@mui/material";

export default observer(function HomePage() {
    const { userStore } = useStore();
    return (
        <Typography>Home Page</Typography>
        // <Segment inverted textAlign="center" vertical className="masthead">
        //     <Container text>
        //         <Header as="h1" inverted>
        //             <Image
        //                 size="massive"
        //                 src="/assets/logo.png"
        //                 alt="logo"
        //                 style={{ marginBottom: 12 }}
        //             />
        //             Reactivities
        //         </Header>
        //         {userStore.isLoggedIn ? (
        //             <>
        //                 <Header
        //                     as="h2"
        //                     inverted
        //                     content={`Welcome back ${userStore.user?.displayName}`}
        //                 />
        //                 <Button as={Link} to="/activities" size="huge" inverted>
        //                     Go to activities!
        //                 </Button>
        //             </>
        //         ) : (
        //             <>
        //                 <Button
        //                     onClick={() => modalStore.openModal(<LoginForm />)}
        //                     size="huge"
        //                     inverted
        //                 >
        //                     Login!
        //                 </Button>
        //                 <Button
        //                     onClick={() =>
        //                         modalStore.openModal(<RegsiterForm />)
        //                     }
        //                     size="huge"
        //                     inverted
        //                 >
        //                     Register
        //                 </Button>
        //             </>
        //         )}
        //     </Container>
        // </Segment>
    );
});
