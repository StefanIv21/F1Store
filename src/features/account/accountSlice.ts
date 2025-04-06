import { createAsyncThunk, createSlice, current, isAnyOf } from "@reduxjs/toolkit";
import { currentUser, getBasket, getBasketStorage, User } from "../../app/modules/user";
import { FieldValues } from "react-hook-form";
import agent from "../../app/api/agent";
import { useNavigate } from "react-router";
import { setBasket } from "../basket/basketSlice";
import { Address } from "../../app/modules/address";

interface AccountState {
    user: User | null;
    current: currentUser | null;
}

const initialState: AccountState = {
    user: null,
    current: null
};

interface AddressState {
    address: Address | null;
    status: string;
}

const initialAddressState: AddressState = {
    address: null,
    status: 'idle'
};

export const addAddressAsync = createAsyncThunk<Address, FieldValues>(
    'account/addAddressAsync',
    async (data) => {
        try {
            return await agent.Address.add(data);
        } catch (error) {
            console.error(error);
        }
    }
);



export const signInUser = createAsyncThunk<User,  FieldValues>(
    'account/signInUser',
    async (data, thunkAPI) => {
        try {
            const user = await agent.Account.login(data);
            const basket = getBasket(user);
            if(basket)
            {
                thunkAPI.dispatch(setBasket(basket));
            }
            localStorage.setItem('user', JSON.stringify(user));
            return user;
        } catch (error) {
            return thunkAPI.rejectWithValue({error: (error as any).data});
        }
    }
)


export const fetCurrentUser = createAsyncThunk<currentUser>(
    'account/currentUser',
    async (_, thunkAPI) => {
        thunkAPI.dispatch(setUser(JSON.parse(localStorage.getItem('user') as string)));
        try {
            const current = await agent.Account.currentUser();
            const basket = getBasketStorage(localStorage.getItem('user') as string);
            if(basket)
            {
                thunkAPI.dispatch(setBasket(basket));
            }
            return current;
        } catch (error) {
            return thunkAPI.rejectWithValue({error: (error as any).data});

        }
    },
    {
        condition: () => {
            if(!localStorage.getItem('user')) {
                return false;
            }
        }
    }
)


export const accountSlice = createSlice({
    name: 'account',
    initialState,
    reducers: {
        signOutUser: (state) => {
            state.user = null;
            localStorage.removeItem('user');
         
        },
        setUser: (state, action) => {
            state.user = action.payload;
        }
    },
    extraReducers: (builder) => {
        builder.addCase(fetCurrentUser.rejected,  (state) => {
            state.user = null;
            state.current = null;
            localStorage.removeItem('user');
        });
        builder
            .addMatcher(isAnyOf(signInUser.fulfilled), (state, action) => {
                state.user = action.payload;
            })
            .addMatcher(isAnyOf(fetCurrentUser.fulfilled), (state, action) => {
                state.current = action.payload;
            });
        builder.addMatcher(isAnyOf(signInUser.rejected), (state, action) => {
            console.log(action.payload);
        });
       

    }

})

export const{signOutUser,setUser} = accountSlice.actions;


export const addressSlice = createSlice({
    name: 'address',
    initialState: initialAddressState,
    reducers: {
        setAddress: (state, action) => {
            state.address = action.payload;
        }

    },
    extraReducers: (builder) => {
        builder
            .addCase(addAddressAsync.pending, (state) => {
                state.status = 'loading';
            })
            .addCase(addAddressAsync.fulfilled, (state, action) => {
                state.status = 'idle';
                state.address = action.payload;
            })
    }
})