import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShoppingCart } from '../models/shoppingcart';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  
  private serviceUrl = "api/shoppingcart";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){}

  addItemToCart(userId: number, gameId: number): Observable<number>{
    const url = `${this.baseUrl}${this.serviceUrl}/${"addToCart"}/${userId}/${gameId}`;
    return this.http.post<number>(url,{});
  }
  getCartItems(userId: number): Observable<ShoppingCart[]> {
    const url = `${this.baseUrl}${this.serviceUrl}/${userId}`
    return this.http.get<ShoppingCart[]>(url,{});
  }

  removeCartItems(userId: number, gameId: number): Observable<number> {
    const url = `${this.baseUrl}${this.serviceUrl}/${userId}/${gameId}`;
    return this.http.delete<number>(url,{});
  }

  deleteOneCartItem(userId: number, gameId: number): Observable<number> {
    const url = `${this.baseUrl}${this.serviceUrl}/${userId}/${gameId}`;
    return this.http.put<number>(url,{});
  }

  clearCart(userId: number): Observable<number> {
    const url = `${this.baseUrl}${this.serviceUrl}/${userId}`;
    return this.http.delete<number>(url,{});
  }

  setCart(oldUserId: number, newUserId: number): Observable<any> {
    const url = `${this.baseUrl}${this.serviceUrl}/${"SetShoppingCart"}/${oldUserId}/${newUserId}`;
    return this.http.get<number>(url,{});
  }

  BuyCart(userId:number): Observable<number> {
    const url = `${this.baseUrl}${this.serviceUrl}/${"BuyCart"}/${userId}`;
    return this.http.post<number>(url,{});
  }
}


