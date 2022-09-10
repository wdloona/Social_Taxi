import { BaseModel, LoginType, UserModel } from "../../Models";
import { RequestService } from "../Request";


const LoginService = {
  login: (login: LoginType): Promise<BaseModel<number>> => RequestService.post('Authorization', login),
  getToken: (): Promise<BaseModel<string>> => RequestService.get(`Authorization`),
  logout: (): Promise<void> => RequestService.get(`Authorization/Logout`),
  getUser: (): Promise<BaseModel<UserModel>> => RequestService.get(`Authorization/user`)
};

export { LoginService };