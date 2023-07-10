import React from "react";
import Snackbar from "@mui/material/Snackbar";
import Slide, { SlideProps } from "@mui/material/Slide";
import MuiAlert, { AlertProps } from "@mui/material/Alert";
import AlertTitle from "@mui/material/AlertTitle";
import { Stack } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../hooks";
import { closeSnackBar } from "./redux/snackbarslice";

const Alert = React.forwardRef<HTMLDivElement, AlertProps>(function Alert(
	props,
	ref,
) {
	return (
		<MuiAlert elevation={6} ref={ref} variant="filled" {...props}>
		</MuiAlert>
	);
});

type TransitionProps = Omit<SlideProps, "direction">;

export function TransitionLeft(props: TransitionProps) {
	return <Slide {...props} direction="left" />;
}

export function TransitionUp(props: TransitionProps) {
	return <Slide {...props} direction="up" />;
}

export function TransitionRight(props: TransitionProps) {
	return <Slide {...props} direction="right" />;
}

export function TransitionDown(props: TransitionProps) {
	return <Slide {...props} direction="down" />;
}

const SnackbarApp: React.FC = () => {
	const snackbarState = useAppSelector(state => state.snack.optionSnackBar);
	const dispatch = useAppDispatch();

	if(!snackbarState.open) return null;

	return (<Stack sx={{ width: "100%", zIndex:10 }}>
		<Snackbar
			open={snackbarState.open}
			onClose={()=> dispatch(closeSnackBar())}
			TransitionComponent={TransitionLeft}
			autoHideDuration={snackbarState.autoHideDuration}
			key={"snackbar-major-village"}>
			<Alert onClose={()=> dispatch(closeSnackBar())} severity={snackbarState.severity} sx={{ width: "100%" }}>
				<AlertTitle>
					{snackbarState.title}
				</AlertTitle>
				{snackbarState.message}
			</Alert>
		</Snackbar>
	</Stack>);
};

export default SnackbarApp;
