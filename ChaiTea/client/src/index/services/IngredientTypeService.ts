import { IIngredeintType } from "../domain/IIngredientType";
import { IReview } from "../domain/IReview";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseServices/BaseEntityService";

export class IngredientTypeService extends BaseEntityService<IIngredeintType> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)){
        super('v1/IngredientTypes', setJwtResponse);
    }

}