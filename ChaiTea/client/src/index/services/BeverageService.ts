import jwtDecode from "jwt-decode";
import { IBeverage } from "../domain/IBeverage";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseServices/BaseEntityService";
import { AxiosError } from "axios";
import { IdentityService } from "./IdentityService";


export class BeverageService extends BaseEntityService<IBeverage> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Beverages', setJwtResponse);
    }

    async getAllBeverages(): Promise<IBeverage[] | undefined> {
        const response = await this.axios.get<IBeverage[]>("");

        console.log('respone', response);
        if (response.status === 200) return response.data;

        return undefined;
    }

    async getBeverage(id: string): Promise<IBeverage | undefined> {
        const response = await this.axios.get<IBeverage>(id)

        console.log('respone', response);
        if (response.status === 200) return response.data;

        if (response.status === 401) return response.data;

        return undefined;
    }

    async postBeverage(jwtData: IJWTResponse, data: IBeverage): Promise<IBeverage | undefined> {
        try {
            const response = await this.axios.post<IBeverage>('', data, {
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
                        const response = await this.axios.post<IBeverage>('', data, {
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

    async GetRecommendedBeverages(userData: IJWTResponse): Promise<IBeverage[] | undefined> {
        try {
            // Get userId of the user whos beverages to display
            let decodedJwt: any = jwtDecode(userData.jwt);
            let userId = decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

            // Send data with the jwt to the server
            const response = await this.axios.get<IBeverage[]>(
                "/Users/" + userId + "/Recommended",
                {
                    headers: {
                        'Authorization': 'Bearer ' + userData.jwt
                    }
                }
            );
            console.log('response', response);

            // Send the data if the response was successful
            if (response.status === 200) return response.data;

        } catch (error) {

            console.log('error: ', (error as Error).message, error);
            if ((error as AxiosError).response?.status === 401) {
                console.error("JWT expired, refreshing!");

                let identityService = new IdentityService();
                let refreshedJwt = await identityService.refreshToken(userData);

                if (refreshedJwt) {
                    this.setJwtResponse(refreshedJwt);

                    // Get userId of the user whos beverages to display
                    let decodedJwt: any = jwtDecode(refreshedJwt.jwt);
                    let userId = decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];


                    const response = await this.axios.get<IBeverage[]>(
                        "/Users/" + userId + "/Recommended",
                        {
                            headers: {
                                'Authorization': 'Bearer ' + userData.jwt
                            }
                        }
                    );
                    console.log('response', response);

                    // Send the data if the response was successful
                    if (response.status === 200) return response.data;
                }
            }
        }
    }

    async deleteBeverage(jwtData: IJWTResponse, beverageId: string): Promise<IBeverage | undefined> {
        try {
            const response = await this.axios.delete<IBeverage>(beverageId, {
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
                        const response = await this.axios.delete<IBeverage>(beverageId, {
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

    async GetUserBeverages(userData: IJWTResponse): Promise<IBeverage[] | undefined> {
        try {
            // Get userId of the user whos beverages to display
            let decodedJwt: any = jwtDecode(userData.jwt);
            let userId = decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

            // Send data with the jwt to the server
            const response = await this.axios.get<IBeverage[]>(
                "/Users/" + userId,
                {
                    headers: {
                        'Authorization': 'Bearer ' + userData.jwt
                    }
                }
            );
            console.log('response', response);

            // Send the data if the response was successful
            if (response.status === 200) return response.data;

        } catch (error) {

            console.log('error: ', (error as Error).message, error);
            if ((error as AxiosError).response?.status === 401) {
                console.error("JWT expired, refreshing!");

                let identityService = new IdentityService();
                let refreshedJwt = await identityService.refreshToken(userData);

                if (refreshedJwt) {
                    this.setJwtResponse(refreshedJwt);

                    // Get userId of the user whos beverages to display
                    let decodedJwt: any = jwtDecode(refreshedJwt.jwt);
                    let userId = decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];


                    const response = await this.axios.get<IBeverage[]>(
                        "/Users/" + userId,
                        {
                            headers: {
                                'Authorization': 'Bearer ' + refreshedJwt.jwt
                            }
                        }
                    );
                    console.log('response', response);

                    // Send the data if the response was successful
                    if (response.status === 200) return response.data;
                }
            }
        }
    }
}
