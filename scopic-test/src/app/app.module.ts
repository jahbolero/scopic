import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {LoginComponent} from './components/login/login.component'
import {ProductListComponent} from './components/product-list/product-list.component'
import { AppRoutingModule } from './app-routing.module';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProductComponent } from './components/product/product.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component'
import { AuthInterceptor } from './helpers/auth.interceptor';
import { CountdownModule } from 'ngx-countdown';
import { TimerValuePipe } from './pipes/timer-value.pipe';
import { HighestBidPipe } from './pipes/highest-bid.pipe';
import { ToLocalTimePipe } from './pipes/to-local-time.pipe';
import { DateTransformPipe } from './pipes/date-transform.pipe';





@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ProductListComponent,
    NavbarComponent,
    ProductComponent,
    AddProductComponent,
    AdminDashboardComponent,
    TimerValuePipe,
    HighestBidPipe,
    ToLocalTimePipe,
    DateTransformPipe,
    UserProfileComponent,

  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule ,
    HttpClientModule,
    AppRoutingModule,
    CountdownModule
  ],
  providers: [ { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },],
  bootstrap: [AppComponent]
})
export class AppModule { }
