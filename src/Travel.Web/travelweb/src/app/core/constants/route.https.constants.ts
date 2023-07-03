const routeApi = process.env.REACT_APP_ROUTE_API_IDENTITY;

export const RouteHttps = {
	userApplication: {
		login: `${routeApi}login`,
		createserapplication: `${routeApi}createuserapplication`,
		DeleteUserApplication: `${routeApi}deleteuserapplication`,
	},
	users: {
		getallusers: `${routeApi}getalluser`,
		createuser: `${routeApi}createuser`,
		deleteuser: `${routeApi}deleteuser/`,
		getUserbyid: `${routeApi}getUserbyid/`,
		updateuser: `${routeApi}updateuser`,
	},
	typeusers: {
		createtypeuser: `${routeApi}createtypeuser`,
		deletetypeuser: `${routeApi}deletetypeuser/`,
		getalltypeuser: `${routeApi}getalltypeuser`,
		gettypeuserbyid: `${routeApi}gettypeuserbyid/`,
		updatetypeuser: `${routeApi}updatetypeuser`,
	},
	typeidentifications: {
		createtypeidentification: `${routeApi}createtypeidentification`,
		deletetypeidentification: `${routeApi}deletetypeidentification/`,
		getalltypeidentification: `${routeApi}getalltypeidentification`,
		gettypeidentificationbyid: `${routeApi}gettypeidentificationbyid/`,
		updatetypeidentification: `${routeApi}updatetypeidentification`,
	}
};