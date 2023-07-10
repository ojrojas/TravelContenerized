import React from "react";
import FormLoginComponent from "./components/formlogin.component";

const LoginPage: React.FC = () => {
	console.log("Environment: ", process.env);
	return (
		<FormLoginComponent />
	);
};

export default LoginPage;
