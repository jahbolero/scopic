import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {LoginComponent} from './components/login/login.component'
import {ProductListComponent} from './components/product-list/product-list.component'

import { NavbarComponent } from './components/navbar/navbar.component';
import { ProductComponent } from './components/product/product.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { RouterModule } from '@angular/router';




@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ProductListComponent,
    NavbarComponent,
    ProductComponent,
    AddProductComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path:'login',component:LoginComponent},
      {path:'products',component:ProductListComponent},
      {path:'product/:productId', component:ProductComponent},
      {path:'addProduct',component:AddProductComponent},
      {path:'editProduct', component:AddProductComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
