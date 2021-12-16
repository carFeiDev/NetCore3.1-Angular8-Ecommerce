import { Injectable } from '@angular/core';
import { BehaviorSubject,Subject } from 'rxjs';
import { Game } from '../models/game';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {
  userData = new BehaviorSubject<User>(new User());
  cartItemcount$ = new Subject<number>();
  searchItemValue$ = new BehaviorSubject<string>('');
  favoritelistItemcount$ = new Subject<number>();
  favoritelistItem$ = new BehaviorSubject<Game[]>([]);
  constructor() { }
}
