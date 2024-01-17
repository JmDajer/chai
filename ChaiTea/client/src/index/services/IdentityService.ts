import { IJWTResponse } from "../dto/IJWTResponse";
import { ILoginData } from "../dto/ILoginData";
import { IRegisterData } from "../dto/IRegisterData";
import { BaseService } from "./BaseServices/BaseService";

export class IdentityService extends BaseService {

    constructor() {
        super('v1/identity/account/');
    }

    async register(data: IRegisterData): Promise<IJWTResponse | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>('register', data);

            console.log('register response', response);
            if (response.status === 200) return response.data;

            return undefined;

        } catch (error) {
            console.log('error: ', (error as Error).message);
            return undefined;
        }
    }

    async login(data: ILoginData): Promise<IJWTResponse | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>('login', data);

            console.log('login response', response);
            if (response.status === 200) return response.data;

            return undefined;

        } catch (error) {
            console.log('error: ', (error as Error).message);
            return undefined;
        }
    }

    async logout(data: IJWTResponse): Promise<true | undefined> {
        console.log(data);

        try {
            const response = await this.axios.post(
                'logout',
                data,
                {
                    headers: {
                        'Authorization': 'Bearer ' + data.jwt
                    }
                }
            );

            console.log('logout response', response);
            if (response.status === 200) {
                localStorage.clear();
                return true;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async refreshToken(data: IJWTResponse): Promise<IJWTResponse | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>(
                'refreshtoken',
                data
            );

            console.log('refresh token response', response);
            if (response.status === 200) {
                this.addJwtTokenToLocalStorage(response.data);
                return response.data;
            }
            return undefined;

        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async addJwtTokenToLocalStorage(data: IJWTResponse): Promise<void> {
        localStorage.setItem("jwt", data.jwt);
        localStorage.setItem("refreshToken", data.refreshToken);
    }
}