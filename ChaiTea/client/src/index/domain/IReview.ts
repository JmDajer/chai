import { IBaseEntity } from "./IBaseEntity"
import { IComment } from "./IComment"

export interface IReview extends IBaseEntity{
    beverageId: string,
    appUserId: string,
    name: string,
    rating: number,
    reviewText?: string | null,
    comments?: IComment[] | null
}