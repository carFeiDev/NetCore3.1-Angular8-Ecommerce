import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OldManagerGamesComponent } from './old-manager-games.component';

describe('OldManagerGamesComponent', () => {
  let component: OldManagerGamesComponent;
  let fixture: ComponentFixture<OldManagerGamesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OldManagerGamesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OldManagerGamesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
