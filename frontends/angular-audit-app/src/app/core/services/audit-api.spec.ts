import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { AuditApiService } from './audit-api';

describe('AuditApiService', () => {
  let service: AuditApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(AuditApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});