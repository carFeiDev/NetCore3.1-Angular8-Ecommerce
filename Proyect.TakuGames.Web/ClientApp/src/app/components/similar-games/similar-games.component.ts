import { Input } from '@angular/core';
import { Component, OnInit, HostListener, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Game } from 'src/app/models/game';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-similar-games',
  templateUrl: './similar-games.component.html',
  styleUrls: ['./similar-games.component.scss']
})

export class SimilarGamesComponent implements OnInit {
  @Input()
  gameId: number;
  @ViewChild("container", { static: true, read: ElementRef })
  container: ElementRef;
  @HostListener("window:resize") windowResize() {
    let newCardsPerPage = this.getCardsPerPage();
    if (newCardsPerPage != this.cardsPerPage) {
      this.cardsPerPage = newCardsPerPage;
      this.initializeSlider();
      if (this.currentPage > this.totalPages) {
        this.currentPage = this.totalPages;
        this.populatePagePosition();
      }
    }
  }

  SimilarGame$: Observable<Game[]>;
  totalCards: number = 9;
  currentPage: number = 1;
  pagePosition: string = "0%";
  cardsPerPage: number;
  totalPages: number;
  overflowWidth: string;
  cardWidth: string;
  containerWidth: number;

  constructor(
    private gameService: GameService,
    private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.gameId = +params.id;
      this.getSimilarGameData();
      });
    this.cardsPerPage = this.getCardsPerPage();
    this.initializeSlider();
  }

  getSimilarGameData() {
    this.SimilarGame$ = this.gameService.getsimilarGames(this.gameId);
  }
  
  initializeSlider() {
    this.totalPages = Math.ceil(this.totalCards / this.cardsPerPage);
    this.overflowWidth = `calc(${this.totalPages * 100}% + ${this.totalPages * 10}px)`;
    this.cardWidth = `calc((${100 / this.totalPages}% - ${this.cardsPerPage * 10}px) / ${this.cardsPerPage})`;
  }

  getCardsPerPage() {
    return Math.floor(this.container.nativeElement.offsetWidth / 300);
  }

  changePage(incrementor) {
    this.currentPage += incrementor;
    this.populatePagePosition();
  }

  populatePagePosition() {
    this.pagePosition = `calc(${-100 * (this.currentPage - 1)}% - ${10 *
      (this.currentPage - 1)}px)`;
  } 
}
