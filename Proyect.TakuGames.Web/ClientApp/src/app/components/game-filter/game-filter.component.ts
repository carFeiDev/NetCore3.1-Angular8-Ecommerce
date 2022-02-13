import { Component, Input, OnInit } from '@angular/core';
import { GameService } from "../../services/game.service";
@Component({
  selector: 'app-game-filter',
  templateUrl: './game-filter.component.html',
  styleUrls: ['./game-filter.component.scss']
})
export class GameFilterComponent implements OnInit {
  @Input('category')
  category: any;
  categoryList: [];

  constructor(private gameService: GameService) { }

  ngOnInit() {
    this.gameService.getCategories().subscribe(
      (categoryData: []) => {
        this.categoryList = categoryData;
      }, error => {
        console.log('Error ocurred  while fetching category list :', error);
      });
  }
}

