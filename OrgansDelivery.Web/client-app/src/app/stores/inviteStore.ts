import { makeAutoObservable, runInAction } from "mobx";
import { CreateInvite, Invite } from "../models/invite";
import agent from "../api/agent";

export default class InviteStore {
    invites: Invite[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    loadInvites = async () => {
        try {
            const invites = await agent.InviteActions.getInvites();
            runInAction(() => (this.invites = invites));
        } catch (error) {
            console.log(error);
        }
    };

    createInvite = async (invite: CreateInvite) => {
        try {
            const created = await agent.InviteActions.createInvite(invite);
            runInAction(() => this.invites.push(created));
        } catch (error) {
            console.log(error);
        }
    };

    deleteInvite = async (inviteId: string) => {
        try {
            await agent.InviteActions.deleteInvite(inviteId);
            runInAction(() => {
                this.invites = this.invites.filter((e) => e.id !== inviteId);
            });
        } catch (error) {
            console.log(error);
        }
    };
}
