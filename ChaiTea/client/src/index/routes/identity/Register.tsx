import { useState, MouseEvent, useContext } from "react";
import RegisterFormView from "./RegisterFormView";
import { IRegisterData } from "../../dto/IRegisterData";
import { IdentityService } from "../../services/IdentityService";
import { JwtContext } from "../Root";
import { useNavigate } from "react-router-dom";

const Register = () => {
    const navigate = useNavigate();

    const [values, setInput] = useState({
        password: "",
        confirmPassword: "",
        email: "",
    } as IRegisterData);

    const [validationErrors, setValidationErrors] = useState([] as string[]);

    const handleChange = (target: EventTarget & HTMLInputElement) => {

        setInput({ ...values, [target.name]: target.value });
    };

    const {jwtResponse, setJwtResponse} = useContext(JwtContext);

    const identityService = new IdentityService();

    const onSubmit = async (event: MouseEvent) => {
        console.log('onSubmit')
        event.preventDefault();

        if (values.email.length === 0) {
            setValidationErrors(["Email field empty"]);
            return;
        }

        if (values.password.length === 0) {
            setValidationErrors(["Password field empty"]);
            return;
        }

        if (values.password !== values.confirmPassword) {
            setValidationErrors(["Passwords don't match"]);
            return;
        }

        // remove errors
        setValidationErrors([]);

        // register the user, get jwt ad refreshtoken
        var jwtData = await identityService.register(values);

        
        if (jwtData === undefined) {
            setValidationErrors(['No JWT was found.']);
            return;
        }

        if (setJwtResponse){
            setJwtResponse(jwtData);
            localStorage.setItem("jwt", jwtData.jwt);
            localStorage.setItem("refreshToken", jwtData.refreshToken);
            navigate('/Home');
       }
    }

    return (
        <RegisterFormView values={values} handleChange={handleChange} onSubmit={onSubmit} validationErrors={validationErrors} />
    );
}

export default Register;