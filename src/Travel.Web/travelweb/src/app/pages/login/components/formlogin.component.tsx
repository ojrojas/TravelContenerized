import React from "react";
import { Button, colors, Grid, Paper, Typography } from "@mui/material";
import InputOutlinedComponent from "./../../../components/forms/input.component";
import styles from "./formlogin.module.css";
import { useForm } from "react-hook-form";
import useYupValidationResolver from "../../../components/forms/resolver.function";
import { schema } from "../schema/formlogin.schema";
import { ILoginApplicationRequest } from "../../../core/dtos/userapplication/loginapplicationrequest";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import { login } from "../redux/login.actions";
import { ILoginApplicationResponse } from "../../../core/dtos/userapplication/loginapplicationresponse";
import { updateLogged } from "../redux/login.slice";
import { RouteConstanstPage } from "../../../core/constants/route.pages.constants";
import { useNavigate } from "react-router-dom";
import { openSnackBar } from "../../../components/snackbar/redux/snackbarslice";

const FormLoginComponent: React.FC = () => {
	const dispatch = useAppDispatch();
	const navigateOn = useNavigate();
	const { logged } = useAppSelector(state => state.login);
	const { register, handleSubmit, formState: { errors } } = useForm<ILoginApplicationRequest>({
		defaultValues: {
			client_id: "travelweb-client",
			client_secret : "07c4d6bf-4da5-433b-98ff-9545172955a7",
			grant_type : "password",
			username : 	"pepe@example.com",
			password : 	"Abc123456#",
			scope : "aggregator"
		},
		mode: "all",
		resolver: useYupValidationResolver(schema)
	});

	React.useEffect(() => {
		if (logged) navigateOn(RouteConstanstPage.dashboard);
		else dispatch(updateLogged(false));
	}, [dispatch, navigateOn, logged]);

	const onSubmit = handleSubmit(async (data) => await dispatch(login(data)).unwrap().then(async (response: ILoginApplicationResponse) => {
		if (response.access_token !== undefined) {
			await dispatch(updateLogged(true));
			navigateOn(RouteConstanstPage.dashboard);
		}
		else dispatch(openSnackBar({
			message: "Error username or password invalid!",
			severity: "warning",
			title: "Login",
		}));
	}).catch(response => {
		console.log("Response catch -- : ", response);
		dispatch(openSnackBar({
			message: response.message,
			severity: "error",
			title: "Login"
		}));
	}));
	
	return (
		<React.Fragment>
			<Grid container className={styles.container}>
				<Paper elevation={12}>
					<Grid className={styles.form}>
						<Typography variant={"body2"} color={colors.blue[400]}>
							Sign In
						</Typography>
						<Typography variant={"h5"} component='span' color={"var(--primary)"}>
							Travel
						</Typography>
						<hr className={styles.lines} />
						<InputOutlinedComponent
							label="Username"
							type={"text"}
							size='small'
							register={register("username", { required: true })}
							errors={errors} />
						<InputOutlinedComponent
							label="Password"
							size="small"
							type={"password"}
							register={register("password", { required: true })}
							errors={errors} />
						<Grid className={styles.buttons}>
							<Button
								onClick={onSubmit}
								variant="outlined"
								color='primary'
								type="button">
								Sign In
							</Button>
						</Grid>
					</Grid>
				</Paper>
			</Grid>
		</React.Fragment>
	);
};

export default FormLoginComponent;