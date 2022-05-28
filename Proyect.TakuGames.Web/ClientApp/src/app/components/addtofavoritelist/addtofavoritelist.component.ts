import { OnChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Input } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Component } from '@angular/core';
import { LoginComponent } from '../login/login.component';
import { Game } from 'src/app/models/game';
import { User } from 'src/app/models/user';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { SubscriptionService } from 'src/app/services/subscription.service';
import { FavoritelistService } from 'src/app/services/favoritelist.service';

@Component({
  selector: 'app-addtofavoritelist',
  templateUrl: './addtofavoritelist.component.html',
  styleUrls: ['./addtofavoritelist.component.scss']
})
export class AddtofavoritelistComponent implements OnChanges {

  @Input()
  gameId: number;
  @Input()
  showButton = false;
  userId:any;
  toggle: boolean;
  buttonText: string;
  favoritelistItems: Game[];
  userData$: Observable<User>;
  userDataSubcription: any;
  userData = new User();

  constructor(private favoritelistService: FavoritelistService,
    private subscriptionService: SubscriptionService,
    private snackBarService: SnackbarService,
    private dialog: MatDialog ) {
      this.userId = localStorage.getItem('userId');
    }

  ngOnChanges(): void  {
    this.subscriptionService.favoritelistItem$.pipe()
      .subscribe((gameData: Game[]) => {
        this.setFavourite(gameData);
        this.setButtonText();
      });
    this.userDataSubcription = this.subscriptionService.userData.asObservable()
      .subscribe(data => {
        this.userData = data;
      });   
  }

  setFavourite(gameData: Game[]): void {
    const favGame = gameData.find(f => f.gameId === this.gameId);
    if (favGame) {
      this.toggle = true;
    } else {
      this.toggle = false;
    }
  }

  setButtonText(): void {
    if (this.toggle) {
      this.buttonText = 'Eliminar de la lista de Favoritos';
    } else {
      this.buttonText = 'Agregar a la lista de Favoritos';
    }
  }

  toggleValue(): void {
    if(this.userData.isLoggedIn){
    this.toggle = !this.toggle;
    this.setButtonText();
    this.favoritelistService.toggleFavoritelistItem(this.userId, this.gameId)
      .subscribe(() => {
        if (this.toggle) {
          this.snackBarService.showSnackBar('Producto agregado a favoritos');
        } else {
          this.snackBarService.showSnackBar('Producto eliminado de favoritos');
        }
      }, error => {
        console.log('Error ocurred while adding to favoritelist : ', error);
      });
    } else {
      this.openLoginDialog();
    }  
  }

  openLoginDialog(): void{
    let dialog = this.dialog.open(LoginComponent,{
      height: '620px',
    })     
  }
}
