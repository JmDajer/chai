import { IBaseEntity } from "./IBaseEntity"

export interface IPicture extends IBaseEntity{
    url: string,
    beverageId: string
}