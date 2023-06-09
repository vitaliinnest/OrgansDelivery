import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { User, Login, Register, UpdateUser } from "../models/user";
import { router } from "../router/Routes";
import { store } from "../stores/store";
import { TenantFormValues, Tenant, CreateTenant } from "../models/tenant";
import { OrganFormValues, Organ } from "../models/organ";
import { Container, ContainerFormValues } from "../models/container";
import { Conditions, ConditionsFormValues } from "../models/conditions";
import { InviteFormValues, Invite } from "../models/invite";
import { Employee } from "../models/employee";
import {
    ConditionsRecord,
    ConditionsViolation,
} from "../models/conditionsRecord";
import { Device, DeviceFormValues } from "../models/device";

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
                if (localStorage.getItem("jwt")) {
                    localStorage.removeItem("jwt");
                }
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
    post: <T>(url: string, body: {}) =>
        axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) =>
        axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const UserActions = {
    login: (login: Login) => requests.post<User>("/auth/login", login),
    register: (register: Register) =>
        requests.post<User>("/auth/register", register),
    confirmEmail: (userId: string, token: string) =>
        requests.post<void>(
            `/auth/confirmEmail?userId=${userId}&token=${token}`,
            {}
        ),
    current: () => requests.get<User>("/user"),
    update: (update: UpdateUser) => requests.put<User>("/user", update),
};

const TenantActions = {
    getTenant: () => requests.get<Tenant>("/tenant"),
    createTenant: (tenant: TenantFormValues) =>
        requests.post<CreateTenant>("/tenant", tenant),
    updateTenant: (tenant: TenantFormValues) =>
        requests.put<Tenant>("/tenant", tenant),
};

const InviteActions = {
    getInvites: () => requests.get<Invite[]>("/invite"),
    createInvite: (invite: InviteFormValues) =>
        requests.post<Invite>("/invite", invite),
    deleteInvite: (inviteId: string) => requests.del(`/invite/${inviteId}`),
};

const EmployeeActions = {
    getEmployees: () => requests.get<Employee[]>("/employee"),
    deleteEmployee: (employeeId: string) =>
        requests.del(`/employee/${employeeId}`),
};

const OrganActions = {
    getOrgans: () => requests.get<Organ[]>("/organ"),
    createOrgan: (organ: OrganFormValues) =>
        requests.post<Organ>("/organ", organ),
    updateOrgan: (organId: string, update: OrganFormValues) =>
        requests.put<Organ>(`/organ/${organId}`, update),
    deleteOrgan: (organId: string) => requests.del(`/organ/${organId}`),
};

const ContainerActions = {
    getContainers: () => requests.get<Container[]>("/container"),
    createContainer: (container: ContainerFormValues) =>
        requests.post<Container>("/container", container),
    updateContainer: (containerId: string, container: ContainerFormValues) =>
        requests.put<Container>(`/container/${containerId}`, container),
    deleteContainer: (containerId: string) =>
        requests.del(`/container/${containerId}`),
};

const ConditionsActions = {
    getConditions: () => requests.get<Conditions[]>("/conditions"),
    createConditions: (conditions: ConditionsFormValues) =>
        requests.post<Conditions>("/conditions", conditions),
    updateConditions: (conditionsId: string, conditions: ConditionsFormValues) =>
        requests.put<Conditions>(`/conditions/${conditionsId}`, conditions),
    deleteConditions: (conditionsId: string) =>
        requests.del(`/conditions/${conditionsId}`),
};

const ConditionsRecordActions = {
    getConditionsRecords: (organId: string) =>
        requests.get<ConditionsRecord[]>(`/record/${organId}`),
    getViolations: (organId: string) =>
        requests.get<ConditionsViolation[]>(`/record/${organId}/violations`),
};

const DeviceActions = {
    getDevices: () => requests.get<Device[]>("/device"),
    addDevice: (device: DeviceFormValues) =>
        requests.post<Device>("/device", device),
    updateDevice: (deviceId: string, update: DeviceFormValues) =>
        requests.put<Device>(`/device/${deviceId}`, update),
    deleteDevice: (deviceId: string) =>
        requests.del(`/device/${deviceId}`),
};

const agent = {
    UserActions,
    TenantActions,
    InviteActions,
    EmployeeActions,
    OrganActions,
    ContainerActions,
    ConditionsActions,
    ConditionsRecordActions,
    DeviceActions,
};

export default agent;
