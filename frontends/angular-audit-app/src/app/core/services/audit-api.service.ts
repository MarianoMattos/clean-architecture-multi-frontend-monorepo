import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuditLog, CreateAuditLogDto } from '../../shared/models/audit-log.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuditApiService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/AuditLogs`;

  getLogsBySource(source: string): Observable<AuditLog[]> {
    return this.http.get<AuditLog[]>(`${this.apiUrl}/source/${source}`);
  }

  createAuditLog(newLog: CreateAuditLogDto): Observable<AuditLog> {
    return this.http.post<AuditLog>(this.apiUrl, newLog);
  }
}