import React from "react";
import { Grid, Typography } from "@mui/material";
import BreadCrumbsComponent from "../breadcrumbs/breadcrumbs.component";
import styles from "./header.module.css";

interface Props {
    namePage: string;
    subNamePage: string;
    actionButtons?: React.ReactNode;
}

const HeaderComponent: React.FC<Props> = ({ namePage, subNamePage, actionButtons }) => {
	return (
		<Grid container flexGrow={1} className={styles.container}>
			<Grid item>
				<Grid container className={styles.containerName}>
					<Grid item md={4}>
						<Typography variant={"h6"} component='h6'>
							{namePage}
						</Typography>
					</Grid>
					<Grid item >
						<BreadCrumbsComponent subNamePage={subNamePage} />
					</Grid>
				</Grid>
			</Grid>
			<Grid item>
				<Grid container className={styles.containerActions}>
					{actionButtons}
				</Grid>
			</Grid>
		</Grid>
	);
};

export default HeaderComponent;

