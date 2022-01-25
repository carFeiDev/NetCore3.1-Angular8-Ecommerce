import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { SubscriptionService } from './subscription.service';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  oldUserId:number;
  private serviceUrl = "api/login";
  constructor(private http: HttpClient,
              private userService: UserService,
              private subscriptionService: SubscriptionService,
              @Inject('BASE_URL') private baseUrl: string) { }

  login(user:User){
    const url = `${this.baseUrl}${this.serviceUrl}`;
    return this.http.post<any>(url, user)
        .pipe(map(response => {
        if (response && response.token) {
          this.oldUserId = parseInt(localStorage.getItem('userId'));
          localStorage.setItem('authToken', response.token);
          this.setUserDetails();
          localStorage.setItem('userId', response.userDetails.userId);
          this.userService.cartItemcount$.next(response.carItemCount);
        }
        return response;
      }));

  }

  setUserDetails() {
    if (localStorage.getItem('authToken')) {
      const userDetails = new User();
      const decodeUserdetails = JSON.parse(atob(localStorage.getItem('authToken').split('.')[1]));

      userDetails.userId = decodeUserdetails.userid;
      userDetails.userName = decodeUserdetails.sub;
      userDetails.userTypeId = Number(decodeUserdetails.userTypeId);
      userDetails.isLoggedIn = true;
      userDetails.image='/UserImage/' + decodeUserdetails.userImage;

      this.subscriptionService.userData.next(userDetails);
    }
  }

  logout() {
    localStorage.clear();
    this.resetSubscription();
    this.setTempUserId();
  }

  setTempUserId() {
    if (!localStorage.getItem('userId')) {
      const tempUserID = this.generateTempUserId();
      localStorage.setItem('userId', tempUserID.toString());
    }
  }

  generateTempUserId() {
    return Math.floor(Math.random() * (99999 - 11111 + 1) + 12345);
  }
  
  resetSubscription() {
    this.subscriptionService.userData.next(new User());
    this.subscriptionService.favoritelistItem$.next([]);
    this.subscriptionService.favoritelistItemcount$.next(0);
    this.subscriptionService.cartItemcount$.next(0);

  }
}
