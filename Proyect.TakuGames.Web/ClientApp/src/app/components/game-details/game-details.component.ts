import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { User } from 'src/app/models/user';
import { SubscriptionService } from 'src/app/services/subscription.service';
import { Game } from '../../models/game';
import { GameService } from "../../services/game.service";

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss']
})

export class GameDetailsComponent implements OnInit {

  public game: Game;
  gameId;
  BookDetails$: Observable<Game>;
  userData$: Observable<User>;

  constructor(private gameServices: GameService,
     private route: ActivatedRoute,
     private router: Router,
     private subscriptionService: SubscriptionService,) {
    this.gameId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.route.queryParams.subscribe(param => {
      this.route.params.subscribe(
        params => {
          this.gameId = +params['id'];
          this.getGameDetails();
        }
      )
    });
    this.userData$ = this.subscriptionService.userData;
  }
  getGameDetails() {
    this.gameServices.getGameById(this.gameId).subscribe(
      (result: Game) => {
        this.game = result;
      }, error => {
        console.log("Error ocurred while fetching game data:", error);
      }
    )
  }
}
