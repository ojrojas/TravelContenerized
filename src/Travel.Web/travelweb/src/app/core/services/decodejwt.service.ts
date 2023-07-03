import jwt_decode from "jwt-decode";
import _ from "lodash";
import { IUser } from "../models/user/user";
export class DecodeJwt {
	static decodeJwt = (token:string): IUser | undefined => {
		try {
			if(token === null || token === undefined)
				return undefined;
			const user = jwt_decode<any>(token);
			return {
				id: user.Id,
				userName: user.UserName,
				email: user.Email,
				normalizedUserName: user.NormalizedUserName
			};
		} catch (error) {
			console.log("error decode token ==>", error);
		}
	};
}