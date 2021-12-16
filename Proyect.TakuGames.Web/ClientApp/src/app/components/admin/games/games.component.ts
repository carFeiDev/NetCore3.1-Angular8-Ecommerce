
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GameFormComponent } from '../game-form/game-form.component';
import { GameService } from '../../../services/game.service';
import { Game } from '../../../models/game';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { DeleteGameComponent } from '../delete-game/delete-game.component';
import { SnackbarService } from '../../../services/snackbar.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss']
})
export class GamesComponent implements OnInit {
  private unsubscribe$ = new Subject<void>();
  public games: Game[];

  constructor(
    private gameService: GameService,
    public dialog: MatDialog,
    public snackBar: MatSnackBar,
    public snackbarService: SnackbarService ) {}

  ngOnInit(): void {
    this.refleshGames();
  }

  opengDialogAddGame(): void{
    let dialog = this.dialog.open(GameFormComponent,{
      height: '870',
      width: '600',
    })
    dialog.afterClosed().subscribe(()=> {
      this.refleshGames();
     });        
  }

  opengDialogEditGame(game:Game): void{
    let dialog = this.dialog.open(GameFormComponent,{
      height: '870',
      width: '600',
      data:game.gameId
    })
    dialog.afterClosed().subscribe(()=> {
      this.refleshGames();
     });        
  }

  openDialogDeleteGame(game: Game): void  {
    let dialog = this.dialog.open(DeleteGameComponent, {
      height: '500',
      width: '400',
      data:game.gameId
    })
    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.gameService.deleteGame(game.gameId).subscribe(response => {
          if (response) {
            this.snackbarService.showSnackBar('El juego se ha eliminado correctamente');
            this.refleshGames();
          }
        })
      }
    });
  }

  refleshGames(): void  {
    this.gameService.getAllGames()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((data: Game[]) => {
        this.games = Object.values(data);
      }, error => {
        console.log('Error ocurred while ferching game details:', error);
      });
  }
}



