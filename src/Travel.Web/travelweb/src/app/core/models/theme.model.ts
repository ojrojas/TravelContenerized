export type ThemeType = "dark" | "light";

export enum ColorApp {
    BLUE =  "#1E90FF",
    DARK_BLUE = "#000080",
    LIGHT_BLUE= "#87CEFA",
    GREY = "#808080",
    DARK_GREY = "#A9A9A9",
    LIGHT_GREY= "#D3D3D3",
    WHITE = "#fff",
}

export interface Theme {
    "--primary": ColorApp;
    "--secondary": ColorApp;
    "--background": ColorApp;
    "--color": ColorApp;
}