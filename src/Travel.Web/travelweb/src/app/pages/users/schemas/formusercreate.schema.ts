import { object, string } from "yup";

export const schema = object({
	firstName: string().required("First name is required"),
	lastName: string().required("Last name is required"),
	identification: string().required("Identification is required"),
	email: string().email().required("Email is required"),
	typeIdentificationId: string().required("Type identification is required"),
	typeUserId: string().required("Type user is required")
});