import { IUser } from "../user/user";

export interface IUserApplication {
    userName:string;
	email:string;
	user: IUser;
	id?: string;
}