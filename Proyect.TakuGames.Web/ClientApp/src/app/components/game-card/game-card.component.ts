import { Router } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { Game } from '../../models/game';
import { Observable } from 'rxjs/internal/Observable';
import { User } from 'src/app/models/user';
import { SubscriptionService } from 'src/app/services/subscription.service';

@Component({
  selector: 'app-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent implements OnInit {

  @Input('game')
  game: Game;
  isActive = false;
  userData$: Observable<User>;
  changeText:boolean
  constructor(private router: Router,private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.userData$ = this.subscriptionService.userData;
    //  this.changeText= false;
    
  }

  goToPage(id: number) {
    this.router.navigate(['/games/details', id]);
  }

}
