import { configureStore } from '@reduxjs/toolkit';
import suppliersReducer from '../features/suppliers/supplierSlice';

export const store = configureStore ({
    reducer : {
        suppliers : suppliersReducer, 
    }
})