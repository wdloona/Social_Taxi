import { LoginResponse, LoginType, UserModel } from "../../Models";
import { RequestService } from "../Request";


const LoginService = {
  login: (login: LoginType): Promise<LoginResponse> => RequestService.post('Authorization', login),
  getToken: (): Promise<string> => RequestService.get(`Authorization`),
  logout: (): Promise<string> => RequestService.get(`Authorization/Logout`),
  getUser: (): Promise<UserModel> => RequestService.get(`Authorization/user`)
};

export { LoginService };