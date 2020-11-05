import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import * as signalR from '@microsoft/signalr'; 
import { Subject,Observable } from 'rxjs';
import { Bid } from '../models/bid';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class BidService {

  private  connection: any = new signalR.HubConnectionBuilder().withUrl(`${environment.apiUrl}/bidSocket`)   // mapping to the chathub as in startup.cs
                                         .configureLogging(signalR.LogLevel.Information)
                                         .build();

  private sharedObj = new Subject<Bid>();

  constructor(private http:HttpClient) { 
    this.connection.onclose(async () => {
      await this.start();
    });
   this.connection.on("ReceiveBid", (bid) => { this.sharedObj.next(bid); });
   this.start();          

  }
  public async start() {
    try {
      await this.connection.start();
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    } 
  }

  public AddBid(productId:string, bidAmount:number ){
    return this.http.post<any>(`${environment.apiUrl}/Bids/addBid`,{ProductId:productId, BidAmount:bidAmount})
   }

   public RetrieveBids(): Observable<Bid> {
    return this.sharedObj.asObservable();
  }
}
