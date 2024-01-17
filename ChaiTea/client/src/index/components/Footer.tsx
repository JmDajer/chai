import { Link } from "react-router-dom";

const Footer = () => {
    return (
        <footer className="border-top footer text-muted footer">
            <div className="container">
                © 2023 - ChaiTea - <Link to="/privacy">Privacy</Link>
            </div>
        </footer>
    );
}

export default Footer;