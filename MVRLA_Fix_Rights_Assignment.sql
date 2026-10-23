-- =========================================================================================
-- AMARA RAJA MVRLA FORMATION - SETTING MASTER, USER MASTER & RIGHTS ASSIGNMENT FIX SCRIPT
-- File: MVRLA_Fix_Rights_Assignment.sql
-- Description:
--   1. Fixes dbo.fget_Module_Name and dbo.fget_Menu_Name functions.
--   2. Fixes dbo.pr_get_Reference_Values for 'User_Type' and 'Role' to populate dropdowns.
--   3. Syncs roles in Refernce_Group_Value (Admin=19, Assembly=20, Formation=21, Finishing=22, Assy=23).
--   4. Fixes dbo.Pr_Load_MD_vModuleName and dbo.pr_get_T_Setting_Menu_Master join conditions so
--      menus are always returned with checkboxes accurately reflecting role permissions.
--   5. Fixes dbo.Pr_Insert_T_Setting_Role_Assignment for saving rights assignments.
--   6. Adds 'Setting Master' (../../Setting/Menus) under Setting (ID: 28) in T_Setting_Menu_Master.
--   7. Assigns full rights for Setting, User_Master, Rights Assignment, and Setting Master to Admin (Role 19).
--   8. Fixes dbo.Pr_Fetch_T_Setting_UserMaster_Details, Pr_Insert_T_Setting_UserMaster,
--      Pr_Update_T_Setting_UserMaster, and Pr_Delete_T_Setting_UserMaster.
--   9. Fixes dbo.Pr_Fetch_T_Setting_Menu_Master_Details and Pr_Load_MM_vMenu_Name.
-- =========================================================================================

USE [MVRLA_Formation];
GO

PRINT '>>> Step 1: Create or alter function dbo.fget_Module_Name';
GO
CREATE OR ALTER FUNCTION dbo.fget_Module_Name
(
    @MID varchar(50)
)
RETURNS varchar(100)
AS
BEGIN
    DECLARE @name varchar(100);
    SELECT TOP 1 @name = MD_vModuleName 
    FROM dbo.T_Settings_Modules 
    WHERE CAST(MD_iid AS varchar(50)) = @MID OR CAST(MD_vMenuID AS varchar(50)) = @MID;
    
    RETURN ISNULL(@name, 'Admin');
END;
GO

PRINT '>>> Step 2: Create or alter function dbo.fget_Menu_Name';
GO
CREATE OR ALTER FUNCTION dbo.fget_Menu_Name
(
    @ID int
)
RETURNS varchar(333)
AS
BEGIN
    DECLARE @data varchar(333);
    IF @ID = 0 OR @ID IS NULL
        RETURN 'Main Menu';

    -- Look up in modern menu master table
    SELECT @data = MM_vMenu_Name 
    FROM dbo.T_Setting_Menu_Master 
    WHERE MM_iId = @ID;

    -- Fallback to legacy menu master if not found
    IF @data IS NULL
        SELECT @data = MM_Display_Name 
        FROM dbo.S_Menu_Master 
        WHERE MM_ID = @ID;

    RETURN ISNULL(@data, 'Main Menu');
END;
GO

PRINT '>>> Step 3: Ensure User_Type and Role values exist in Refernce_Group_Value';
GO
-- Ensure Role 19 (Admin) exists in Refernce_Group_Value
IF NOT EXISTS (SELECT 1 FROM dbo.Refernce_Group_Value WHERE RGV_iID = 19)
BEGIN
    SET IDENTITY_INSERT dbo.Refernce_Group_Value ON;
    INSERT INTO dbo.Refernce_Group_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vCreatedby, RGV_dCreatedDate, RGV_vStatus)
    VALUES (19, 9, '001', 'Admin', 'admin', GETDATE(), 'A');
    SET IDENTITY_INSERT dbo.Refernce_Group_Value OFF;
END
ELSE
BEGIN
    UPDATE dbo.Refernce_Group_Value 
    SET RGV_vDesciption = 'Admin', RGV_vStatus = 'A' 
    WHERE RGV_iID = 19;
END

-- Ensure Role 20 (Assembly)
IF NOT EXISTS (SELECT 1 FROM dbo.Refernce_Group_Value WHERE RGV_iID = 20)
BEGIN
    SET IDENTITY_INSERT dbo.Refernce_Group_Value ON;
    INSERT INTO dbo.Refernce_Group_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vCreatedby, RGV_dCreatedDate, RGV_vStatus)
    VALUES (20, 9, '005', 'Assembly', 'Admin', GETDATE(), 'A');
    SET IDENTITY_INSERT dbo.Refernce_Group_Value OFF;
END
ELSE
BEGIN
    UPDATE dbo.Refernce_Group_Value SET RGV_vDesciption = 'Assembly', RGV_vStatus = 'A' WHERE RGV_iID = 20;
END

-- Ensure Role 21 (Formation)
IF NOT EXISTS (SELECT 1 FROM dbo.Refernce_Group_Value WHERE RGV_iID = 21)
BEGIN
    SET IDENTITY_INSERT dbo.Refernce_Group_Value ON;
    INSERT INTO dbo.Refernce_Group_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vCreatedby, RGV_dCreatedDate, RGV_vStatus)
    VALUES (21, 9, '004', 'Formation', 'Admin', GETDATE(), 'A');
    SET IDENTITY_INSERT dbo.Refernce_Group_Value OFF;
END
ELSE
BEGIN
    UPDATE dbo.Refernce_Group_Value SET RGV_vDesciption = 'Formation', RGV_vStatus = 'A' WHERE RGV_iID = 21;
END

-- Ensure Role 22 (Finishing)
IF NOT EXISTS (SELECT 1 FROM dbo.Refernce_Group_Value WHERE RGV_iID = 22)
BEGIN
    SET IDENTITY_INSERT dbo.Refernce_Group_Value ON;
    INSERT INTO dbo.Refernce_Group_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vCreatedby, RGV_dCreatedDate, RGV_vStatus)
    VALUES (22, 9, '006', 'Finishing', 'Admin', GETDATE(), 'A');
    SET IDENTITY_INSERT dbo.Refernce_Group_Value OFF;
END
ELSE
BEGIN
    UPDATE dbo.Refernce_Group_Value SET RGV_vDesciption = 'Finishing', RGV_vStatus = 'A' WHERE RGV_iID = 22;
END

-- Ensure Role 23 (Assy)
IF NOT EXISTS (SELECT 1 FROM dbo.Refernce_Group_Value WHERE RGV_iID = 23)
BEGIN
    SET IDENTITY_INSERT dbo.Refernce_Group_Value ON;
    INSERT INTO dbo.Refernce_Group_Value (RGV_iID, RGV_iRG_ID, RGV_vCode, RGV_vDesciption, RGV_vCreatedby, RGV_dCreatedDate, RGV_vStatus)
    VALUES (23, 9, '003', 'Assy', 'admin', GETDATE(), 'A');
    SET IDENTITY_INSERT dbo.Refernce_Group_Value OFF;
END
ELSE
BEGIN
    UPDATE dbo.Refernce_Group_Value SET RGV_vDesciption = 'Assy', RGV_vStatus = 'A' WHERE RGV_iID = 23;
END
GO

PRINT '>>> Step 4: Ensure Reference_Group has User_Type and Role codes';
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Reference_Group WHERE RG_vCode = 'User_Type')
BEGIN
    INSERT INTO dbo.Reference_Group (RG_iID, RG_vCode, RG_vDescription, RG_vUpdatedBy, RG_dUpdateDate, RG_vStatus)
    VALUES (14, 'User_Type', 'User Type', 'Admin', GETDATE(), 'A');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Reference_Group WHERE RG_vCode = 'Role')
BEGIN
    INSERT INTO dbo.Reference_Group (RG_iID, RG_vCode, RG_vDescription, RG_vUpdatedBy, RG_dUpdateDate, RG_vStatus)
    VALUES (15, 'Role', 'Role', 'Admin', GETDATE(), 'A');
END
GO

PRINT '>>> Step 5: Create or alter stored procedure dbo.pr_get_Reference_Values';
GO
CREATE OR ALTER PROC dbo.pr_get_Reference_Values              
    @type varchar(50)              
AS              
BEGIN              
    IF (@type IN ('User_Type', 'Role', 'User Type'))
    BEGIN
        SELECT 
            RGV_iID,
            RGV_vDesciption,
            RGV_vCode
        FROM dbo.Refernce_Group_Value
        WHERE (RGV_iID IN (19, 20, 21, 22, 23) OR RGV_vDesciption IN ('Admin', 'Assembly', 'Formation', 'Finishing', 'Assy'))
          AND RGV_vStatus = 'A'
        ORDER BY RGV_iID;
    END
    ELSE
    BEGIN
        SELECT 
            v.RGV_iID,
            v.RGV_vDesciption,
            v.RGV_vCode
        FROM dbo.Refernce_Group_Value v
        LEFT OUTER JOIN dbo.Reference_Group g ON v.RGV_iRG_ID = g.RG_iID              
        WHERE (g.RG_vCode = @type OR v.RGV_vCode = @type) 
          AND v.RGV_vStatus = 'A'          
        ORDER BY v.RGV_vDesciption;
    END
END;
GO

PRINT '>>> Step 6: Create or alter stored procedure dbo.Pr_Load_MD_vModuleName';
GO
CREATE OR ALTER PROC dbo.Pr_Load_MD_vModuleName      
AS      
BEGIN      
    SELECT 
        ISNULL(NULLIF(MD_vMenuID, 0), 1) AS MD_iid, 
        MD_vModuleName  
    FROM dbo.T_Settings_Modules      
    WHERE MD_vStatus = 'A'
    ORDER BY MD_iid;
END;
GO

PRINT '>>> Step 7: Create or alter stored procedure dbo.pr_get_T_Setting_Menu_Master';
GO
CREATE OR ALTER PROC dbo.pr_get_T_Setting_Menu_Master
    @Role int = 0,                
    @Module int = 1,
    @Organization int = 1                
AS                
BEGIN                
    SELECT           
        ISNULL(ra.RA_iID, 0) AS RA_iID,          
        ISNULL(ra.RA_iRole, @Role) AS RA_iRole,          
        mm.MM_vMenu_Name,          
        mm.MM_iId,          
        dbo.fget_Menu_Name(mm.MM_iParentID) AS Parent,      
        ISNULL(ra.RA_iVisibility, 0) AS RA_iVisibility,          
        ISNULL(ra.RA_iView, 0) AS RA_iView,          
        ISNULL(ra.RA_iAdd, 0) AS RA_iAdd,          
        ISNULL(ra.RA_iEdit, 0) AS RA_iEdit,          
        ISNULL(ra.RA_iDelete, 0) AS RA_iDelete          
    FROM dbo.T_Setting_Menu_Master mm                  
    LEFT OUTER JOIN dbo.T_Setting_Role_Assignment ra
        ON mm.MM_iId = ra.RA_iMenuID 
        AND ra.RA_iRole = @Role
        AND ra.RA_vStatus = 'A'
    WHERE mm.MM_vStatus = 'A' 
      AND (
          mm.MM_vModule = CAST(@Module AS varchar(20)) 
          OR @Module = 0 
          OR @Module = 6
          OR mm.MM_vModule = '1'
      )
    ORDER BY mm.MM_iParentID, mm.MM_iMenu_Order;
END;
GO

PRINT '>>> Step 8: Create or alter stored procedure dbo.Pr_Insert_T_Setting_Role_Assignment';
GO
CREATE OR ALTER PROC dbo.Pr_Insert_T_Setting_Role_Assignment        
    @RA_iRole int,        
    @RA_iMenuID int,        
    @RA_iVisibility int,        
    @RA_iView int,        
    @RA_iAdd int,        
    @RA_iEdit int,        
    @RA_iDelete int,        
    @RA_iOrganization int = 1,        
    @RA_vCreatedby varchar(50) = 'Admin'        
AS        
BEGIN        
    IF EXISTS (
        SELECT 1 
        FROM dbo.T_Setting_Role_Assignment 
        WHERE RA_iRole = @RA_iRole 
          AND RA_iMenuID = @RA_iMenuID 
          AND RA_iOrganization = @RA_iOrganization
    )      
    BEGIN      
        UPDATE dbo.T_Setting_Role_Assignment      
        SET      
            RA_iVisibility = @RA_iVisibility,      
            RA_iView = @RA_iView,      
            RA_iAdd = @RA_iAdd,      
            RA_iEdit = @RA_iEdit,      
            RA_iDelete = @RA_iDelete,      
            RA_vUpdatedBy = @RA_vCreatedby,
            RA_dUpdateDate = GETDATE(),
            RA_vStatus = 'A'      
        WHERE RA_iRole = @RA_iRole 
          AND RA_iMenuID = @RA_iMenuID 
          AND RA_iOrganization = @RA_iOrganization;      
    END      
    ELSE      
    BEGIN      
        INSERT INTO dbo.T_Setting_Role_Assignment        
        (        
            RA_iRole,        
            RA_iMenuID,        
            RA_iVisibility,        
            RA_iView,        
            RA_iAdd,        
            RA_iEdit,        
            RA_iDelete,        
            RA_iOrganization,        
            RA_vCreatedby,        
            RA_dCreatedDate,        
            RA_vUpdatedBy,        
            RA_dUpdateDate,        
            RA_vStatus        
        )        
        VALUES        
        (        
            @RA_iRole,        
            @RA_iMenuID,        
            @RA_iVisibility,        
            @RA_iView,        
            @RA_iAdd,        
            @RA_iEdit,        
            @RA_iDelete,        
            @RA_iOrganization,        
            @RA_vCreatedby,        
            GETDATE(),        
            NULL,        
            NULL,        
            'A'        
        );      
    END        

    SELECT 'True' AS Result;
END;
GO

PRINT '>>> Step 9: Add Setting Master under Setting (Parent ID 28) in T_Setting_Menu_Master';
GO
IF NOT EXISTS (
    SELECT 1 
    FROM dbo.T_Setting_Menu_Master 
    WHERE MM_vURL LIKE '%Setting/Menus%' OR (MM_vMenu_Name = 'Setting Master' AND MM_iParentID = 28)
)
BEGIN
    SET IDENTITY_INSERT dbo.T_Setting_Menu_Master ON;
    INSERT INTO dbo.T_Setting_Menu_Master
    (
        MM_iId,
        MM_vModule,
        MM_vMenu_Name,
        MM_vUpdatedBy,
        MM_iMenu_Order,
        MM_iMenuLevel,
        MM_iParentID,
        MM_vURL,
        MM_vIcon,
        MM_iOrganization,
        MM_vCreatedby,
        MM_dCreatedDate,
        MM_vStatus
    )
    VALUES
    (
        1114,
        '1',
        'Setting Master',
        'Setting_Master',
        3,
        2,
        28,
        '../../Setting/Menus',
        'fa fa-circle-o',
        1,
        'Admin',
        GETDATE(),
        'A'
    );
    SET IDENTITY_INSERT dbo.T_Setting_Menu_Master OFF;
    PRINT 'Setting Master menu item inserted (MM_iId=1114).';
END
ELSE
BEGIN
    UPDATE dbo.T_Setting_Menu_Master
    SET MM_vMenu_Name = 'Setting Master',
        MM_iParentID = 28,
        MM_vURL = '../../Setting/Menus',
        MM_vStatus = 'A'
    WHERE MM_vURL LIKE '%Setting/Menus%' OR (MM_vMenu_Name = 'Setting Master' AND MM_iParentID = 28);
    PRINT 'Setting Master menu item updated.';
END
GO

PRINT '>>> Step 10: Assign Setting, User_Master, Rights Assignment, and Setting Master rights to Admin (Role 19)';
GO
-- Ensure parent menu Setting (28) and child menus (8, 9, 1114) are fully granted to Admin (Role 19)
MERGE dbo.T_Setting_Role_Assignment AS target
USING (
    SELECT 19 AS RA_iRole, 28 AS RA_iMenuID, 1 AS RA_iVisibility, 1 AS RA_iView, 1 AS RA_iAdd, 1 AS RA_iEdit, 1 AS RA_iDelete, 1 AS RA_iOrganization
    UNION ALL
    SELECT 19, 8, 1, 1, 1, 1, 1, 1
    UNION ALL
    SELECT 19, 9, 1, 1, 1, 1, 1, 1
    UNION ALL
    SELECT 19, 1114, 1, 1, 1, 1, 1, 1
) AS src
ON target.RA_iRole = src.RA_iRole AND target.RA_iMenuID = src.RA_iMenuID AND target.RA_iOrganization = src.RA_iOrganization
WHEN MATCHED THEN
    UPDATE SET 
        target.RA_iVisibility = src.RA_iVisibility,
        target.RA_iView = src.RA_iView,
        target.RA_iAdd = src.RA_iAdd,
        target.RA_iEdit = src.RA_iEdit,
        target.RA_iDelete = src.RA_iDelete,
        target.RA_vStatus = 'A'
WHEN NOT MATCHED THEN
    INSERT (RA_iRole, RA_iMenuID, RA_iVisibility, RA_iView, RA_iAdd, RA_iEdit, RA_iDelete, RA_iOrganization, RA_vCreatedby, RA_dCreatedDate, RA_vStatus)
    VALUES (src.RA_iRole, src.RA_iMenuID, src.RA_iVisibility, src.RA_iView, src.RA_iAdd, src.RA_iEdit, src.RA_iDelete, src.RA_iOrganization, 'Admin', GETDATE(), 'A');
GO

PRINT '>>> Step 11: Create or alter stored procedures for T_Setting_UserMaster';
GO
-- Fetch User Master details
CREATE OR ALTER PROC dbo.Pr_Fetch_T_Setting_UserMaster_Details      
AS      
BEGIN      
    SELECT 
        u.UM_iid AS U_ID,      
        u.UM_vUser_Name AS U_UserName,      
        u.UM_vPassword AS U_Password,      
        ISNULL(r.RGV_vDesciption, u.UM_vUserCode) AS UM_iRightst,      
        u.UM_vCreatedBy, 
        u.UM_vUserCode AS U_Rights,     
        CONVERT(varchar, u.UM_dCreatedDate, 103) AS UM_dCreatedDate,      
        u.UM_vStatus      
    FROM dbo.T_Setting_UserMaster u
    LEFT OUTER JOIN dbo.Refernce_Group_Value r 
        ON TRY_CAST(u.UM_vUserCode AS int) = r.RGV_iID      
    WHERE u.UM_vStatus = 'A'      
    ORDER BY u.UM_iid DESC;
END;
GO

-- Insert User Master
CREATE OR ALTER PROC dbo.Pr_Insert_T_Setting_UserMaster      
    @UM_vUser_Name varchar(350) = NULL,      
    @UM_vPassword varchar(350) = NULL,      
    @UM_iRightst int = NULL,      
    @UM_vCreatedBy varchar(50) = NULL,    
    @UM_vUserCode varchar(33) = NULL,
    @U_UserName varchar(350) = NULL,
    @U_Password varchar(350) = NULL,
    @U_Rights varchar(33) = NULL,
    @U_CreatedDBy varchar(50) = NULL,
    @U_LodgeID varchar(33) = NULL
AS      
BEGIN      
    DECLARE @userName varchar(350) = ISNULL(@U_UserName, @UM_vUser_Name);
    DECLARE @pwd varchar(350) = ISNULL(@U_Password, @UM_vPassword);
    DECLARE @createdBy varchar(50) = ISNULL(@U_CreatedDBy, @UM_vCreatedBy);
    DECLARE @userCode varchar(33) = ISNULL(@U_Rights, @UM_vUserCode);
    DECLARE @rightsId int = ISNULL(TRY_CAST(@U_LodgeID AS int), ISNULL(@UM_iRightst, TRY_CAST(@userCode AS int)));
    
    DECLARE @UM_iid int;
    SELECT @UM_iid = ISNULL(MAX(UM_iid), 0) + 1 FROM dbo.T_Setting_UserMaster;
    
    INSERT INTO dbo.T_Setting_UserMaster      
    (      
        UM_iid,      
        UM_vUser_Name,      
        UM_vPassword,      
        UM_iRightst,      
        UM_vCreatedBy,      
        UM_dCreatedDate,      
        UM_vStatus,    
        UM_vUserCode      
    )      
    VALUES      
    (      
        @UM_iid,      
        @userName,      
        @pwd,      
        @rightsId,      
        @createdBy,      
        GETDATE(),      
        'A',    
        @userCode    
    );

    SELECT 'True' AS Result;
END;
GO

-- Update User Master
CREATE OR ALTER PROC dbo.Pr_Update_T_Setting_UserMaster      
    @UM_iid int = NULL,      
    @UM_vUser_Name varchar(350) = NULL,      
    @UM_vPassword varchar(350) = NULL,      
    @UM_iRightst int = NULL,      
    @UM_vCreatedBy varchar(50) = NULL,    
    @UM_vUserCode varchar(33) = NULL,
    @U_ID int = NULL,
    @U_UserName varchar(350) = NULL,
    @U_Password varchar(350) = NULL,
    @U_Rights varchar(33) = NULL,
    @U_CreatedDBy varchar(50) = NULL,
    @U_LodgeID varchar(33) = NULL
AS      
BEGIN      
    DECLARE @id int = ISNULL(@U_ID, @UM_iid);
    DECLARE @userName varchar(350) = ISNULL(@U_UserName, @UM_vUser_Name);
    DECLARE @pwd varchar(350) = ISNULL(@U_Password, @UM_vPassword);
    DECLARE @createdBy varchar(50) = ISNULL(@U_CreatedDBy, @UM_vCreatedBy);
    DECLARE @userCode varchar(33) = ISNULL(@U_Rights, @UM_vUserCode);
    DECLARE @rightsId int = ISNULL(TRY_CAST(@U_LodgeID AS int), ISNULL(@UM_iRightst, TRY_CAST(@userCode AS int)));

    UPDATE dbo.T_Setting_UserMaster      
    SET        
        UM_vUser_Name = @userName,    
        UM_vPassword = @pwd,      
        UM_iRightst = @rightsId,    
        UM_vUserCode = @userCode,    
        UM_vCreatedBy = ISNULL(@createdBy, UM_vCreatedBy),
        UM_dCreatedDate = GETDATE()
    WHERE UM_iid = @id;

    SELECT 'True' AS Result;
END;
GO

-- Delete User Master
CREATE OR ALTER PROC dbo.Pr_Delete_T_Setting_UserMaster    
    @UM_iid int = NULL,
    @ID int = NULL
AS    
BEGIN    
    DECLARE @idToDel int = ISNULL(@ID, @UM_iid);
    UPDATE dbo.T_Setting_UserMaster    
    SET UM_vStatus = 'D'    
    WHERE UM_iid = @idToDel;

    SELECT 'True' AS Result;
END;
GO

PRINT '>>> Step 12: Create or alter stored procedures for T_Setting_Menu_Master (Menus page)';
GO
-- Fetch Menu Master Details
CREATE OR ALTER PROC dbo.Pr_Fetch_T_Setting_Menu_Master_Details          
AS          
BEGIN          
    SELECT 
        mm.MM_iId,          
        dbo.fget_Module_Name(mm.MM_vModule) AS MM_vModule,          
        mm.MM_vMenu_Name,          
        mm.MM_iMenu_Order,          
        mm.MM_iMenuLevel,          
        dbo.fget_Menu_Name(mm.MM_iParentID) AS Parent_Name,
        mm.MM_iParentID,          
        mm.MM_vURL,          
        mm.MM_vIcon,          
        mm.MM_iOrganization,          
        mm.MM_vCreatedby,          
        CONVERT(varchar, mm.MM_dCreatedDate, 103) AS MM_dCreatedDate,          
        mm.MM_vUpdatedBy,          
        CONVERT(varchar, mm.MM_dUpdateDate, 103) AS MM_dUpdateDate,          
        mm.MM_vStatus          
    FROM dbo.T_Setting_Menu_Master mm         
    WHERE mm.MM_vStatus = 'A'          
    ORDER BY mm.MM_iId DESC;        
END;
GO

-- Load Menu Name dropdown by level and module
CREATE OR ALTER PROC dbo.Pr_Load_MM_vMenu_Name           
    @id int,      
    @module int          
AS            
BEGIN            
    IF (@id = 1)            
    BEGIN            
        SELECT 0 AS MM_iId, 'Main' AS MM_vMenu_Name;             
    END            
    ELSE            
    BEGIN            
        SELECT 
            MM_iId, 
            MM_vMenu_Name  
        FROM dbo.T_Setting_Menu_Master            
        WHERE MM_vStatus = 'A' 
          AND MM_iMenuLevel = (@id - 1)     
          AND (
              MM_vModule = CAST(@module AS varchar(20)) 
              OR @module = 0 
              OR @module = 6 
              OR MM_vModule = '1'
          )
        ORDER BY MM_vMenu_Name;        
    END            
END;
GO

-- Insert Menu Master
CREATE OR ALTER PROC dbo.Pr_Insert_T_Setting_Menu_Master              
    @MM_vModule int,              
    @MM_vMenu_Name varchar(250),              
    @MM_iMenu_Order int,              
    @MM_iMenuLevel int,              
    @MM_iParentID int,              
    @MM_vURL varchar(550),              
    @MM_vIcon varchar(250),              
    @MM_iOrganization int = 1,              
    @MM_vCreatedby varchar(50) = 'Admin'              
AS              
BEGIN              
    INSERT INTO dbo.T_Setting_Menu_Master              
    (              
        MM_vModule,              
        MM_vMenu_Name,              
        MM_iMenu_Order,              
        MM_iMenuLevel,              
        MM_iParentID,              
        MM_vURL,              
        MM_vIcon,              
        MM_iOrganization,              
        MM_vCreatedby,              
        MM_dCreatedDate,              
        MM_vUpdatedBy,              
        MM_dUpdateDate,              
        MM_vStatus              
    )              
    VALUES              
    (              
        @MM_vModule,              
        @MM_vMenu_Name,              
        @MM_iMenu_Order,              
        @MM_iMenuLevel,              
        @MM_iParentID,              
        @MM_vURL,              
        @MM_vIcon,              
        @MM_iOrganization,              
        @MM_vCreatedby,              
        GETDATE(),              
        NULL,              
        NULL,              
        'A'              
    );              

    DECLARE @newMenuId int = SCOPE_IDENTITY();            

    -- Automatically assign rights to Admin (Role 19)
    INSERT INTO dbo.T_Setting_Role_Assignment            
    (            
        RA_iRole,            
        RA_iMenuID,            
        RA_iVisibility,
        RA_iView,            
        RA_iAdd,            
        RA_iEdit,            
        RA_iDelete,            
        RA_iOrganization,            
        RA_vCreatedby,            
        RA_dCreatedDate,            
        RA_vUpdatedBy,            
        RA_dUpdateDate,            
        RA_vStatus            
    )            
    VALUES            
    (            
        19, -- Admin role           
        @newMenuId,            
        1,            
        1,            
        1,            
        1,            
        1,
        @MM_iOrganization,            
        'System',            
        GETDATE(),            
        NULL,            
        NULL,            
        'A'            
    );            

    SELECT 'True' AS Result;
END;
GO

PRINT '=========================================================================================';
PRINT '>>> MVRLA_Fix_Rights_Assignment.sql executed successfully!';
PRINT '=========================================================================================';
GO
