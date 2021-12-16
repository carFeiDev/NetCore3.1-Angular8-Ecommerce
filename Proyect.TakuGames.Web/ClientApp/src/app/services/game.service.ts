
import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { shareReplay } from 'rxjs/operators';
import { Game} from '../models/game'

@Injectable({
  providedIn: 'root'
})
export class GameService {
  
  games$ = this.getAllGames().pipe(shareReplay(1));
  private apiURL = "api/game";
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getCategories(): Observable<Game[]>{
    const url = `${this.baseUrl}${this.apiURL}/${"GetCategoriesList"}`;
    return this.http.get<Game[]>(url);
  }

  getAllGames(): Observable<Game[]> {
    const url = `${this.baseUrl}${this.apiURL}`;
    return this.http.get<Game[]>(url);
  }

  getGameById<Data>(id:number): Observable<Game> {
    const url = `${this.baseUrl}${this.apiURL}/${id}`;
    return this.http.get<Game>(url);
  }
 
  addGame(game:FormData): Observable<Game> {
    const url = `${this.baseUrl}${this.apiURL}`;
    return this.http.post<Game>(url, game);
  }

  updateGameDetails(game: FormData, id:number): Observable<Game[]> {
    const url = `${this.baseUrl}${this.apiURL}/${id}`;
    return this.http.put<Game[]>(url, game);
  }

  deleteGame(id: number): Observable<Game> {
    const url = `${this.baseUrl}${this.apiURL}/${id}`;
    return this.http.delete<Game>(url);
  }

  getsimilarGames(gameId: number) {
    const url = `${this.baseUrl}${this.apiURL}/${"GetSimilarGames"}/${gameId}`;
    return this.http.get<Game[]>(url);
  }
}
