import { IUser } from "../../models/user/user";

export interface IGetAllUserResponse {
    applicationsUsers: IUser[];
}