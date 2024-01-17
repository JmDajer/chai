import { IBaseEntity } from "./IBaseEntity"
import { ITag } from "./ITag"

export interface ITagType extends IBaseEntity{
    name: string,
    tags?: ITag[] | null
}