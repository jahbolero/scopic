import { Bid } from './bid';

export class Product {
    productId: string;
    productName: string;
    productDescription: string;
    imgUrl:string;
    expiryDate:Date;
    uploadDate:Date;
    bids:Array<Bid>;
}