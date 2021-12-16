import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CustomerOrder } from '../models/customerorder';


@Injectable({
  providedIn: 'root'
})
export class OrderService
{
  baseUrl: string;
  constructor(private http: HttpClient) {
    this.baseUrl = '/api/customerorder/';
  }

  getAllCustomerOrderUser(userId: number) :Observable<CustomerOrder[]>{
    return this.http.get<CustomerOrder[]>(this.baseUrl + `${userId}`, {})
  }

  myOrderDetails(userId: number) {
    return this.http.get(this.baseUrl + userId);
  }

}
