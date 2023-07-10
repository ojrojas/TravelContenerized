export interface SnackBarOptions {
    message: string;
    severity: "success" | "info" | "warning" | "error";
    title?: string;
    open?: boolean;
    autoHideDuration?: number | null | undefined;
    action?: (result: boolean) => void;
}