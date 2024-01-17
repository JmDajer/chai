import { AxiosError } from "axios";
import { IReview } from "../domain/IReview";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseServices/BaseEntityService";
import { IdentityService } from "./IdentityService";

export class ReviewService extends BaseEntityService<IReview> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void), beverageId: string) {
        super(`v1/Beverages/${beverageId}/Reviews`, setJwtResponse);
    }

    async getBeverageReview(beverageId: string): Promise<IReview[] | undefined> {
        const response = await this.axios.get<IReview[]>("");

        console.log('respone', response);
        if (response.status === 200) return response.data;

        return undefined;
    }

    async postReview(jwtData: IJWTResponse, data: IReview): Promise<IReview | undefined> {
        try {
            const response = await this.axios.post<IReview>('', data, {
                headers: {
                    'Authorization': 'Bearer ' + jwtData.jwt
                }
            });

            console.log('response', response);
            if (response.status === 201) return response.data;

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

                    try {
                        const response = await this.axios.post<IReview>('', data, {
                            headers: {
                                'Authorization': 'Bearer ' + refreshedJwt.jwt
                            }
                        });

                        if (response.status === 201) return response.data;
                    } catch (error) {
                        console.log('error: ', (error as Error).message, error);
                    }
                }
            }
            return undefined;
        }
    }

    async updateReview(jwtData: IJWTResponse, data: IReview): Promise<IReview | undefined> {
        try {
            const response = await this.axios.put<IReview>(data.id!, data, {
                headers: {
                    'Authorization': 'Bearer ' + jwtData.jwt
                }
            });

            console.log('response', response);
            if (response.status === 201) return response.data;

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

                    try {
                        const response = await this.axios.put<IReview>(data.id!, data, {
                            headers: {
                                'Authorization': 'Bearer ' + refreshedJwt.jwt
                            }
                        });

                        if (response.status === 201) return response.data;
                    } catch (error) {
                        console.log('error: ', (error as Error).message, error);
                    }
                }
            }
            return undefined;
        }
    }

    async deleteReview(jwtData: IJWTResponse, reviewId: string): Promise<IReview | undefined> {
        try {
            const response = await this.axios.delete<IReview>(reviewId, {
                headers: {
                    'Authorization': 'Bearer ' + jwtData.jwt
                }
            });

            console.log('response', response);
            if (response.status === 201) return response.data;

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

                    try {
                        const response = await this.axios.delete<IReview>(reviewId, {
                            headers: {
                                'Authorization': 'Bearer ' + refreshedJwt.jwt
                            }
                        });

                        if (response.status === 201) return response.data;
                    } catch (error) {
                        console.log('error: ', (error as Error).message, error);
                    }
                }
            }
            return undefined;
        }
    }
}