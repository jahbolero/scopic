import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, NavigationEnd  } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  isAdd :boolean
  imgPath:string;
  imgUrl:any;
  constructor(private route: ActivatedRoute, private formBuilder:FormBuilder,private productService:ProductService, private router:Router) { }
  addProductForm:FormGroup
  product:Product
  success:string;
  error:string;
  disabled:boolean = false;

  ngOnInit(): void {
    this.initForm();
    this.route.paramMap.subscribe(params=>{
      this.isAdd =params.get('productId') == null ? true : false;

      if(!this.isAdd){
        this.productService.GetProductById(params.get('productId')).subscribe(response=>{
          this.product = response;
        });
      }
     
    })
  }
  initForm(){
    this.addProductForm = this.formBuilder.group({
      productName:['',[Validators.required,Validators.minLength(5), Validators.maxLength(30)]],
      productDescription:['',[Validators.required,Validators.minLength(5), Validators.maxLength(200)]],
      expiryDate:['',Validators.required],
      imageFile:[null],
    })
  }
  imagePreview(files) {
    if (files.length === 0)
      return;
 
    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    console.log(files[0]);
    var reader = new FileReader();
    this.imgPath = files;
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
      this.imgUrl = reader.result; 
    }
    this.addProductForm.patchValue({
      imageFile: files[0]
    });
    this.addProductForm.get('imageFile').updateValueAndValidity()
  }
  addProduct(){
    if(this.addProductForm.valid){
      
      this.disabled = true;
      var formData: any = new FormData();
      var utcDate = new Date(this.addProductForm.value.expiryDate).toISOString();
      formData.append("productName", this.addProductForm.value.productName);
      formData.append("productDescription", this.addProductForm.value.productDescription);
      formData.append("expiryDate", utcDate);
      formData.append("imgFile", this.addProductForm.value.imageFile);
    this.productService.AddProduct(formData).subscribe(response=>{
      this.router.navigate(['/admin'])
      this.disabled = false;
    },error=>{
      this.success =null;
      this.error = error.error;
      this.disabled = false;
    })
    }
  }
  editProduct(){
    if(this.addProductForm.valid){
      this.disabled = true;
      var formData: any = new FormData();
      var utcDate = new Date(this.addProductForm.value.expiryDate).toISOString();
      formData.append("productId",this.product.productId);
      formData.append("productName", this.addProductForm.value.productName);
      formData.append("productDescription", this.addProductForm.value.productDescription);
      formData.append("expiryDate", utcDate);
      formData.append("imgFile", this.addProductForm.value.imageFile);
    this.productService.EditProduct(formData).subscribe(
      response=>{
      this.success =response.message;
      this.error = null;
      this.disabled = false;
    },error=>{
      this.error = error.error;
      this.success = null;
      this.disabled = false;
    })
    }
  }
  get productName(){return this.addProductForm.get("productName")}
  get productDescription(){return this.addProductForm.get("productDescription")}
  get expiryDate(){return this.addProductForm.get("expiryDate")}
  
}
