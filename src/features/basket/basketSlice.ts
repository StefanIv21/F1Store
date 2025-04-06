import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Basket } from "../../app/modules/basket";
import agent from "../../app/api/agent";
import { clear } from "console";

interface BasketState {
    basket: Basket | null;
    status: string;
}

const initialState: BasketState = {
    basket: null,
    status: 'idle'
};

export const addBasketItemAsync = createAsyncThunk<Basket, {productId:string ,quantity?:number}>(
    'basket/addBasketItemAsync',
    async ({productId,quantity = 1}) => {
        try
        {
            return await agent.Basket.addItem(productId,quantity);
        } catch (error) {
            console.error(error);
        }  
    }
);

export const removeBasketItemAsync = createAsyncThunk<void, {productId:string,quantity:number}>(
    'basket/removeBasketItemAsync',
    async ({productId,quantity}) => {
        try
        {
            await agent.Basket.removeItem(productId,quantity);
        } catch (error) {
            console.error(error);
        }  
    }
);


export const basketSlice = createSlice({
    name: 'basket',
    initialState,
    reducers: {
        setBasket: (state, action) => {
            state.basket = action.payload;
        },
        clearBasket: (state) => {
            state.basket = null;
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(addBasketItemAsync.pending, (state) => {
                state.status = 'loading';
            })
            .addCase(addBasketItemAsync.fulfilled, (state, action) => {
                state.status = 'idle'; 
                const userString = localStorage.getItem("user");
                let userData = userString ? JSON.parse(userString) : null;
                if (userData && userData.response && userData.response.user) {
                    userData.response.user.basket = action.payload; 
                    localStorage.setItem("user", JSON.stringify(userData));
                }
                state.basket = action.payload;
            })
            .addCase(removeBasketItemAsync.pending, (state) => {
                state.status = 'loading';
            })
            .addCase(removeBasketItemAsync.fulfilled, (state,action) => {
                state.status = 'idle';
                const {productId,quantity} = action.meta.arg;
                const itemIndex = state.basket?.items.findIndex(item => item.productId === productId);
                if (itemIndex === undefined || itemIndex === -1) {
                    return;
                }
                if (state.basket && itemIndex >= 0) {
                    state.basket.items[itemIndex].quantity -= quantity;
                    if (state.basket.items[itemIndex].quantity === 0) {
                        state.basket.items.splice(itemIndex, 1);
                    }
                }
                const userString = localStorage.getItem("user");
                let userData = userString ? JSON.parse(userString) : null;
                if (userData && userData.response && userData.response.user) {
                    userData.response.user.basket = state.basket; 
                    localStorage.setItem("user", JSON.stringify(userData));
                }
            });
    },
});

export const { setBasket,clearBasket} = basketSlice.actions;



