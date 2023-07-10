import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { ILoginApplicationRequest } from "../../../core/dtos/userapplication/loginapplicationrequest";
import { ILoginApplicationResponse } from "../../../core/dtos/userapplication/loginapplicationresponse";
import { IUser } from "../../../core/models/user/user";
import { DecodeJwt } from "../../../core/services/decodejwt.service";
import { login, logout } from "./login.actions";

interface State {
	loginApplicationRequest: ILoginApplicationRequest | undefined;
	loginApplicationResponse: ILoginApplicationResponse | undefined;
	user?: IUser;
	loading: boolean;
	error: any;
	logged: boolean;
}

const loginStateInitial: State = {
	loginApplicationRequest: undefined,
	loginApplicationResponse: undefined,
	loading: false,
	error: undefined,
	logged: false,
};

export const authSlice = createSlice({
	name: "auth",
	initialState: loginStateInitial,
	reducers: {
		updateLogged: (state, action: PayloadAction<boolean>) => {
			state.logged = action.payload;
		},
	},
	extraReducers: builder => {
		builder.addCase(login.pending, (state, action) => {
			state.loading = true;
			state.loginApplicationRequest = action.meta.arg;
		});
		builder.addCase(login.fulfilled, (state, action) => {
			state.loginApplicationResponse = action.payload;
			console.log("Response login -- full: -- " ,action.payload);
			if (state.loginApplicationResponse.access_token !== undefined)
				state.user = DecodeJwt.decodeJwt(state.loginApplicationResponse.access_token);
			state.loginApplicationRequest = undefined;
			state.loading = false;
		});
		builder.addCase(login.rejected, (state) => {
			console.log("Response login: -- reject");
			state.loading = false;
			state.logged = false;
			state.loginApplicationRequest = undefined;
			state.loginApplicationResponse = undefined;
		});
		builder.addCase(logout.pending, (state) => {
			state.loading = true;
		});
		builder.addCase(logout.fulfilled, (state) => {
			state.loading = false;
			state.logged = false;
			state.loginApplicationResponse = undefined;
			state.loginApplicationRequest = undefined;
		});
		builder.addCase(logout.rejected, (state) => {
			state.logged = false;
			state.loginApplicationResponse = undefined;
			state.loginApplicationRequest = undefined;
			state.loading = false;
		});
	}
});

export const { updateLogged } = authSlice.actions;


export default authSlice.reducer;