import { TestBed } from '@angular/core/testing';

import { DataSharingServiceService } from './data-sharing-service.service';

describe('DataSharingServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DataSharingServiceService = TestBed.get(DataSharingServiceService);
    expect(service).toBeTruthy();
  });
});
