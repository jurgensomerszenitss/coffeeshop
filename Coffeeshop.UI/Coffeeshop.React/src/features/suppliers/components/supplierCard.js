/*
import { Link } from 'react-router-dom';
import { useDispatch } from "react-redux";
import { fetchItem } from '../supplierSlice';
import config from '../../config';

const SupplierCard = ({ section, supplier }) => {
    const dispatch = useDispatch();

    const select = () => {
        dispatch(fetchItem( { section, supplier:supplier.name})); 
    }
     
    return (
        <div className="card h-100 text-center">
            <div className="card-header">
                <h5 className="card-title">{category.name}</h5>
            </div>   
            <div className="card-body align-middle">
                <Link onClick={() => select()} to={category.name}>
                    <img src={imageUrl} className="card-img"/>
                </Link>  
            </div>
            <div className="card-footer">
                <div className="row">
                    <div className="col-3 col-md-6">
                        {category.albums > 0 && <Link onClick={() => select()} to={`/${rootCategoryUrl}/albums`}><i className="bi bi-images"></i> {category.albums}</Link>}
                        {category.albums == 0 && <span className="text-warning"><i className="bi bi-images"></i> {category.albums}</span>}
                    </div>
                    <div className="col-3  col-md-6">
                        {category.pictures > 0 && <Link onClick={() => select()} to={`/${rootCategoryUrl}/pictures`}><i className="bi bi-image"></i> {category.pictures}</Link>}
                        {category.pictures == 0 && <span className="text-warning"><i className="bi bi-image"></i> {category.pictures}</span>}
                    </div>
                    <div className="col-3  col-md-6">
                        {category.videos > 0 && <Link onClick={() => select()} to={`/${rootCategoryUrl}/videos`}><i className="bi bi-camera-video"></i> {category.videos}</Link>}
                        {category.videos == 0 && <span className="text-warning"><i className="bi bi-camera-video"></i> {category.videos}</span>}
                    </div>
                    <div className="col-3  col-md-6">
                        {category.vr > 0 && <Link onClick={() => select()} to={`/${rootCategoryUrl}/vr`}><i className="bi bi-badge-vr"></i> {category.vr}</Link>}
                        {category.vr == 0 && <span className="text-warning"><i className="bi bi-badge-vr"></i> {category.vr}</span>}
                    </div>
                </div>
            </div>
        </div> 
    )
}
export default CategoryCard
*/