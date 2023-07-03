import { createAsyncThunk } from "@reduxjs/toolkit";
import { RouteHttps } from "../../../core/constants/route.https.constants";
import { ICreateUserApplicationRequest } from "../../../core/dtos/userapplication/createuserapplicationrequest";
import { ICreateUserApplicationResponse } from "../../../core/dtos/userapplication/createuserapplicationresponse";
import HttpClientApplication from "../../../core/services/api.service";

export const createUserApplication = createAsyncThunk(
	"userapplication/createuserapplication", async (request: ICreateUserApplicationRequest)=> {
		const api = new HttpClientApplication();
		const response = await api.Post<ICreateUserApplicationResponse>(
			RouteHttps.userApplication.createserapplication,
			request
		);
		return response;
	});