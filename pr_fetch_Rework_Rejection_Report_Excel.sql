-- =========================================================================
-- SCRIPT: pr_fetch_Rework_Rejection_Report_Excel.sql
-- DESCRIPTION: Stored procedures providing clean, human-readable column
--              headers without the 'FRR_' prefix for Excel export
--              and the Rework / Rejection Report.
-- =========================================================================

USE [MVRLA]; -- Adjust database name if required
GO

-- =========================================================================
-- 1. STORED PROCEDURE: pr_fetch_Rework_Rejection_Report_Excel
-- Description: Returns defect records with clean, formatted column titles
--              specifically designed for Excel export (NO FRR_ prefix).
-- =========================================================================
IF OBJECT_ID('dbo.pr_fetch_Rework_Rejection_Report_Excel', 'P') IS NOT NULL
    DROP PROCEDURE dbo.pr_fetch_Rework_Rejection_Report_Excel;
GO

CREATE PROCEDURE dbo.pr_fetch_Rework_Rejection_Report_Excel
    @Type VARCHAR(50) = 'All',
    @FromDate VARCHAR(20) = NULL,
    @ToDate VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @Type = NULLIF(LTRIM(RTRIM(@Type)), '');
    IF (@Type = 'All') SET @Type = NULL;

    SET @FromDate = NULLIF(LTRIM(RTRIM(@FromDate)), '');
    SET @ToDate = NULLIF(LTRIM(RTRIM(@ToDate)), '');

    DECLARE @ParsedFrom DATE = NULL;
    DECLARE @ParsedTo DATE = NULL;

    IF (@FromDate IS NOT NULL)
    BEGIN
        SET @ParsedFrom = CASE 
            WHEN ISDATE(@FromDate) = 1 THEN CAST(@FromDate AS DATE)
            ELSE TRY_CAST(@FromDate AS DATE)
        END;
    END

    IF (@ToDate IS NOT NULL)
    BEGIN
        SET @ParsedTo = CASE 
            WHEN ISDATE(@ToDate) = 1 THEN CAST(@ToDate AS DATE)
            ELSE TRY_CAST(@ToDate AS DATE)
        END;
    END

    SELECT 
        ROW_NUMBER() OVER(ORDER BY R.FRR_ID DESC) AS [S.No],
        'F' + RIGHT('000' + CAST(R.FRR_ID AS VARCHAR(10)), 3) AS [Formation ID],
        ISNULL(R.FRR_QRCode, '') AS [QR Code],
        ISNULL(NULLIF(R.FRR_BatteryType, ''), ISNULL(M.mv_BatteryType, '')) AS [Battery Type],
        ISNULL(NULLIF(R.FRR_CKTNo, ''), ISNULL(M.mv_CKTNo, '')) AS [CKT No],
        ISNULL(R.FRR_Type_Name, '') AS [Type],
        ISNULL(R.FRR_Reason, '') AS [Defect Reason],
        ISNULL(R.FRR_DefectReasonType, '') AS [Defect Reason Type],
        ISNULL(R.FRR_DefectReason, ISNULL(R.FRR_Reason, '')) AS [Defect Code Reason],
        ISNULL(R.FRR_Employee_Name, '') AS [Employee Name],
        ISNULL(R.FRR_Created_By, '') AS [Created By],
        CONVERT(VARCHAR, R.FRR_Created_Date, 103) + ' ' + CONVERT(VARCHAR(5), R.FRR_Created_Date, 108) AS [Date & Time],
        ISNULL(R.FRR_Cell1, '') AS [Cell 1],
        ISNULL(R.FRR_Cell2, '') AS [Cell 2],
        ISNULL(R.FRR_Cell3, '') AS [Cell 3],
        ISNULL(R.FRR_Cell4, '') AS [Cell 4],
        ISNULL(R.FRR_Cell5, '') AS [Cell 5],
        ISNULL(R.FRR_Cell6, '') AS [Cell 6],
        ISNULL(R.FRR_DefectCell, '') AS [Defect Cell],
        ISNULL(R.FRR_OCV, '') AS [OCV],
        ISNULL(R.FRR_PlateNo, '') AS [Plate No],
        ISNULL(R.FRR_Location, '') AS [Location],
        ISNULL(R.FRR_PlateType, '') AS [Plate Type]
    FROM [dbo].[Formation_Rework_Rejection] R
    OUTER APPLY (
        SELECT TOP 1 mv_BatteryType, mv_CKTNo
        FROM [dbo].[MVRLA]
        WHERE mv_QRCode = R.FRR_QRCode
        ORDER BY mv_ID DESC
    ) M
    WHERE R.FRR_Status = 'A'
      AND (@Type IS NULL OR R.FRR_Type_Name = @Type)
      AND (@ParsedFrom IS NULL OR CAST(R.FRR_Created_Date AS DATE) >= @ParsedFrom)
      AND (@ParsedTo IS NULL OR CAST(R.FRR_Created_Date AS DATE) <= @ParsedTo)
    ORDER BY R.FRR_ID DESC;
END;
GO

-- =========================================================================
-- 2. STORED PROCEDURE: pr_fetch_Rework_Rejection_Report (Updated)
-- Description: Updated to return clean column names without FRR_ prefix
--              while preserving legacy aliases for backwards compatibility.
-- =========================================================================
IF OBJECT_ID('dbo.pr_fetch_Rework_Rejection_Report', 'P') IS NOT NULL
    DROP PROCEDURE dbo.pr_fetch_Rework_Rejection_Report;
GO

CREATE PROCEDURE dbo.pr_fetch_Rework_Rejection_Report
    @Type VARCHAR(50) = 'All',
    @FromDate VARCHAR(20) = NULL,
    @ToDate VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @Type = NULLIF(LTRIM(RTRIM(@Type)), '');
    IF (@Type = 'All') SET @Type = NULL;

    SET @FromDate = NULLIF(LTRIM(RTRIM(@FromDate)), '');
    SET @ToDate = NULLIF(LTRIM(RTRIM(@ToDate)), '');

    DECLARE @ParsedFrom DATE = NULL;
    DECLARE @ParsedTo DATE = NULL;

    IF (@FromDate IS NOT NULL)
    BEGIN
        SET @ParsedFrom = CASE 
            WHEN ISDATE(@FromDate) = 1 THEN CAST(@FromDate AS DATE)
            ELSE TRY_CAST(@FromDate AS DATE)
        END;
    END

    IF (@ToDate IS NOT NULL)
    BEGIN
        SET @ParsedTo = CASE 
            WHEN ISDATE(@ToDate) = 1 THEN CAST(@ToDate AS DATE)
            ELSE TRY_CAST(@ToDate AS DATE)
        END;
    END

    SELECT 
        ROW_NUMBER() OVER(ORDER BY R.FRR_ID DESC) AS [SNo],
        R.FRR_ID AS [ID],
        'F' + RIGHT('000' + CAST(R.FRR_ID AS VARCHAR(10)), 3) AS [Formation_ID],
        ISNULL(R.FRR_QRCode, '') AS [QRCode],
        ISNULL(NULLIF(R.FRR_BatteryType, ''), ISNULL(M.mv_BatteryType, '')) AS [BatteryType],
        ISNULL(NULLIF(R.FRR_CKTNo, ''), ISNULL(M.mv_CKTNo, '')) AS [CKTNo],
        ISNULL(R.FRR_Type_Name, '') AS [Type_Name],
        ISNULL(R.FRR_Reason, '') AS [Reason],
        ISNULL(R.FRR_DefectReasonType, '') AS [DefectReasonType],
        ISNULL(R.FRR_DefectReason, ISNULL(R.FRR_Reason, '')) AS [DefectReason],
        ISNULL(R.FRR_Employee_Name, '') AS [Employee_Name],
        ISNULL(R.FRR_Created_By, '') AS [Created_By],
        CONVERT(VARCHAR, R.FRR_Created_Date, 103) + ' ' + CONVERT(VARCHAR(5), R.FRR_Created_Date, 108) AS [Created_Date],
        ISNULL(R.FRR_Cell1, '') AS [Cell1],
        ISNULL(R.FRR_Cell2, '') AS [Cell2],
        ISNULL(R.FRR_Cell3, '') AS [Cell3],
        ISNULL(R.FRR_Cell4, '') AS [Cell4],
        ISNULL(R.FRR_Cell5, '') AS [Cell5],
        ISNULL(R.FRR_Cell6, '') AS [Cell6],
        ISNULL(R.FRR_DefectCell, '') AS [DefectCell],
        ISNULL(R.FRR_OCV, '') AS [OCV],
        ISNULL(R.FRR_PlateNo, '') AS [PlateNo],
        ISNULL(R.FRR_Location, '') AS [Location],
        ISNULL(R.FRR_PlateType, '') AS [PlateType],
        -- Legacy aliases for backwards compatibility
        R.FRR_ID AS [FRR_ID],
        ISNULL(R.FRR_QRCode, '') AS [FRR_QRCode],
        ISNULL(NULLIF(R.FRR_BatteryType, ''), ISNULL(M.mv_BatteryType, '')) AS [FRR_BatteryType],
        ISNULL(NULLIF(R.FRR_CKTNo, ''), ISNULL(M.mv_CKTNo, '')) AS [FRR_CKTNo],
        ISNULL(R.FRR_Type_Name, '') AS [FRR_Type_Name],
        ISNULL(R.FRR_Reason, '') AS [FRR_Reason],
        ISNULL(R.FRR_DefectReasonType, '') AS [FRR_DefectReasonType],
        ISNULL(R.FRR_DefectReason, ISNULL(R.FRR_Reason, '')) AS [FRR_DefectReason],
        ISNULL(R.FRR_Employee_Name, '') AS [FRR_Employee_Name],
        ISNULL(R.FRR_Created_By, '') AS [FRR_Created_By],
        CONVERT(VARCHAR, R.FRR_Created_Date, 103) + ' ' + CONVERT(VARCHAR(5), R.FRR_Created_Date, 108) AS [FRR_Created_Date],
        ISNULL(R.FRR_Cell1, '') AS [FRR_Cell1],
        ISNULL(R.FRR_Cell2, '') AS [FRR_Cell2],
        ISNULL(R.FRR_Cell3, '') AS [FRR_Cell3],
        ISNULL(R.FRR_Cell4, '') AS [FRR_Cell4],
        ISNULL(R.FRR_Cell5, '') AS [FRR_Cell5],
        ISNULL(R.FRR_Cell6, '') AS [FRR_Cell6],
        ISNULL(R.FRR_DefectCell, '') AS [FRR_DefectCell],
        ISNULL(R.FRR_OCV, '') AS [FRR_OCV],
        ISNULL(R.FRR_PlateNo, '') AS [FRR_PlateNo],
        ISNULL(R.FRR_Location, '') AS [FRR_Location],
        ISNULL(R.FRR_PlateType, '') AS [FRR_PlateType]
    FROM [dbo].[Formation_Rework_Rejection] R
    OUTER APPLY (
        SELECT TOP 1 mv_BatteryType, mv_CKTNo
        FROM [dbo].[MVRLA]
        WHERE mv_QRCode = R.FRR_QRCode
        ORDER BY mv_ID DESC
    ) M
    WHERE R.FRR_Status = 'A'
      AND (@Type IS NULL OR R.FRR_Type_Name = @Type)
      AND (@ParsedFrom IS NULL OR CAST(R.FRR_Created_Date AS DATE) >= @ParsedFrom)
      AND (@ParsedTo IS NULL OR CAST(R.FRR_Created_Date AS DATE) <= @ParsedTo)
    ORDER BY R.FRR_ID DESC;
END;
GO
