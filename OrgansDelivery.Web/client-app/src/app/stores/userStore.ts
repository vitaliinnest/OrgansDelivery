import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { router } from "../router/Routes";
import { store } from "./store";
import { Login, Register, UpdateUser, User } from "../models/user";

export default class UserStore {
    user: User | null = null;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: Login) => {
        try {
            runInAction(() => (this.isLoading = true));
            const user = await agent.UserActions.login(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => (this.user = user));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => (this.isLoading = false));
        }
    };

    register = async (creds: Register) => {
        try {
            runInAction(() => (this.isLoading = true));
            await agent.UserActions.register(creds);
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => (this.isLoading = false));
        }
    };

    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
    };

    getUser = async () => {
        try {
            runInAction(() => (this.isLoading = true));
            const user = await agent.UserActions.current();
            runInAction(() => (this.user = user));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => (this.isLoading = false));
        }
    };

    updateUser = async (user: UpdateUser) => {
        try {
            runInAction(() => (this.isLoading = true));
            store.modalStore.closeModal();
            const updated = await agent.UserActions.update(user);
            runInAction(() => (this.user = updated));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => (this.isLoading = false));
        }
    };
}
