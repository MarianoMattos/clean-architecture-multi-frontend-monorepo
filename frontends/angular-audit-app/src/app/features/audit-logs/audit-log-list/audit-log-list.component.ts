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
    performedBy: '',
    payload: ''
  };

  ngOnInit(): void {
    this.onFilterChange();
  }

  onFilterChange(): void {
    this.auditService.loadLogsBySource(this.selectedSourceFilter);
  }

  onSearchChange(): void {
    this.auditService.setSearchTerm(this.searchTerm);
  }

  onSort(column: keyof AuditLog): void {
    this.auditService.toggleSort(column);
  }

  onPageSizeChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const size = Number(selectElement.value);
    this.auditService.setPageSize(size);
  }

  openPayloadModal(log: AuditLog): void {
    this.auditService.selectLog(log);
  }

  closeModal(): void {
    this.auditService.selectLog(null);
  }

  openCreateModal(): void {
    this.newLogForm = {
      systemSource: this.selectedSourceFilter,
      action: '',
      severity: AuditSeverity.Information,
      performedBy: 'AdminUser',
      payload: '{\n  "status": "Success"\n}'
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
        this.showToast('Record created successfully!');
      },
      error: (err) => {
        this.showToast(err.message);
      }
    });
  }

  showToast(message: string): void {
    this.toastMessage = message;
    setTimeout(() => {
      this.toastMessage = null;
    }, 4000);
  }

  formatJson(payload?: string): string {
    if (!payload) return 'Sin payload asociado.';
    try {
      return JSON.stringify(JSON.parse(payload), null, 2);
    } catch {
      return payload;
    }
  }
}