import { Button, Card, CardActions, CardContent, CardHeader, CardMedia, ListItem, Typography } from "@mui/material";
import { Product } from "../../app/modules/product";
import { Link } from "react-router-dom";
import { useState } from "react";
import agent from "../../app/api/agent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { addBasketItemAsync, setBasket } from "../basket/basketSlice";


interface ProductCardProps {
    product: Product;
}


export default function ProductCard({product}: ProductCardProps) {
    const {status} = useAppSelector(state => state.basket);
    const dispatch = useAppDispatch();

    return (
        <Card sx={{ width: 270 }}>
        <CardMedia
          sx={{ height: 140}}
          image="/static/images/cards/contemplative-reptile.jpg"
          title={product.name}
        />
        <CardContent>
          <Typography gutterBottom color='secondary' variant="h5" component="div">
            {product.price} €
          </Typography>
          <Typography variant="body2" sx={{ color: 'text.secondary' }}>
            {product.description} 
          </Typography>
        </CardContent>
        <CardActions>
          <Button loading={status.includes('pending')} onClick={() => dispatch(addBasketItemAsync({productId: product.id}))} size="small">Add to cart</Button>
          <Button component={Link} to={`/catalog/${product.id}`} size="small">View</Button>
        </CardActions>
      </Card>
    )
}