import { createAsyncThunk } from "@reduxjs/toolkit";
import { RouteHttps } from "../../../core/constants/route.https.constants";
import { ILoginApplicationRequest } from "../../../core/dtos/userapplication/loginapplicationrequest";
import { ILoginApplicationResponse } from "../../../core/dtos/userapplication/loginapplicationresponse";
import HttpClientApplication, { IHeaderModel } from "../../../core/services/api.service";
import { RouteApplication } from "../../../core/route/routesapplication";

export const login = createAsyncThunk<ILoginApplicationResponse, ILoginApplicationRequest>(
	RouteApplication.loginRoutes.token, async (login: ILoginApplicationRequest) => {
		const headers:IHeaderModel[] = [
			{name: "Content-Type", value: "application/x-www-form-urlencoded"},
			{name: "Accept", value: "*/*"}
		];

		let formData = new URLSearchParams();
		formData.append("grant_type",login.grant_type!);
		formData.append("username", login.username);
		formData.append("password",login.password);
		formData.append("scope", login.scope!);
		formData.append("client_id",login.client_id!);
		formData.append("client_secret", login.client_secret!);

		const api = new HttpClientApplication(headers);
		const response = await api.Login<ILoginApplicationResponse>(RouteHttps.userApplication.login, formData);
		return response;
	});

export const logout = createAsyncThunk("auth/logged", async () => {
	return false;
});