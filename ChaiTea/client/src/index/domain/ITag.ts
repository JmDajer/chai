import { IBaseEntity } from "./IBaseEntity"

export interface ITag extends IBaseEntity{
    name: string,
    tagTypeId: string
}