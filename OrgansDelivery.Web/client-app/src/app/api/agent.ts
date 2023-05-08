import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { User, Login, Register } from "../models/user";
import { router } from "../router/Routes";
import { store } from "../stores/store";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    });
};

axios.defaults.baseURL = "https://localhost:4000/api";

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.request.use((config) => {
    const token = store.commonStore.token;
    if (token && config.headers)
        config.headers.Authorization = `Bearer ${token}`;
    return config;
});

axios.interceptors.response.use(
    async (response) => {
        if (process.env.NODE_ENV === "development") await sleep(1000);
        return response;
    },
    (error: AxiosError) => {
        const { data, status, config } = error.response as AxiosResponse;
        switch (status) {
            case 400:
                if (
                    config.method === "get" &&
                    data.errors.hasOwnProperty("id")
                ) {
                    router.navigate("/not-found");
                }
                if (data.errors) {
                    const modalStateErrors = [];
                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            modalStateErrors.push(data.errors[key]);
                        }
                    }
                    throw modalStateErrors.flat();
                } else {
                    toast.error(data);
                }
                break;
            case 401:
                toast.error("unauthorised");
                break;
            case 403:
                toast.error("forbidden");
                break;
            case 404:
                router.navigate("/not-found");
                break;
            case 500:
                // store.commonStore.setServerError(data);
                router.navigate("/server-error");
                break;
        }
        return Promise.reject(error);
    }
);

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const UserActions = {
    login: (login: Login) => requests.post<User>("/auth/login", login),
    register: (register: Register) => requests.post<User>("/auth/register", register),
    confirmEmail: (userId: string, token: string) => 
        requests.post<void>(`/auth/confirmEmail?userId=${userId}&token=${token}`, {}),
    current: () => requests.get<User>("/user"),
};

const agent = {
    UserActions,
};

export default agent;
