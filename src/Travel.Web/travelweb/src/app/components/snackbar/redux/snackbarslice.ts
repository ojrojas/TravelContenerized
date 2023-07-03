import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { MajorVillageSnackBarOptions } from "../majorvillagesnackbaroptions";

const initialOptions: MajorVillageSnackBarOptions = {
	open: false,
	message: "",
	severity: "info",
	title:undefined,
	action:undefined,
	autoHideDuration:undefined
};

interface State {
	optionSnackBar: MajorVillageSnackBarOptions;
	optionSnackBarActions: MajorVillageSnackBarOptions;
}

const initialState: State = {
	optionSnackBar: initialOptions,
	optionSnackBarActions: initialOptions
};

const snackBarSlice = createSlice({
	name: "snackbar",
	initialState: initialState,
	reducers: {
		openSnackBarMajorVillage: (state, action: PayloadAction<MajorVillageSnackBarOptions>) => {
			state.optionSnackBar.message = action.payload.message;
			state.optionSnackBar.open = true;
			state.optionSnackBar.severity = action.payload.severity;
			state.optionSnackBar.title = action.payload.title;
			state.optionSnackBar.autoHideDuration = action.payload.autoHideDuration;
		},
		closeSnackBarMajorVillage: (state) => {
			state.optionSnackBar = initialOptions;
		},
		openSnackBarActionsMajorVillage: (state, action: PayloadAction<MajorVillageSnackBarOptions>) => {
			state.optionSnackBarActions.message = action.payload.message;
			state.optionSnackBarActions.title = action.payload.title;
			state.optionSnackBarActions.severity = action.payload.severity;
			state.optionSnackBarActions.autoHideDuration = action.payload.autoHideDuration;
			state.optionSnackBarActions.action = action.payload.action;
			state.optionSnackBarActions.open = true;
		},
		closeSnackBarActionsMajorVillage: (state) => {
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
	openSnackBarMajorVillage,
	closeSnackBarMajorVillage,
	openSnackBarActionsMajorVillage,
	closeSnackBarActionsMajorVillage,
	executeAction,
} = snackBarSlice.actions;