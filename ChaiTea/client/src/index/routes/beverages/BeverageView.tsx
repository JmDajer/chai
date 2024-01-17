import { Link, useParams } from "react-router-dom";
import { BeverageService } from "../../services/BeverageService";
import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../Root";
import { IBeverage } from "../../domain/IBeverage";
import BeverageRating from "./BeverageRating";
import BeveragePicture from "./BeveragePicture";
import ReviewsView from "../reviews/ReviewsView";
import jwtDecode from "jwt-decode";


const BeverageView = () => {
    let { id } = useParams();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const beverageService = new BeverageService(setJwtResponse!);
    const [data, setData] = useState({} as IBeverage);
    const imageFolderUrl = process.env.PUBLIC_URL + "/images/";

    useEffect(() => {
        beverageService.getBeverage(id!)
            .then(response => {
                if (response) setData(response);
            }
            );
    }, []);

    let userId = "";

    if (jwtResponse) {
        let decodedJwt: any = jwtDecode(jwtResponse.jwt);
        let userId = decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    }

    const handleDeleteBeverage = async (beverage: IBeverage) => {
        if (jwtResponse) {
            if (data.appUserId == userId) {
                let beverageService = new BeverageService(setJwtResponse!);
                let deleteResult = await beverageService.deleteBeverage(jwtResponse!, data.id!);


                if (deleteResult) {
                    console.log('Delete beverage:', beverage);
                }
            }
        }
    }

    return <>
        <div className="beverageView">
            <div className="beverageName">{data.name}</div>
            <div className="beveragePicture"><BeveragePicture pictures={data.pictures} /></div>
            <div className="beverageDetails">
                <BeverageRating reviews={data.reviews} />
                <button className="deleteButton" onClick={() => handleDeleteBeverage(data)}>Delete</button>
                <hr />
                <p>UPC: {data.upc}</p>
                <p>{data.tags?.map(tag => {
                    let typeName = "";
                    if (tag.tagTypeId == "4ac53f43-cb4d-4787-98ac-d9703a0df6ac") typeName = "companyTag";
                    if (tag.tagTypeId == "ac6a1a61-451c-44f9-8df2-f6e94769674c") typeName = "typeTag";
                    if (tag.tagTypeId == "6ea7a9c1-d14a-48ae-a7c2-b9de80ee5f18") typeName = "categoryTag";
                    if (tag.tagTypeId == "6aa8701a-ee73-40b6-a49f-53ef718530e1") typeName = "flavourTag";

                    return <span className={`tag ${typeName}`}>{tag.name}</span>
                })}</p>
            </div>
            <div className="beverageInfo">
                <h3>Description</h3>
                <p>{data.description === null ? "Description is missing!" : data.description}</p>
                <hr />

                <div>{data.ingredients == null ? '' :
                    <><h3>Added ingredients</h3>
                        <p>{data.ingredients?.length == 0 ? "Tea does not contain any added ingredients!" : data.ingredients?.map(ingredient => {
                            return <div>{ingredient.name}</div>
                        })}</p>
                        <hr /></>}
                </div>

                <h3>Added teas</h3>
                <p>{data.parentBeverages?.length == 0 ? "Tea is not made out of any other tea!" : data.parentBeverages?.map(parentBeverages => {
                    return <div><Link to={`/Beverages/${parentBeverages.id}`}>{parentBeverages.name}</Link></div>
                })}</p>
                <hr />
            </div>
        </div>
        <ReviewsView beverageId={id!} />

    </>
    //TODO add other users review and their comments;
}

export default BeverageView;
