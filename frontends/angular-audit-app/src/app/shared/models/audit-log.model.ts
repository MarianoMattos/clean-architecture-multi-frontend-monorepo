export enum AuditSeverity {
  Information = 1,
  Warning = 2,
  Error = 3,
  Critical = 4
}

export interface AuditLog {
  id: string;
  systemSource: string;
  action: string;
  severity: AuditSeverity | number | string;
  payload?: string;
  performedBy?: string;
}

export interface CreateAuditLogDto {
  systemSource: string;
  action: string;
  severity: AuditSeverity | number;
  payload?: string;
  performedBy?: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  pageIndex: number;
  totalPages: number;
  totalCount: number;
}