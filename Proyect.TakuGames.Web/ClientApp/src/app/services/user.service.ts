import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl: string;
  cartItemcount$ = new Subject<any>();

  constructor(private http: HttpClient) {
    this.baseUrl = '/api/user/';
  }

  registerUser(userdetails) {
    return this.http.post(this.baseUrl, userdetails)
      .pipe(map(response => {
        return response;
      }));
  }
  
  getCartItemCount(userId: number): Observable<number> {
    return this.http.get<number>(this.baseUrl + userId);
  }

}
