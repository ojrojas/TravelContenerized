import React, { ReactNode } from "react";
import { Box, CardHeader, Divider, Grid, IconButton, Paper } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import { createPortal } from "react-dom";
import styles from "./modal.module.css";

interface Props {
    open: boolean;
    title: string;
    children: ReactNode;
    onClose: () => void;
}

const container = document.getElementById("modal-root")!;

const ModalComponent: React.FC<Props> = ({open, title, children, onClose }) => {

	if(!open) return null;

	return createPortal(
		<Box component={"div"} className={styles.overlay}>
			<Paper className={styles.modal} elevation={10}>
				<CardHeader
					action={
						<IconButton aria-label='close' onClick={onClose}>
							<CloseIcon />
						</IconButton>
					}
					title={title} />
				<Divider></Divider>
				<Grid className={styles.content}>
					{children}
				</Grid>
			</Paper>
		</Box>,
		container
	);
};

export default ModalComponent;