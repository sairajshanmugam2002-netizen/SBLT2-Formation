-- =========================================================================================
-- Enhancement Script: Formation Rework & Rejection Additional Fields
-- Database: MVRLA_Formation
-- Description:
--   1. Adds 11 new columns to Formation_Rework_Rejection (Cell 1-6, Defect Cell, OCV,
--      Plate No, Location, Plate Type)
--   2. Updates pr_insert_Formation_Rework_Rejection to save the new fields
--   3. Updates pr_fetch_Formation_Rework_Rejection_List to return the new fields
--   4. Updates pr_fetch_Rework_Rejection_Report to include the new fields in the report grid & export
-- =========================================================================================

USE [MVRLA_Formation];
GO

-- 1. ADD NEW COLUMNS TO Formation_Rework_Rejection
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell1')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell1 VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell2')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell2 VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell3')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell3 VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell4')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell4 VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell5')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell5 VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Cell6')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Cell6 VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_DefectCell')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_DefectCell VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_OCV')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_OCV VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_PlateNo')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_PlateNo VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_Location')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_Location VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_PlateType')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_PlateType VARCHAR(50) NULL;
END
GO

-- 2. UPDATE INSERT STORED PROCEDURE: pr_insert_Formation_Rework_Rejection
CREATE OR ALTER PROC pr_insert_Formation_Rework_Rejection
    @FRR_Formation_ID  INT = 68,
    @FRR_Type_ID       INT,
    @FRR_Type_Name     VARCHAR(50),
    @FRR_Reason        VARCHAR(500),
    @FRR_Employee_ID   INT,
    @FRR_Employee_Name VARCHAR(100) = NULL,
    @FRR_Created_By    VARCHAR(50)  = 'System',
    @FRR_QRCode        VARCHAR(50)  = NULL,
    @FRR_BatteryType   VARCHAR(100) = NULL,
    @FRR_CKTNo         VARCHAR(50)  = NULL,
    @FRR_Cell1         VARCHAR(50)  = NULL,
    @FRR_Cell2         VARCHAR(50)  = NULL,
    @FRR_Cell3         VARCHAR(50)  = NULL,
    @FRR_Cell4         VARCHAR(50)  = NULL,
    @FRR_Cell5         VARCHAR(50)  = NULL,
    @FRR_Cell6         VARCHAR(50)  = NULL,
    @FRR_DefectCell    VARCHAR(50)  = NULL,
    @FRR_OCV           VARCHAR(50)  = NULL,
    @FRR_PlateNo       VARCHAR(50)  = NULL,
    @FRR_Location      VARCHAR(50)  = NULL,
    @FRR_PlateType     VARCHAR(50)  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- If employee name was not supplied, lookup from Employee_Master
    IF (@FRR_Employee_Name IS NULL OR @FRR_Employee_Name = '')
    BEGIN
        SELECT @FRR_Employee_Name = EM_Employee_Name 
        FROM Employee_Master 
        WHERE EM_ID = @FRR_Employee_ID;
    END

    -- If type name was not supplied, lookup from Reference_Group
    IF (@FRR_Type_Name IS NULL OR @FRR_Type_Name = '')
    BEGIN
        SELECT @FRR_Type_Name = RG_vDescription 
        FROM Reference_Group 
        WHERE RG_iID = @FRR_Type_ID;
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
        FRR_PlateType
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
        @FRR_PlateType
    );
END
GO

-- 3. UPDATE TRANSACTION LIST STORED PROCEDURE: pr_fetch_Formation_Rework_Rejection_List
CREATE OR ALTER PROC pr_fetch_Formation_Rework_Rejection_List
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
END
GO

-- 4. UPDATE REPORT STORED PROCEDURE: pr_fetch_Rework_Rejection_Report
CREATE OR ALTER PROCEDURE pr_fetch_Rework_Rejection_Report
    @Type VARCHAR(50) = 'All',
    @FromDate VARCHAR(20) = NULL,
    @ToDate VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

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
          @FromDate IS NULL 
          OR LTRIM(RTRIM(@FromDate)) = '' 
          OR CAST(R.FRR_Created_Date AS DATE) >= TRY_CAST(@FromDate AS DATE)
      )
      AND (
          @ToDate IS NULL 
          OR LTRIM(RTRIM(@ToDate)) = '' 
          OR CAST(R.FRR_Created_Date AS DATE) <= TRY_CAST(@ToDate AS DATE)
      )
    ORDER BY R.FRR_ID DESC;
END
GO
