import axios from "axios";
import { serverUrl } from "../Api.module";
import { createBrowserHistory } from "history";

const history = createBrowserHistory();

const instance = axios.create({
    baseURL: `${serverUrl}`
});

instance.interceptors.request.use((config) => {
    let token = localStorage.getItem("jwt");
    config.headers.Accept = "application/json";
    config.headers["Content-Type"] = "application/json";
    config.headers.Authorization = `Bearer ${token}`;

    return config;
}, (error) => Promise.reject(error));

instance.interceptors.response.use((response) => {
    debugger;
    if (response.status == 200) {
        return response;
    }
    return Promise.reject(response.data);
}, (error) => {
    if (error.response && error.response.status == 401) {
        window.location.replace("/login")
    }
    else {
        Promise.reject(error);
    }
});

export const http = instance;