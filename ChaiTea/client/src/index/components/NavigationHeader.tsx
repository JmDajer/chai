import { useContext } from "react";
import { Link } from "react-router-dom";
import { JwtContext } from "../routes/Root";
import jwtDecode from "jwt-decode";

const NavigationHeader = () => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    if (jwtResponse) {
        let decodedJwt: any = jwtDecode(jwtResponse.jwt);
        let userId = decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

        return (
            <>
                <li className="nav-item">
                    <Link to={`/Beverages/Users/${userId}`} className="nav-link">MY TEAS</Link>
                </li>
                <li className="nav-item">
                    <Link to={`Beverages/Users/${userId}/Recommended`} className="nav-link">RECOMMENDED</Link>
                </li>
                <li className="nav-item">
                    <Link to={`Beverages/Users/${userId}/CustomBeverage`} className="nav-link">CUSTOM TEA</Link>
                </li>
                <li className="nav-item">
                    <Link to="privacy" className="nav-link">PRIVACY</Link>
                </li>
            </>
        );
    }
    return (
        <>
            <li className="nav-item">
                <Link to="privacy" className="nav-link">PRIVACY</Link>
            </li>
        </>
    );
}



export default NavigationHeader;