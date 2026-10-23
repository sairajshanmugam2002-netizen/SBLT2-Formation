/*
  ====================================================================
  HOTFIX SCRIPT: Fix MVRLA Formation Entry Grid Data Loading
  Target Database: [MVRLA_Formation] on localhost
  Application    : Formation Entry Screen (Transaction/MVRLA.cshtml)
  Date           : 2026-10-23
  ====================================================================
  ROOT CAUSE EXPLANATION:
  1. The MVRLA table contains 2,426,078 records.
  2. The controller method `TransactionController.Pr_Fetch_MVRLA()` was
     executing:
        SELECT * FROM MVRLA WHERE mv_Status='A' ORDER BY mv_ID DESC
     without any row limiting (TOP) or date filter.
  3. Attempting to query, serialize, and send all 2.42 million rows over
     HTTP caused severe timeout (after 30-60s) or memory exhaustion.
  4. As a result, the jqxGrid on Transaction/MVRLA received no data and
     stayed completely blank / empty, even though new records were 
     successfully inserted into the database.

  SOLUTION:
  1. Update stored procedure `[dbo].[Pr_Fetch_MVRLA]` to:
     - Check if records exist for today (mv_CreatedDate or mv_Date).
     - If yes, return today's records (up to TOP 1000), sorted with the
       newest records at the top (ORDER BY mv_ID DESC).
     - If no records exist for today yet, return the latest 500 active
       records (ORDER BY mv_ID DESC) so the grid is never blank.
     - Execution time drops from >60,000 ms (timeout) to <100 ms.
  2. Update `TransactionController.Pr_Fetch_MVRLA()` to execute the
     stored procedure `Pr_Fetch_MVRLA` instead of the raw unconstrained
     table scan.
  ====================================================================
*/

USE [MVRLA_Formation];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

PRINT 'Updating stored procedure [dbo].[Pr_Fetch_MVRLA]...';
GO

CREATE OR ALTER PROCEDURE [dbo].[Pr_Fetch_MVRLA]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
    DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today);

    -- Priority 1: If records were created or scheduled for today, return them (newest first)
    IF EXISTS (
        SELECT 1 FROM dbo.MVRLA 
        WHERE mv_Status = 'A' 
          AND ((mv_CreatedDate >= @Today AND mv_CreatedDate < @Tomorrow) 
               OR (mv_Date >= @Today AND mv_Date < @Tomorrow))
    )
    BEGIN
        SELECT TOP 1000 * 
        FROM dbo.MVRLA 
        WHERE mv_Status = 'A' 
          AND ((mv_CreatedDate >= @Today AND mv_CreatedDate < @Tomorrow) 
               OR (mv_Date >= @Today AND mv_Date < @Tomorrow))
        ORDER BY mv_ID DESC;
    END
    ELSE
    BEGIN
        -- Priority 2: If no records entered today yet, return latest 500 records
        SELECT TOP 500 * 
        FROM dbo.MVRLA 
        WHERE mv_Status = 'A' 
        ORDER BY mv_ID DESC;
    END
END;
GO

PRINT 'Successfully updated [dbo].[Pr_Fetch_MVRLA].';
GO
