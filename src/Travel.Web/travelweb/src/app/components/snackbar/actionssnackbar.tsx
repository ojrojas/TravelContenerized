import React from "react";
import Snackbar from "@mui/material/Snackbar";
import CloseIcon from "@mui/icons-material/Close";
import { Button, CardActions, CardContent, CardHeader, Divider, IconButton, Paper, PaperProps, Stack, Typography } from "@mui/material";
import styles from "./actionssnackbar.module.css";
import { useAppDispatch, useAppSelector } from "../../hooks";
import { closeSnackBarActionsMajorVillage, executeAction } from "./redux/snackbarslice";
import { TransitionLeft } from "./snackbar.component";

const CardSnackBar = React.forwardRef<HTMLDivElement, PaperProps>(function Alert(
	props,
	ref,
) {
	return (
		<Paper elevation={6} ref={ref} {...props}>
		</Paper>
	);
});

const SnackbarMajorVillageAction: React.FC = () => {
	const snackBarState = useAppSelector(state => state.snack.optionSnackBarActions);
	const dispatch = useAppDispatch();

	const returnColor = (color: string) => {
		switch (color) {
		case "success":
			return "green";
		case "info":
			return "blue";
		case "warning":
			return "orange";
		case "error":
			return "red";
		default: return "info";
		}
	};

	return (
		<Stack spacing={2} sx={{ width: "100%" }}>
			<Snackbar
				open={snackBarState.open}
				onClose={() => dispatch(closeSnackBarActionsMajorVillage())}
				TransitionComponent={TransitionLeft}
				autoHideDuration={snackBarState.autoHideDuration}
				key={"snackbaraction-majorvillage"}>
				<CardSnackBar sx={{ width: "100%" }}>
					<CardHeader
						action={
							<IconButton aria-label="settings" onClick={
								() => {
									dispatch(executeAction(false));
									dispatch(closeSnackBarActionsMajorVillage());
								}}>
								<CloseIcon />
							</IconButton>
						}
						title={snackBarState.title}
						style={{ backgroundColor: returnColor(snackBarState.severity), color: "white" }}
					/>
					<CardContent className={styles.cardheader}>
						{snackBarState.message}
					</CardContent>
					<Divider></Divider>
					<CardActions>
						<Button onClick={() => { 
							dispatch(executeAction(true));
							dispatch(closeSnackBarActionsMajorVillage());
						}}> Accept </Button>
						<Button onClick={
							() => {
								dispatch(executeAction(false));
								dispatch(closeSnackBarActionsMajorVillage());
							}}> Cancel </Button>
					</CardActions>
				</CardSnackBar>
			</Snackbar>
		</Stack>
	);
};

export default SnackbarMajorVillageAction;