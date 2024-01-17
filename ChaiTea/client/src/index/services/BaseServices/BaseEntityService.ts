import { AxiosError } from "axios";
import { IBaseEntity } from "../../domain/IBaseEntity";
import { IJWTResponse } from "../../dto/IJWTResponse";
import { BaseService } from "./BaseService";
import { IdentityService } from "../IdentityService";

export abstract class BaseEntityService<TEntity extends IBaseEntity> extends BaseService {
    constructor(baseUrl: string,
        protected setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super(baseUrl);
    }

    async getAll(jwtData: IJWTResponse): Promise<TEntity[] | undefined> {
        try {
            const response = await this.axios.get<TEntity[]>('', {
                headers: {
                    'Authorization': 'Bearer ' + jwtData.jwt
                }
            });

            console.log('respone', response);
            if (response.status === 200) return response.data;

            return undefined;

        } catch (error) {
            console.log('error: ', (error as Error).message, error);
            if ((error as AxiosError).response?.status === 401) {
                console.error("JWT expired, refreshing!");

                let identityService = new IdentityService();
                let refreshedJwt = await identityService.refreshToken(jwtData);
                if (refreshedJwt) {
                    this.setJwtResponse(refreshedJwt);
                    localStorage.setItem("jwt", refreshedJwt.jwt);
                    localStorage.setItem("refreshToken", refreshedJwt.refreshToken);

                    const response = await this.axios.get<TEntity[]>('', {
                        headers: {
                            'Authorization': 'Bearer' + refreshedJwt.jwt
                        }
                    });

                    if (response.status === 200) return response.data;
                }
            }
            return undefined;
        }
    }

    async getById(jwtData: IJWTResponse, id: string): Promise<TEntity | undefined> {
        try {
            const response = await this.axios.get<TEntity>(id, {
                headers: {
                    'Authorization': 'Bearer ' + jwtData.jwt
                }
            });

            console.log('respone', response);
            if (response.status === 200) return response.data;

            return undefined;

        } catch (error) {
            console.log('error: ', (error as Error).message, error);
            if ((error as AxiosError).response?.status === 401) {
                console.error("JWT expired, refreshing!");

                let identityService = new IdentityService();
                let refreshedJwt = await identityService.refreshToken(jwtData);
                if (refreshedJwt) {
                    this.setJwtResponse(refreshedJwt);
                    localStorage.setItem("jwt", refreshedJwt.jwt);
                    localStorage.setItem("refreshToken", refreshedJwt.refreshToken);

                    const response = await this.axios.get<TEntity>(id, {
                        headers: {
                            'Authorization': 'Bearer' + refreshedJwt.jwt
                        }
                    });

                    if (response.status === 200) return response.data;
                }
            }
            return undefined;
        }
    }

    async post(jwtData: IJWTResponse): Promise<TEntity | undefined> {
        try {
            
            const response = await this.axios.post<TEntity>('', {
                headers: {
                    'Authorization': 'Bearer ' + jwtData.jwt
                }
            });

            console.log('respone', response);
            if (response.status === 200) return response.data;

            return undefined;

        } catch (error) {
            console.log('error: ', (error as Error).message, error);
            if ((error as AxiosError).response?.status === 401) {
                console.error("JWT expired, refreshing!");

                let identityService = new IdentityService();
                let refreshedJwt = await identityService.refreshToken(jwtData);
                if (refreshedJwt) {
                    this.setJwtResponse(refreshedJwt);
                    localStorage.setItem("jwt", refreshedJwt.jwt);
                    localStorage.setItem("refreshToken", refreshedJwt.refreshToken);

                    const response = await this.axios.get<TEntity>('', {
                        headers: {
                            'Authorization': 'Bearer' + refreshedJwt.jwt
                        }
                    });

                    if (response.status === 200) return response.data;
                }
            }
            return undefined;
        }
    }

    async put(jwtData: IJWTResponse, data: TEntity): Promise<TEntity | undefined> {
        try {
            
            const response = await this.axios.put<TEntity>('', {
                headers: {
                    'Authorization': 'Bearer ' + jwtData.jwt
                }
            });

            console.log('respone', response);
            if (response.status === 200) return response.data;

            return undefined;

        } catch (error) {
            console.log('error: ', (error as Error).message, error);
            if ((error as AxiosError).response?.status === 401) {
                console.error("JWT expired, refreshing!");

                let identityService = new IdentityService();
                let refreshedJwt = await identityService.refreshToken(jwtData);
                if (refreshedJwt) {
                    this.setJwtResponse(refreshedJwt);
                    localStorage.setItem("jwt", refreshedJwt.jwt);
                    localStorage.setItem("refreshToken", refreshedJwt.refreshToken);

                    const response = await this.axios.put<TEntity>('', {
                        headers: {
                            'Authorization': 'Bearer' + refreshedJwt.jwt
                        }
                    });

                    if (response.status === 200) return response.data;
                }
            }
            return undefined;
        }
    }

    async delete(jwtData: IJWTResponse, id: string): Promise<TEntity | undefined> {
        try {
            const response = await this.axios.delete<TEntity>(id, {
                headers: {
                    'Authorization': 'Bearer ' + jwtData.jwt
                }
            });

            console.log('respone', response);
            if (response.status === 200) return response.data;

            return undefined;

        } catch (error) {
            console.log('error: ', (error as Error).message, error);
            if ((error as AxiosError).response?.status === 401) {
                console.error("JWT expired, refreshing!");

                let identityService = new IdentityService();
                let refreshedJwt = await identityService.refreshToken(jwtData);
                if (refreshedJwt) {
                    this.setJwtResponse(refreshedJwt);
                    localStorage.setItem("jwt", refreshedJwt.jwt);
                    localStorage.setItem("refreshToken", refreshedJwt.refreshToken);

                    const response = await this.axios.get<TEntity>(id, {
                        headers: {
                            'Authorization': 'Bearer' + refreshedJwt.jwt
                        }
                    });

                    if (response.status === 200) return response.data;
                }
            }
            return undefined;
        }
    }
}