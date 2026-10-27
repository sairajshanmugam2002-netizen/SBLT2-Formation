-- =========================================================================
-- SCRIPT: pr_Rework_Rejection_Edit_Delete_Procedures.sql
-- DESCRIPTION: Stored procedures for Soft-Delete and Update operations
--              on the Formation_Rework_Rejection table.
-- =========================================================================

USE [MVRLA]; -- Adjust database name if needed
GO

-- =========================================================================
-- 1. STORED PROCEDURE: pr_delete_Formation_Rework_Rejection
-- Description: Soft deletes a record by setting FRR_Status = 'D'
-- =========================================================================
IF OBJECT_ID('dbo.pr_delete_Formation_Rework_Rejection', 'P') IS NOT NULL
    DROP PROCEDURE dbo.pr_delete_Formation_Rework_Rejection;
GO

CREATE PROCEDURE dbo.pr_delete_Formation_Rework_Rejection
    @ID VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF (@ID IS NULL OR LTRIM(RTRIM(@ID)) = '')
    BEGIN
        SELECT 'False' AS [Result], 'Invalid or empty ID parameter' AS [Message];
        RETURN;
    END

    UPDATE [dbo].[Formation_Rework_Rejection]
    SET [FRR_Status] = 'D'
    WHERE [FRR_ID] = CAST(@ID AS INT);

    IF @@ROWCOUNT > 0
    BEGIN
        SELECT 'True' AS [Result], 'Record Deleted Successfully' AS [Message];
    END
    ELSE
    BEGIN
        SELECT 'False' AS [Result], 'Record not found or already deleted' AS [Message];
    END
END;
GO

-- =========================================================================
-- 2. STORED PROCEDURE: pr_update_Formation_Rework_Rejection
-- Description: Updates defect reasons, classifications, employee, and cells
--              for an active Formation_Rework_Rejection record.
-- =========================================================================
IF OBJECT_ID('dbo.pr_update_Formation_Rework_Rejection', 'P') IS NOT NULL
    DROP PROCEDURE dbo.pr_update_Formation_Rework_Rejection;
GO

CREATE PROCEDURE dbo.pr_update_Formation_Rework_Rejection
    @FRR_ID INT,
    @FRR_Type_Name VARCHAR(50) = NULL,
    @FRR_Reason VARCHAR(500) = NULL,
    @FRR_DefectReasonType VARCHAR(100) = NULL,
    @FRR_DefectReason VARCHAR(500) = NULL,
    @FRR_Employee_Name VARCHAR(100) = NULL,
    @FRR_DefectCell VARCHAR(50) = NULL,
    @FRR_OCV VARCHAR(50) = NULL,
    @FRR_PlateNo VARCHAR(50) = NULL,
    @FRR_Location VARCHAR(50) = NULL,
    @FRR_PlateType VARCHAR(50) = NULL,
    @FRR_Cell1 VARCHAR(50) = NULL,
    @FRR_Cell2 VARCHAR(50) = NULL,
    @FRR_Cell3 VARCHAR(50) = NULL,
    @FRR_Cell4 VARCHAR(50) = NULL,
    @FRR_Cell5 VARCHAR(50) = NULL,
    @FRR_Cell6 VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF (@FRR_ID IS NULL OR @FRR_ID <= 0)
    BEGIN
        SELECT 'False' AS [Result], 'Invalid Record ID' AS [Message];
        RETURN;
    END

    -- Determine Type_ID corresponding to Type_Name (12 = Rework, 13 = Rejection)
    DECLARE @CalculatedTypeID INT = NULL;
    IF (@FRR_Type_Name = 'Rework')
        SET @CalculatedTypeID = 12;
    ELSE IF (@FRR_Type_Name = 'Rejection')
        SET @CalculatedTypeID = 13;

    UPDATE [dbo].[Formation_Rework_Rejection]
    SET
        [FRR_Type_ID]          = ISNULL(@CalculatedTypeID, [FRR_Type_ID]),
        [FRR_Type_Name]        = ISNULL(NULLIF(@FRR_Type_Name, ''), [FRR_Type_Name]),
        [FRR_Reason]           = ISNULL(NULLIF(@FRR_Reason, ''), [FRR_Reason]),
        [FRR_DefectReasonType] = ISNULL(@FRR_DefectReasonType, [FRR_DefectReasonType]),
        [FRR_DefectReason]     = ISNULL(NULLIF(@FRR_DefectReason, ''), [FRR_DefectReason]),
        [FRR_Employee_Name]    = ISNULL(NULLIF(@FRR_Employee_Name, ''), [FRR_Employee_Name]),
        [FRR_DefectCell]       = ISNULL(@FRR_DefectCell, [FRR_DefectCell]),
        [FRR_OCV]              = ISNULL(@FRR_OCV, [FRR_OCV]),
        [FRR_PlateNo]          = ISNULL(@FRR_PlateNo, [FRR_PlateNo]),
        [FRR_Location]         = ISNULL(@FRR_Location, [FRR_Location]),
        [FRR_PlateType]        = ISNULL(@FRR_PlateType, [FRR_PlateType]),
        [FRR_Cell1]            = ISNULL(@FRR_Cell1, [FRR_Cell1]),
        [FRR_Cell2]            = ISNULL(@FRR_Cell2, [FRR_Cell2]),
        [FRR_Cell3]            = ISNULL(@FRR_Cell3, [FRR_Cell3]),
        [FRR_Cell4]            = ISNULL(@FRR_Cell4, [FRR_Cell4]),
        [FRR_Cell5]            = ISNULL(@FRR_Cell5, [FRR_Cell5]),
        [FRR_Cell6]            = ISNULL(@FRR_Cell6, [FRR_Cell6])
    WHERE [FRR_ID] = @FRR_ID
      AND [FRR_Status] = 'A';

    IF @@ROWCOUNT > 0
    BEGIN
        SELECT 'True' AS [Result], 'Record Updated Successfully' AS [Message];
    END
    ELSE
    BEGIN
        SELECT 'False' AS [Result], 'Record not found or inactive' AS [Message];
    END
END;
GO
