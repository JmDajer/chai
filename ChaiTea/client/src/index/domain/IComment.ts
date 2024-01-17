import { IBaseEntity } from "./IBaseEntity";

export interface IComment extends IBaseEntity {
    text: string,
    name: string,
    appUserId: string,
    reviewId: string
}