import axios, { AxiosResponse } from "axios";
import { Response } from './Api.module'
export class AuthService {
    private _serverUrl: string;

    constructor(serverUrl: string) {
        this._serverUrl = serverUrl;
    }

    async login(email: string, password: string): Promise<AxiosResponse<Response<string>>> {
        try {
            return axios.post(`${this._serverUrl}Auth/Login`, JSON.stringify({ email, password }), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
        }
        catch (error) {
            console.error('Error during login: ', error);
            throw error;
        }
    }
}