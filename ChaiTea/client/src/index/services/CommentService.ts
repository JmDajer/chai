import { AxiosError } from "axios";
import { IComment } from "../domain/IComment";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseServices/BaseEntityService";
import { IdentityService } from "./IdentityService";

export class CommentService extends BaseEntityService<IComment> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void), reviewId: string){
        super(`v1/Reviews/${reviewId}/Comments`, setJwtResponse);
    }

    async getReviewComments(reviewId: string): Promise<IComment[] | undefined> {
        const response = await this.axios.get<IComment[]>('');

        console.log('respone', response);
        if (response.status === 200) return response.data;

        return undefined;
    }

    async postComment(jwtData: IJWTResponse, data: IComment): Promise<IComment | undefined> {
        try {
            const response = await this.axios.post<IComment>('', data, {
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
                        const response = await this.axios.post<IComment>('', data, {
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

    async updateComment(jwtData: IJWTResponse, data: IComment): Promise<IComment | undefined> {
        try {
            const response = await this.axios.put<IComment>(data.id!, data, {
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
                        const response = await this.axios.put<IComment>(data.id!, data, {
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

    
    async deleteComment(jwtData: IJWTResponse, commentId: string, reviewId: string): Promise<IComment | undefined> {
        try {
            const response = await this.axios.delete<IComment>(commentId, {
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
                        const response = await this.axios.delete<IComment>(commentId, {
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