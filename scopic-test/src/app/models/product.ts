import { Bid } from './bid';
import { UserProduct } from './userProduct';

export class Product {
    productId: string;
    productName: string;
    productDescription: string;
    imgUrl:string;
    expiryDate:Date;
    uploadDate:Date;
    bids:Array<Bid>;
    status:number;
    userProduct:UserProduct;
}