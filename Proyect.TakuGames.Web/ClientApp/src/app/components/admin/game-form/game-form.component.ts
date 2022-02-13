import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Game } from '../../../models/game';
import { GameService } from '../../../services/game.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.scss']
})
export class GameFormComponent implements OnInit, OnDestroy {

  private formData = new FormData();
  gameForm: FormGroup;
  game: Game = new Game();
  formTitle = 'Agregar';
  coverImagePath:any;
  categoryList: [];
  files:any;
  private unsubscribe$ = new Subject<void>();

  constructor(
    private gameService: GameService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<GameFormComponent>,
    @Inject(MAT_DIALOG_DATA) public gameId: any) {         
      this.gameForm = this.fb.group({
        gameId: 0,
        title: ['', Validators.required],
        description: ['', Validators.required],
        developer: ['', Validators.required],
        publisher: ['', Validators.required],
        platform: ['', Validators.required],
        category: ['', Validators.required],
        price: ['', [Validators.required, Validators.min(1)]],
        coverFileName:[''],
    })
      if (this.route.snapshot.params['id']) {
        this.gameId = this.route.snapshot.paramMap.get('id');
      }
  }

  ngOnInit() {
    this.gameService.getCategories()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((categoryData: []) => {
        this.categoryList = categoryData
      }, error => {
        console.log("Error ocurred while fetching category list", error);
      });

    if (this.gameId) {
      this.formTitle = "Editar";
      this.gameService.getGameById(this.gameId)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe((result: Game) => {
          this.setGameFormData(result);   
        }, error => {
          console.log('Error ocurred while fetching game data:', error);
        });
    }
  }

  setGameFormData(gameFormData) {
    this.gameForm.setValue({
      gameId: gameFormData.gameId,
      title: gameFormData.title,
      description: gameFormData.description,
      developer: gameFormData.developer,
      publisher: gameFormData.publisher,
      platform: gameFormData.platform,
      category: gameFormData.category,
      price: gameFormData.price,
      coverFileName: gameFormData.coverFileName,
    });

   this.coverImagePath = '/Upload/' + gameFormData.coverFileName;
  }

  saveGameData() {
    if (!this.gameForm.valid) {
      return;
    }
    if (this.files && this.files.length > 0) {
      for (let i = 0; i < this.files.length; i++) {
        this.formData.append('file' + i, this.files[i]);
      }
    }
    this.formData.append('gameFormData', JSON.stringify(this.gameForm.value));
    if (this.gameId) {
      this.gameService.updateGameDetails(this.formData, this.gameId)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe(
          () => {
            this.snackBar.open('Se edito el juego con exito', '', {
              duration: 2000
            });
            this.dialogClose();
          }, error => {
            console.log('Error ocurred while updating game data:', error);
          });
    } else {
      this.gameService.addGame(this.formData)
        .pipe(takeUntil(this.unsubscribe$))
          .subscribe(
            () => {
              this.snackBar.open('Se inserto el juego con exito', '', {
                duration: 2000
              });
              this.dialogClose();
            }, error => {
              //reset form data show a toaster
              this.gameForm.reset();
              console.log('Error ocurred while adding game data :', error);
            });
    }
  }
  uploadImage(event) {
    this.files = event.target.files;
    const reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (myevent: ProgressEvent) => {
      this.coverImagePath = (myevent.target as FileReader).result;
    };
  }

  cancel() { 
    this.dialogClose();
  }
  dialogClose(){
    this.dialogRef.close();
  }
  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
  get title() {
    return this.gameForm.get('title');
  }
  get description() {
    return this.gameForm.get('description');
  }
  get developer() {
    return this.gameForm.get('developer');
  }
  get publisher() {
    return this.gameForm.get('publisher');
  }
  get platform() {
    return this.gameForm.get('platform');
  }
  get category() {
    return this.gameForm.get('category');
  }
  get price() {
    return this.gameForm.get('price');
  }
  get coverFileName() {
    return this.gameForm.get('coverFileName');
  }

}
