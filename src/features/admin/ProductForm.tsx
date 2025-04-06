import { Box, Paper, Typography, Grid, Button } from '@mui/material';
import { FieldValues, useForm } from 'react-hook-form';
import AppTextInput from '../../app/components/AppTextInput';
import { Product } from '../../app/modules/product';
import { use, useEffect } from 'react';
import agent from '../../app/api/agent';
import { useAppDispatch } from '../../app/store/configureStore';
import { set } from 'lodash';
import { useNavigate } from 'react-router';
import { n } from 'react-router/dist/development/fog-of-war-CvttGpNz';

interface Props{
    product?: Product;
    cancelEdit: () => void;
}


export default function ProductForm({product, cancelEdit}: Props) {
    const navigate = useNavigate();
    const { control, reset,handleSubmit, formState:{isDirty,isSubmitting}} = useForm();
    const dispatch = useAppDispatch();

    useEffect(() => {
        if(product && !isDirty){
            reset(product);
        }
    }, [product, reset,isDirty]);

    async function onSubmit(data: FieldValues) {
        try{
            let response:Product;
            if(product){
                data.id = product.id;
                response = await agent.Admin.updateProduct(data);
                navigate('/catalog');
            } else
            {
                response = await agent.Admin.createProduct(data);
                navigate('/catalog');
            }
            cancelEdit();

        }catch(error){
            console.log(error);
        }
    }

    return (
        <Box component={Paper} sx={{ p: 4 }}>
            <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
                Product Details
            </Typography>
            <form onSubmit={handleSubmit(onSubmit)}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput control={control} name='name' label='Product name' />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <AppTextInput  control={control} name='brand' label='Brand' />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <AppTextInput  control={control} name='type' label='Type' />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <AppTextInput type="number" control={control} name='price' label='Price' />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <AppTextInput type="number" control={control} name='stock' label='Quantity in Stock' />
                    </Grid>
                    <Grid item xs={12}>
                        <AppTextInput multiline={true} rows={3} control={control} name='description' label='Description'
                        />
                    </Grid>
                </Grid>
                <Box display='flex' justifyContent='space-between' sx={{ mt: 3 }}>
                    <Button  onClick={cancelEdit} variant='contained' color='inherit'>Cancel</Button>
                    <Button loading={isSubmitting}
                        type='submit' 
                        variant='contained' 
                        color='success'>Submit</Button>
                </Box>
            </form>
        </Box>
    )
}