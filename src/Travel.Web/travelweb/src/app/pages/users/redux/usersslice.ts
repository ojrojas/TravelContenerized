import { createSlice } from "@reduxjs/toolkit";
import { ICreateUserRequest } from "../../../core/dtos/user/createuserrequest";
import { ICreateUserResponse } from "../../../core/dtos/user/createuserresponse";
import { IDeleteUserRequest } from "../../../core/dtos/user/deleteuserequest";
import { IDeleteUserResponse } from "../../../core/dtos/user/deleteuserresponse";
import { IGetAllUserResponse } from "../../../core/dtos/user/getalluserresponse";
import { IUpdateUserRequest } from "../../../core/dtos/user/updateuserrequest";
import { IUpdateUserResponse } from "../../../core/dtos/user/updateuserresponse";
import { ICreateUserApplicationResponse } from "../../../core/dtos/userapplication/createuserapplicationresponse";

import { createUserApplication } from "./userapplication-actions";
import { createUser, deleteUser, getAllUsers, updateUser } from "./users-actions";

export interface IUserState {
    loading: boolean;
    getAllUserResponse?: IGetAllUserResponse ;
    createUserRequest?: ICreateUserRequest ;
    createUserResponse?: ICreateUserResponse ;
    updateUserRequest?: IUpdateUserRequest ;
    updateUserResponse?: IUpdateUserResponse ;
    deleteUserRequest?: IDeleteUserRequest ;
    deleteUserResponse?: IDeleteUserResponse ;
    createUserApplicationResponse?: ICreateUserApplicationResponse
    error: any;
}

const InitialUserState: IUserState = {
	loading: false,
	error: undefined
};

export const userSlice = createSlice({
	name: "user",
	initialState: InitialUserState,
	reducers: {},
	extraReducers: builder => {
		builder.addCase(getAllUsers.pending, (state) => {
			state.loading = true;
		});
		builder.addCase(getAllUsers.fulfilled, (state, action) => {
			state.loading = false;
			state.getAllUserResponse = action.payload;
		});
		builder.addCase(getAllUsers.rejected, (state, action) => {
			state.loading = false;
			state.error = action.error;
		});
		builder.addCase(createUser.pending, (state, action) => {
			state.loading= true;
			state.createUserResponse = undefined;
			state.createUserRequest = action.payload;
		});
		builder.addCase(createUser.fulfilled, (state, action) => {
			state.createUserResponse = action.payload;
			state.createUserRequest = undefined;
			state.loading = false;
		});
		builder.addCase(createUser.rejected, (state, action) => {
			state.loading = false;
			state.error = action.error;
			state.createUserRequest = undefined;
			state.createUserResponse = undefined;
		});
		builder.addCase(updateUser.pending, (state, action) => {
			state.loading= true;
			state.updateUserRequest = action.payload;
			state.updateUserResponse = undefined;
		});
		builder.addCase(updateUser.fulfilled, (state, action) => {
			state.updateUserResponse = action.payload;
			state.updateUserRequest = undefined;
			state.loading = false;
		});
		builder.addCase(updateUser.rejected, (state, action) => {
			state.loading = false;
			state.updateUserRequest = undefined;
			state.updateUserResponse = undefined;
			state.error = action.error;
		});
		builder.addCase(deleteUser.pending, (state, action) => {
			state.loading= true;
			state.deleteUserRequest = action.payload;
			state.deleteUserResponse = undefined;
		});
		builder.addCase(deleteUser.fulfilled, (state, action) => {
			state.deleteUserResponse = action.payload;
			state.deleteUserRequest = undefined;
			state.loading = false;
		});
		builder.addCase(deleteUser.rejected, (state, action) => {
			state.loading = false;
			state.deleteUserRequest = undefined;
			state.deleteUserResponse = undefined;
			state.error = action.error;
		});
		builder.addCase(createUserApplication.pending, (state, action) => {
			state.loading = true;
		});
		builder.addCase(createUserApplication.fulfilled, (state, action) => {
			state.loading = false;
			state.createUserApplicationResponse = action.payload;
		});
		builder.addCase(createUserApplication.rejected, (state, action)=>{
			state.loading = false;
			state.error = action.error;
			state.createUserApplicationResponse = undefined;
		});
	}
});

export default userSlice.reducer;