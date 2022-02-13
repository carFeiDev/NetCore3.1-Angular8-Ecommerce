import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { Observable } from 'rxjs';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { Game } from 'src/app/models/game';
import { FavoritelistService } from 'src/app/services/favoritelist.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { SubscriptionService } from 'src/app/services/subscription.service';

@Component({
  selector: 'app-favoritelist',
  templateUrl: './favoritelist.component.html',
  styleUrls: ['./favoritelist.component.scss']
})
export class FavoritelistComponent implements OnInit {

  favoritelistItems$: Observable<Game[]>;
  isLoading: boolean;
  userId;
  private unsubscribe$ = new Subject<void>();

  constructor(
    private subscriptionService: SubscriptionService,
    private favoritelistService: FavoritelistService,
    private snackBarService: SnackbarService) {
    this.userId = localStorage.getItem('userId');
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.getFavoritelistItems();
  }

  getFavoritelistItems(): void {
    this.favoritelistItems$ = this.subscriptionService.favoritelistItem$;
    this.isLoading = false;
  }

  clearFavoritelist(): void {
    this.favoritelistService.clearFavoritelist(this.userId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(result => {
          this.subscriptionService.favoritelistItemcount$.next(result);
          this.snackBarService.showSnackBar('Favorios limpio!!!');
        }, error => {
          console.log('Error ocurred while deleting Favorite item : ', error);
        });
  }
}
