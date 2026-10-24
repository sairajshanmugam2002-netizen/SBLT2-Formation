-- =========================================================================================
-- MVRLA Formation - Rework & Rejection Complete Database Fix & Stored Procedures
-- File: MVRLA_Fix_Rework_Rejection_And_Report.sql
-- Database: MVRLA_Formation
--
-- Description:
--   1. Ensures all columns exist in Formation_Rework_Rejection (11 cell/plate detail fields,
--      FRR_DefectReason, FRR_DefectReasonType).
--   2. Ensures RGV_vDefectReasonType exists in ReferenceGroup_Value.
--   3. Updates pr_fetch_Rework_Rejection_Reasons (supports Type_ID and ReferenceType mapping).
--   4. Updates pr_insert_Formation_Rework_Rejection (handles all fields).
--   5. Updates pr_fetch_Formation_Rework_Rejection_List (returns all fields in order).
--   6. Updates pr_fetch_Rework_Rejection_Report (returns full defect details and robust date filtering).
--   7. Includes verification queries and test execution.
-- =========================================================================================

USE [MVRLA_Formation];
GO

-- =========================================================================================
-- SECTION 1: TABLE COLUMN VERIFICATION & ALTERATIONS
-- =========================================================================================

-- 1.1 ReferenceGroup_Value: RGV_vDefectReasonType
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('ReferenceGroup_Value') AND name = 'RGV_vDefectReasonType')
BEGIN
    ALTER TABLE ReferenceGroup_Value ADD RGV_vDefectReasonType VARCHAR(100) NULL;
    PRINT 'Added RGV_vDefectReasonType column to ReferenceGroup_Value.';
END
GO

-- 1.2 Formation_Rework_Rejection: Cell & Plate Detail Columns
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell1')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell1 VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell2')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell2 VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell3')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell3 VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell4')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell4 VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell5')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell5 VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell6')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell6 VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_DefectCell')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_DefectCell VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_OCV')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_OCV VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_PlateNo')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_PlateNo VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Location')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Location VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_PlateType')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_PlateType VARCHAR(50) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_DefectReasonType')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_DefectReasonType VARCHAR(100) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_DefectReason')
    ALTER TABLE Formation_Rework_Rejection ADD FRR_DefectReason VARCHAR(500) NULL;
GO

PRINT 'Formation_Rework_Rejection columns verified successfully.';
GO

-- =========================================================================================
-- SECTION 2: STORED PROCEDURE: pr_fetch_Rework_Rejection_Types
-- =========================================================================================
CREATE OR ALTER PROCEDURE pr_fetch_Rework_Rejection_Types
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        RG_iID,
        RG_vCode,
        RG_vDescription
    FROM Reference_Group
    WHERE RG_vStatus = 'A'
      AND (
          RG_vDescription IN ('Rework', 'Rejection')
          OR RG_vCode IN ('Rework', 'Rejection')
          OR RG_iID IN (12, 13)
      )
    ORDER BY RG_iID ASC;
END;
GO

-- =========================================================================================
-- SECTION 3: STORED PROCEDURE: pr_fetch_Rework_Rejection_Reasons
-- =========================================================================================
CREATE OR ALTER PROCEDURE pr_fetch_Rework_Rejection_Reasons
    @Type_ID       VARCHAR(100) = NULL,
    @ReferenceType VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @Type_ID = NULLIF(LTRIM(RTRIM(@Type_ID)), '');
    SET @ReferenceType = NULLIF(LTRIM(RTRIM(@ReferenceType)), '');

    IF (@Type_ID = 'ALL') SET @Type_ID = NULL;
    IF (@ReferenceType = 'ALL') SET @ReferenceType = NULL;

    -- Route single ReferenceType passed in @Type_ID to Defect Reasons (17)
    IF (@ReferenceType IS NULL AND @Type_ID IS NOT NULL
        AND EXISTS (
            SELECT 1 FROM ReferenceGroup_Value 
            WHERE RGV_vStatus = 'A' 
              AND LTRIM(RTRIM(RGV_vDefectReasonType)) = @Type_ID
        )
        AND @Type_ID NOT IN ('12', '13', 'Rework', 'Rejection')
    )
    BEGIN
        SET @ReferenceType = @Type_ID;
        SET @Type_ID = '17';
    END

    DECLARE @ParsedID INT = TRY_CAST(@Type_ID AS INT);

    SELECT DISTINCT
        v.RGV_iID,
        v.RGV_vDesciption,
        v.RGV_vCode,
        v.RGV_iRG_ID,
        ISNULL(NULLIF(LTRIM(RTRIM(v.RGV_vDefectReasonType)), ''), g.RG_vDescription) AS Reference_Type,
        ISNULL(v.RGV_vDefectReasonType, '') AS DefectReasonType
    FROM ReferenceGroup_Value v
    INNER JOIN Reference_Group g ON v.RGV_iRG_ID = g.RG_iID
    WHERE v.RGV_vStatus = 'A'
      AND g.RG_vStatus  = 'A'
      AND (
          @ReferenceType IS NULL
          OR LTRIM(RTRIM(v.RGV_vDefectReasonType)) = @ReferenceType
          OR LTRIM(RTRIM(g.RG_vDescription)) = @ReferenceType
          OR LTRIM(RTRIM(g.RG_vCode)) = @ReferenceType
      )
      AND (
          (@ParsedID IS NOT NULL AND v.RGV_iRG_ID = @ParsedID)
          OR (@ParsedID IS NULL AND @Type_ID IS NOT NULL
              AND (g.RG_vCode = @Type_ID OR g.RG_vDescription = @Type_ID))
          OR (@Type_ID IS NULL AND @ReferenceType IS NOT NULL
              AND (v.RGV_iRG_ID = 17 OR LTRIM(RTRIM(v.RGV_vDefectReasonType)) = @ReferenceType))
          OR (@Type_ID IS NULL AND @ReferenceType IS NULL
              AND v.RGV_iRG_ID IN (12, 13, 17))
      )
    ORDER BY v.RGV_vDesciption ASC;
END;
GO

-- =========================================================================================
-- SECTION 4: STORED PROCEDURE: pr_insert_Formation_Rework_Rejection
-- =========================================================================================
CREATE OR ALTER PROCEDURE pr_insert_Formation_Rework_Rejection
    @FRR_Formation_ID       INT = 68,
    @FRR_Type_ID            INT,
    @FRR_Type_Name          VARCHAR(50),
    @FRR_Reason             VARCHAR(500),
    @FRR_Employee_ID        INT,
    @FRR_Employee_Name      VARCHAR(100) = NULL,
    @FRR_Created_By         VARCHAR(50)  = 'System',
    @FRR_QRCode             VARCHAR(50)  = NULL,
    @FRR_BatteryType        VARCHAR(100) = NULL,
    @FRR_CKTNo              VARCHAR(50)  = NULL,
    @FRR_Cell1              VARCHAR(50)  = NULL,
    @FRR_Cell2              VARCHAR(50)  = NULL,
    @FRR_Cell3              VARCHAR(50)  = NULL,
    @FRR_Cell4              VARCHAR(50)  = NULL,
    @FRR_Cell5              VARCHAR(50)  = NULL,
    @FRR_Cell6              VARCHAR(50)  = NULL,
    @FRR_DefectCell         VARCHAR(50)  = NULL,
    @FRR_OCV                VARCHAR(50)  = NULL,
    @FRR_PlateNo            VARCHAR(50)  = NULL,
    @FRR_Location           VARCHAR(50)  = NULL,
    @FRR_PlateType          VARCHAR(50)  = NULL,
    @FRR_DefectReasonType   VARCHAR(100) = NULL,
    @FRR_DefectReason       VARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Lookup employee name if missing
    IF (@FRR_Employee_Name IS NULL OR @FRR_Employee_Name = '')
    BEGIN
        SELECT @FRR_Employee_Name = EM_Employee_Name 
        FROM Employee_Master 
        WHERE EM_ID = @FRR_Employee_ID;
    END

    -- Lookup type name if missing
    IF (@FRR_Type_Name IS NULL OR @FRR_Type_Name = '')
    BEGIN
        SELECT @FRR_Type_Name = RG_vDescription 
        FROM Reference_Group 
        WHERE RG_iID = @FRR_Type_ID;
    END

    -- Ensure reason fields are populated
    IF (@FRR_Reason IS NULL OR @FRR_Reason = '')
    BEGIN
        SET @FRR_Reason = @FRR_DefectReason;
    END
    IF (@FRR_DefectReason IS NULL OR @FRR_DefectReason = '')
    BEGIN
        SET @FRR_DefectReason = @FRR_Reason;
    END

    INSERT INTO Formation_Rework_Rejection (
        FRR_Formation_ID,
        FRR_Type_ID,
        FRR_Type_Name,
        FRR_Reason,
        FRR_Employee_ID,
        FRR_Employee_Name,
        FRR_Created_By,
        FRR_Created_Date,
        FRR_Status,
        FRR_QRCode,
        FRR_BatteryType,
        FRR_CKTNo,
        FRR_Cell1,
        FRR_Cell2,
        FRR_Cell3,
        FRR_Cell4,
        FRR_Cell5,
        FRR_Cell6,
        FRR_DefectCell,
        FRR_OCV,
        FRR_PlateNo,
        FRR_Location,
        FRR_PlateType,
        FRR_DefectReasonType,
        FRR_DefectReason
    )
    VALUES (
        @FRR_Formation_ID,
        @FRR_Type_ID,
        @FRR_Type_Name,
        @FRR_Reason,
        @FRR_Employee_ID,
        @FRR_Employee_Name,
        @FRR_Created_By,
        GETDATE(),
        'A',
        @FRR_QRCode,
        @FRR_BatteryType,
        @FRR_CKTNo,
        @FRR_Cell1,
        @FRR_Cell2,
        @FRR_Cell3,
        @FRR_Cell4,
        @FRR_Cell5,
        @FRR_Cell6,
        @FRR_DefectCell,
        @FRR_OCV,
        @FRR_PlateNo,
        @FRR_Location,
        @FRR_PlateType,
        @FRR_DefectReasonType,
        @FRR_DefectReason
    );
END;
GO

-- =========================================================================
-- SECTION 5: STORED PROCEDURE: pr_fetch_Formation_Rework_Rejection_List
-- =========================================================================
CREATE OR ALTER PROCEDURE pr_fetch_Formation_Rework_Rejection_List
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ROW_NUMBER() OVER(ORDER BY FRR_ID DESC) AS [SNo],
        FRR_ID,
        ISNULL(FRR_QRCode, '') AS FRR_QRCode,
        ISNULL(FRR_BatteryType, '') AS FRR_BatteryType,
        ISNULL(FRR_CKTNo, '') AS FRR_CKTNo,
        FRR_Type_Name,
        FRR_Reason,
        ISNULL(FRR_DefectReasonType, '') AS FRR_DefectReasonType,
        ISNULL(FRR_DefectReason, ISNULL(FRR_Reason, '')) AS FRR_DefectReason,
        FRR_Employee_Name,
        FRR_Created_By,
        CONVERT(VARCHAR, FRR_Created_Date, 103) + ' ' + CONVERT(VARCHAR(5), FRR_Created_Date, 108) AS FRR_Created_Date,
        FRR_Status,
        ISNULL(FRR_Cell1, '') AS FRR_Cell1,
        ISNULL(FRR_Cell2, '') AS FRR_Cell2,
        ISNULL(FRR_Cell3, '') AS FRR_Cell3,
        ISNULL(FRR_Cell4, '') AS FRR_Cell4,
        ISNULL(FRR_Cell5, '') AS FRR_Cell5,
        ISNULL(FRR_Cell6, '') AS FRR_Cell6,
        ISNULL(FRR_DefectCell, '') AS FRR_DefectCell,
        ISNULL(FRR_OCV, '') AS FRR_OCV,
        ISNULL(FRR_PlateNo, '') AS FRR_PlateNo,
        ISNULL(FRR_Location, '') AS FRR_Location,
        ISNULL(FRR_PlateType, '') AS FRR_PlateType
    FROM Formation_Rework_Rejection
    WHERE FRR_Status = 'A'
    ORDER BY FRR_ID DESC;
END;
GO

-- =========================================================================
-- SECTION 6: STORED PROCEDURE: pr_fetch_Rework_Rejection_Report
-- =========================================================================
CREATE OR ALTER PROCEDURE pr_fetch_Rework_Rejection_Report
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
        R.FRR_ID,
        'F' + RIGHT('000' + CAST(R.FRR_ID AS VARCHAR(10)), 3) AS [Formation_ID],
        ISNULL(R.FRR_QRCode, '') AS [FRR_QRCode],
        ISNULL(NULLIF(R.FRR_BatteryType, ''), ISNULL(M.mv_BatteryType, '')) AS [FRR_BatteryType],
        ISNULL(NULLIF(R.FRR_CKTNo, ''), ISNULL(M.mv_CKTNo, '')) AS [FRR_CKTNo],
        R.FRR_Formation_ID,
        R.FRR_Type_ID,
        R.FRR_Type_Name,
        R.FRR_Reason,
        ISNULL(R.FRR_DefectReasonType, '') AS [FRR_DefectReasonType],
        ISNULL(R.FRR_DefectReason, ISNULL(R.FRR_Reason, '')) AS [FRR_DefectReason],
        R.FRR_Employee_ID,
        R.FRR_Employee_Name,
        R.FRR_Created_By,
        CONVERT(VARCHAR, R.FRR_Created_Date, 103) + ' ' + CONVERT(VARCHAR(5), R.FRR_Created_Date, 108) AS FRR_Created_Date,
        R.FRR_Status,
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
    FROM Formation_Rework_Rejection R
    OUTER APPLY (
        SELECT TOP 1 mv_BatteryType, mv_CKTNo
        FROM MVRLA
        WHERE mv_QRCode = R.FRR_QRCode
        ORDER BY mv_ID DESC
    ) M
    WHERE R.FRR_Status = 'A'
      AND (
          @Type IS NULL 
          OR @Type = '' 
          OR @Type = 'All' 
          OR R.FRR_Type_Name = @Type 
          OR CAST(R.FRR_Type_ID AS VARCHAR(50)) = @Type
      )
      AND (
          @ParsedFrom IS NULL
          OR CAST(R.FRR_Created_Date AS DATE) >= @ParsedFrom
      )
      AND (
          @ParsedTo IS NULL
          OR CAST(R.FRR_Created_Date AS DATE) <= @ParsedTo
      )
    ORDER BY R.FRR_ID DESC;
END;
GO

PRINT 'All Stored Procedures created/altered successfully.';
GO

-- =========================================================================
-- SECTION 7: VERIFICATION & TEST QUERIES
-- =========================================================================
PRINT '--- Verifying Latest Entry (Record with Serial 701D567766) ---';
SELECT TOP 1 
    FRR_ID,
    FRR_QRCode,
    FRR_BatteryType,
    FRR_CKTNo,
    FRR_Type_Name,
    FRR_Reason,
    FRR_DefectReasonType,
    FRR_DefectReason,
    FRR_Employee_Name,
    FRR_Created_Date
FROM Formation_Rework_Rejection 
ORDER BY FRR_ID DESC;
GO

PRINT '--- Verifying Report Query for Today ---';
EXEC pr_fetch_Rework_Rejection_Report @Type = 'All';
GO
