import { Edit, Delete } from '@mui/icons-material';
import { Box, Typography, Button, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Grid2, Pagination } from '@mui/material';
import { useState } from 'react';
import agent from '../../app/api/agent';
import { LoadingButton } from '@mui/lab';
import useProducts from '../../app/hooks/useProducts';
import ProductForm from './ProductForm';
import { Product } from '../../app/modules/product';
import { useNavigate } from 'react-router';
import ConfirmDialog from '../../app/components/ComfirmDialog';

export default function Inventory() {
    const navigate = useNavigate();
    const { products, metaData, productParams, handlePageChange } = useProducts();
    const [editMode, setEditMode] = useState(false);
    const [selectedProduct, setSelectedProduct] = useState<Product | undefined>(undefined);
    const [loading, setLoading] = useState(false);
    const [target, setTarget] = useState('');
    const [openDialog, setOpenDialog] = useState(false);
    const [deleteId, setDeleteId] = useState<string | null>(null);

    function handleSelectProduct(product: Product) {
        setSelectedProduct(product);
        setEditMode(true);
    }

    function handleDeleteProduct(id: string) {
        setDeleteId(id);
        setOpenDialog(true);
    }

    function confirmDelete() {
        if (deleteId) {
            setLoading(true);
            setTarget(deleteId);
            agent.Admin.deleteProduct(deleteId)
                .then(() => {
                    navigate('/catalog');
                    setLoading(false);
                })
                .catch(error => {
                    console.log(error);
                    setLoading(false);
                })
                .finally(() => {
                    setOpenDialog(false);
                    setDeleteId(null);
                });
        }
    }

    function cancelEdit() {
        setSelectedProduct(undefined);
        setEditMode(false);
    }

    if (editMode) {
        return <ProductForm product={selectedProduct} cancelEdit={cancelEdit} />;
    }

    return (
        <>
            <Box display='flex' justifyContent='space-between'>
                <Typography sx={{ p: 2 }} variant='h4'>Inventory</Typography>
                <Button
                    onClick={() => setEditMode(true)}
                    sx={{ m: 2 }}
                    size='large' variant='contained'
                >
                    Create
                </Button>
            </Box>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>#</TableCell>
                            <TableCell align="left">Product</TableCell>
                            <TableCell align="right">Price</TableCell>
                            <TableCell align="center">Type</TableCell>
                            <TableCell align="center">Brand</TableCell>
                            <TableCell align="center">Quantity</TableCell>
                            <TableCell align="right"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {products.map((product) => (
                            <TableRow
                                key={product.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {product.id}
                                </TableCell>
                                <TableCell align="left">
                                    <Box display='flex' alignItems='center'>
                                        <span>{product.name}</span>
                                    </Box>
                                </TableCell>
                                <TableCell align="right">{product.price}</TableCell>
                                <TableCell align="center">{product.type}</TableCell>
                                <TableCell align="center">{product.brand}</TableCell>
                                <TableCell align="center">{product.stock}</TableCell>
                                <TableCell align="right">
                                    <Button onClick={() => handleSelectProduct(product)}
                                        startIcon={<Edit />}
                                    />
                                    <LoadingButton 
                                        onClick={() => handleDeleteProduct(product.id)}
                                        loading={loading && target === product.id}
                                        startIcon={<Delete />} 
                                        color='error'
                                    />
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Grid2 sx={{ width: "100%", display: "flex", justifyContent: "center", p: 2 }}>
                <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
                    <Typography>Display {(metaData?.currentPage - 1) * metaData?.pageSize + 1}-
                        {metaData.currentPage * metaData.pageSize > metaData.totalCount ?
                            metaData.totalCount : metaData.currentPage * metaData.pageSize} of {metaData.totalCount}
                    </Typography>
                    <Pagination count={metaData?.totalPages} size="large" color="secondary" page={productParams.page} onChange={handlePageChange} />
                </Box>
            </Grid2>

            <ConfirmDialog
                open={openDialog}
                onClose={() => setOpenDialog(false)}
                onConfirm={confirmDelete}
                title="Confirm Delete"
                message="Are you sure you want to delete this product?"
            />
        </>
    );
}
