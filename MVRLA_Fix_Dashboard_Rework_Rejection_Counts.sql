/*
  ====================================================================
  HOTFIX SCRIPT: Fix Dashboard Rework Today & Rejection Today Counts
  Target Database: [MVRLA_Formation] on localhost
  Application    : Formation Operational Dashboard (Home/Dashboard.cshtml)
  Date           : 2026-10-26
  ====================================================================
  REQUIREMENTS SATISFIED:
  1. Real-time current day only counts for Rework and Rejection.
  2. Queries existing transaction data in dbo.Formation_Rework_Rejection.
  3. No INSERT into dashboard table; calculated dynamically.
  4. Automatically resets on new day / midnight because filter is 
     CAST(FRR_Created_Date AS DATE) = CAST(GETDATE() AS DATE).
  5. Provides standalone SPs:
     - pr_fetch_Today_Rework_Count
     - pr_fetch_Today_Rejection_Count
     - pr_fetch_Today_Rework_Rejection_Counts (parameterized by @Type)
     - pr_fetch_Today_Rework_Chart (defect reason breakdown)
     - pr_fetch_Today_Rejection_Chart (defect reason breakdown)
  ====================================================================
*/

USE [MVRLA_Formation];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- --------------------------------------------------------------------
-- 1. STORED PROCEDURE: pr_fetch_Today_Rework_Count
-- --------------------------------------------------------------------
PRINT 'Creating/Updating stored procedure [dbo].[pr_fetch_Today_Rework_Count]...';
GO

CREATE OR ALTER PROCEDURE [dbo].[pr_fetch_Today_Rework_Count]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
    DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today);

    SELECT ISNULL(COUNT(*), 0) AS Rework_Today
    FROM dbo.Formation_Rework_Rejection
    WHERE FRR_Status = 'A'
      AND (FRR_Type_Name = 'Rework' OR FRR_Type_ID = 12)
      AND (FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow);
END;
GO

-- --------------------------------------------------------------------
-- 2. STORED PROCEDURE: pr_fetch_Today_Rejection_Count
-- --------------------------------------------------------------------
PRINT 'Creating/Updating stored procedure [dbo].[pr_fetch_Today_Rejection_Count]...';
GO

CREATE OR ALTER PROCEDURE [dbo].[pr_fetch_Today_Rejection_Count]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
    DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today);

    SELECT ISNULL(COUNT(*), 0) AS Rejection_Today
    FROM dbo.Formation_Rework_Rejection
    WHERE FRR_Status = 'A'
      AND (FRR_Type_Name = 'Rejection' OR FRR_Type_ID = 13)
      AND (FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow);
END;
GO

-- --------------------------------------------------------------------
-- 3. STORED PROCEDURE: pr_fetch_Today_Rework_Rejection_Counts
-- --------------------------------------------------------------------
PRINT 'Creating/Updating stored procedure [dbo].[pr_fetch_Today_Rework_Rejection_Counts]...';
GO

CREATE OR ALTER PROCEDURE [dbo].[pr_fetch_Today_Rework_Rejection_Counts]
    @Type VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
    DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today);

    IF (@Type = 'Rework')
    BEGIN
        SELECT ISNULL(COUNT(*), 0) AS Rework_Today
        FROM dbo.Formation_Rework_Rejection
        WHERE FRR_Status = 'A'
          AND (FRR_Type_Name = 'Rework' OR FRR_Type_ID = 12)
          AND (FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow);
        RETURN;
    END

    IF (@Type = 'Rejection')
    BEGIN
        SELECT ISNULL(COUNT(*), 0) AS Rejection_Today
        FROM dbo.Formation_Rework_Rejection
        WHERE FRR_Status = 'A'
          AND (FRR_Type_Name = 'Rejection' OR FRR_Type_ID = 13)
          AND (FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow);
        RETURN;
    END

    SELECT 
        ISNULL(COUNT(CASE WHEN (FRR_Type_Name = 'Rework' OR FRR_Type_ID = 12) THEN 1 END), 0) AS Rework_Today,
        ISNULL(COUNT(CASE WHEN (FRR_Type_Name = 'Rejection' OR FRR_Type_ID = 13) THEN 1 END), 0) AS Rejection_Today
    FROM dbo.Formation_Rework_Rejection
    WHERE FRR_Status = 'A'
      AND (FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow);
END;
GO

-- --------------------------------------------------------------------
-- 4. STORED PROCEDURE: pr_fetch_Today_Rework_Chart
-- --------------------------------------------------------------------
PRINT 'Creating/Updating stored procedure [dbo].[pr_fetch_Today_Rework_Chart]...';
GO

CREATE OR ALTER PROCEDURE [dbo].[pr_fetch_Today_Rework_Chart]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
    DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today);

    SELECT 
        ISNULL(NULLIF(RTRIM(LTRIM(FRR_Reason)), ''), 'Unknown') AS Reason,
        COUNT(1) AS Qty
    FROM dbo.Formation_Rework_Rejection
    WHERE FRR_Status = 'A'
      AND (FRR_Type_Name = 'Rework' OR FRR_Type_ID = 12)
      AND (FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow)
    GROUP BY ISNULL(NULLIF(RTRIM(LTRIM(FRR_Reason)), ''), 'Unknown')
    ORDER BY Qty DESC;
END;
GO

-- --------------------------------------------------------------------
-- 5. STORED PROCEDURE: pr_fetch_Today_Rejection_Chart
-- --------------------------------------------------------------------
PRINT 'Creating/Updating stored procedure [dbo].[pr_fetch_Today_Rejection_Chart]...';
GO

CREATE OR ALTER PROCEDURE [dbo].[pr_fetch_Today_Rejection_Chart]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
    DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today);

    SELECT 
        ISNULL(NULLIF(RTRIM(LTRIM(FRR_Reason)), ''), 'Unknown') AS Reason,
        COUNT(1) AS Qty
    FROM dbo.Formation_Rework_Rejection
    WHERE FRR_Status = 'A'
      AND (FRR_Type_Name = 'Rejection' OR FRR_Type_ID = 13)
      AND (FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow)
    GROUP BY ISNULL(NULLIF(RTRIM(LTRIM(FRR_Reason)), ''), 'Unknown')
    ORDER BY Qty DESC;
END;
GO

PRINT 'Successfully created/updated Dashboard Rework and Rejection procedures.';
GO
