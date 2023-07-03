import React from "react";
import { MajorVillageSnackBarOptions } from "../../components/snackbar/majorvillagesnackbaroptions";
import { useAppSelector } from "../../hooks";

export const SnackBarMajorVillageContext = React.createContext<MajorVillageSnackBarOptions>({
	message: "",
	severity: "info",
	title: undefined,
	autoHideDuration: undefined,
	action: undefined
});

interface Props {
    children: React.ReactNode;
}

export const SnackBarMajorVillageProvider:React.FC<Props> = ({children}) => {
	const {optionSnackBar, optionSnackBarActions} = useAppSelector(state => state.snack);
	return (<SnackBarMajorVillageContext.Provider value={{
		title: optionSnackBar.title ?? optionSnackBarActions.title,
		autoHideDuration: optionSnackBar.autoHideDuration ?? optionSnackBarActions.autoHideDuration,
		message: optionSnackBar.message ?? optionSnackBarActions.message,
		severity: optionSnackBar.severity ?? optionSnackBarActions.severity,
		action: optionSnackBar.action ?? optionSnackBarActions.action,
		open: optionSnackBar.open ?? optionSnackBarActions.open,
	}}>
		{children}
	</SnackBarMajorVillageContext.Provider>);
};

