import { RootState } from "../../store";

class StorageAppService {
	private static STATEAPP = "STATEAPP";

	static GetState = () => {
		const state =  localStorage.getItem(this.STATEAPP);
		return JSON.parse(state ?? "{}");
	};

	static SaveState = (state: RootState): void => {
		localStorage.setItem(this.STATEAPP, JSON.stringify(state));
	};
}

export default StorageAppService;