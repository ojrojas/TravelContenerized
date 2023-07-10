import React from "react";
import { Box, Button, Grid } from "@mui/material";
import { useForm } from "react-hook-form";
import styles from "./formusercreate.module.css";
import { IUser } from "../../../core/models/user/user";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import useYupValidationResolver from "../../../components/forms/resolver.function";
import { createUser, updateUser, getAllUsers } from "../redux/users-actions";
import LoadingBackdropComponent from "../../../components/loaders/backdrop.component";
import InputOutlinedComponent from "../../../components/forms/input.component";
import { schema } from "../schemas/formusercreate.schema";
import { openSnackBar } from "../../../components/snackbar/redux/snackbarslice";

interface createUserForm {
    onClose: () => void;
    userExists?: IUser;
    typeComponent: "EDIT" | "CREATE"
}

const FormUserCreateComponent: React.FC<createUserForm> = ({ onClose, userExists, typeComponent }) => {
	const dispatch = useAppDispatch();
	const { loading, error } = useAppSelector(state => state.user);
	const userApp = useAppSelector(state => state.login).user;
	const state = useAppSelector(x => x.login);

	const { register, handleSubmit, formState: { errors } } = useForm<IUser>({
		mode: "all",
		defaultValues: { ...userExists },
		resolver: useYupValidationResolver(schema)
	});

	const handlerSubmit = handleSubmit(async (user: IUser) => {
		if (userApp === null) throw Error("Error operation exception");
		if (typeComponent === "CREATE") {
			dispatch(createUser({ user })).unwrap().then(async (response) => {
				if (response?.userCreated === null) {
					dispatch(openSnackBar({
						message: `Error, ${JSON.stringify(error, null, 2)}`,
						severity: "error",
						title: "Users"
					}));
				}
				else {
					dispatch(openSnackBar({
						message: "User created!",
						severity: "success",
						title: "Users"
					}));
				}
			});
		}
		else {
			await dispatch(updateUser({ user })).unwrap().then(async (response) => {
				if (response?.userUpdated === null) {
					await dispatch(openSnackBar({
						message: `Error, ${JSON.stringify(error, null, 2)}`,
						severity: "error"
					}));
				}
				else {
					await dispatch(openSnackBar({
						message: "User created!",
						severity: "success"
					}));
				}
			});
		}
		await dispatch(getAllUsers());
	});

	return (
		<React.Fragment>
			<LoadingBackdropComponent open={loading} />
			<Box className={styles.formpaper} component={"form"} onSubmit={handlerSubmit}>
				<Grid container spacing={3}>
					<Grid item xs={12} md={12} sm={12}>
						<Grid container spacing={1}>
							<Grid item md={4} sm={4} xs={12}>
								<InputOutlinedComponent
									fullWidth
									label={"User Name"}
									type={"text"}
									register={register("userName", { required: true })}
									errors={errors}
								/>
							</Grid>
							<Grid item md={4} sm={4} xs={12}>
								<InputOutlinedComponent
									fullWidth
									label={"Email"}
									type={"email"}
									register={register("email", { required: true })}
									errors={errors}
								/>
							</Grid>
						</Grid>
					</Grid>
				</Grid>
				<Button variant="text" className={styles.buttonlogin} type="submit">{typeComponent} User</Button>
			</Box>
		</React.Fragment>
	);
};

export default FormUserCreateComponent;