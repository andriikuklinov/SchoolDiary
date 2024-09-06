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

    async register(email: string, password: string, confirmPassword: string): Promise<AxiosResponse<Response<string>>> {
        try {
            return axios.post(`${this._serverUrl}Auth/Register`, JSON.stringify({ email, password, confirmPassword }), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
        }
        catch (error) {
            console.log('Error during registration: ', error);
            throw error;
        }
    }

    async forgotPassword(email: string): Promise<AxiosResponse<Response<string>>> {
        try {
            return axios.post(`${this._serverUrl}Auth/ForgotPassword`, JSON.stringify({ email }), {
                headers: {
                    'Content-Type': 'application/json'
                }
            });
        }
        catch (error) {
            console.log('Error during reset password', error);
            throw error;
        }
    }

    async resetPassword(token: string | null, email: string | null, password: string, confirmPassword: string): Promise<AxiosResponse<Response<string>>> {
        try {
            return axios.post(`${this._serverUrl}Auth/ResetPassword`, JSON.stringify({ token, email, password, confirmPassword }), {
                headers: {
                    'Content-Type': 'application/json'
                }
            });
        }
        catch (error) {
            console.log('Error during reset password.');
            throw error;
        }
    }
}