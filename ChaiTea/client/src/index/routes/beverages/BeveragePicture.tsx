import { IPicture } from "../../domain/IPicture";
import placeholder from "../../../images/no-image.jpg";
interface PictureProps {
    pictures?: IPicture[] | null
}

const BeveragePicture = ({pictures}: PictureProps) => {

    const imageFolderUrl = process.env.PUBLIC_URL + "/images/";

    return <img src={pictures == null || pictures?.length < 1 ? placeholder : `${imageFolderUrl}${pictures[0].url}`} />  
}

export default BeveragePicture;