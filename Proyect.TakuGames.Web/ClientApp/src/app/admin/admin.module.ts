import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameFormComponent } from '../components/admin/game-form/game-form.component';
import { DeleteGameComponent } from '../components/admin/delete-game/delete-game.component';
import { AdminRoutingModule } from './admin-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgMaterialModule } from '../ng-material/ng-material.module';
import { GamesComponent } from '../components/admin/games/games.component';
import { AdminFilterGamesPipe } from '../pipes/admin-filter-games.pipe';
import { OldManagerGamesComponent } from '../components/admin/old-manager-games/old-manager-games.component';


@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgMaterialModule,
  ],
  exports: [],
  declarations: [
    GameFormComponent,
    DeleteGameComponent,
    GamesComponent,
    OldManagerGamesComponent, 
    AdminFilterGamesPipe,
  ],
  providers: [],
  entryComponents: [DeleteGameComponent]
})
export class AdminModule { }
