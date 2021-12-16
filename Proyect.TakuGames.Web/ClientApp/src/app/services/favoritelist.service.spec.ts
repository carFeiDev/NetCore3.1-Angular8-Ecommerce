import { TestBed } from '@angular/core/testing';

import { FavoritelistService } from './favoritelist.service';

describe('FavoritelistService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FavoritelistService = TestBed.get(FavoritelistService);
    expect(service).toBeTruthy();
  });
});
