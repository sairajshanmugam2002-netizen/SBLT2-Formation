/*
  ====================================================================
  DATA SYNC SCRIPT: MVRLA_Formation (OLD) <-- Data from Amaron2_Web_Assembly_v2 (NEW)
  Generated: 2026-10-23
  
  This script inserts rows that exist in NEW but are MISSING in OLD:
    - T_Setting_Menu_Master    : 3 new rows (MM_iId: 110, 1112, 1113)
    - T_Setting_Role_Assignment: 4 new rows (RA_iID: 211, 212, 213, 214)
    - Formation_Rework_Rejection: 23 new rows (from the NEW DB)
    - Reference_Group          : 2 new rows (RG_iID: 12 'Rework', 13 'Rejection')
    - ReferenceGroup_Value     : 12 new rows (Rework & Rejection defect reasons)
  
  Tables with IDENTICAL data (no sync needed):
    - T_Setting_UserMaster     : 9 rows each, all PKs match
    - T_Settings_Modules       : 2 rows each, all PKs match
    - Refernce_Group_Value     : 6 rows each, all PKs match (100% identical)
  
  MVRLA: SKIPPED (transactional table - OLD has 2,426,076 rows, NEW has 14,899)
  ====================================================================
  HOW TO RUN:
    1. Back up MVRLA_Formation first.
    2. Run on localhost connected to MVRLA_Formation.
    3. Script uses IF NOT EXISTS - safe to re-run.
  ====================================================================
*/

USE [MVRLA_Formation];
GO
SET NOCOUNT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    -- ================================================================
    -- STEP 1: T_Setting_Menu_Master (3 new rows in NEW)
    -- ================================================================
    --  MM_iId=110  : Filling Report menu entry
    IF NOT EXISTS (SELECT 1 FROM [dbo].[T_Setting_Menu_Master] WHERE [MM_iId] = 110)
    BEGIN
        SET IDENTITY_INSERT [dbo].[T_Setting_Menu_Master] ON;
        INSERT INTO [dbo].[T_Setting_Menu_Master]
            ([MM_iId],[MM_vModule],[MM_vMenu_Name],[MM_vUpdatedBy],[MM_iMenu_Order],
             [MM_iMenuLevel],[MM_iParentID],[MM_vURL],[MM_vIcon],[MM_iOrganization],
             [MM_vCreatedby],[MM_dCreatedDate],[MM_dUpdateDate],[MM_vStatus])
        VALUES
            (110, 1, N'Filling Report', N'Filling_Report', 2,
             2, 27, N'../../Reports/Filling_Report', N'fa fa-circle-o', 1,
             N'admin', '2022-07-27 17:41:01', NULL, N'A');
        SET IDENTITY_INSERT [dbo].[T_Setting_Menu_Master] OFF;
        PRINT 'Inserted T_Setting_Menu_Master MM_iId=110';
    END

    -- MM_iId=1112 : Rework and Rejection transaction menu
    IF NOT EXISTS (SELECT 1 FROM [dbo].[T_Setting_Menu_Master] WHERE [MM_iId] = 1112)
    BEGIN
        SET IDENTITY_INSERT [dbo].[T_Setting_Menu_Master] ON;
        INSERT INTO [dbo].[T_Setting_Menu_Master]
            ([MM_iId],[MM_vModule],[MM_vMenu_Name],[MM_vUpdatedBy],[MM_iMenu_Order],
             [MM_iMenuLevel],[MM_iParentID],[MM_vURL],[MM_vIcon],[MM_iOrganization],
             [MM_vCreatedby],[MM_dCreatedDate],[MM_dUpdateDate],[MM_vStatus])
        VALUES
            (1112, 1, N'Rework and Rejection', N'Rework_Rejection', 12,
             2, 68, N'../../Transaction/Rework_Rejection', N'fa fa-circle-o', 1,
             N'Admin', '2026-10-13 19:09:43', NULL, N'A');
        SET IDENTITY_INSERT [dbo].[T_Setting_Menu_Master] OFF;
        PRINT 'Inserted T_Setting_Menu_Master MM_iId=1112';
    END

    -- MM_iId=1113 : Rework / Rejection report menu
    IF NOT EXISTS (SELECT 1 FROM [dbo].[T_Setting_Menu_Master] WHERE [MM_iId] = 1113)
    BEGIN
        SET IDENTITY_INSERT [dbo].[T_Setting_Menu_Master] ON;
        INSERT INTO [dbo].[T_Setting_Menu_Master]
            ([MM_iId],[MM_vModule],[MM_vMenu_Name],[MM_vUpdatedBy],[MM_iMenu_Order],
             [MM_iMenuLevel],[MM_iParentID],[MM_vURL],[MM_vIcon],[MM_iOrganization],
             [MM_vCreatedby],[MM_dCreatedDate],[MM_dUpdateDate],[MM_vStatus])
        VALUES
            (1113, 1, N'Rework / Rejection', N'Rework_Rejection_Report', 3,
             2, 27, N'../../Reports/Rework_Rejection', N'fa fa-circle-o', 1,
             N'System', '2026-10-14 12:06:28', '2026-10-14 12:06:28', N'A');
        SET IDENTITY_INSERT [dbo].[T_Setting_Menu_Master] OFF;
        PRINT 'Inserted T_Setting_Menu_Master MM_iId=1113';
    END

    -- ================================================================
    -- STEP 2: T_Setting_Role_Assignment (4 new rows in NEW)
    -- ================================================================
    -- RA_iID=211 : Role 21 -> Menu 1112 (Rework/Rejection transaction)
    IF NOT EXISTS (SELECT 1 FROM [dbo].[T_Setting_Role_Assignment] WHERE [RA_iID] = 211)
    BEGIN
        SET IDENTITY_INSERT [dbo].[T_Setting_Role_Assignment] ON;
        INSERT INTO [dbo].[T_Setting_Role_Assignment]
            ([RA_iID],[RA_iRole],[RA_iMenuID],[RA_iVisibility],[RA_iView],
             [RA_iAdd],[RA_iEdit],[RA_iDelete],[RA_iOrganization],
             [RA_vCreatedby],[RA_dCreatedDate],[RA_vUpdatedBy],[RA_dUpdateDate],[RA_vStatus])
        VALUES
            (211, 21, 1112, 1, 1,
             1, 1, 1, 1,
             N'Admin', '2026-10-13 19:09:43', NULL, NULL, N'A');
        SET IDENTITY_INSERT [dbo].[T_Setting_Role_Assignment] OFF;
        PRINT 'Inserted T_Setting_Role_Assignment RA_iID=211';
    END

    -- RA_iID=212 : Role 19 -> Menu 1112 (Rework/Rejection transaction)
    IF NOT EXISTS (SELECT 1 FROM [dbo].[T_Setting_Role_Assignment] WHERE [RA_iID] = 212)
    BEGIN
        SET IDENTITY_INSERT [dbo].[T_Setting_Role_Assignment] ON;
        INSERT INTO [dbo].[T_Setting_Role_Assignment]
            ([RA_iID],[RA_iRole],[RA_iMenuID],[RA_iVisibility],[RA_iView],
             [RA_iAdd],[RA_iEdit],[RA_iDelete],[RA_iOrganization],
             [RA_vCreatedby],[RA_dCreatedDate],[RA_vUpdatedBy],[RA_dUpdateDate],[RA_vStatus])
        VALUES
            (212, 19, 1112, 1, 1,
             1, 1, 1, 1,
             N'Admin', '2026-10-13 19:09:43', NULL, NULL, N'A');
        SET IDENTITY_INSERT [dbo].[T_Setting_Role_Assignment] OFF;
        PRINT 'Inserted T_Setting_Role_Assignment RA_iID=212';
    END

    -- RA_iID=213 : Role 19 -> Menu 1113 (Rework/Rejection report)
    IF NOT EXISTS (SELECT 1 FROM [dbo].[T_Setting_Role_Assignment] WHERE [RA_iID] = 213)
    BEGIN
        SET IDENTITY_INSERT [dbo].[T_Setting_Role_Assignment] ON;
        INSERT INTO [dbo].[T_Setting_Role_Assignment]
            ([RA_iID],[RA_iRole],[RA_iMenuID],[RA_iVisibility],[RA_iView],
             [RA_iAdd],[RA_iEdit],[RA_iDelete],[RA_iOrganization],
             [RA_vCreatedby],[RA_dCreatedDate],[RA_vUpdatedBy],[RA_dUpdateDate],[RA_vStatus])
        VALUES
            (213, 19, 1113, 1, 1,
             1, 1, 1, 1,
             N'System', '2026-10-14 12:06:28', N'System', '2026-10-14 12:06:28', N'A');
        SET IDENTITY_INSERT [dbo].[T_Setting_Role_Assignment] OFF;
        PRINT 'Inserted T_Setting_Role_Assignment RA_iID=213';
    END

    -- RA_iID=214 : Role 21 -> Menu 1113 (Rework/Rejection report)
    IF NOT EXISTS (SELECT 1 FROM [dbo].[T_Setting_Role_Assignment] WHERE [RA_iID] = 214)
    BEGIN
        SET IDENTITY_INSERT [dbo].[T_Setting_Role_Assignment] ON;
        INSERT INTO [dbo].[T_Setting_Role_Assignment]
            ([RA_iID],[RA_iRole],[RA_iMenuID],[RA_iVisibility],[RA_iView],
             [RA_iAdd],[RA_iEdit],[RA_iDelete],[RA_iOrganization],
             [RA_vCreatedby],[RA_dCreatedDate],[RA_vUpdatedBy],[RA_dUpdateDate],[RA_vStatus])
        VALUES
            (214, 21, 1113, 1, 1,
             1, 1, 1, 1,
             N'System', '2026-10-14 12:06:28', N'System', '2026-10-14 12:06:28', N'A');
        SET IDENTITY_INSERT [dbo].[T_Setting_Role_Assignment] OFF;
        PRINT 'Inserted T_Setting_Role_Assignment RA_iID=214';
    END

    -- ================================================================
    -- STEP 3: Formation_Rework_Rejection (23 rows in NEW, 0 in OLD)
    --         These are the actual rework/rejection records from production.
    --         Only inserted if OLD table has 0 rows for that FRR_ID.
    -- ================================================================

    -- NOTE: FRR_ID is IDENTITY in the new schema. We use SET IDENTITY_INSERT
    --       to preserve the original IDs from the NEW DB.

    IF OBJECT_ID(N'[dbo].[Formation_Rework_Rejection]', 'U') IS NULL
    BEGIN
        RAISERROR('Table Formation_Rework_Rejection does not exist. Run MVRLA_Upgrade.sql first!', 16, 1);
        RETURN;
    END

    SET IDENTITY_INSERT [dbo].[Formation_Rework_Rejection] ON;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=2)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (2,68,12,N'Rework',N'battery',16,N'Rambabu. I (1007875)',N'formation','2026-10-13 22:37:04',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=3)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (3,68,12,N'Rework',N'Charging Problem',14,N'Ashoka Chakravarthi. V (1005637)',N'formation','2026-10-14 11:24:40',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=4)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (4,68,13,N'Rejection',N'Physical Damage',9,N'Aji Basha. S.K (1001816)',N'formation','2026-10-14 11:24:57',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=5)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (5,68,13,N'Rejection',N'Quality Failure',12,N'Betappa Naidu. N (1002889)',N'formation','2026-10-14 11:25:37',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=6)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (6,68,13,N'Rejection',N'Wrong Product',14,N'Ashoka Chakravarthi. V (1005637)',N'formation','2026-10-14 11:37:11',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=7)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (7,68,12,N'Rework',N'Charging Problem',19,N'Arun Raj. E.P (1004603)',N'formation','2026-10-14 11:37:28',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=8)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (8,68,12,N'Rework',N'Label Problem',14,N'Ashoka Chakravarthi. V (1005637)',N'formation','2026-10-14 11:37:45',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=9)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (9,68,13,N'Rejection',N'Quality Failure',9,N'Aji Basha. S.K (1001816)',N'formation','2026-10-14 11:38:14',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=10)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (10,68,12,N'Rework',N'Charging Problem',19,N'Arun Raj. E.P (1004603)',N'formation','2026-10-14 12:12:11',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=11)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (11,68,12,N'Rework',N'human problem',14,N'Ashoka Chakravarthi. V (1005637)',N'formation','2026-10-14 12:31:23',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=14)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (14,68,12,N'Rework',N'Terminal Problem',9,N'Aji Basha. S.K (1001816)',N'formation','2026-10-14 16:25:54',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=15)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (15,68,13,N'Rejection',N'product failure',9,N'Aji Basha. S.K (1001816)',N'formation','2026-10-14 17:12:12',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=16)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (16,68,13,N'Rejection',N'product damage',11,N'Bhujendra. T (1002099)',N'formation','2026-10-14 17:12:54',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=17)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (17,68,13,N'Rejection',N'Physical Damage',19,N'Arun Raj. E.P (1004603)',N'formation','2026-10-16 17:57:10',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=18)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (18,68,12,N'Rework',N'Charging Problem',11,N'Bhujendra. T (1002099)',N'formation','2026-10-20 16:17:47',N'A',NULL,NULL,NULL);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=22)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (22,68,12,N'Rework',N'Charging Problem',9,N'Aji Basha. S.K (1001816)',N'formation','2026-10-21 13:17:10',N'A',N'636A339221',N'26AH(EXP)',N'15A-51A');

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=23)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (23,68,13,N'Rejection',N'Physical Damage',12,N'Betappa Naidu. N (1002889)',N'formation','2026-10-21 13:17:37',N'A',N'636A339221',N'26AH(EXP)',N'15A-51A');

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=24)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (24,68,12,N'Rework',N'Charging Problem',13,N'Uday Kumar. E (1003291)',N'formation','2026-10-21 16:56:31',N'A',N'636A339221',N'26AH(EXP)',N'15A-51A');

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=25)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (25,68,12,N'Rework',N'Charging Problem',6,N'Munikrishnama Naidu. M (1000720)',N'formation','2026-10-21 16:58:08',N'A',N'636A339221',N'26AH(EXP)',N'15A-51A');

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=26)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (26,68,12,N'Rework',N'human problem',14,N'Ashoka Chakravarthi. V (1005637)',N'formation','2026-10-21 19:53:27',N'A',N'636A339221',N'26AH(EXP)',N'15A-51A');

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=27)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (27,68,13,N'Rejection',N'product damage',19,N'Arun Raj. E.P (1004603)',N'formation','2026-10-21 19:53:55',N'A',N'636A339221',N'26AH(EXP)',N'15A-51A');

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=28)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (28,68,12,N'Rework',N'Charging Problem',11,N'Bhujendra. T (1002099)',N'formation','2026-10-22 12:33:21',N'A',N'730C214693',N'42AH(NEW)',N'15A-07H');

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Formation_Rework_Rejection] WHERE [FRR_ID]=29)
        INSERT INTO [dbo].[Formation_Rework_Rejection] ([FRR_ID],[FRR_Formation_ID],[FRR_Type_ID],[FRR_Type_Name],[FRR_Reason],[FRR_Employee_ID],[FRR_Employee_Name],[FRR_Created_By],[FRR_Created_Date],[FRR_Status],[FRR_QRCode],[FRR_BatteryType],[FRR_CKTNo])
        VALUES (29,68,12,N'Rework',N'SEVER PROBLEM',14,N'Ashoka Chakravarthi. V (1005637)',N'formation','2026-10-22 15:44:20',N'A',N'636A339225',N'26AH(EXP)',N'15A-51A');

    SET IDENTITY_INSERT [dbo].[Formation_Rework_Rejection] OFF;
    PRINT 'Formation_Rework_Rejection: 23 rows synced.';

    -- ================================================================
    -- STEP 4: Reference_Group (2 new rows: 12=Rework, 13=Rejection)
    -- ================================================================
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Reference_Group] WHERE [RG_iID] = 12)
    BEGIN
        INSERT INTO [dbo].[Reference_Group] ([RG_iID], [RG_vCode], [RG_vDescription], [RG_vUpdatedBy], [RG_dUpdateDate], [RG_vStatus])
        VALUES (12, 'Rework', 'Rework', 'Admin', '2026-10-13 16:03:45', 'A');
        PRINT 'Inserted Reference_Group RG_iID=12 (Rework)';
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Reference_Group] WHERE [RG_iID] = 13)
    BEGIN
        INSERT INTO [dbo].[Reference_Group] ([RG_iID], [RG_vCode], [RG_vDescription], [RG_vUpdatedBy], [RG_dUpdateDate], [RG_vStatus])
        VALUES (13, 'Rejection', 'Rejection', 'Admin', '2026-10-13 16:03:45', 'A');
        PRINT 'Inserted Reference_Group RG_iID=13 (Rejection)';
    END

    -- ================================================================
    -- STEP 5: ReferenceGroup_Value (Rework & Rejection defect reasons)
    -- ================================================================
    -- Dynamic PK generation (RGV_iID) to avoid primary key collisions
    -- with existing production master records.
    DECLARE @rgv_id INT;

    DECLARE @NewReasons TABLE (
        RG_ID INT,
        Code VARCHAR(50),
        [Description] VARCHAR(250),
        UpdatedBy VARCHAR(50),
        UpdateDate DATETIME,
        [Status] VARCHAR(50)
    );

    INSERT INTO @NewReasons (RG_ID, Code, [Description], UpdatedBy, UpdateDate, [Status]) VALUES
        (12, '01',    'test',             'Oct 13 2026  5:39PM', '2026-10-13 17:39:44', 'D'),
        (12, '',      'text 1',           'Oct 14 2026 10:05AM', '2026-10-14 10:05:50', 'D'),
        (12, 'RW01',  'Terminal Problem', 'Oct 14 2026 10:50AM', '2026-10-14 10:50:20', 'A'),
        (12, 'RW02',  'Charging Problem', 'Oct 14 2026 10:50AM', '2026-10-14 10:50:20', 'A'),
        (12, 'RW03',  'Label Problem',    'Oct 14 2026 11:13AM', '2026-10-14 11:13:10', 'A'),
        (12, 'RW04',  'human problem',    'Oct 14 2026 12:31PM', '2026-10-14 12:31:03', 'A'),
        (12, 'RW005', 'SEVER PROBLEM',    'Oct 19 2026 11:47PM', '2026-10-19 23:47:31', 'A'),
        (13, 'RJ01',  'Physical Damage',  'Oct 14 2026 10:50AM', '2026-10-14 10:50:20', 'A'),
        (13, 'RJ02',  'Wrong Product',    'Oct 14 2026 10:50AM', '2026-10-14 10:50:20', 'A'),
        (13, 'RJ03',  'Quality Failure',  'Oct 14 2026 11:13AM', '2026-10-14 11:13:10', 'A'),
        (13, 'RJ04',  'product failure',  'Oct 14 2026  5:09PM', '2026-10-14 17:09:45', 'A'),
        (13, 'RJ05',  'product damage',   'Oct 14 2026  5:10PM', '2026-10-14 17:10:41', 'A');

    DECLARE @cur_rgid INT, @cur_code VARCHAR(50), @cur_desc VARCHAR(250), @cur_upby VARCHAR(50), @cur_dt DATETIME, @cur_st VARCHAR(50);
    DECLARE cur CURSOR LOCAL FAST_FORWARD FOR
        SELECT RG_ID, Code, [Description], UpdatedBy, UpdateDate, [Status] FROM @NewReasons;

    OPEN cur;
    FETCH NEXT FROM cur INTO @cur_rgid, @cur_code, @cur_desc, @cur_upby, @cur_dt, @cur_st;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF NOT EXISTS (
            SELECT 1 FROM [dbo].[ReferenceGroup_Value] 
            WHERE [RGV_iRG_ID] = @cur_rgid AND [RGV_vDesciption] = @cur_desc
        )
        BEGIN
            SELECT @rgv_id = ISNULL(MAX(RGV_iID), 0) + 1 FROM [dbo].[ReferenceGroup_Value];
            INSERT INTO [dbo].[ReferenceGroup_Value] 
                ([RGV_iID], [RGV_iRG_ID], [RGV_vCode], [RGV_vDesciption], [RGV_vUpdatedBy], [RGV_dUpdateDate], [RGV_vStatus])
            VALUES 
                (@rgv_id, @cur_rgid, @cur_code, @cur_desc, @cur_upby, @cur_dt, @cur_st);
            PRINT 'Inserted ReferenceGroup_Value RGV_iID=' + CAST(@rgv_id AS VARCHAR) + ' (' + @cur_desc + ')';
        END
        FETCH NEXT FROM cur INTO @cur_rgid, @cur_code, @cur_desc, @cur_upby, @cur_dt, @cur_st;
    END
    CLOSE cur;
    DEALLOCATE cur;

    -- ================================================================
    -- SUMMARY
    -- ================================================================
    SELECT 
        'T_Setting_Menu_Master'      AS [Table], COUNT(*) AS [Total_Rows] FROM [dbo].[T_Setting_Menu_Master]
    UNION ALL
    SELECT 'T_Setting_Role_Assignment', COUNT(*) FROM [dbo].[T_Setting_Role_Assignment]
    UNION ALL
    SELECT 'Formation_Rework_Rejection', COUNT(*) FROM [dbo].[Formation_Rework_Rejection]
    UNION ALL
    SELECT 'Reference_Group', COUNT(*) FROM [dbo].[Reference_Group]
    UNION ALL
    SELECT 'ReferenceGroup_Value', COUNT(*) FROM [dbo].[ReferenceGroup_Value]
    UNION ALL
    SELECT 'Refernce_Group_Value', COUNT(*) FROM [dbo].[Refernce_Group_Value];

    COMMIT TRANSACTION;
    PRINT '============================================';
    PRINT 'DATA SYNC completed successfully.';
    PRINT '============================================';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    SET IDENTITY_INSERT [dbo].[T_Setting_Menu_Master] OFF;
    SET IDENTITY_INSERT [dbo].[T_Setting_Role_Assignment] OFF;
    SET IDENTITY_INSERT [dbo].[Formation_Rework_Rejection] OFF;
    DECLARE @ErrMsg   NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrSev   INT            = ERROR_SEVERITY();
    DECLARE @ErrState INT            = ERROR_STATE();
    PRINT 'FAILED: ' + @ErrMsg;
    RAISERROR(@ErrMsg, @ErrSev, @ErrState);
END CATCH
GO
