import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Inject } from '@angular/core';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  private serviceUrl = "api/CheckOut";
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  placeOrder(userId: number, checkedOutItems: Order) {
    const url = `${this.baseUrl}${this.serviceUrl}/${userId}`;
    return this.http.post<number>(url,checkedOutItems);
  }
}
