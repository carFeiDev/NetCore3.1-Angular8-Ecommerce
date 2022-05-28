
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Game } from '../../../models/game';
import { GameFormComponent } from '../game-form/game-form.component';
import { DeleteGameComponent } from '../delete-game/delete-game.component';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { SnackbarService } from '../../../services/snackbar.service';
import { GameService } from '../../../services/game.service';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-old-manager-games',
  templateUrl: './old-manager-games.component.html',
  styleUrls: ['./old-manager-games.component.scss']
})

export class OldManagerGamesComponent implements OnInit {
  
  displayedColumns: string[] = ['id','coverFileName', 'title', 'description','developer','publisher','platform','category', 'price', 'operation'];
  dataSource = new MatTableDataSource<Game>();
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  private unsubscribe$ = new Subject<void>();

  constructor(private gameService: GameService,
              public dialog: MatDialog,
              public snackBar: MatSnackBar,
              public snackbarService: SnackbarService ) {}

  ngOnInit(): void {
    this.refleshGames();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  refleshGames(): void  {
    this.gameService.getAllGames()
      .pipe(takeUntil(this.unsubscribe$))
        .subscribe((data: Game[]) => {
          this.dataSource.data = Object.values(data);
        }, error => {
          console.log('Error ocurred while ferching game details:', error);
        });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
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
        this.gameService.deleteGame(game.gameId)
          .subscribe(response => {
            if (response) {
              this.snackbarService.showSnackBar('El juego se ha eliminado correctamente');
              this.refleshGames();
            }
        })
      }
    });
  } 
}
