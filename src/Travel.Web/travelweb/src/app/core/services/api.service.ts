import StorageAppService from "./storage.service";
export interface IHeaderModel {
	name:string,
	value:string
}

class HttpClientApplication {
	private headers: Headers;

	constructor(headersList:IHeaderModel[] | undefined) {
		this.headers = new Headers();
		const state = StorageAppService.GetState();
		if (state.login?.loginApplicationResponse?.access_token !== undefined)
			this.headers.append("authorization", `Bearer ${state.login?.loginApplicationResponse?.access_token}`);
		if(headersList ===undefined) {
			this.headers.append("mode", "cors");
			this.headers.append("Accept", "application/json");
			this.headers.append("Content-Type", "application/json");
			this.headers.append("Access-Control-Allow-Origin", "*");
		}
		else{
			headersList.forEach(element => {
				this.headers.append(element.name, element.value);
			});
		}
	}

	public Get = async <T>(url: string): Promise<T> => {
		try {
			const response = await fetch(url, { headers: this.headers });
			return response.json() as T;
		} catch (error) {
			throw Error(`Error: ${error}`);
		}
	};

	public Login = async <T>(url: string, bodyData: any): Promise<T> => {
		try {
			const response = await fetch(url, { method: "POST", body: bodyData, headers: this.headers });
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