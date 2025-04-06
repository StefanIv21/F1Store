import { Button, Menu, Fade, MenuItem } from "@mui/material";
import { useState } from "react";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import { Link } from 'react-router-dom';
import { signOutUser } from "../../features/account/accountSlice";
import { get } from "lodash";
import { getEmail } from "../modules/user";
import { clearBasket } from "../../features/basket/basketSlice";

export default function SignedInMenu() {
  const dispatch = useAppDispatch();
  const { user } = useAppSelector(state => state.account);
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: any) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <Button
        color='secondary'
        onClick={handleClick}
        sx={{ typography: 'h6' }}
      >
       
        {user ? getEmail(user) : "Guest"}
      </Button>
      <Menu
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        TransitionComponent={Fade}
      >
        <MenuItem component={Link} to='/address'>My ShippingAddress</MenuItem>

        <MenuItem component={Link} to='/orders' >My orders</MenuItem>
        <MenuItem onClick={() =>
         {
          dispatch(signOutUser());
          dispatch(clearBasket());
         }
        } style={{color: 'red'
        }}>Logout</MenuItem>
      </Menu>
    </>
  );
}