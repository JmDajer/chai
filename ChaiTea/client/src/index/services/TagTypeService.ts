import { ITagType } from "../domain/ITagType";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseServices/BaseEntityService";

export class TagTypeService extends BaseEntityService<ITagType> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)){
        super('v1/TagTypes', setJwtResponse);
    }

}