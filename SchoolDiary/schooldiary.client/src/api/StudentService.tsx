import axios, { AxiosResponse } from "axios";
import { Response } from './Api.module'
import ModelBase from "../components/common/model/ModelBase";
import { http } from "./http/AxiosConfig";

export class StudentService {
    private _serverUrl: string;

    constructor(serverUrl: string) {
        this._serverUrl = serverUrl;
    }

    async getStudents(filter: string = '', orderBy: string = '', page: number = 0, pageSize: number = 10): Promise<AxiosResponse<Response<ModelBase[]>>> {
        try {
            return http.get(`${this._serverUrl}Student/GetStudents?filter=${filter}&orderBy=${orderBy}&page=${page}&pageSize=${pageSize}`);
        }
        catch (error) {
            console.error('Error during get students list: ', error);
            throw error;
        }
    }

    async removeStudent(id: string): Promise<AxiosResponse<Response<ModelBase>>> {
        try {
            return http.delete(`${this._serverUrl}Student/DeleteStudent`, {
                data: id
            });
        }
        catch (error) {
            console.log('Error during remove student: ', error);
            throw error;
        }
    }
} 