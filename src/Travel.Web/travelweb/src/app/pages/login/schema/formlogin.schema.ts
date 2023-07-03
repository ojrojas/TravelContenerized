import {object, string} from "yup";

export const schema = object({
	userName: string().required("Username is required"),
	password: string().required("Password is required")
});