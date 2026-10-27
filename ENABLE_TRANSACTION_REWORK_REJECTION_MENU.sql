-- =========================================================================
-- Script: ENABLE_TRANSACTION_REWORK_REJECTION_MENU.sql
-- Description: Ensures the 'Rework and Rejection' menu item is registered under
--              Transactions in T_Setting_Menu_Master and visible to all roles
--              in T_Setting_Role_Assignment.
-- =========================================================================

SET NOCOUNT ON;

PRINT '====================================================================';
PRINT 'Ensuring Transaction -> Rework and Rejection Menu Visibility';
PRINT '====================================================================';

-- 1. Identify or default Parent Menu ID for Transactions
DECLARE @ParentId INT = NULL;

-- Try finding parent 'Transactions' or 'Formation'
SELECT TOP 1 @ParentId = [MM_iId]
FROM [dbo].[T_Setting_Menu_Master]
WHERE [MM_iMenuLevel] = 1 
  AND ([MM_vUpdatedBy] IN ('Transactions', 'Transaction', 'Formation')
       OR [MM_vMenu_Name] LIKE '%Transaction%'
       OR [MM_vMenu_Name] LIKE '%Formation%')
ORDER BY [MM_iId] ASC;

IF @ParentId IS NULL
BEGIN
    SET @ParentId = 68; -- Standard parent ID in MVRLA schema
END

PRINT 'Using Parent ID: ' + CAST(@ParentId AS NVARCHAR(20));

-- 2. Insert or update menu entry in T_Setting_Menu_Master
DECLARE @MenuId INT = NULL;

SELECT TOP 1 @MenuId = [MM_iId]
FROM [dbo].[T_Setting_Menu_Master]
WHERE [MM_vUpdatedBy] = 'Rework_Rejection'
   OR [MM_vURL] LIKE '%Transaction/Rework_Rejection%';

IF @MenuId IS NULL
BEGIN
    PRINT 'Adding Rework and Rejection menu entry to T_Setting_Menu_Master...';
    
    -- Check if identity insert is required
    SELECT @MenuId = ISNULL(MAX([MM_iId]), 1111) + 1 FROM [dbo].[T_Setting_Menu_Master];
    IF @MenuId < 1112 SET @MenuId = 1112;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[T_Setting_Menu_Master] WHERE [MM_iId] = @MenuId)
    BEGIN
        SET IDENTITY_INSERT [dbo].[T_Setting_Menu_Master] ON;
        INSERT INTO [dbo].[T_Setting_Menu_Master]
            ([MM_iId], [MM_vModule], [MM_vMenu_Name], [MM_vUpdatedBy], [MM_iMenu_Order],
             [MM_iMenuLevel], [MM_iParentID], [MM_vURL], [MM_vIcon], [MM_iOrganization],
             [MM_vCreatedby], [MM_dCreatedDate], [MM_dUpdateDate], [MM_vStatus])
        VALUES
            (@MenuId, 1, N'Rework and Rejection', N'Rework_Rejection', 12,
             2, @ParentId, N'../../Transaction/Rework_Rejection', N'fa fa-circle-o', 1,
             N'Admin', GETDATE(), GETDATE(), N'A');
        SET IDENTITY_INSERT [dbo].[T_Setting_Menu_Master] OFF;
        PRINT 'Inserted Menu ID: ' + CAST(@MenuId AS NVARCHAR(20));
    END
END
ELSE
BEGIN
    PRINT 'Menu entry already exists with ID: ' + CAST(@MenuId AS NVARCHAR(20));
    UPDATE [dbo].[T_Setting_Menu_Master]
    SET [MM_vStatus] = 'A',
        [MM_vURL] = '../../Transaction/Rework_Rejection',
        [MM_vMenu_Name] = 'Rework and Rejection',
        [MM_iParentID] = @ParentId
    WHERE [MM_iId] = @MenuId;
END

-- 3. Grant visibility to all active roles in T_Setting_Role_Assignment
PRINT 'Configuring role visibility for Menu ID: ' + CAST(@MenuId AS NVARCHAR(20));

DECLARE @RoleId INT;
DECLARE role_cursor CURSOR LOCAL FOR
    SELECT DISTINCT [RM_iId] 
    FROM [dbo].[T_Setting_Role_Master]
    WHERE ISNULL([RM_vStatus], 'A') = 'A'
    UNION
    SELECT DISTINCT [RA_iRole] 
    FROM [dbo].[T_Setting_Role_Assignment];

OPEN role_cursor;
FETCH NEXT FROM role_cursor INTO @RoleId;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM [dbo].[T_Setting_Role_Assignment] 
        WHERE [RA_iRole] = @RoleId AND [RA_iMenuID] = @MenuId
    )
    BEGIN
        INSERT INTO [dbo].[T_Setting_Role_Assignment]
            ([RA_iRole], [RA_iMenuID], [RA_iVisibility], [RA_iView],
             [RA_iAdd], [RA_iEdit], [RA_iDelete], [RA_iOrganization],
             [RA_vCreatedby], [RA_dCreatedDate], [RA_vStatus])
        VALUES
            (@RoleId, @MenuId, 1, 1,
             1, 1, 1, 1,
             N'Admin', GETDATE(), N'A');
        PRINT 'Granted visibility to Role: ' + CAST(@RoleId AS NVARCHAR(20));
    END
    ELSE
    BEGIN
        UPDATE [dbo].[T_Setting_Role_Assignment]
        SET [RA_iVisibility] = 1,
            [RA_iView] = 1,
            [RA_vStatus] = 'A'
        WHERE [RA_iRole] = @RoleId AND [RA_iMenuID] = @MenuId;
    END

    FETCH NEXT FROM role_cursor INTO @RoleId;
END;

CLOSE role_cursor;
DEALLOCATE role_cursor;

PRINT '====================================================================';
PRINT 'Done! Rework and Rejection menu visibility is now enabled.';
PRINT '====================================================================';
