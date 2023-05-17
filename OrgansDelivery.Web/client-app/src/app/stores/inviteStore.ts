import { makeAutoObservable, runInAction } from "mobx";
import { InviteFormValues, Invite } from "../models/invite";
import agent from "../api/agent";

export default class InviteStore {
    invites: Invite[] = [];
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadInvites = async () => {
        try {
            runInAction(() => this.isLoading = true);
            const invites = await agent.InviteActions.getInvites();
            runInAction(() => (this.invites = invites));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };

    createInvite = async (invite: InviteFormValues) => {
        try {
            runInAction(() => this.isLoading = true);
            const created = await agent.InviteActions.createInvite(invite);
            runInAction(() => this.invites.push(created));
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };

    deleteInvite = async (inviteId: string) => {
        try {
            runInAction(() => this.isLoading = true);
            await agent.InviteActions.deleteInvite(inviteId);
            runInAction(() => {
                this.invites = this.invites.filter((e) => e.id !== inviteId);
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.isLoading = false);
        }
    };
}
