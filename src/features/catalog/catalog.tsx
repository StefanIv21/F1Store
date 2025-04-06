import { Box, Button, Grid2, Pagination, Paper, TextField, Typography } from "@mui/material";
import { Product, ProductParams } from "../../app/modules/product"
import ProductList from "./ProductList";
import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import { MetaData } from "../../app/modules/pagination";

function initParams()
{
    return {
        search: "",
        page: 1,
        pageSize: 8, 
    }
}


function getAxiosParams(params: ProductParams)
{
    const axiosParams = new URLSearchParams();
    axiosParams.append("page", params.page.toString());
    axiosParams.append("pageSize", params.pageSize.toString());
    if(params.search){
        axiosParams.append("search", params.search);
    }
    return axiosParams;
}

export default function Catalog() {
    const [products, setProducts] = useState<Product[]>([]);
    const [productParams, setProductParams] = useState<ProductParams>(initParams());
    const [metaData, setMetaData] = useState<MetaData>({currentPage: 1, totalPages: 1, pageSize: 8, totalCount: 0});

    useEffect(() => {
        const axiosParams = getAxiosParams(productParams);
        const response = agent.Catalog.list(axiosParams);
        response.then((res) => {
            setProducts(res.items.response.data);
            setMetaData(res.metadata);
        })
    }, [productParams]);

    const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setProductParams(prev => ({
            ...prev,
            search: event.target.value,
            page: 1,
        }));
    };

    const handlePageChange = (event: React.ChangeEvent<unknown>, value: number) => {
        setProductParams(prev => ({
            ...prev,
            page: value,
        }));
    };

    
    return (
        <Grid2 container sx={{ display: "flex", flexDirection: "column", minHeight: "100vh" }}>
        <Grid2 container sx={{ display: "flex", flex: "1" }}>
            <Grid2 sx={{ flex: "20%" }}> 
                <Paper sx={{ p: 1 }}>
                    <TextField label="Search" variant="outlined" fullWidth  value={productParams.search} onChange={handleSearchChange}  />
                </Paper>
            </Grid2>
    
            <Grid2 sx={{ flex: "80%" }}>
                <ProductList products={products} />
            </Grid2>
        </Grid2>
    
        <Grid2 sx={{ width: "100%", display: "flex", justifyContent: "center", p: 2 }}>
            <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
                <Typography>Display {(metaData?.currentPage-1) * metaData?.pageSize+1}- 
                                    {metaData.currentPage * metaData.pageSize > metaData.totalCount ? 
                                    metaData.totalCount : metaData.currentPage * metaData.pageSize} of {metaData.totalCount}
                </Typography>
                <Pagination count={metaData?.totalPages} size="large" color="secondary" page={productParams.page}  onChange={handlePageChange}  />
            </Box>
        </Grid2>
    </Grid2>
    

    )
}