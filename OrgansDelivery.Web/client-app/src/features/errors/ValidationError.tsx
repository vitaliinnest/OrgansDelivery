import { Typography } from "@mui/material";

interface Props {
    errors: any;
}

export default function ValidationError({errors}: Props) {
    return (
        <Typography>Validation Error</Typography>
        // <Message error>
        //     {errors && (
        //         <Message.List>
        //             {errors.map((err: string, i: any) => (
        //                 <Message.Item key={i}>{err}</Message.Item>
        //             ))}
        //         </Message.List>
        //     )}
        // </Message>
    )
}