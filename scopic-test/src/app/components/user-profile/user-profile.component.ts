import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  constructor(private userService:UserService) { }
  user:User;
  awardedProducts:Array<Product> = new Array<Product>();
  ongoingProducts:Array<Product> =new Array<Product>();;
  lostProducts:Array<Product>=new Array<Product>();;
  ngOnInit(): void {
    this.userService.GetProfile().subscribe(response=>{
    this.user = response.user;
    response.productsBidOn.forEach(product => {
    if(product.status != 1){
        this.ongoingProducts.push(product)
    }else if(product.userProduct.userId == this.user.userId){
        this.awardedProducts.push(product)
    }else if(product.status == 1 ){
        this.lostProducts.push(product);
      }
      
    });
    console.log(response);
    })
  }

}
