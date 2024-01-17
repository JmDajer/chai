import { IIngredient } from "../domain/IIngredient";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseServices/BaseEntityService";

export class IngredientService extends BaseEntityService<IIngredient> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)){
        super('v1/Ingredients', setJwtResponse);
    }

    async getAllIngredients(): Promise<IIngredient[] | undefined> {
        const response = await this.axios.get<IIngredient[]>("");

        console.log('respone', response);
        if (response.status === 200) return response.data;

        return undefined;
    }

}