import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IGame } from '../data/IGame';
import { tap } from 'rxjs/operators';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class GamesService {

  private filePath: any;
  private downloadURL: Observable<string>;

  private apiURL = "api/games";
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getGames(): Observable<IGame[]> {
    const url = `${this.baseUrl}${this.apiURL}`;
    return this.http.get<IGame[]>(url);
  }
  getGame<Data>(id: number): Observable<IGame> {
    const url = `${this.baseUrl}${this.apiURL}/${id}`;
    return this.http.get<IGame>(url);
  }
  addGame(game: IGame): Observable<IGame> {
    const url = `${this.baseUrl}${this.apiURL}`;
    return this.http.post<IGame>(url, game);
  }
  editGame(game: IGame): Observable<IGame> {
    const url = `${this.baseUrl}${this.apiURL}/${game.gameId}`;
    return this.http.put<IGame>(url, game);
  }
  deleteGame(id: number): Observable<IGame> {
    const url = `${this.baseUrl}${this.apiURL}/${id}`;
    return this.http.delete<IGame>(url);
  }
}



