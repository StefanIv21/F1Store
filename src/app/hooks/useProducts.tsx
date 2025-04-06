import { useEffect, useState } from "react";
import { Product, ProductParams } from "../modules/product";
import { MetaData } from "../modules/pagination";
import agent from "../api/agent";

export default function useProducts() {
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

    return{
        products,
        productParams,
        metaData,
        setProductParams,
        handleSearchChange,
        handlePageChange
    }



}