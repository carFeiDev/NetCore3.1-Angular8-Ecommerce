import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerOrder } from '../models/customerorder';


@Injectable({
  providedIn: 'root'
})
export class OrderService
{
  
  private serviceUrl = "api/customerorder";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAllCustomerOrderUser(userId: number) :Observable<CustomerOrder[]>{
    const url = `${this.baseUrl}${this.serviceUrl}/${userId}`;
    return this.http.get<CustomerOrder[]>(url,{});
  }

  myOrderDetails(userId: number) {
    const url = `${this.baseUrl}${this.serviceUrl}/${userId}`;
    return this.http.get<number>(url,{});
  }

}
