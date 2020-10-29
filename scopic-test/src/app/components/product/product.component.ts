import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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
      this.getProduct();
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
  get bidAmount(){return this.bidForm.get("bidAmount")}
}
