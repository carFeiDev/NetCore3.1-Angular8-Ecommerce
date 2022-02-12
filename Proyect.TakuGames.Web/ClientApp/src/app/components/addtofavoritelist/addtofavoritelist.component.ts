import { OnChanges } from '@angular/core';
import { Input } from '@angular/core';
import { Component } from '@angular/core';
import { Game } from 'src/app/models/game';
import { FavoritelistService } from 'src/app/services/favoritelist.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { SubscriptionService } from 'src/app/services/subscription.service';
import { User } from 'src/app/models/user';
import { Observable } from 'rxjs/internal/Observable';
import { LoginComponent } from '../login/login.component';
import { MatDialog } from '@angular/material/dialog';


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
  

  constructor(
    private favoritelistService: FavoritelistService,
    private subscriptionService: SubscriptionService,
    private snackBarService: SnackbarService,
    private dialog: MatDialog ) {
      this.userId = localStorage.getItem('userId');
    }

  ngOnChanges(): void  {
    this.subscriptionService.favoritelistItem$.pipe().subscribe(
      (bookData: Game[]) => {
        this.setFavourite(bookData);
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
    this.favoritelistService.toggleFavoritelistItem(this.userId, this.gameId).subscribe(
      () => {
        if (this.toggle) {
          this.snackBarService.showSnackBar('Producto agregado a favoritos');
        } else {
          this.snackBarService.showSnackBar('Producto eliminado de favoritos');
        }
      }, error => {
        console.log('Error ocurred while adding to favoritelist : ', error);
      });
    } else{
      this.opengDialog();
    }
    
  }

  opengDialog(): void{
    let dialog = this.dialog.open(LoginComponent,{
      height: '620px',
    })     
  }
}
