import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) {

   }
   public GetProducts(page?:string,searchString?:string, sort?:string):Observable<any>{
    
    let params = new HttpParams().set("Page",page).set("SearchString", searchString).set("Sort",sort); //Create new HttpParams
    return this.http.get<any>(`${environment.apiUrl}/Products/`,{params})
   }

   public GetProductById(productId:string){
   return this.http.get<any>(`${environment.apiUrl}/Products/${productId}`)
   }
  
}
