import './App.css';
import { Routes, Route } from 'react-router-dom';
import SuppliersListView from './features/suppliers/views/supplierListView';

function App() {
  return (
    <div className="app-container">
      <header className="h-full">
        <h3 className="font-bold text-3xl text-amber-400 bg-amber-800 my-2 rounded-md px-2 py-1">Coffeeshop</h3>
        <Routes>
          <Route path='/' element={<SuppliersListView></SuppliersListView>}></Route>
          <Route path='/suppliers' element={<SuppliersListView></SuppliersListView>}></Route>
        </Routes>
      </header>
    </div>
  );
}

export default App;
