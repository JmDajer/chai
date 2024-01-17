import { IBaseEntity } from "./IBaseEntity"

export interface IIngredient extends IBaseEntity {
    name: string,
    ingredientTypeId: string
}