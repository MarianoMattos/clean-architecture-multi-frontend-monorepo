-- sqlcmd -S .\SQLEXPRESS -d AuditSystemDb -i database/scripts/seed.sql -b

USE [AuditSystemDb];
GO

SET NOCOUNT ON;

BEGIN TRANSACTION;

BEGIN TRY
    -- 1. Semilla para [DeliveryJobs]
    IF NOT EXISTS (SELECT 1 FROM [dbo].[DeliveryJobs])
    BEGIN
        INSERT INTO [dbo].[DeliveryJobs] ([Id], [JobCode], [ClientName], [Status], [RetryCount], [CompletedAt], [CreatedAt])
        VALUES 
        (NEWID(), 'JOB-YPF-001', N'YPF S.A.', 'Completed', 0, GETUTCDATE(), GETUTCDATE()),
        (NEWID(), 'JOB-EDN-002', N'Edenor S.A.', 'Failed', 2, NULL, GETUTCDATE()),
        (NEWID(), 'JOB-TEL-003', N'Telecom Argentina', 'Pending', 0, NULL, GETUTCDATE());
        
        PRINT 'Semilla de [DeliveryJobs] insertada con éxito.';
    END
    ELSE
    BEGIN
        PRINT 'La tabla [DeliveryJobs] ya contiene datos. Se omitió la semilla.';
    END

    -- 2. Semilla para [AuditLogs]
    IF NOT EXISTS (SELECT 1 FROM [dbo].[AuditLogs])
    BEGIN
        INSERT INTO [dbo].[AuditLogs] ([Id], [SystemSource], [Action], [Payload], [Severity], [PerformedBy], [CreatedAt])
        VALUES 
        (NEWID(), 'KTA_Automation', 'ExtractDocuments', N'{"extracted": 283, "pending": 30, "status": "Success"}', 1, 'KTA_Service', GETUTCDATE()),
        (NEWID(), 'Database_Optimizer', 'TruncateLogs', N'{"truncated_rows": 15420, "space_recovered_mb": 450}', 1, 'DB_Maintenance_Job', GETUTCDATE()),
        (NEWID(), 'B2B_Gateway', 'ProcessDelivery', N'{"error": "Timeout connecting to client endpoint", "attempt": 3}', 3, 'Integration_Engine', GETUTCDATE()),
        (NEWID(), 'Identity_Server', 'UnauthorizedAccessAttempt', N'{"ip": "192.168.1.105", "user": "admin_test"}', 4, 'Security_Module', GETUTCDATE());

        PRINT 'Semilla de [AuditLogs] insertada con éxito.';
    END
    ELSE
    BEGIN
        PRINT 'La tabla [AuditLogs] ya contiene datos. Se omitió la semilla.';
    END

    COMMIT TRANSACTION;
    PRINT 'Proceso de semilla completado de forma segura.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();

    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
GO