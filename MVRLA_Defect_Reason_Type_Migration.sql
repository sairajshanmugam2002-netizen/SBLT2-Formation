-- =========================================================================================
-- Enhancement Script: Defect Reason Type Configuration & Integration
-- Database: MVRLA_Formation
-- Description:
--   1. Adds RGV_vDefectReasonType to ReferenceGroup_Value
--   2. Adds FRR_DefectReasonType and FRR_DefectReason to Formation_Rework_Rejection
--   3. Configures 'Defect Reason Type' and 'Defect Reason' in Reference_Group & ReferenceGroup_Value
--   4. Updates Reference Master Stored Procedures (Pr_Insert_ReferenceGroup_Value,
--      Pr_Update_ReferenceGroup_Value, Pr_Fetch_ReferenceGroup_Value_Details)
--   5. Creates pr_fetch_Defect_Reason_Types and pr_fetch_Defect_Reasons_ByType
--   6. Updates pr_insert_Formation_Rework_Rejection, pr_fetch_Formation_Rework_Rejection_List,
--      and pr_fetch_Rework_Rejection_Report
-- =========================================================================================

USE [MVRLA_Formation];
GO

-- 1. ADD COLUMNS TO ReferenceGroup_Value
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('ReferenceGroup_Value') AND name = 'RGV_vDefectReasonType')
BEGIN
    ALTER TABLE ReferenceGroup_Value ADD RGV_vDefectReasonType VARCHAR(100) NULL;
END
GO

-- 2. ADD COLUMNS TO Formation_Rework_Rejection
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_DefectReasonType')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_DefectReasonType VARCHAR(100) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_DefectReason')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_DefectReason VARCHAR(500) NULL;
END
GO

-- 3. CONFIGURE REFERENCE GROUPS FOR 'Defect Reason Type' AND 'Defect Reason'
IF NOT EXISTS (SELECT 1 FROM Reference_Group WHERE RG_vDescription = 'Defect Reason Type')
BEGIN
    DECLARE @NewRGID INT = (SELECT ISNULL(MAX(RG_iID), 0) + 1 FROM Reference_Group);
    INSERT INTO Reference_Group (RG_iID, RG_vCode, RG_vDescription, RG_vStatus)
    VALUES (@NewRGID, 'DRT', 'Defect Reason Type', 'A');
END
GO

IF NOT EXISTS (SELECT 1 FROM Reference_Group WHERE RG_vDescription = 'Defect Reason')
BEGIN
    DECLARE @NewRGID2 INT = (SELECT ISNULL(MAX(RG_iID), 0) + 1 FROM Reference_Group);
    INSERT INTO Reference_Group (RG_iID, RG_vCode, RG_vDescription, RG_vStatus)
    VALUES (@NewRGID2, 'DR', 'Defect Reason', 'A');
END
GO

-- 4. CONFIGURE SEED DATA FOR DEFECT REASON TYPES (Electrical, Mechanical, Quality)
DECLARE @DRT_ID INT = (SELECT RG_iID FROM Reference_Group WHERE RG_vDescription = 'Defect Reason Type');

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_iRG_ID = @DRT_ID AND RGV_vDesciption = 'Electrical')
BEGIN
    DECLARE @NextID INT = (SELECT ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value);
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES (@NextID, @DRT_ID, 'ELEC', 'Electrical', 'System', GETDATE(), 'A', 'Electrical');
END

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_iRG_ID = @DRT_ID AND RGV_vDesciption = 'Mechanical')
BEGIN
    DECLARE @NextID2 INT = (SELECT ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value);
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES (@NextID2, @DRT_ID, 'MECH', 'Mechanical', 'System', GETDATE(), 'A', 'Mechanical');
END

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_iRG_ID = @DRT_ID AND RGV_vDesciption = 'Quality')
BEGIN
    DECLARE @NextID3 INT = (SELECT ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value);
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES (@NextID3, @DRT_ID, 'QUAL', 'Quality', 'System', GETDATE(), 'A', 'Quality');
END
GO

-- 5. CONFIGURE SEED DEFECT REASONS MAPPED TO DEFECT REASON TYPES
DECLARE @DR_ID INT = (SELECT RG_iID FROM Reference_Group WHERE RG_vDescription = 'Defect Reason');
IF @DR_ID IS NULL SET @DR_ID = 12;

-- Electrical Reasons
IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'OCV High')
BEGIN
    DECLARE @ID1 INT = (SELECT ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value);
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES (@ID1, @DR_ID, 'EL01', 'OCV High', 'System', GETDATE(), 'A', 'Electrical');
END

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'OCV Low')
BEGIN
    DECLARE @ID2 INT = (SELECT ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value);
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES (@ID2, @DR_ID, 'EL02', 'OCV Low', 'System', GETDATE(), 'A', 'Electrical');
END

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'Cell Voltage Issue')
BEGIN
    DECLARE @ID3 INT = (SELECT ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value);
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES (@ID3, @DR_ID, 'EL03', 'Cell Voltage Issue', 'System', GETDATE(), 'A', 'Electrical');
END

-- Mechanical Reasons
IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'Plate Damage')
BEGIN
    DECLARE @ID4 INT = (SELECT ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value);
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES (@ID4, @DR_ID, 'MC01', 'Plate Damage', 'System', GETDATE(), 'A', 'Mechanical');
END

IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'Plate Crack')
BEGIN
    DECLARE @ID5 INT = (SELECT ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value);
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES (@ID5, @DR_ID, 'MC02', 'Plate Crack', 'System', GETDATE(), 'A', 'Mechanical');
END

-- Quality Reasons
IF NOT EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = 'Wrong Plate')
BEGIN
    DECLARE @ID6 INT = (SELECT ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value);
    INSERT INTO ReferenceGroup_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vUpdatedBy, RGV_dUpdateDate, RGV_vStatus, RGV_vDefectReasonType)
    VALUES (@ID6, @DR_ID, 'QL01', 'Wrong Plate', 'System', GETDATE(), 'A', 'Quality');
END
GO

-- Also update existing rework/rejection reasons with default Defect Reason Types if null
UPDATE ReferenceGroup_Value SET RGV_vDefectReasonType = 'Electrical' WHERE RGV_vDesciption IN ('Charging Problem', 'Terminal Problem') AND RGV_vDefectReasonType IS NULL;
UPDATE ReferenceGroup_Value SET RGV_vDefectReasonType = 'Mechanical' WHERE RGV_vDesciption IN ('Physical Damage', 'Label Problem') AND RGV_vDefectReasonType IS NULL;
UPDATE ReferenceGroup_Value SET RGV_vDefectReasonType = 'Quality' WHERE RGV_vDesciption IN ('Quality Failure', 'Wrong Product') AND RGV_vDefectReasonType IS NULL;
GO

-- 6. STORED PROCEDURE: Pr_Insert_ReferenceGroup_Value
CREATE OR ALTER PROC Pr_Insert_ReferenceGroup_Value    
    @RGV_iRG_ID int,    
    @RGV_vCode varchar(50),    
    @RGV_vDesciption varchar(250),
    @RGV_vDefectReasonType varchar(100) = NULL
AS    
BEGIN    
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = @RGV_vDesciption AND RGV_vStatus = 'A' AND RGV_iRG_ID = @RGV_iRG_ID)                          
    BEGIN                          
        RAISERROR ('Description ''%s'' Already Exists, Try New', 16, -1, @RGV_vDesciption);
        RETURN;
    END          

    DECLARE @RGV_iID INT;
    SELECT @RGV_iID = ISNULL(MAX(RGV_iID), 0) + 1 FROM ReferenceGroup_Value;

    INSERT INTO ReferenceGroup_Value (    
        RGV_iID,    
        RGV_iRG_ID,    
        RGV_vCode,    
        RGV_vDesciption,    
        RGV_vUpdatedBy,    
        RGV_dUpdateDate,    
        RGV_vStatus,
        RGV_vDefectReasonType
    )    
    VALUES (    
        @RGV_iID,    
        @RGV_iRG_ID,    
        @RGV_vCode,    
        @RGV_vDesciption,    
        'System',    
        GETDATE(),    
        'A',
        @RGV_vDefectReasonType
    );
END 
GO

-- 7. STORED PROCEDURE: Pr_Update_ReferenceGroup_Value
CREATE OR ALTER PROC [dbo].[Pr_Update_ReferenceGroup_Value]
    @RGV_iID int,
    @RGV_vCode varchar(50),
    @RGV_vDesciption varchar(250),
    @RGV_vDefectReasonType varchar(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM ReferenceGroup_Value WHERE RGV_vDesciption = @RGV_vDesciption AND RGV_vStatus = 'A' AND RGV_iID != @RGV_iID)
    BEGIN
        RAISERROR ('Description ''%s'' Already Exists, Try New', 16, -1, @RGV_vDesciption);
        RETURN;
    END

    UPDATE ReferenceGroup_Value
    SET
        RGV_vCode = @RGV_vCode,
        RGV_vDesciption = @RGV_vDesciption,
        RGV_vDefectReasonType = ISNULL(@RGV_vDefectReasonType, RGV_vDefectReasonType),
        RGV_dUpdateDate = GETDATE()
    WHERE RGV_iID = @RGV_iID;
END
GO

-- 8. STORED PROCEDURE: Pr_Fetch_ReferenceGroup_Value_Details
CREATE OR ALTER PROC Pr_Fetch_ReferenceGroup_Value_Details    
    @ref_ID int  
AS    
BEGIN    
    SET NOCOUNT ON;

    SELECT     
        RGV_iID AS ID,   
        RGV_vCode AS Code,    
        RGV_vDesciption AS Desciption,    
        RGV_vUpdatedBy AS [Updated By],
        ISNULL(RGV_vDefectReasonType, '') AS DefectReasonType
    FROM ReferenceGroup_Value    
    WHERE RGV_vStatus = 'A'    
      AND RGV_iRG_ID = @ref_ID
    ORDER BY RGV_iID DESC;
END 
GO

-- 9. STORED PROCEDURE: pr_fetch_Defect_Reason_Types
CREATE OR ALTER PROCEDURE pr_fetch_Defect_Reason_Types
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT 
        DefectReasonType
    FROM (
        SELECT LTRIM(RTRIM(RGV_vDefectReasonType)) AS DefectReasonType
        FROM ReferenceGroup_Value
        WHERE RGV_vStatus = 'A' 
          AND RGV_vDefectReasonType IS NOT NULL 
          AND LTRIM(RTRIM(RGV_vDefectReasonType)) <> ''

        UNION

        SELECT LTRIM(RTRIM(v.RGV_vDesciption)) AS DefectReasonType
        FROM ReferenceGroup_Value v
        INNER JOIN Reference_Group g ON v.RGV_iRG_ID = g.RG_iID
        WHERE v.RGV_vStatus = 'A' AND g.RG_vDescription = 'Defect Reason Type'

        UNION
        SELECT 'Electrical'
        UNION
        SELECT 'Mechanical'
        UNION
        SELECT 'Quality'
    ) T
    WHERE DefectReasonType IS NOT NULL AND DefectReasonType <> ''
    ORDER BY DefectReasonType ASC;
END
GO

-- 10. STORED PROCEDURE: pr_fetch_Defect_Reasons_ByType
CREATE OR ALTER PROCEDURE pr_fetch_Defect_Reasons_ByType
    @DefectReasonType VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF (@DefectReasonType IS NULL OR LTRIM(RTRIM(@DefectReasonType)) = '' OR @DefectReasonType = '0')
    BEGIN
        SELECT 
            v.RGV_iID AS ID,
            v.RGV_vDesciption AS Reason,
            v.RGV_vCode AS Code,
            ISNULL(v.RGV_vDefectReasonType, '') AS DefectReasonType
        FROM ReferenceGroup_Value v
        WHERE 1 = 0;
        RETURN;
    END

    SELECT DISTINCT
        v.RGV_iID AS ID,
        v.RGV_vDesciption AS Reason,
        v.RGV_vCode AS Code,
        @DefectReasonType AS DefectReasonType
    FROM ReferenceGroup_Value v
    LEFT JOIN Reference_Group g ON v.RGV_iRG_ID = g.RG_iID
    WHERE v.RGV_vStatus = 'A'
      AND LTRIM(RTRIM(v.RGV_vDesciption)) <> LTRIM(RTRIM(@DefectReasonType))
      AND (
          LTRIM(RTRIM(v.RGV_vDefectReasonType)) = LTRIM(RTRIM(@DefectReasonType))
          OR LTRIM(RTRIM(g.RG_vDescription)) = LTRIM(RTRIM(@DefectReasonType))
      )
    ORDER BY v.RGV_vDesciption ASC;
END
GO

-- 11. UPDATE pr_insert_Formation_Rework_Rejection
CREATE OR ALTER PROC pr_insert_Formation_Rework_Rejection
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

    -- Ensure FRR_Reason and FRR_DefectReason are populated
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
END
GO

-- 12. UPDATE pr_fetch_Formation_Rework_Rejection_List
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
END
GO

-- 13. UPDATE pr_fetch_Rework_Rejection_Report
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
