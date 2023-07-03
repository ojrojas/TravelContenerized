import { Box, Grid, Paper, Typography } from "@mui/material";
import React from "react";

const DashboardPage: React.FC = () => {
	return (
		<Grid container sx={{ padding: 1, height: "100vh" }} gridRow={1}>
			<Grid item xs={12} md={12} lg={12} xl={12}>
				<Paper  elevation={4} sx={{ backgroundColor: "#fff", height: "99%",  padding:5 }}>
					<Typography variant={"h6"} component='h6'>
                            Dashboard
					</Typography>
				</Paper>
			</Grid>
		</Grid>
	);
};

export default DashboardPage;