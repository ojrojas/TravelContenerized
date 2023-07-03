import { Backdrop, CircularProgress } from "@mui/material";
import React from "react";

interface Props {
    open: boolean;
    handleClose?: ()=> void;
}

const LoadingBackdropComponent: React.FC<Props> = ({ open, handleClose }) => {
	return (
      
		<Backdrop
			sx={{ color: "red", zIndex: (theme) => theme.zIndex.drawer + 1000 }}
			open={open}
			onClick={handleClose}>
			<CircularProgress color="inherit" />
		</Backdrop>
	);
};

export default  LoadingBackdropComponent;