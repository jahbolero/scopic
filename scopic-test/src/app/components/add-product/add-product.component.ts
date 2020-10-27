import { Component, OnInit } from '@angular/core';
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
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.isAdd = this.route.snapshot.queryParams.id == null ? true : false;
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

}
