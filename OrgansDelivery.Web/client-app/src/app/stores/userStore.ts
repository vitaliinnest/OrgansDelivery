import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { router } from "../router/Routes";
import { store } from "./store";
import { Login, Register, User } from "../models/user";

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
            const user = await agent.UserActions.register(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => (this.user = user));
            router.navigate('/create-tenant');
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => (this.isLoading = false));
        }
    };

    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
        router.navigate("/sign-in");
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
}
