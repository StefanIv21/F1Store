import { ShoppingCart } from "@mui/icons-material";
import { AppBar, Badge, Box, IconButton, List, ListItem, Toolbar, Typography } from "@mui/material";
import { Link, NavLink } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import { Sign } from "crypto";
import SignedInMenu from "./SingedInMenu";
import { fetCurrentUser } from "../../features/account/accountSlice";
import { getRole } from "../modules/user";

const midLinks = [
    { title:'catalog', path: '/catalog'}
]

const rightLinks = [
    { title:'login', path: '/login'},
    { title:'register', path: '/register'}
]

const navStyles =
    {
    '&:hover': {color: 'gray'},
    '&.active': {color: 'text.secondary'},
    color: 'inheit',
    typography : 'inherit',
    textDecoration: 'none'
    }

export default function Header() {
    const {user,current} = useAppSelector(state => state.account);
    const {basket} = useAppSelector(state => state.basket);
    const itemCount = basket?.items.reduce((acc, item) => acc + item.quantity, 0) ?? 0;
    return (
        <AppBar position="static" sx={{mb: 4, backgroundColor: 'background.paper'}}>
            <Toolbar sx={{display: 'flex', justifyContent: 'space-between'}}>
                <Typography variant="h6" component={NavLink} to="/" sx={navStyles}
                >F1 Store</Typography>
            <List sx={{display: 'flex', marginRight: 'auto'}}>
                {midLinks.map(({title,path}) => (
                    <ListItem 
                        component={NavLink}
                        to={path}
                        key={path}
                        sx={navStyles}
                        >
                        {title.toUpperCase()}
                    </ListItem>   
                ))}
                {user &&
                 <ListItem 
                 component={NavLink}
                 to={'/feedback'}
                 sx={navStyles}
                 >
                 Feedback
             </ListItem>}
                {user && getRole(user) === 'Admin' &&
                <ListItem 
                    component={NavLink}
                    to={'/inventory'}
                    sx={navStyles}
                    >
                    Inventory
                </ListItem>}
            </List>
            <Box display='flex' >
                <IconButton component={Link} to="/basket"
                    size = "large"
                    sx={{navStyles}}>
                    <Badge badgeContent={itemCount} color="secondary">
                        <ShoppingCart />
                    </Badge>
                </IconButton>
                { user? (
                    <SignedInMenu />
                ) : (
                    <List sx={{display: 'flex', marginLeft: 'auto'}}>
                        {rightLinks.map(({title,path}) => (
                            <ListItem 
                                component={NavLink}
                                to={path}
                                key={path}
                                sx={navStyles}
                        >
                                {title.toUpperCase()}
                            </ListItem>
                        
                        ))}
                    </List>
                )}
            </Box>
            </Toolbar>
        </AppBar>
    ) 
}