import 'jquery';
import 'popper.js';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';

import './site.css';

import React from 'react';
import ReactDOM from 'react-dom/client';
import {
  createBrowserRouter,
  RouterProvider,
  Navigate
} from "react-router-dom";

import Root from './index/routes/Root';
import Home from './index/routes/Home';
import Privacy from './index/routes/Privacy';
import Login from './index/routes/identity/Login';
import Register from './index/routes/identity/Register';
import ErrorPage from './index/routes/ErrorPage';
import Beverages from './index/routes/beverages/Beverages';
import Info from './index/routes/identity/Info';
import BeverageView from './index/routes/beverages/BeverageView';
import UserBeverageView from './index/routes/beverages/UserBeverageView';
import RecommendedView from './index/routes/beverages/RecommendedView';
import CustomBeverageView from './index/routes/beverages/CustomBeverageView';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: <Navigate to="/Home" />,
      },
      {
        path: "/Home",
        element: <Home />,
      },
      {
        path: "/info",
        element: <Info />,
      },
      {
        path: "/Login",
        element: <Login />,
      },
      {
        path: "/Register",
        element: <Register />,
      },
      {
        path: "/Privacy",
        element: <Privacy />,
      },
      {
        path: "/Beverages",
        element: <Beverages />,
      },
      {
        path: "/Beverages/:id",
        element: <BeverageView />,
      },
      {
        path: "/Beverages/Users/:id",
        element: <UserBeverageView />,
      },
      {
        path: "/Beverages/Users/:id/CustomBeverage",
        element: <CustomBeverageView />,
      },
      {
        path: "/Beverages/Users/:id/Recommended",
        element: <RecommendedView />,
      },
    ]
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);