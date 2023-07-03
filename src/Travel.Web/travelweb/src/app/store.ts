import { configureStore, ThunkAction, Action } from "@reduxjs/toolkit";
import LoginReducer from "../app/pages/login/redux/login.slice";
import UserReducer from "../app/pages/users/redux/usersslice";
import SnackBarReducer from "../app/components/snackbar/redux/snackbarslice";
import { throttle } from "lodash";
import StorageAppService from "./core/services/storage.service";

const presistedState = StorageAppService.GetState();

export const store = configureStore({
	reducer: {
		login: LoginReducer,
		user: UserReducer,
		snack: SnackBarReducer
	}, preloadedState: presistedState,
	middleware: (getDefaultMiddleware) => getDefaultMiddleware({serializableCheck: false}),
});


store.subscribe(
	throttle(() => {

		if (process.env.NODE_ENV === "development") {
			console.info("state", store.getState());
			console.info("actions",store.dispatch);
		}
		StorageAppService.SaveState({
			...store.getState(),
		});
	}, 1000),
);

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
	ReturnType,
	RootState,
	unknown,
	Action<string>
>;
