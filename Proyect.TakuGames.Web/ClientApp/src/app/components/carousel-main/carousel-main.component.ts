import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CarouselConfig } from 'ngx-bootstrap/carousel';


@Component({
  selector: 'app-carousel-main',
  templateUrl: './carousel-main.component.html',
  styleUrls: ['./carousel-main.component.scss'],
  providers: [
    { provide: CarouselConfig, useValue: { interval: 2500, noPause: true, showIndicators: true } }
  ]
})
export class CarouselMainComponent implements OnInit {
  title = 'testfunctions';


  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {

  }
  
}
