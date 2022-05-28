import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { switchMap } from 'rxjs/operators';
import { User } from 'src/app/models/user';
import { Game } from 'src/app/models/game';
import { GameService } from 'src/app/services/game.service';
import { SubscriptionService } from 'src/app/services/subscription.service';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.scss']
})
export class ProductSearchComponent implements OnInit {

  public games: Game[];
  public filteredProducts: Game[];
  category: string;
  searchItem: string;
  showSpinner: boolean = false;
  isLoading: boolean;
  priceRange = Number.MAX_SAFE_INTEGER;
  userData$: Observable<User>;

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private subscriptionService: SubscriptionService) {
    
  }
  ngOnInit() {
    this.showSpinner=true;
    this.getAllGameData();
    this.userData$ = this.subscriptionService.userData;
  }

  getAllGameData() {
    this.gameService.getAllGames().pipe(switchMap((data: Game[]) => {
      this.filteredProducts = data;
      return this.route.queryParams;
    })).subscribe(params => {
      this.category = params.category;
      this.searchItem = params.item;
      this.subscriptionService.searchItemValue$.next(this.searchItem);
      this.activeSpinner();
      //  this.showSpinner=false;
      this.filterGameData();
    });
 }
 filterGameData() {
  const filteredData = this.filteredProducts.filter(b => b.price <= this.priceRange).slice();

  if (this.category) {
    this.games = filteredData.filter(b => b.category.toLowerCase() === this.category.toLowerCase());
  } else if (this.searchItem) {
    this.games = filteredData.filter(b => b.title.toLowerCase().indexOf(this.searchItem) !== -1
      || b.publisher.toLowerCase().indexOf(this.searchItem) !== -1);
  } else {
    this.games = filteredData;
  }
  this.isLoading = false;
}

 activeSpinner(){
  this.showSpinner= true;
  setTimeout(() =>{
    this.showSpinner= false;
  },1000);
 }
}
