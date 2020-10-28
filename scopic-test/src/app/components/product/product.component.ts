import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { GetHighestBid, ConvertToLocalTime } from 'src/app/helpers/transform';
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
  constructor(private productService:ProductService, private route: ActivatedRoute, private formBuilder:FormBuilder,private bidSerivce:BidService) { }

  ngOnInit(): void {
    this.initForm();
    this.getProduct();
  }
  initForm(){
    this.bidForm = this.formBuilder.group({
      bidAmount:['',[Validators.required,Validators.min(1)]],

    })
  }
  getProduct(){
    this.route.paramMap.subscribe(params=>{
      this.productService.GetProductById(params.get('productId')).subscribe(response=>{
        console.log(response);
        this.product = response;
      })
    });
  }
  submitBid(){
    if(this.bidForm.valid){
      console.log(this.bidForm.value)
    this.bidSerivce.AddBid(this.product.productId,this.bidForm.value.bidAmount).subscribe(response=>{
      this.getProduct();
      this.success= "Added bid!"
      this.error = null
    },error=>{
      console.log(error.error);
      this.error =error.error;
      this.success = null;
    })
    }
    
  }
  get bidAmount(){return this.bidForm.get("bidAmount")}
  convertToLocalTime = ConvertToLocalTime;
  getHighestBid = GetHighestBid;
}
