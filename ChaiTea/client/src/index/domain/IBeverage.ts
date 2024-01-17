import { IBaseEntity } from "./IBaseEntity";
import { IIngredient } from "./IIngredient";
import { IPicture } from "./IPicture";
import { IReview } from "./IReview";
import { ITag } from "./ITag";

export interface IBeverage extends IBaseEntity {
    name: string,
    upc?: string | null,
    description?: string | null,
    rating?: number | null,
    appUserId: string,
    parentBeverages?: IBeverage[] | null ,
    subBeverages?: IBeverage[] | null,
    tags?: ITag[] | null,
    reviews?: IReview[] | null,
    ingredients?: IIngredient[] | null,
    pictures?: IPicture[] | null
}