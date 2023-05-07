import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { router } from "../router/Routes";
import { store } from "./store";
import { Login, Register, User } from "../models/user";

export default class UserStore {
    user: User | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: Login) => {
        try {
            const user = await agent.Account.login(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => (this.user = user));
        } catch (error) {
            throw error;
        }
    };

    register = async (creds: Register) => {
        try {
            const user = await agent.Account.register(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => (this.user = user));
        } catch (error) {
            throw error;
        }
    };

    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
        router.navigate("/");
    };

    getUser = async () => {
        try {
            const user = await agent.Account.current();
            runInAction(() => (this.user = user));
        } catch (error) {
            console.log(error);
        }
    };

    // setImage = (image: string) => {
    //     if (this.user) this.user.image = image;
    // };

    // setUserPhoto = (url: string) => {
    //     if (this.user) this.user.image = url;
    // };

    // setDisplayName = (name: string) => {
    //     if (this.user) this.user.displayName = name;
    // };
}
