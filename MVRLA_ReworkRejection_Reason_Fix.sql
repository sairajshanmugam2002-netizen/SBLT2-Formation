-- =========================================================================================
-- MVRLA Formation - Defect Reason & Reference Type Mapping
-- File: MVRLA_ReworkRejection_Reason_Fix.sql
-- Purpose:
--   1. Ensures RGV_vDefectReasonType column exists in ReferenceGroup_Value.
--   2. Alters pr_fetch_Rework_Rejection_Reasons to support Reference Type mapping
--      so that selecting or querying a Reference Type (e.g. Electrical, Mechanical)
--      returns only the Defect Reasons mapped to that Reference Type.
--   3. Preserves full backward compatibility for:
--      - Rework button (Type_ID = 12 / 'Rework') -> returns Rework reasons
--      - Rejection button (Type_ID = 13 / 'Rejection') -> returns Rejection reasons
--      - Combined queries (Type_ID = 17 / 'DR' with ReferenceType)
--   4. Demonstrates test queries for all scenarios.
-- =========================================================================================

USE [MVRLA_Formation];
GO

-- =========================================================================================
-- PART B: DATABASE COLUMN CHECKS
-- =========================================================================================
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'ReferenceGroup_Value' 
      AND COLUMN_NAME = 'RGV_vDefectReasonType'
)
BEGIN
    ALTER TABLE ReferenceGroup_Value ADD RGV_vDefectReasonType VARCHAR(100) NULL;
    PRINT 'Added RGV_vDefectReasonType column to ReferenceGroup_Value.';
END
ELSE
BEGIN
    PRINT 'Column RGV_vDefectReasonType already exists in ReferenceGroup_Value.';
END
GO

-- =========================================================================================
-- PART A: ALTER STORED PROCEDURE pr_fetch_Rework_Rejection_Reasons
-- =========================================================================================
CREATE OR ALTER PROCEDURE pr_fetch_Rework_Rejection_Reasons
    @Type_ID       VARCHAR(100) = NULL,
    @ReferenceType VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Trim input parameters
    SET @Type_ID = NULLIF(LTRIM(RTRIM(@Type_ID)), '');
    SET @ReferenceType = NULLIF(LTRIM(RTRIM(@ReferenceType)), '');

    -- Normalize 'ALL' to NULL (all reasons)
    IF (@Type_ID = 'ALL') SET @Type_ID = NULL;
    IF (@ReferenceType = 'ALL') SET @ReferenceType = NULL;

    -- If a single argument was passed into @Type_ID matching a known Reference Type
    -- (e.g. 'Electrical', 'Mechanical', 'Quality', 'Pneumatic') while @ReferenceType is NULL,
    -- route it to @ReferenceType and default @Type_ID to 17 (Defect Reason group)
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
      -- 1. Reference Type Filter (Electrical, Mechanical, Quality, Pneumatic, etc.)
      AND (
          @ReferenceType IS NULL
          OR LTRIM(RTRIM(v.RGV_vDefectReasonType)) = @ReferenceType
          OR LTRIM(RTRIM(g.RG_vDescription)) = @ReferenceType
          OR LTRIM(RTRIM(g.RG_vCode)) = @ReferenceType
      )
      -- 2. Group / Type Filter (12=Rework, 13=Rejection, 17=Defect Reason)
      AND (
          -- Exact Group ID passed as integer (e.g. 12, 13, 17)
          (@ParsedID IS NOT NULL AND v.RGV_iRG_ID = @ParsedID)
          -- Group code or description passed as string (e.g. 'Rework', 'Rejection', 'DR')
          OR (@ParsedID IS NULL AND @Type_ID IS NOT NULL
              AND (g.RG_vCode = @Type_ID OR g.RG_vDescription = @Type_ID))
          -- If only @ReferenceType is supplied (and @Type_ID is NULL), return Defect Reasons (17)
          OR (@Type_ID IS NULL AND @ReferenceType IS NOT NULL
              AND (v.RGV_iRG_ID = 17 OR LTRIM(RTRIM(v.RGV_vDefectReasonType)) = @ReferenceType))
          -- If neither @Type_ID nor @ReferenceType is supplied, return all active Rework, Rejection, Defect Reasons
          OR (@Type_ID IS NULL AND @ReferenceType IS NULL
              AND v.RGV_iRG_ID IN (12, 13, 17))
      )
    ORDER BY v.RGV_vDesciption ASC;
END;
GO

PRINT 'Stored procedure pr_fetch_Rework_Rejection_Reasons updated successfully.';
GO

-- =========================================================================================
-- PART C: TEST QUERIES
-- =========================================================================================
PRINT '';
PRINT '=============================================================';
PRINT 'TEST 1: Reference Type = Electrical';
PRINT 'Expected: OCV High, OCV Low, Cell Voltage Issue';
PRINT 'Must NOT show: Plate Damage, Plate Crack';
PRINT '=============================================================';
EXEC pr_fetch_Rework_Rejection_Reasons @ReferenceType = 'Electrical';

PRINT '';
PRINT '=============================================================';
PRINT 'TEST 2: Reference Type = Mechanical';
PRINT 'Expected: Plate Crack, Plate Damage';
PRINT 'Must NOT show: OCV High, OCV Low';
PRINT '=============================================================';
EXEC pr_fetch_Rework_Rejection_Reasons @ReferenceType = 'Mechanical';

PRINT '';
PRINT '=============================================================';
PRINT 'TEST 3: Reference Type = Quality';
PRINT 'Expected: Wrong Plate';
PRINT '=============================================================';
EXEC pr_fetch_Rework_Rejection_Reasons @ReferenceType = 'Quality';

PRINT '';
PRINT '=============================================================';
PRINT 'TEST 4: Rework Button Click (Type_ID = 12 / Rework)';
PRINT 'Expected: Terminal Problem, Charging Problem, Label Problem, etc.';
PRINT '=============================================================';
EXEC pr_fetch_Rework_Rejection_Reasons @Type_ID = '12';

PRINT '';
PRINT '=============================================================';
PRINT 'TEST 5: Rejection Selection (Type_ID = 13 / Rejection)';
PRINT 'Expected: Physical Damage, Wrong Product, Quality Failure, etc.';
PRINT '=============================================================';
EXEC pr_fetch_Rework_Rejection_Reasons @Type_ID = '13';

PRINT '';
PRINT '=============================================================';
PRINT 'TEST 6: Combined Parameters (Type_ID = 17 [Defect Reason] AND ReferenceType = Electrical)';
PRINT '=============================================================';
EXEC pr_fetch_Rework_Rejection_Reasons @Type_ID = '17', @ReferenceType = 'Electrical';
GO
