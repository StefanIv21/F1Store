import * as React from 'react';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormLabel from '@mui/material/FormLabel';
import Grid from '@mui/material/Grid2';
import OutlinedInput from '@mui/material/OutlinedInput';
import { styled } from '@mui/material/styles';
import { Box, Button } from '@mui/material';
import { FieldValues, useForm } from 'react-hook-form';
import AppTextInput from '../../app/components/AppTextInput';
import agent from '../../app/api/agent';
import { useAppDispatch } from '../../app/store/configureStore';
import { addAddressAsync } from './accountSlice';
import { Address } from '../../app/modules/address';

const FormGrid = styled(Grid)(() => ({
  display: 'flex',
  flexDirection: 'column',
}));


export default function AddressForm() {
    const { control, handleSubmit, setValue} = useForm();
    const [loading, setLoading] = React.useState(true);
    const [address, setAddress] = React.useState<Address | undefined>(undefined);
    const dispatch = useAppDispatch();

  

    async function submitForm(data:FieldValues) {
      let response:Address;
      if(address){
          response = await agent.Address.update(data);
      } else
      {
          response = await agent.Address.add(data);
      }

    }
  
    React.useEffect(() => {
      agent.Address.fetchAddress()
        .then(address => {
          if (address) {
            address = address.response;
            setAddress(address);
            setValue('AddressLine1', address.addressLine1 || '');
            setValue('AddressLine2', address.addressLine2 || '');
            setValue('City', address.city || '');
            setValue('State', address.state || '');
            setValue('ZipCode', address.zipCode || '');
            setValue('Country', address.country || '');
          }
        })
        .catch(error => {
            console.error("Error fetching address:", error);
        })
        .finally(() => setLoading(false)); 
    }, [setValue]);

  return (
    <form onSubmit={handleSubmit((data) => submitForm(data))}>
    <Box sx = {{padding: 4}}> 
     <Grid container spacing={3}>
      <FormGrid size={{ xs: 12 }}>
        <AppTextInput control={control} name='AddressLine1' label ='Address line 1'></AppTextInput>
      </FormGrid>
      <FormGrid size={{ xs: 12 }}>
        <AppTextInput control={control} name='AddressLine2' label ='Address line 2'></AppTextInput>
      </FormGrid>
      <FormGrid size={{ xs: 6 }}>
        <AppTextInput control={control} name='City' label ='City'></AppTextInput>
      </FormGrid>
      <FormGrid size={{ xs: 6 }}>
        <AppTextInput control={control} name='State' label ='State'></AppTextInput>
      </FormGrid>
      <FormGrid size={{ xs: 6 }}>
        <AppTextInput control={control} name='ZipCode' label ='Zip'></AppTextInput>
      </FormGrid>
      <FormGrid size={{ xs: 6 }}>
         <AppTextInput control={control} name='Country' label ='Country'></AppTextInput>
      </FormGrid>
    <Button type = 'submit' variant='contained' color='primary'>Submit
    </Button>
    </Grid>
    </Box> 
    </form> 
  );
}

