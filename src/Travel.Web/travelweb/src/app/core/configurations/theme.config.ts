import { ColorApp, Theme, ThemeType } from "../models/theme.model";

export const THEMES: Record<ThemeType, Theme> = {
	light: {
		"--primary": ColorApp.GREY,
		"--secondary": ColorApp.BLUE,
		"--background": ColorApp.LIGHT_GREY,
		"--color": ColorApp.WHITE
	},
	dark: {
		"--primary": ColorApp.BLUE,
		"--secondary": ColorApp.GREY,
		"--background": ColorApp.LIGHT_BLUE,
		"--color": ColorApp.WHITE
	}
};