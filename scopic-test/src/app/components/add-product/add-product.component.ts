import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, NavigationEnd  } from '@angular/router';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  isAdd:boolean;
  imgPath:string;
  imgUrl:any;
  constructor(private route: ActivatedRoute, private formBuilder:FormBuilder) { }
  addProductForm:FormGroup

  ngOnInit(): void {
    this.initForm();
    this.isAdd = this.route.snapshot.queryParams.id == null ? true : false;
    console.log(this.isAdd);
  }
  initForm(){
    this.addProductForm = this.formBuilder.group({
      productName:['',[Validators.required,Validators.minLength(5), Validators.maxLength(30)]],
      productDescription:['',[Validators.required,Validators.minLength(5), Validators.maxLength(200)]],
      expiryDate:['',Validators.required],
      imageFile:[''],
    })
  }
  imagePreview(files) {
    if (files.length === 0)
      return;
 
    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
 
    var reader = new FileReader();
    this.imgPath = files;
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
      this.imgUrl = reader.result; 
    }
  }
  addProduct(){
    if(this.addProductForm.valid){

    }
  }

}
