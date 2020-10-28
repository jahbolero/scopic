import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BidService {

  constructor(private http:HttpClient) { }
  public AddBid(productId:string, bidAmount:number ){
    return this.http.post<any>(`${environment.apiUrl}/Bids/addBid`,{ProductId:productId, BidAmount:bidAmount})
   }
}
