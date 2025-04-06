import { use, useEffect, useState } from "react";
import { Product } from "../modules/product";
import { randomUUID } from "crypto";
import Catalog from "../../features/catalog/catalog";
import { CssBaseline, Typography } from "@mui/material";
import Header from "./Header";
import { Navigate, Route, Routes, useNavigate } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import ProductDetails from "../../features/catalog/ProductDetails";
import BasketPage from "../../features/basket/basketPage";
import { getCookie } from "../util/util";
import agent from "../api/agent";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import { set } from "lodash";
import { setBasket } from "../../features/basket/basketSlice";
import { fetCurrentUser } from "../../features/account/accountSlice";
import Address from "../../features/account/Address";
import Inventory from "../../features/admin/Inventory";
import Feedback from "../../features/account/FeedBack";

function App() {
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);
  const { user } = useAppSelector((state) => state.account);

  useEffect(() => {
    const buyerId = getCookie("userId");
    dispatch(fetCurrentUser());
    if(buyerId) {
      agent.Basket.get()
      .then(basket => dispatch(setBasket(basket)))
      .catch(err => console.log(err))
      .finally(() => setLoading(false));
    }
    else {
      setLoading(false);
    }
  },[dispatch]);

  if(loading) {
    return <Typography variant="h2">Loading...</Typography>
  }

   

  return (
    <>
        <CssBaseline></CssBaseline>
        <Header></Header>
        <Routes> 
                <Route path="/" element={<HomePage />} />
                <Route path="/catalog" element={<Catalog />} />
                <Route path="/feedback" Component={Feedback} />
                <Route path="/inventory" Component={Inventory}/>
                <Route path="/catalog/:id" element={<ProductDetails />} />
                <Route path="/basket" element={<BasketPage />} />
                <Route path="/login" Component={Login} />
                <Route path="/register" Component={Register} />
                <Route path="/address" Component={Address}/>
        </Routes>
    </>
  );
}

export default App;
