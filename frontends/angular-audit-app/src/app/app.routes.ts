import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'audit-logs',
    pathMatch: 'full'
  },
  {
    path: 'audit-logs',
    loadComponent: () => 
      import('./features/audit-logs/audit-log-list/audit-log-list.component')
        .then(m => m.AuditLogListComponent)
  }
];