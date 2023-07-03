import { createAsyncThunk } from "@reduxjs/toolkit";
import { RouteHttps } from "../../../core/constants/route.https.constants";
import { ILoginApplicationRequest } from "../../../core/dtos/userapplication/loginapplicationrequest";
import { ILoginApplicationResponse } from "../../../core/dtos/userapplication/loginapplicationresponse";
import HttpClientApplication from "../../../core/services/api.service";

export const login = createAsyncThunk<ILoginApplicationResponse, ILoginApplicationRequest>("auth/login", async (login: ILoginApplicationRequest) => {
	const api = new HttpClientApplication();
	const response = await api.Post<ILoginApplicationResponse>(RouteHttps.userApplication.login,login);
	return response;
});

export const logout = createAsyncThunk("auth/logged", async () => {
	return false;
});