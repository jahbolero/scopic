import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private  connection: any = new signalR.HubConnectionBuilder().withUrl(`${environment.apiUrl}/productSocket`)   // mapping to the chathub as in startup.cs
                                         .configureLogging(signalR.LogLevel.Information)
                                         .build();

  private sharedObj = new Subject<Product>();

  constructor(private http: HttpClient) {
    this.connection.onclose(async () => {
      await this.start();
    });
   this.connection.on("ReceiveProduct", (product) => { this.sharedObj.next(product); });
   this.start();          

  }
  public async start() {
    try {
      await this.connection.start();
      console.log("connected to product");
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    } 
  }
   public GetProducts(page?:string,searchString?:string, sort?:string):Observable<any>{
    let params = new HttpParams().set("Page",page).set("SearchString", searchString).set("Sort",sort); //Create new HttpParams
    return this.http.get<any>(`${environment.apiUrl}/Products/`,{params})
   }

   public GetProductById(productId:string){
   return this.http.get<any>(`${environment.apiUrl}/Products/${productId}`)
   }
   public DeleteProduct(productId:string){
    return this.http.delete<any>(`${environment.apiUrl}/Products/${productId}`)
   }
   public AddProduct(product:FormData){
     console.log(product.get("expiryDate"));
    return this.http.post<any>(`${environment.apiUrl}/Products/addProduct`,product);
   }
   public EditProduct(product:FormData){
    console.log(product.get("expiryDate"));
   return this.http.post<any>(`${environment.apiUrl}/Products/editProduct`,product);
  }
  
  public RetrieveProduct(): Observable<Product> {
    return this.sharedObj.asObservable();
  }

}
