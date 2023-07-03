import StorageAppService from "./storage.service";

class HttpClientApplication {
	private headers: Headers;

	constructor() {
		this.headers = new Headers();
		const state = StorageAppService.GetState();
		if (state.login?.loginApplicationResponse?.token !== undefined)
			this.headers.append("authorization", `Bearer ${state.login?.loginApplicationResponse?.token}`);
		this.headers.append("mode", "cors");
		this.headers.append("Accept", "application/json");
		this.headers.append("Content-Type", "application/json");
		this.headers.append("Access-Control-Allow-Origin", "*");
	}

	public Get = async <T>(url: string): Promise<T> => {
		try {
			const response = await fetch(url, { headers: this.headers });
			return response.json() as T;
		} catch (error) {
			throw Error(`Error: ${error}`);
		}
	};

	public Post = async <T>(url: string, bodyData: any): Promise<T> => {
		try {
			const response = await fetch(url, { method: "POST", body: JSON.stringify(bodyData), headers: this.headers });
			return response.json() as T;
		} catch (error) {
			throw Error(`Error: ${error}`);
		}
	};

	public Put = async <T>(url: string, bodyData: any): Promise<T> => {
		try {
			const response = await fetch(url, { method: "PUT", body: JSON.stringify(bodyData), headers: this.headers });
			return response.json() as T;
		} catch (error) {
			throw Error(`Error: ${error}`);
		}
	};

	public Patch = async <T>(url: string, bodyData: any): Promise<T> => {
		try {
			const response = await fetch(url, { method: "PATCH", body: JSON.stringify(bodyData), headers: this.headers });
			return response.json() as T;
		} catch (error) {
			throw Error(`Error: ${error}`);
		}
	};

	public Delete = async <T>(url: string): Promise<T> => {
		try {
			const response = await fetch(url, { method: "DELETE", headers: this.headers });
			return response.json() as T;
		} catch (error) {
			throw Error(`Error: ${error}`);
		}
	};
}

export default HttpClientApplication;