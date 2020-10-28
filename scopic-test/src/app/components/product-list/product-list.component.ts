import { Component, OnInit } from '@angular/core';
import { Bid } from 'src/app/models/bid';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';
import {GetDateDifferenceInSeconds, GetHighestBid} from '../../helpers/transform';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products:Array<Product>;
  searchString:string;
  sort:string;
  page:Number;
  dataLoaded:boolean = false;
  constructor(private productService:ProductService) { }

  ngOnInit(): void {
    this.searchString = "";
    this.sort = "";
    this.page = 1;
    this.GetProducts();
    
  }
  number = 0;
  call(){
    console.log(this.number++);
  }
  GetProducts(){
    this.productService.GetProducts(this.page.toString(),this.searchString,this.sort).subscribe(data=>{
      this.products = data
      this.dataLoaded = true;
      console.log(this.dataLoaded);
    })
  }
  timerValue = GetDateDifferenceInSeconds;
  getHighestBid = GetHighestBid

}
