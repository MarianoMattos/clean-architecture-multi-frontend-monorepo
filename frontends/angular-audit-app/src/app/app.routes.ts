import { Routes } from '@angular/router';

export const routes: Routes = [
  { 
    path: '', 
    redirectTo: 'audit-logs', 
    pathMatch: 'full' 
  },
  {
    path: 'audit-logs',
    loadChildren: () =>
      import('./features/audit-logs/audit-logs.routes').then(
        (m) => m.AUDIT_LOGS_ROUTES
      )
  },
  { 
    path: '**', 
    redirectTo: 'audit-logs' 
  }
];