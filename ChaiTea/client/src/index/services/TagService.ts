import { ITag } from "../domain/ITag";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseServices/BaseEntityService";

export class TagService extends BaseEntityService<ITag> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)){
        super('v1/Tags', setJwtResponse);
    }

    async getAllTags(): Promise<ITag[] | undefined> {
        const response = await this.axios.get<ITag[]>("");

        console.log('respone', response);
        if (response.status === 200) return response.data;

        return undefined;
    }

}