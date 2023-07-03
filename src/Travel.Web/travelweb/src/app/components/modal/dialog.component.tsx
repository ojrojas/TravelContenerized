import React from "react";
import { Breakpoint, Dialog, DialogContent, DialogTitle, Slide } from "@mui/material";
import IconButton from "@mui/material/IconButton";
import CloseIcon from "@mui/icons-material/Close";
import { TransitionProps } from "@mui/material/transitions";

const Transition = React.forwardRef(function Transition(
	props: TransitionProps & {
      children: React.ReactElement<any, any>;
    },
	ref: React.Ref<unknown>,
) {
	return <Slide direction="up" ref={ref} {...props} />;
});

export interface DialogTitleProps {
    id: string;
    titleDialog: string;
    children?: React.ReactNode;
    onClose: () => void;
    open: boolean;
    fullWidth?: boolean;
    maxWidth?: Breakpoint | false | undefined;
}

const DialogComponent: React.FC<DialogTitleProps> = (
	{ id, titleDialog, open, children, onClose, fullWidth = false, maxWidth = "sm", ...other }) => {
	return (
		<Dialog open={open} onClose={onClose} maxWidth={maxWidth} fullWidth={fullWidth} TransitionComponent={Transition}>
			<DialogTitle sx={{ m: 0, p: 2 }} {...other}>
				{titleDialog}
				{onClose ? (
					<IconButton
						aria-label="close"
						onClick={onClose}
						sx={{
							position: "absolute",
							right: 8,
							top: 8,
							color: (theme) => theme.palette.grey[500],
						}}
					>
						<CloseIcon />
					</IconButton>
				) : null}
			</DialogTitle>
			<DialogContent dividers={true}>
				{children}
			</DialogContent>
		</Dialog>
	);
};

export default DialogComponent;