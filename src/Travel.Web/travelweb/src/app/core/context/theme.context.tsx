
import React, { Dispatch, SetStateAction } from "react";
import { THEMES } from "../configurations/theme.config";
import { Theme, ThemeType } from "../models/theme.model";

interface ThemeProviderProps {
    children: React.ReactNode
}

interface ThemeContextProps {
    themeType: ThemeType;
    theme: Theme;
    setCurrentTheme: Dispatch<SetStateAction<ThemeType>>;
}

export const ThemeContext = React.createContext<ThemeContextProps>({
	themeType: "light",
	theme: THEMES["light"],
} as ThemeContextProps);

export const ThemeProvider: React.FC<ThemeProviderProps> = ({children}) => {
	const [currentTheme, setCurrentTheme] = React.useState<ThemeType>("light");

	return (<ThemeContext.Provider value={{
		themeType: currentTheme,
		theme: THEMES[currentTheme],
		setCurrentTheme
	}}>
		{children}
	</ThemeContext.Provider>);
};

