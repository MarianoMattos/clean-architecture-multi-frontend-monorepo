import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuditLogService } from '../../../core/services/audit-log.service';
import { AuditLog, AuditSeverity } from '../../../shared/models/audit-log.model';

@Component({
  selector: 'app-audit-log-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './audit-log-list.component.html',
  styleUrl: './audit-log-list.component.css'
})
export class AuditLogListComponent implements OnInit {
  protected auditService = inject(AuditLogService);
  
  sources: string[] = ['B2B_Gateway', 'DeliveryService', 'CoreAPI'];
  selectedSourceFilter: string = 'B2B_Gateway';
  searchTerm: string = '';

  AuditSeverity = AuditSeverity;

  severities = [
    { label: 'Information', value: AuditSeverity.Information }, 
    { label: 'Warning', value: AuditSeverity.Warning },         
    { label: 'Error', value: AuditSeverity.Error },             
    { label: 'Critical', value: AuditSeverity.Critical }        
  ];

  isCreateModalOpen: boolean = false;
  toastMessage: string | null = null;

  newLogForm = {
    systemSource: 'B2B_Gateway',
    action: '',
    severity: AuditSeverity.Information, 
    payload: '',
    performedBy: 'SystemUser'
  };

  ngOnInit(): void {
    this.onFilterChange();
  }

  onFilterChange(): void {
    this.auditService.loadLogsBySource(this.selectedSourceFilter);
  }

  openCreateModal(): void {
    this.newLogForm = {
      systemSource: this.selectedSourceFilter,
      action: '',
      severity: AuditSeverity.Information,
      payload: '{\n  "status": "Success"\n}',
      performedBy: 'AdminUser'
    };
    this.isCreateModalOpen = true;
  }

  closeCreateModal(): void {
    this.isCreateModalOpen = false;
  }

  submitNewLog(): void {
    if (!this.newLogForm.action.trim()) return;

    const payloadToSend = {
      ...this.newLogForm,
      severity: Number(this.newLogForm.severity)
    };

    this.auditService.createAuditLog(payloadToSend).subscribe({
      next: () => {
        this.closeCreateModal();
        this.showToast('¡Registro guardado correctamente!');
      },
      error: (err) => {
        console.error('Error al guardar el registro:', err);
      }
    });
  }

  showToast(message: string): void {
    this.toastMessage = message;
    setTimeout(() => { this.toastMessage = null; }, 4000);
  }

  getSeverityLabel(severity: number | string): string {
    const numericValue = Number(severity);
    switch (numericValue) {
      case AuditSeverity.Information: return 'Information';
      case AuditSeverity.Warning: return 'Warning';
      case AuditSeverity.Error: return 'Error';
      case AuditSeverity.Critical: return 'Critical';
      default: return String(severity);
    }
  }
}