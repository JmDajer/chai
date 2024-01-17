import { useContext, useState } from "react";
import { Link } from "react-router-dom";
import { JwtContext } from "../routes/Root";
import IdentityHeader from "./IdentityHeader";
import NavigationHeader from "./NavigationHeader";


const SeachIcon = () => {
    return <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-search" viewBox="0 0 16 16">
        <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z" />
    </svg>
}

const Header = () => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const [searchQuery, setSearchQuery] = useState(""); // State for storing the search query

    const handleSearch = (e: { preventDefault: () => void; }) => {
        e.preventDefault();
        // Perform the search logic here with the searchQuery state
        console.log("Searching for:", searchQuery);
        // Reset the search query after performing the search
        setSearchQuery("");
    };


    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light header border-bottom box-shadow mb-3">
                <div className="container-fluid">
                    <Link to="/Home" className="navbar-brand">ChaiTea</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            <NavigationHeader />
                        </ul>
                        <ul className="navbar-nav">
                            <IdentityHeader />
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
};

export default Header;