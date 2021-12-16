import { NgModule } from '@angular/core';
import { RouterModule, Routes, } from '@angular/router';
import { GameFormComponent } from '../components/admin/game-form/game-form.component';
import { GamesComponent } from '../components/admin/games/games.component';
import { DeleteGameComponent } from '../components/admin/delete-game/delete-game.component';
import { OldManagerGamesComponent } from '../components/admin/old-manager-games/old-manager-games.component';


const adminRoutes: Routes = [
  {
    path: '',
    children: [
      { path: '', component: GamesComponent },
      { path: 'old-manager-games', component: OldManagerGamesComponent }, 
      { path: 'new', component: GameFormComponent },
      { path: ':id', component: GameFormComponent },
      { path: 'delete-game', component: DeleteGameComponent, pathMatch: 'full' },       
    ]  
  },
]

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule],
  declarations: [],
  providers: [],
})
export class AdminRoutingModule { }
