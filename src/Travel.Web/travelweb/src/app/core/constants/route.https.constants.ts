const routeApi = process.env.REACT_APP_ROUTE_API_IDENTITY;

export const RouteHttps = {
	userApplication: {
		login: `${routeApi}/connect/token`,
		createserapplication: `${routeApi}/createuserapplication`,
		DeleteUserApplication: `${routeApi}/deleteuserapplication`,
	},
	users: {
		getallusers: `${routeApi}/getallusers`,
		createuser: `${routeApi}/createuser`,
		deleteuser: `${routeApi}/deleteuser`,
		getUserbyid: `${routeApi}/getuserbyid`,
		updateuser: `${routeApi}/updateuser`,
	}
};