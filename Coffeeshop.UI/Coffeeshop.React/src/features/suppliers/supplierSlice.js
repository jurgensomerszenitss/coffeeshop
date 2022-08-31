import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import config from '../../config'

export const fetchList = createAsyncThunk('suppliers/search', async (req) => {
    const url = `${config.API_URL}/supplier`;
    const response = await axios.get(url)
    return response.data
}) 

// export const deleteItem = createAsyncThunk('suppliers/delete', async (req) => {
//     const { id } = req;
//     const url = `${config.API_URL}/supplier/${id}`;
//     const response = await axios.delete(url)
//     return response
// }) 

const initialState = {
    list : [],
    listStatus : 'idle',
    count : 0,
    sortBy : "asc",
    selected : {}
};

const supplierSlice = createSlice({
    name : 'suppliers',
    initialState,
    reducers:{
        onSelected : (state, action) => {
            state.selected = action.payload;
        },
        onListReset : (state) => {
            state.list = [];
            state.listStatus = "idle";
        }
    },
    extraReducers(builder) {
        builder.addCase(fetchList.pending, (state, action) => {
            state.listStatus = 'loading'
        })
        .addCase(fetchList.fulfilled, (state, action) => {
            state.listStatus = 'succeeded'
            state.list = action.payload  
            state.count = action.payload.length          
        })
        .addCase(fetchList.rejected, (state, action) => {
            state.listStatus = 'failed' 
            console.error(action.error.message)
        })
        // .addCase(deleteItem.pending, (state, action) => {
        //     state.listStatus = 'loading'
        // })
        // .addCase(deleteItem.fulfilled, (state, action) => {
        //     state.listStatus = 'succeed'
        //     dispatchEvent()
        // })
        // .addCase(deleteItem.rejected, (state, action) => {
        //     state.listStatus = 'failed'
        //     console.log('failed')
        // })        
    }
})

export const getList = (state) => state.suppliers.list;
export const getListStatus = (state) => state.suppliers.listStatus;

export const { onSelected, onListReset } = supplierSlice.actions;
export default supplierSlice.reducer;