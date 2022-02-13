import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Game } from 'src/app/models/game';
import { SubscriptionService } from 'src/app/services/subscription.service';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  public games: Game[];
  searchControl = new FormControl();
  filteredGames: Observable<Game[]>;
  
  constructor(
    private gameService: GameService,
    private router: Router,
    private subscriptionService: SubscriptionService) { }

  ngOnInit(): void {
    this.loadGameData();
    this.setSearchControlValue();
    this.filterGameData();
  }

  searchStore(event) {
    const searchItem = this.searchControl.value;
    if (searchItem !== '') {
      this.router.navigate(['/search'], {
        queryParams: {
          item: searchItem.toLowerCase()
        }
      });
    }
  }

  private loadGameData() {
    this.gameService.games$.subscribe(
      (data: Game[]) => {
        this.games = data;
      }
    );
  }

  private setSearchControlValue() {
    this.subscriptionService.searchItemValue$.subscribe(
      data => {
        if (data) {
          this.searchControl.setValue(data);
        } else {
          this.searchControl.setValue('');
        }
      }
    );
  }

  private filterGameData() {
    this.filteredGames = this.searchControl.valueChanges
      .pipe(
        startWith(''),
        map(value => value.length >= 1 ? this._filter(value) : [])
      );
  }

  private _filter(value: string) {
    const filterValue = value.toLowerCase();
    return this.games.filter(option => option.title.toLowerCase().includes(filterValue)
      || option.publisher.toLowerCase().includes(filterValue));
  }

}
