import Axios, { AxiosInstance } from 'axios';

export abstract class BaseService {
    private static hostBaseURL = "http://localhost:5130/api/";

    protected axios: AxiosInstance;

    constructor(baseURL: string) {
        this.axios = Axios.create(
            {
                baseURL: BaseService.hostBaseURL + baseURL,
                headers: {
                common: {
                    'Content-Type': 'application/json'
                    }
                }
            }
        )
    }
}