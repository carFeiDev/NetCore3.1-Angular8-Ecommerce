import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Game } from '../models/game';
import { GameService } from "../services/game.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {


  constructor(private route: ActivatedRoute, private gameService: GameService) {}
  ngOnInit() {}


  }




