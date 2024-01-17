import { Link } from "react-router-dom";
import { BeverageService } from "../../services/BeverageService";
import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../Root";
import { IBeverage } from "../../domain/IBeverage";
import StarRating from "./BeverageRating";
import BeveragePicture from "./BeveragePicture";


const RecommendedView = () => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const beverageService = new BeverageService(setJwtResponse!);

    const [data, setData] = useState([] as IBeverage[]);

    useEffect(() => {
        if (jwtResponse) {
            // Get custom teas
        }

        beverageService.GetRecommendedBeverages(jwtResponse!)
            .then(response => {
                if (response) setData(response);
                else setData([]);
            }
            );
    }, []);

    if (data.length > 0) {
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
    } else {
        return <h1>You don't have any reccomendations. Review some teas first!</h1>
    }
}

export default RecommendedView;
