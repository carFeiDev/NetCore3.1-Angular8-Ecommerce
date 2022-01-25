import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Inject } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User} from '../models/user'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  cartItemcount$ = new Subject<any>();
  private apiURL = "api/user";
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  registerUser(userdetails) {
    const url = `${this.baseUrl}${this.apiURL}`;
    return this.http.post<User>(url, userdetails);
  }
  
  getCartItemCount(Id: number): Observable<number> {
    const url = `${this.baseUrl}${this.apiURL}/${Id}`;
    return this.http.get<number>(url);
  }
  getUserById<Use>(UserId:number): Observable<User> {
    const url = `${this.baseUrl}${this.apiURL}/${"GetUser"}/${UserId}`;
    return this.http.get<User>(url);
  }

}
