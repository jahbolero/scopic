import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product';
import { AuthService } from 'src/app/services/auth.service';
import { ProductService } from 'src/app/services/product.service';
import {GetHighestBid} from '../../helpers/transform';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products:Array<Product>;
  searchString:string;
  success:string;
  sort:string;
  page:number;
  role:string;
  constructor(private productService:ProductService,private authService:AuthService) { }

  ngOnInit(): void {
    this.searchString = "";
    this.refreshPagination()
    this.getProducts();
    this.role = this.authService.userValue.role;
  }
  getProducts(){
    this.productService.GetProducts(this.page.toString(),this.searchString,this.sort).subscribe(response=>{
      this.products = response;
    })
  }

  searchProduct(searchString:string){
    this.searchString = searchString;
    this.refreshPagination();

    this.getProducts();
  }
  sortProducts(){
  this.sort = this.sort ==""? "ASC":this.sort=="ASC"?"DESC":"ASC";
  this.page = 1;
  this.getProducts();
  } 
  deleteProduct(productId:string){
    this.productService.DeleteProduct(productId).subscribe(response=>{
      this.getProducts();
      this.products = this.products.filter(product => product.productId != productId)
      this.success = "Successfully Deleted";
      setTimeout(()=>{
      this.success =null;
      },5000)
    },error=>{
      alert(error.error);
    });

  }
  nextPage(){
  this.page = this.page + 1;
  this.getProducts();
  }
  previousPage(){
  this.page = this.page - 1;
  this.getProducts();
  }
  refreshPagination(){
    this.page = 1;
    this.sort ="";
  }
  getHighestBid = GetHighestBid

}
