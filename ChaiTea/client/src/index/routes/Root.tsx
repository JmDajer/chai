import { Await, Outlet } from "react-router-dom";
import { createContext, useState, useEffect } from "react";

import Footer from "../components/Footer";
import Header from "../components/Header";
import { IJWTResponse } from "../dto/IJWTResponse";
import { IdentityService } from "../services/IdentityService";

export const JwtContext = createContext<{
  jwtResponse: IJWTResponse | null;
  setJwtResponse: ((data: IJWTResponse | null) => void) | null;
}>({ jwtResponse: null, setJwtResponse: null });

const identityService = new IdentityService();

const Root = () => {
  const [jwtResponse, setJwtResponse] = useState<IJWTResponse | null>(null);

  useEffect(() => {
    // Check if JWT and Refresh Key exist in localStorage
    const storedJwt = localStorage.getItem("jwt");
    const storedRefreshToken = localStorage.getItem("refreshToken");

    if (storedJwt && storedRefreshToken) {
      // Create an object with the stored JWT and Refresh Key
      const storedData: IJWTResponse = {
        jwt: storedJwt,
        refreshToken: storedRefreshToken
      };

      const refreshJwt = async () => {
        let refreshedJwt = await identityService.refreshToken(storedData);
        
        // Set the stored data in JwtContext
        setJwtResponse(refreshedJwt!);
      }

      refreshJwt();
  }
  }, []);

return (
  <JwtContext.Provider value={{ jwtResponse, setJwtResponse }}>
    <Header />

    <div className="container">
      <main role="main" className="pb-3">
        <Outlet />
      </main>
    </div>

    <Footer />
  </JwtContext.Provider>
);
};

export default Root;