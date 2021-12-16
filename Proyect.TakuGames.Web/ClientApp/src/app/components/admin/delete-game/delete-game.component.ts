
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Game } from '../../../models/game';
import { GameService } from '../../../services/game.service';

@Component({
  selector: 'app-delete-game',
  templateUrl: './delete-game.component.html',
  styleUrls: ['./delete-game.component.scss']
})
export class DeleteGameComponent implements OnInit {
  public gameData = new Game();

  constructor(
    public dialogRef: MatDialogRef<DeleteGameComponent>,
    @Inject(MAT_DIALOG_DATA) public gameId: number,
    private gameService: GameService) { }
    
  ngOnInit(): void {
    this.gameService.getGameById(this.gameId).subscribe(
      (result: Game) => {
        this.gameData = result;
      }, error => {
        console.log('Error ocurred while fetcing game datos:', error)
      });
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
