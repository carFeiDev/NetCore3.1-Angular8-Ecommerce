
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Game } from 'src/app/models/game';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-price-filter',
  templateUrl: './price-filter.component.html',
  styleUrls: ['./price-filter.component.scss']
})
export class PriceFilterComponent implements OnInit {

  @Output()
  priceValue = new EventEmitter<number>(true);
  max: number;
  min: number;
  value: number;
  step = 100;
  thumbLabel = true;

  constructor(private gameService: GameService) { }

  ngOnInit(): void {
    this.setPriceFilterProperties();
  }

  setPriceFilterProperties() {
    this.gameService.games$.pipe().subscribe(
      (data: Game[]) => {
        this.setMinValue(data);
        this.setMaxValue(data);
      }
    );
  }

  formatLabel(value: number) {
    if (value >= 1000) {
      return Math.round(value / 1000) + 'k';
    }
    return value;
  }

  onChange(event) {
    this.priceValue.emit(event.value);
  }

  setMinValue(games: Game[]) {
    this.min = games.reduce((prev, curr) => {
      return prev.price < curr.price ? prev : curr;
    }).price;
  }

  setMaxValue(games: Game[]) {
    this.value = this.max = games.reduce((prev, curr) => {
      return prev.price > curr.price ? prev : curr;
    }).price;
  }

}
