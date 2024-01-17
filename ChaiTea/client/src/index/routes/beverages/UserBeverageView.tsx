import { useContext, useEffect, useState } from "react";
import { BeverageService } from "../../services/BeverageService";
import { JwtContext } from "../Root";
import { IBeverage } from "../../domain/IBeverage";
import { Link } from "react-router-dom";
import StarRating from "./BeverageRating";
import BeveragePicture from "./BeveragePicture";


const UserBeverageView = () => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const beverageService = new BeverageService(setJwtResponse!);

    const [data, setData] = useState([] as IBeverage[]);

    useEffect(() => {
        if (jwtResponse) {
            beverageService.GetUserBeverages(jwtResponse)
                .then(response => {
                    if (response) setData(response);
                    else setData([]);
                });
        }
    }, []);

    return <div className="gridContainer">
        {data.map(beverage =>
            <Link to={`/Beverages/${beverage.id}`} className="containerLink">
                <div className="containerItem">
                    <div className="containerPicture">
                        <BeveragePicture pictures={beverage.pictures} />
                    </div>

                    <StarRating reviews={beverage.reviews} />

                    <div className="containerDetails">
                        {beverage.name.length > 32 ?
                            `${beverage.name.substring(0, 32)}...` :
                            beverage.name}
                    </div>
                </div>
            </Link>)
        }
    </div>;
}

export default UserBeverageView;
