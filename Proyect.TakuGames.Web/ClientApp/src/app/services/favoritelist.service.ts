import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Inject } from '@angular/core';
import { map } from 'rxjs/internal/operators/map';
import { Game } from '../models/game';
import { SubscriptionService } from './subscription.service';

@Injectable({
  providedIn: 'root'
})
export class FavoritelistService {

  private serviceUrl = "api/favoritelist";
  
  constructor(private http: HttpClient,private subscriptionService: SubscriptionService, @Inject('BASE_URL') private baseUrl: string) { }

  toggleFavoritelistItem(userId: number, gameId: number) {
    const url = `${this.baseUrl}${this.serviceUrl}/${"ToggleFavoritelist"}/${userId}/${gameId}`;
    return this.http.post<Game[]>(url,{})
      .pipe(map((response: Game[]) => {
        this.setFavoritelist(response);
        return response;
       }));
  }

  getFavoritelistItems(userId: number) {
    const url = `${this.baseUrl}${this.serviceUrl}/${userId}`;
    return this.http.get(url)
      .pipe(map((response: Game[]) => {
        this.setFavoritelist(response);
        return response;
    }));
  }

  clearFavoritelist(userId: number) {
    const url = `${this.baseUrl}${this.serviceUrl}/${userId}`;
    return this.http.delete<number>(url, {})
      .pipe(map((response: number) => {
        this.subscriptionService.favoritelistItem$.next([]);
        return response;
    }));
  }

  setFavoritelist(response: Game[]) {
    this.subscriptionService.favoritelistItemcount$.next(response.length);
    this.subscriptionService.favoritelistItem$.next(response);
  }
}
