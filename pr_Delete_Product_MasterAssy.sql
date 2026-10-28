-- =========================================================================
-- SCRIPT: pr_Delete_Product_MasterAssy.sql
-- DESCRIPTION: Soft-delete stored procedure for Product Master (Product_Master_Assembly).
--              Marks PM_Status = 'D' for the given PM_iID.
-- =========================================================================

USE [MVRLA]; -- Adjust database name if needed
GO

IF OBJECT_ID('dbo.pr_Delete_Product_MasterAssy', 'P') IS NOT NULL
    DROP PROCEDURE dbo.pr_Delete_Product_MasterAssy;
GO

CREATE PROCEDURE dbo.pr_Delete_Product_MasterAssy
    @PM_iID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF (@PM_iID IS NULL OR @PM_iID <= 0)
    BEGIN
        SELECT 'False' AS [Result], 'Invalid Record ID' AS [Message];
        RETURN;
    END

    UPDATE [dbo].[Product_Master_Assembly]
    SET [PM_Status] = 'D'
    WHERE [PM_iID] = @PM_iID;

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
