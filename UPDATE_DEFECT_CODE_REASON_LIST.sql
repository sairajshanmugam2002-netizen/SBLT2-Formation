-- =========================================================================================
-- MVRLA Formation: Defect Reason List Update Query
-- File: UPDATE_DEFECT_CODE_REASON_LIST.sql
-- Description:
--   Updates Reference Master entries under Type 'Defect Reason' so that the
--   Defect Code Reason dropdown on Transaction/Rework_Rejection fetches
--   the correct mapped defect reasons (Electrical, Mechanical, Quality, etc.).
-- =========================================================================================

USE [MVRLA_Formation];
GO

-- -----------------------------------------------------------------------------------------
-- STEP 1: Ensure RGV_vDefectReasonType column exists
-- -----------------------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM sys.columns 
    WHERE object_id = OBJECT_ID('ReferenceGroup_Value') 
      AND name = 'RGV_vDefectReasonType'
)
BEGIN
    ALTER TABLE ReferenceGroup_Value ADD RGV_vDefectReasonType VARCHAR(100) NULL;
    PRINT 'Added RGV_vDefectReasonType column to ReferenceGroup_Value.';
END
GO

-- -----------------------------------------------------------------------------------------
-- STEP 2: Find or create the 'Defect Reason' Reference Group (Group 17)
-- -----------------------------------------------------------------------------------------
DECLARE @DefectReasonGroupID INT;
SELECT @DefectReasonGroupID = RG_iID 
FROM Reference_Group 
WHERE RG_vDescription = 'Defect Reason' OR RG_vCode = 'DR';

IF (@DefectReasonGroupID IS NULL)
BEGIN
    SET @DefectReasonGroupID = 17;
    IF NOT EXISTS (SELECT 1 FROM Reference_Group WHERE RG_iID = 17)
    BEGIN
        INSERT INTO Reference_Group (RG_iID, RG_vCode, RG_vDescription, RG_vStatus)
        VALUES (17, 'DR', 'Defect Reason', 'A');
        PRINT 'Created Reference Group 17: Defect Reason.';
    END
END
PRINT 'Using Defect Reason Group ID: ' + CAST(@DefectReasonGroupID AS VARCHAR(10));
GO

-- -----------------------------------------------------------------------------------------
-- STEP 3: UPDATE QUERIES - Map Reference Master Reasons to Defect Code Reason Types
-- -----------------------------------------------------------------------------------------

DECLARE @DR_ID INT;
SELECT @DR_ID = RG_iID FROM Reference_Group WHERE RG_vDescription = 'Defect Reason';
IF @DR_ID IS NULL SET @DR_ID = 17;

-- 3.1. Update Electrical Defect Reasons
UPDATE ReferenceGroup_Value
SET 
    RGV_iRG_ID = @DR_ID,
    RGV_vDefectReasonType = 'Electrical',
    RGV_dUpdateDate = GETDATE(),
    RGV_vUpdatedBy = 'Admin'
WHERE RGV_vDesciption IN (
    'OCV High', 
    'OCV Low', 
    'Cell Voltage Issue', 
    'Cell Reverse', 
    'Short Circuit', 
    'High Resistance',
    'Low Voltage',
    'Open Circuit'
);
PRINT 'Updated Electrical defect reasons.';

-- 3.2. Update Mechanical Defect Reasons
UPDATE ReferenceGroup_Value
SET 
    RGV_iRG_ID = @DR_ID,
    RGV_vDefectReasonType = 'Mechanical',
    RGV_dUpdateDate = GETDATE(),
    RGV_vUpdatedBy = 'Admin'
WHERE RGV_vDesciption IN (
    'Plate Crack', 
    'Plate Damage', 
    'Terminal Damage', 
    'Container Damage', 
    'Cover Damage', 
    'Post Damage',
    'Intercell Weld Defect',
    'Separator Damage'
);
PRINT 'Updated Mechanical defect reasons.';

-- 3.3. Update Quality Defect Reasons
UPDATE ReferenceGroup_Value
SET 
    RGV_iRG_ID = @DR_ID,
    RGV_vDefectReasonType = 'Quality',
    RGV_dUpdateDate = GETDATE(),
    RGV_vUpdatedBy = 'Admin'
WHERE RGV_vDesciption IN (
    'Wrong Plate', 
    'Acid Leakage', 
    'Specific Gravity Low', 
    'Specific Gravity High', 
    'Wrong Product',
    'Color Mismatch',
    'Visual Defect'
);
PRINT 'Updated Quality defect reasons.';
GO

-- -----------------------------------------------------------------------------------------
-- STEP 4: Insert any missing Defect Code Reasons into ReferenceGroup_Value if not present
-- -----------------------------------------------------------------------------------------
DECLARE @DR_ID INT;
SELECT @DR_ID = RG_iID FROM Reference_Group WHERE RG_vDescription = 'Defect Reason';
IF @DR_ID IS NULL SET @DR_ID = 17;

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'OCV High')
BEGIN
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES ((SELECT ISNULL(MAX(RGV_iID),0)+1 FROM ReferenceGroup_Value), @DR_ID, 'EL01', 'OCV High', 'Admin', GETDATE(), 'A', 'Electrical');
END

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'OCV Low')
BEGIN
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES ((SELECT ISNULL(MAX(RGV_iID),0)+1 FROM ReferenceGroup_Value), @DR_ID, 'EL02', 'OCV Low', 'Admin', GETDATE(), 'A', 'Electrical');
END

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'Plate Crack')
BEGIN
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES ((SELECT ISNULL(MAX(RGV_iID),0)+1 FROM ReferenceGroup_Value), @DR_ID, 'ME01', 'Plate Crack', 'Admin', GETDATE(), 'A', 'Mechanical');
END

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'Plate Damage')
BEGIN
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES ((SELECT ISNULL(MAX(RGV_iID),0)+1 FROM ReferenceGroup_Value), @DR_ID, 'ME02', 'Plate Damage', 'Admin', GETDATE(), 'A', 'Mechanical');
END

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'Wrong Plate')
BEGIN
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES ((SELECT ISNULL(MAX(RGV_iID),0)+1 FROM ReferenceGroup_Value), @DR_ID, 'QU01', 'Wrong Plate', 'Admin', GETDATE(), 'A', 'Quality');
END
GO

-- -----------------------------------------------------------------------------------------
-- STEP 5: VERIFICATION QUERY - View the updated Defect Code Reason List
-- -----------------------------------------------------------------------------------------
SELECT 
    v.RGV_iID                AS [ID],
    g.RG_vDescription        AS [Reference_Group_Type],
    v.RGV_vCode              AS [Defect_Code],
    v.RGV_vDesciption        AS [Defect_Code_Reason],
    v.RGV_vDefectReasonType  AS [Mapped_Type],
    v.RGV_vStatus            AS [Status],
    v.RGV_dUpdateDate        AS [Updated_Date]
FROM ReferenceGroup_Value v
INNER JOIN Reference_Group g ON v.RGV_iRG_ID = g.RG_iID
WHERE g.RG_vDescription = 'Defect Reason' OR v.RGV_iRG_ID = 17
ORDER BY v.RGV_vDefectReasonType, v.RGV_vDesciption;
GO
