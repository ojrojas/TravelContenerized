import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { SnackBarOptions } from "../snackbaroptions";

const initialOptions: SnackBarOptions = {
	open: false,
	message: "",
	severity: "info",
	title:undefined,
	action:undefined,
	autoHideDuration:undefined
};

interface State {
	optionSnackBar: SnackBarOptions;
	optionSnackBarActions: SnackBarOptions;
}

const initialState: State = {
	optionSnackBar: initialOptions,
	optionSnackBarActions: initialOptions
};

const snackBarSlice = createSlice({
	name: "snackbar",
	initialState: initialState,
	reducers: {
		openSnackBar: (state, action: PayloadAction<SnackBarOptions>) => {
			state.optionSnackBar.message = action.payload.message;
			state.optionSnackBar.open = true;
			state.optionSnackBar.severity = action.payload.severity;
			state.optionSnackBar.title = action.payload.title;
			state.optionSnackBar.autoHideDuration = action.payload.autoHideDuration;
		},
		closeSnackBar: (state) => {
			state.optionSnackBar = initialOptions;
		},
		openSnackBarActions: (state, action: PayloadAction<SnackBarOptions>) => {
			state.optionSnackBarActions.message = action.payload.message;
			state.optionSnackBarActions.title = action.payload.title;
			state.optionSnackBarActions.severity = action.payload.severity;
			state.optionSnackBarActions.autoHideDuration = action.payload.autoHideDuration;
			state.optionSnackBarActions.action = action.payload.action;
			state.optionSnackBarActions.open = true;
		},
		closeSnackBarActions: (state) => {
			state.optionSnackBarActions = initialOptions;
		},
		executeAction: (state, action: PayloadAction<boolean>) => {
			console.log("optionAction is ", state.optionSnackBarActions.action);
			if(state.optionSnackBarActions.action !== undefined)
				state.optionSnackBarActions.action(action.payload);
		}
	}
});

export default snackBarSlice.reducer;
export const {
	openSnackBar,
	closeSnackBar,
	openSnackBarActions,
	closeSnackBarActions,
	executeAction,
} = snackBarSlice.actions;