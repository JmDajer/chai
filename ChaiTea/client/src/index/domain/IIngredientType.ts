import { IBaseEntity } from "./IBaseEntity"
import { IIngredient } from "./IIngredient"

export interface IIngredeintType extends IBaseEntity{
    name: string,
    ingredients?: IIngredient[] | null
}