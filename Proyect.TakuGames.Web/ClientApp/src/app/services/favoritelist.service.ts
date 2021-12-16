import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/internal/operators/map';
import { Game } from '../models/game';
import { SubscriptionService } from './subscription.service';

@Injectable({
  providedIn: 'root'
})
export class FavoritelistService {

  baseURL: string;

  constructor(
    private http: HttpClient,
    private subscriptionService: SubscriptionService) {
    this.baseURL = '/api/favoritelist/';
  }

  toggleFavoritelistItem(userId: number, gameId: number) {
    return this.http.post<Game[]>(this.baseURL + `ToggleFavoritelist/${userId}/${gameId}`, {})
      .pipe(map((response: Game[]) => {
        this.setFavoritelist(response);
        return response;
      }));
  }

  getFavoritelistItems(userId: number) {
    return this.http.get(this.baseURL + userId)
      .pipe(map((response: Game[]) => {
        this.setFavoritelist(response);
        return response;
      }));
  }

  setFavoritelist(response: Game[]) {
    this.subscriptionService.favoritelistItemcount$.next(response.length);
    this.subscriptionService.favoritelistItem$.next(response);
  }

  clearFavoritelist(userId: number) {
    return this.http.delete<number>(this.baseURL + `${userId}`, {}).pipe(
      map((response: number) => {
        this.subscriptionService.favoritelistItem$.next([]);
        return response;
      })
    );
  }
}
