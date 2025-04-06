import { Grid2} from "@mui/material";
import { Product } from "../../app/modules/product";
import ProductCard from "./ProductCard";

interface ProductListProps {
    products: Product[];
}

export default function ProductList(props: ProductListProps) {
    return (
        <Grid2 container spacing={3}>
                {props.products.map((product) => (
                    <Grid2 sx={{  p:3 }} key={product.id}>
                        <ProductCard product={product}></ProductCard>
                    </Grid2>
                ))}
        </Grid2>
    )
}
