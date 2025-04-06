import { use, useEffect, useState } from "react";
import { Basket } from "../../app/modules/basket";
import agent from "../../app/api/agent";
import { Button, Grid2, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { Add, Delete, Remove } from "@mui/icons-material";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { addBasketItemAsync, removeBasketItemAsync, setBasket } from "./basketSlice";

export default function BasketPage() {
    const {basket,status} = useAppSelector(state => state.basket);
    const dispatch = useAppDispatch();

    if(!basket) {
        return <Typography variant="h2">Basket is empty</Typography>
    }
    
    return (
        <>
        <TableContainer component={Paper} >
        <Table sx={{ minWidth: 650 }}>
          <TableHead>
            <TableRow>
              <TableCell>Product</TableCell>
              <TableCell align="right">Price</TableCell>
              <TableCell align="center">Quantity</TableCell>
              <TableCell align="right">SubTotal</TableCell>
              <TableCell align="right"></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {basket.items.map((item) => (
              <TableRow
                key={item.productId}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {item.name}
                </TableCell>
                <TableCell align="right">$ {item.price}</TableCell>
                <TableCell align="center">
                    <Button  loading={status === ('pendingRemoveItem' + item.productId)}
                             onClick={() => dispatch(removeBasketItemAsync({productId: item.productId,quantity:1}))} color="error">
                        <Remove />
                    </Button>
                    {item.quantity}
                    <Button loading={status === ('pendingAddItem' + item.productId)} 
                            onClick={() => dispatch(addBasketItemAsync({productId: item.productId}))} color="secondary">
                        <Add />
                    </Button>
                </TableCell>
                <TableCell align="right">$ {item.price * item.quantity}</TableCell>
                <TableCell align="right">
                    <Button loading={status === ('pendingRemoveItem' + item.productId)} 
                            onClick={() => dispatch(removeBasketItemAsync({productId: item.productId,quantity:item.quantity}))} color = "error">
                        <Delete />
                    </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      </>
    )
}
