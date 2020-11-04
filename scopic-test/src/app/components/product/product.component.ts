import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Bid } from 'src/app/models/bid';
import { Product } from 'src/app/models/product';
import { BidService } from 'src/app/services/bid.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  product:Product;
  bidForm: FormGroup;
  error:string;
  success:string;
  disabled:boolean = false;
  constructor(private productService:ProductService, private route: ActivatedRoute, private formBuilder:FormBuilder,private bidSerivce:BidService) { }

  ngOnInit(): void {
    this.initForm();
    this.getProduct();
    this.bidSerivce.RetrieveBids().subscribe( (receivedObj: Bid) => { this.updateBids(receivedObj);});
    this.productService.RetrieveProduct().subscribe((receivedObj:Product)=>{this.updateProduct(receivedObj)})
  }
  initForm(){
    this.bidForm = this.formBuilder.group({
      bidAmount:['',[Validators.required,Validators.min(1)]],
    })
  }
  getProduct(){
    this.route.paramMap.subscribe(params=>{
      this.productService.GetProductById(params.get('productId')).subscribe(response=>{
        this.product = response;
      })
    });
  }
  submitBid(){
    if(this.bidForm.valid){
      this.disabled=true;
    this.bidSerivce.AddBid(this.product.productId,this.bidForm.value.bidAmount).subscribe(response=>{
      this.success= "Added bid!"
      this.error = null
      this.disabled=false;
    },error=>{
      this.error =error.error;
      this.success = null;
      this.disabled=false;
    })
    }
  }
  timerFinished(){
    this.getProduct();
  }
  updateBids(bid :Bid){
    bid.bidDate = new Date(bid.bidDate.toString().replace("Z",""));
    this.product.bids = [bid].concat(this.product.bids);
  }
  updateProduct(product :Product){
    product.expiryDate = new Date(product.expiryDate.toString().replace("Z",""));
    this.product.imgUrl="";
    this.product = product;
  }
  get bidAmount(){return this.bidForm.get("bidAmount")}
}
