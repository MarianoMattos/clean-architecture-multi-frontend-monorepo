import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuditLog } from '../../shared/models/audit-log.model';
import { catchError, of, tap } from 'rxjs';

export type SortDirection = 'asc' | 'desc';

@Injectable({
  providedIn: 'root'
})
export class AuditLogService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5185/api/AuditLogs';

  private state = signal<{
    logs: AuditLog[];
    loading: boolean;
    error: string | null;
    selectedSource: string;
    searchTerm: string;
    selectedLog: AuditLog | null;
    currentPage: number;
    pageSize: number;
    sortColumn: keyof AuditLog;
    sortDirection: SortDirection;
  }>({
    logs: [],
    loading: false,
    error: null,
    selectedSource: 'B2B_Gateway',
    searchTerm: '',
    selectedLog: null,
    currentPage: 1,
    pageSize: 5,
    sortColumn: 'id',
    sortDirection: 'asc'
  });

  readonly loading = computed(() => this.state().loading);
  readonly error = computed(() => this.state().error);
  readonly selectedSource = computed(() => this.state().selectedSource);
  readonly selectedLog = computed(() => this.state().selectedLog);
  readonly searchTerm = computed(() => this.state().searchTerm);
  readonly currentPage = computed(() => this.state().currentPage);
  readonly pageSize = computed(() => this.state().pageSize);
  readonly sortColumn = computed(() => this.state().sortColumn);
  readonly sortDirection = computed(() => this.state().sortDirection);

  readonly totalLogsCount = computed(() => this.state().logs.length);
  readonly payloadCount = computed(() => this.state().logs.filter(l => !!l.payload).length);

  readonly processedLogs = computed(() => {
    let result = [...this.state().logs];
    const term = this.state().searchTerm.toLowerCase();

    if (term) {
      result = result.filter(log =>
        log.action.toLowerCase().includes(term) ||
        log.id.toLowerCase().includes(term)
      );
    }

    const col = this.state().sortColumn;
    const dir = this.state().sortDirection === 'asc' ? 1 : -1;
    result.sort((a, b) => {
      const valA = a[col] ?? '';
      const valB = b[col] ?? '';
      return valA.localeCompare(valB) * dir;
    });

    return result;
  });

  readonly totalFilteredCount = computed(() => this.processedLogs().length);
  readonly totalPages = computed(() => 
    Math.ceil(this.totalFilteredCount() / this.state().pageSize) || 1
  );

  readonly paginatedLogs = computed(() => {
    const page = this.state().currentPage;
    const size = this.state().pageSize;
    const start = (page - 1) * size;
    return this.processedLogs().slice(start, start + size);
  });

  loadLogsBySource(source: string) {
    this.state.update(s => ({ 
      ...s, 
      loading: true, 
      error: null, 
      selectedSource: source, 
      currentPage: 1 
    }));

    this.http.get<AuditLog[]>(`${this.apiUrl}/source/${source}`)
      .pipe(
        tap((logs) => this.state.update(s => ({ ...s, logs, loading: false }))),
        catchError(() => {
          this.state.update(s => ({ ...s, loading: false, error: 'Error al cargar los logs.' }));
          return of([]);
        })
      )
      .subscribe();
  }

  createAuditLog(newLog: { systemSource: string; action: string; severity: string; payload?: string }) {
    this.state.update(s => ({ ...s, loading: true, error: null }));

    return this.http.post<AuditLog>(this.apiUrl, newLog).pipe(
      tap((createdLog) => {
        this.state.update(s => {
          const isCurrentSource = s.selectedSource === createdLog.systemSource;
          const updatedLogs = isCurrentSource ? [createdLog, ...s.logs] : s.logs;

          return {
            ...s,
            logs: updatedLogs,
            loading: false,
            currentPage: 1
          };
        });
      }),
      catchError((err) => {
        this.state.update(s => ({ 
          ...s, 
          loading: false, 
          error: 'No se pudo guardar el registro de auditoría en la BD.' 
        }));
        throw err;
      })
    );
  }

  setSearchTerm(term: string) {
    this.state.update(s => ({ ...s, searchTerm: term, currentPage: 1 }));
  }

  setPage(page: number) {
    if (page >= 1 && page <= this.totalPages()) {
      this.state.update(s => ({ ...s, currentPage: page }));
    }
  }

  setPageSize(size: number) {
    this.state.update(s => ({ ...s, pageSize: size, currentPage: 1 }));
  }

  toggleSort(column: keyof AuditLog) {
    this.state.update(s => {
      const isSameColumn = s.sortColumn === column;
      const nextDirection: SortDirection = isSameColumn && s.sortDirection === 'asc' ? 'desc' : 'asc';
      return { ...s, sortColumn: column, sortDirection: nextDirection };
    });
  }

  selectLog(log: AuditLog | null) {
    this.state.update(s => ({ ...s, selectedLog: log }));
  }
}