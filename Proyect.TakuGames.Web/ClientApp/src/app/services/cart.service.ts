import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Game } from '../models/game';
import { ShoppingCart } from '../models/shoppingcart';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  cartItemCount = 0;
  baseUrl: string;

  constructor(private http: HttpClient) {

    this.baseUrl = '/api/shoppingcart/';
  }

  addItemToCart(userId: number, gameId: number): Observable<number>{
    return this.http.post<number>(this.baseUrl + `addToCart/${userId}/${gameId}`, {});
  }
  getCartItems(userId: number): Observable<ShoppingCart[]> {
    return this.http.get<ShoppingCart[]>(this.baseUrl + userId, {});
  }

  removeCartItems(userId: number, gameId: number): Observable<number> {
    return this.http.delete<number>(this.baseUrl + `${userId}/${gameId}`, {});
  }

  deleteOneCartItem(userId: number, gameId: number): Observable<number> {
    return this.http.put<number>(this.baseUrl + `${userId}/${gameId}`, {});
  }

  clearCart(userId: number): Observable<number> {
    return this.http.delete<number>(this.baseUrl + `${userId}`, {});
  }

  setCart(oldUserId: number, newUserId: number): Observable<any> {
    return this.http.get(this.baseUrl + `SetShoppingCart/${oldUserId}/${newUserId}`, {})
      .pipe(map((response: any) => {
        this.cartItemCount = response;
        return response
      }));
  }

  BuyCart(userId:number): Observable<number> {
    return this.http.post<number>(this.baseUrl + `BuyCart/${userId}`, {});
  }
 
  getAllOrderruItems(userId: number): Observable<any[]> {
    return this.http.get(this.baseUrl + `${userId}`, {})
      .pipe(map((response: any[]) => {
        return response;
      }));
  }
}


