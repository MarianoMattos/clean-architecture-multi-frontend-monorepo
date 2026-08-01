import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuditApiService {
  private http = inject(HttpClient);
   
  private apiUrl = 'http://localhost:5185/api/audits';

  getAuditLogs(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}