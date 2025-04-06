import { Divider, Grid2, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Product } from "../../app/modules/product";
import axios from "axios";
import agent from "../../app/api/agent";

export default function ProductDetails() {
    const {id} = useParams<{id:string}>();
    const [product, setProduct] = useState<Product | null>();
    const [loading, setLoading] = useState(true);
    

    useEffect(() => {
        if (id) {
            agent.Catalog.details(id)
            .then((res) => {
                setProduct(res.response);
                setLoading(false);
            })
            .catch((err) => console.log(err));
        } else {
            setLoading(false);
        }
    },[id])

    if(loading) {
        return <Typography variant="h2">Loading...</Typography>
    }
    if(!product) {
        return <Typography variant="h2">Product not found</Typography>
    }


    return (
        <Grid2 container spacing={6} sx={{ p:6, justifyContent: "center" }}>
                <Typography variant="h2">{product.name}</Typography>
                <TableContainer>
                    <Table>
                        <TableBody>
                            <TableRow>
                                <TableCell>Price</TableCell>
                                <TableCell>{product.price} €</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Description</TableCell>
                                <TableCell>{product.description}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Brand</TableCell>
                                <TableCell>{product.brand}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Type</TableCell>
                                <TableCell>{product.type}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Stock</TableCell>
                                <TableCell>{product.stock}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
    
                
        </Grid2>
    )
}