/*
  ====================================================================
  HOTFIX SCRIPT: Fix Battery Type Wise Passed Qty & Failed Qty Charts
  Target Database: [MVRLA_Formation] on localhost
  Application    : Formation Operational Dashboard (Home/Dashboard.cshtml)
  Date           : 2026-10-23
  ====================================================================
  ROOT CAUSE EXPLANATION:
  1. The existing stored procedures `pr_fetch_passedqty_chart` and 
     `pr_fetch_failedqty_chart` executed:
        SELECT [mv_BatteryType], SUM(CAST([mv_CDPassedQty] AS INT)) AS Qty 
        FROM [dbo].[MVRLA] GROUP BY [mv_BatteryType]
     and
        SELECT [mv_BatteryType], SUM(CAST([mv_CDFailQty] AS INT)) AS Qty 
        FROM [dbo].[MVRLA] GROUP BY [mv_BatteryType]
  2. In this traceability system, each barcode scan represents an individual 
     battery unit (mv_QRCode), and columns `mv_CDPassedQty` and `mv_CDFailQty` 
     are always saved as empty strings ('') in dbo.MVRLA (0 out of 2.42M rows have values).
     In SQL Server, CAST('' AS INT) = 0. Therefore, every single battery type returned Qty = 0.
  3. Rejections / defects are recorded in the new table `dbo.Formation_Rework_Rejection`.
     The old SP never queried `Formation_Rework_Rejection` for today's rejected batteries.
  4. The modern SVG donut chart (modern-donut-chart.js) discards rows where Qty <= 0.
     Since all quantities were 0, the chart displayed: "No data available".

  SOLUTION:
  1. Update `pr_fetch_passedqty_chart` to:
     - Filter by today's operational window (mv_CreatedDate or mv_Date).
     - Count each active scanned battery as 1 passed quantity (or mv_CDPassedQty if numeric).
     - Exclude any batteries that were rejected today in Formation_Rework_Rejection.
     - Filter out zero quantities (HAVING Qty > 0).
  2. Update `pr_fetch_failedqty_chart` to:
     - Aggregate today's Rejections from `dbo.Formation_Rework_Rejection` by FRR_BatteryType.
     - Union any explicit fail quantities in `dbo.MVRLA` (if mv_CDFailQty is numeric > 0).
     - Filter out zero quantities (HAVING Qty > 0).
  ====================================================================
*/

USE [MVRLA_Formation];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- --------------------------------------------------------------------
-- 1. STORED PROCEDURE: pr_fetch_passedqty_chart
-- --------------------------------------------------------------------
PRINT 'Updating stored procedure [dbo].[pr_fetch_passedqty_chart]...';
GO

CREATE OR ALTER PROCEDURE [dbo].[pr_fetch_passedqty_chart]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
    DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today);

    SELECT 
        ISNULL(NULLIF(RTRIM(LTRIM(mv_BatteryType)), ''), 'Unknown') AS mv_BatteryType,
        SUM(CASE 
            WHEN ISNUMERIC(mv_CDPassedQty) = 1 AND CAST(mv_CDPassedQty AS INT) > 0 
            THEN CAST(mv_CDPassedQty AS INT) 
            ELSE 1 
        END) AS Qty
    FROM dbo.MVRLA
    WHERE mv_Status = 'A'
      AND ((mv_CreatedDate >= @Today AND mv_CreatedDate < @Tomorrow)
           OR (mv_Date >= @Today AND mv_Date < @Tomorrow))
      AND (mv_QRCode IS NULL OR mv_QRCode = '' OR mv_QRCode NOT IN (
          SELECT FRR_QRCode 
          FROM dbo.Formation_Rework_Rejection 
          WHERE FRR_Status = 'A' 
            AND (FRR_Type_Name = 'Rejection' OR FRR_Type_ID = 13)
            AND FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow
            AND FRR_QRCode IS NOT NULL AND FRR_QRCode <> ''
      ))
    GROUP BY ISNULL(NULLIF(RTRIM(LTRIM(mv_BatteryType)), ''), 'Unknown')
    HAVING SUM(CASE 
            WHEN ISNUMERIC(mv_CDPassedQty) = 1 AND CAST(mv_CDPassedQty AS INT) > 0 
            THEN CAST(mv_CDPassedQty AS INT) 
            ELSE 1 
        END) > 0
    ORDER BY Qty DESC;
END;
GO

-- --------------------------------------------------------------------
-- 2. STORED PROCEDURE: pr_fetch_failedqty_chart
-- --------------------------------------------------------------------
PRINT 'Updating stored procedure [dbo].[pr_fetch_failedqty_chart]...';
GO

CREATE OR ALTER PROCEDURE [dbo].[pr_fetch_failedqty_chart]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
    DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today);

    SELECT 
        BatteryType AS mv_BatteryType,
        SUM(Qty) AS Qty
    FROM (
        -- 1. Rejections from Formation_Rework_Rejection for today
        SELECT 
            ISNULL(NULLIF(RTRIM(LTRIM(COALESCE(r.FRR_BatteryType, m.mv_BatteryType))), ''), 'Unknown') AS BatteryType,
            COUNT(1) AS Qty
        FROM dbo.Formation_Rework_Rejection r
        LEFT JOIN dbo.MVRLA m ON r.FRR_QRCode = m.mv_QRCode AND m.mv_Status = 'A'
        WHERE r.FRR_Status = 'A'
          AND (r.FRR_Type_Name = 'Rejection' OR r.FRR_Type_ID = 13)
          AND r.FRR_Created_Date >= @Today AND r.FRR_Created_Date < @Tomorrow
        GROUP BY ISNULL(NULLIF(RTRIM(LTRIM(COALESCE(r.FRR_BatteryType, m.mv_BatteryType))), ''), 'Unknown')

        UNION ALL

        -- 2. Any explicit fail quantities logged in MVRLA table for today
        SELECT 
            ISNULL(NULLIF(RTRIM(LTRIM(mv_BatteryType)), ''), 'Unknown') AS BatteryType,
            SUM(CAST(mv_CDFailQty AS INT)) AS Qty
        FROM dbo.MVRLA
        WHERE mv_Status = 'A'
          AND ((mv_CreatedDate >= @Today AND mv_CreatedDate < @Tomorrow)
               OR (mv_Date >= @Today AND mv_Date < @Tomorrow))
          AND ISNUMERIC(mv_CDFailQty) = 1 
          AND CAST(mv_CDFailQty AS INT) > 0
        GROUP BY ISNULL(NULLIF(RTRIM(LTRIM(mv_BatteryType)), ''), 'Unknown')
    ) t
    GROUP BY BatteryType
    HAVING SUM(Qty) > 0
    ORDER BY Qty DESC;
END;
GO

PRINT 'Successfully updated [pr_fetch_passedqty_chart] and [pr_fetch_failedqty_chart].';
GO
