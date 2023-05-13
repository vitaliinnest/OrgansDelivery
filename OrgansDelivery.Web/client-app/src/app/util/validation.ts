import { validate } from "uuid";
import * as Yup from "yup";

export function guid() {
    return Yup.string().test("is-uuid", "Invalid invide code", (value) => {
        return value === undefined || validate(value);
    });
}
