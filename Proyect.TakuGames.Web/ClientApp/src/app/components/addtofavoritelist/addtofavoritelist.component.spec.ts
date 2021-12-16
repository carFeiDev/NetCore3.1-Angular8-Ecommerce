import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddtofavoritelistComponent } from './addtofavoritelist.component';

describe('AddtofavoritelistComponent', () => {
  let component: AddtofavoritelistComponent;
  let fixture: ComponentFixture<AddtofavoritelistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddtofavoritelistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddtofavoritelistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
