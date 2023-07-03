import React from "react";
import { RouteConstanstPage } from "../../../core/constants/route.pages.constants";
import { MenuItem } from "../../../core/models/menulink.model";
import DashboardIcon from "@mui/icons-material/Dashboard";
import PersonIcon from "@mui/icons-material/Person";

export const MenuLinkList: MenuItem[] = [
	{
		name: "Dashboard",
		icon: <DashboardIcon />,
		route: RouteConstanstPage.dashboard
	},
	{
		name: "Users",
		icon: <PersonIcon />,
		route: RouteConstanstPage.users
	}
];