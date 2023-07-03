import { createAsyncThunk } from "@reduxjs/toolkit";
import { RouteHttps } from "../../../core/constants/route.https.constants";
import { ICreateUserRequest } from "../../../core/dtos/user/createuserrequest";
import { ICreateUserResponse } from "../../../core/dtos/user/createuserresponse";
import { IDeleteUserRequest } from "../../../core/dtos/user/deleteuserequest";
import { IDeleteUserResponse } from "../../../core/dtos/user/deleteuserresponse";
import { IGetAllUserResponse } from "../../../core/dtos/user/getalluserresponse";
import { IGetUserByIdRequest } from "../../../core/dtos/user/getuserbyidrequest";
import { IGetUserByIdResponse } from "../../../core/dtos/user/getuserbyidresponse";
import { IUpdateUserRequest } from "../../../core/dtos/user/updateuserrequest";
import { IUpdateUserResponse } from "../../../core/dtos/user/updateuserresponse";
import HttpClientApplication from "../../../core/services/api.service";

export const getAllUsers = createAsyncThunk<IGetAllUserResponse>("users/getallusers", async () => {
	const api = new HttpClientApplication();
	const response = await api.Get<IGetAllUserResponse>(RouteHttps.users.getallusers);
	return response;
});

export const createUser = createAsyncThunk<ICreateUserResponse, ICreateUserRequest>("users/createuser", async (createUserRequest: ICreateUserRequest) => {
	const api = new HttpClientApplication();
	const response = await api.Post<ICreateUserResponse>(RouteHttps.users.createuser, createUserRequest);
	return response;
});

export const updateUser = createAsyncThunk<IUpdateUserResponse, IUpdateUserRequest>("users/updateuser", async (updateUserRequest: IUpdateUserRequest) => {
	const api = new HttpClientApplication();
	const response = await api.Patch<IUpdateUserResponse>(RouteHttps.users.updateuser, updateUserRequest);
	return response;
});

export const deleteUser = createAsyncThunk<IDeleteUserResponse, IDeleteUserRequest>("users/delete", async (deleteUserRequest: IDeleteUserRequest) => {
	const api = new HttpClientApplication();
	const response = await api.Delete<IDeleteUserResponse>(RouteHttps.users.deleteuser + deleteUserRequest.id);
	return response;
});

export const getUserById = createAsyncThunk<IGetUserByIdResponse, IGetUserByIdRequest>("users/getuserbyid", async (getUserByIdRequest: IGetUserByIdRequest) => {
	const api = new HttpClientApplication();
	const response = await api.Get<IGetUserByIdResponse>(RouteHttps.users.getUserbyid + getUserByIdRequest.id);
	return response;
});