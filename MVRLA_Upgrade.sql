/*
  ====================================================================
  Upgrade Script: MVRLA_Formation  -->  Amaron2_Web_Assembly_v2
  Generated  : 2026-10-23 17:52:18
  Target DB  : [MVRLA_Formation] on localhost
  Reference  : [Amaron2_Web_Assembly_v2] on LAPTOP-7KC3K6QQ\SA
  SQL Server : 2017+ recommended (uses CREATE OR ALTER)
  ====================================================================
  HOW TO RUN:
    1. Back up [MVRLA_Formation] first.
    2. Open in SSMS connected to localhost.
    3. Execute. The script is wrapped in TRY/CATCH with ROLLBACK.
  ====================================================================
*/

USE [MVRLA_Formation];
GO
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;


    -- ================================================================
    -- STEP 1: Create new table: dbo.Formation_Rework_Rejection
    --         (Includes all 13 new columns detected in NEW schema)
    -- ================================================================
    IF NOT EXISTS (SELECT 1 FROM sys.tables t JOIN sys.schemas s ON t.schema_id=s.schema_id
                   WHERE s.name=N'dbo' AND t.name=N'Formation_Rework_Rejection')
    BEGIN
        CREATE TABLE [dbo].[Formation_Rework_Rejection] (
            [FRR_ID]            INT           NOT NULL IDENTITY(1,1),
            [FRR_QRCode]        VARCHAR(50)   NULL,
            [FRR_BatteryType]   VARCHAR(100)  NULL,
            [FRR_CKTNo]         VARCHAR(50)   NULL,
            [FRR_Type_ID]       INT           NOT NULL,
            [FRR_Type_Name]     VARCHAR(50)   NOT NULL,
            [FRR_Reason]        VARCHAR(500)  NOT NULL,
            [FRR_Employee_ID]   INT           NOT NULL,
            [FRR_Employee_Name] VARCHAR(100)  NULL,
            [FRR_Created_By]    VARCHAR(50)   NULL,
            [FRR_Created_Date]  DATETIME      NULL CONSTRAINT [DF_FRR_Created_Date]  DEFAULT (GETDATE()),
            [FRR_Status]        VARCHAR(10)   NULL CONSTRAINT [DF_FRR_Status]        DEFAULT ('A'),
            [FRR_Formation_ID]  INT           NULL CONSTRAINT [DF_FRR_Formation_ID]  DEFAULT ((68)),
            CONSTRAINT [PK_Formation_Rework_Rejection] PRIMARY KEY CLUSTERED ([FRR_ID])
        );
        PRINT 'Created table [dbo].[Formation_Rework_Rejection]';
    END
    ELSE
    BEGIN
        -- Table already exists; safely add any missing columns:
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_QRCode')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_QRCode] VARCHAR(50) NULL;
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_BatteryType')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_BatteryType] VARCHAR(100) NULL;
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_CKTNo')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_CKTNo] VARCHAR(50) NULL;
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_Type_ID')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_Type_ID] INT NOT NULL CONSTRAINT [DF_FRR_Type_ID_tmp] DEFAULT (0);
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_Type_Name')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_Type_Name] VARCHAR(50) NOT NULL CONSTRAINT [DF_FRR_Type_Name_tmp] DEFAULT ('');
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_Reason')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_Reason] VARCHAR(500) NOT NULL CONSTRAINT [DF_FRR_Reason_tmp] DEFAULT ('');
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_Employee_ID')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_Employee_ID] INT NOT NULL CONSTRAINT [DF_FRR_Employee_ID_tmp] DEFAULT (0);
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_Employee_Name')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_Employee_Name] VARCHAR(100) NULL;
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_Created_By')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_Created_By] VARCHAR(50) NULL;
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_Created_Date')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_Created_Date] DATETIME NULL CONSTRAINT [DF_FRR_Created_Date2] DEFAULT (GETDATE());
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_Status')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_Status] VARCHAR(10) NULL CONSTRAINT [DF_FRR_Status2] DEFAULT ('A');
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'dbo' AND TABLE_NAME=N'Formation_Rework_Rejection' AND COLUMN_NAME=N'FRR_Formation_ID')
            ALTER TABLE [dbo].[Formation_Rework_Rejection] ADD [FRR_Formation_ID] INT NULL CONSTRAINT [DF_FRR_Formation_ID2] DEFAULT ((68));
        -- Add PK if missing (requires FRR_ID to be IDENTITY -- verify first)
        IF NOT EXISTS (SELECT 1 FROM sys.key_constraints kc JOIN sys.tables t ON kc.parent_object_id=t.object_id WHERE kc.name=N'PK_Formation_Rework_Rejection')
            ALTER TABLE [dbo].[Formation_Rework_Rejection]
                ADD CONSTRAINT [PK_Formation_Rework_Rejection] PRIMARY KEY CLUSTERED ([FRR_ID]);
    END
    -- ================================================================
    -- STEP 2: Stored Procedures
    -- ================================================================
    -- MODIFIED: [dbo].[Pr_Delete_MVRLA]
    EXEC sp_executesql N'CREATE OR ALTER proc Pr_Delete_MVRLA
@ID varchar(50)
as
begin
Update [dbo].[MVRLA] set [mv_Status]=''D''
where [mv_ID]=@ID
end

';

    -- MODIFIED: [dbo].[Pr_Delete_Reference_Master]
    EXEC sp_executesql N'CREATE OR ALTER proc Pr_Delete_Reference_Master
@ID varchar(50)
as
begin
Update [dbo].[ReferenceGroup_Value] set RGV_vStatus=''D''
where RGV_iID=@ID
end';

    -- ADDED: [dbo].[pr_fetch_Active_Employees]
    EXEC sp_executesql N'CREATE OR ALTER PROC pr_fetch_Active_Employees
AS
BEGIN
    SELECT 
        EM_ID, 
        EM_Code, 
        EM_Employee_Name,
        LTRIM(RTRIM(EM_Employee_Name)) + '' ('' + ISNULL(LTRIM(RTRIM(EM_Code)), '''') + '')'' AS EM_Display_Name
    FROM Employee_Master
    WHERE EM_Status = ''A''
    ORDER BY EM_Employee_Name ASC
END';

    -- MODIFIED: [dbo].[Pr_Fetch_FillingReport]
    EXEC sp_executesql N'CREATE OR ALTER proc dbo.Pr_Fetch_FillingReport --  Pr_Fetch_FillingReport''20240701'',''20240731'',''26ah''      
@from varchar(50), @to varchar(50), @shift varchar(50)      
as            
begin            
select [mv_BatteryType],[mv_FillingLine],mv_Shift,mv_Remarks,count([mv_BatteryType]) as Qty ,[mv_BankNo],[mv_TubNo]        
from MVRLA         
where mv_Status=''A'' and          
convert(varchar,[mv_CreatedDate],112)>=@from and         
convert(varchar,[mv_CreatedDate],112)<=@to and       
((mv_Shift=@shift or isnull(@shift,'''')='''') or ([mv_BatteryType]=@shift or isnull(@shift,'''')=''''))   
group by [mv_BatteryType],[mv_FillingLine],mv_Shift,mv_Remarks,[mv_BankNo],[mv_TubNo]       
end';

    -- MODIFIED: [dbo].[Pr_Fetch_FillingReport2]
    EXEC sp_executesql N'CREATE OR ALTER proc dbo.Pr_Fetch_FillingReport2 --  Pr_Fetch_FillingReport2 ''20240701'',''20240731'',''26ah''      
@from varchar(50), @to varchar(50), @shift varchar(50), @bank varchar(50)          
as            
begin            
select [mv_BatteryType],[mv_FillingLine],mv_Shift,mv_Remarks,count([mv_BatteryType]) as Qty ,[mv_BankNo],[mv_TubNo]        
from MVRLA         
where mv_Status=''A'' and          
convert(varchar,[mv_CreatedDate],112)>=@from and         
convert(varchar,[mv_CreatedDate],112)<=@to and        
[mv_TubNo]= @shift and ([mv_BankNo]=@bank or isnull(@bank,'''')='''')
group by [mv_BatteryType],[mv_FillingLine],mv_Shift,mv_Remarks,[mv_BankNo],[mv_TubNo]       
end';

    -- ADDED: [dbo].[pr_fetch_Formation_Rework_Rejection_List]
    EXEC sp_executesql N'CREATE OR ALTER PROC pr_fetch_Formation_Rework_Rejection_List
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ROW_NUMBER() OVER(ORDER BY FRR_ID DESC) AS [SNo],
        FRR_ID,
        ISNULL(FRR_QRCode, '''') AS FRR_QRCode,
        ISNULL(FRR_BatteryType, '''') AS FRR_BatteryType,
        ISNULL(FRR_CKTNo, '''') AS FRR_CKTNo,
        FRR_Type_Name,
        FRR_Reason,
        FRR_Employee_Name,
        FRR_Created_By,
        CONVERT(VARCHAR, FRR_Created_Date, 103) + '' '' + CONVERT(VARCHAR(5), FRR_Created_Date, 108) AS FRR_Created_Date,
        FRR_Status
    FROM Formation_Rework_Rejection
    WHERE FRR_Status = ''A''
    ORDER BY FRR_ID DESC;
END';

    -- MODIFIED: [dbo].[Pr_Fetch_MVRLA]
    EXEC sp_executesql N'CREATE OR ALTER proc Pr_Fetch_MVRLA  
as  
begin  
select * from MVRLA where mv_Status=''A'' and
convert(varchar,[mv_CreatedDate],112)=convert(varchar,getdate(),112)
end';

    -- MODIFIED: [dbo].[Pr_Fetch_MVRLA_PDF]
    EXEC sp_executesql N'CREATE OR ALTER proc Pr_Fetch_MVRLA_PDF --  Pr_Fetch_MVRLA_PDF     
@mv_QRCode varchar(50), @from varchar(50), @to varchar(50), @tub varchar(50)      
as      
begin      
select [mv_BankNo] as [mv_ID],[mv_BatteryType],[mv_FillingLine],[mv_BankNo],convert(varchar,[mv_Date],105) as [mv_Date],[mv_NoOfCells],    
convert(varchar,[mv_FillingStartTime],8) as [mv_FillingStartTime],convert(varchar,[mv_FillingEndTime],8) as [mv_FillingEndTime],      
[mv_CDPassedQty],[mv_CDFailQty],[mv_AcidSPG],[mv_AcidVolume],[mv_TubNo],[mv_CKTNo],[mv_CellShortQty],[mv_Temperature],      
[mv_PRDSupervisior],[mv_FillingOperator],[mv_QAInspector],      
[mv_ShiftEngineer],[mv_Status],[mv_CreatedDate],[mv_User],[mv_QRCode]      
from MVRLA       
where ([mv_BankNo]=@mv_QRCode or isnull(@mv_QRCode,'''')='''') and convert(varchar,[mv_CreatedDate],112)>=@from and convert(varchar,[mv_CreatedDate],112)<=@to and  
[mv_TubNo]=@tub  
end ';

    -- MODIFIED: [dbo].[pr_Fetch_QRCode_Available]
    EXEC sp_executesql N'CREATE OR ALTER proc pr_Fetch_QRCode_Available
@ID varchar(50)
as  
begin  
select mv_QRCode from MVRLA 
where mv_QRCode=@ID
end';

    -- ADDED: [dbo].[pr_fetch_Rework_Rejection_Reasons]
    EXEC sp_executesql N'CREATE OR ALTER PROCEDURE pr_fetch_Rework_Rejection_Reasons
    @Type_ID VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF (@Type_ID IS NULL OR LTRIM(RTRIM(@Type_ID)) = '''' OR @Type_ID = ''0'')
    BEGIN
        SELECT 
            v.RGV_iID,
            v.RGV_vDesciption,
            v.RGV_vCode,
            v.RGV_iRG_ID,
            g.RG_vDescription AS Reference_Type
        FROM ReferenceGroup_Value v
        INNER JOIN Reference_Group g ON v.RGV_iRG_ID = g.RG_iID
        WHERE 1 = 0;
        RETURN;
    END

    DECLARE @ParsedID INT = TRY_CAST(@Type_ID AS INT);

    SELECT 
        v.RGV_iID,
        v.RGV_vDesciption,
        v.RGV_vCode,
        v.RGV_iRG_ID,
        g.RG_vDescription AS Reference_Type
    FROM ReferenceGroup_Value v
    INNER JOIN Reference_Group g ON v.RGV_iRG_ID = g.RG_iID
    WHERE v.RGV_vStatus = ''A''
      AND g.RG_vStatus = ''A''
      AND (
          (@ParsedID IS NOT NULL AND v.RGV_iRG_ID = @ParsedID)
          OR g.RG_vDescription = @Type_ID
          OR g.RG_vCode = @Type_ID
      )
    ORDER BY v.RGV_vDesciption ASC;
END';

    -- ADDED: [dbo].[pr_fetch_Rework_Rejection_Report]
    EXEC sp_executesql N'CREATE OR ALTER PROCEDURE pr_fetch_Rework_Rejection_Report
    @Type VARCHAR(50) = ''All'',
    @FromDate VARCHAR(20) = NULL,
    @ToDate VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ROW_NUMBER() OVER(ORDER BY R.FRR_ID DESC) AS [SNo],
        R.FRR_ID,
        ''F'' + RIGHT(''000'' + CAST(R.FRR_ID AS VARCHAR(10)), 3) AS [Formation_ID],
        ISNULL(R.FRR_QRCode, '''') AS [FRR_QRCode],
        ISNULL(NULLIF(R.FRR_BatteryType, ''''), ISNULL(M.mv_BatteryType, '''')) AS [FRR_BatteryType],
        ISNULL(NULLIF(R.FRR_CKTNo, ''''), ISNULL(M.mv_CKTNo, '''')) AS [FRR_CKTNo],
        R.FRR_Formation_ID,
        R.FRR_Type_ID,
        R.FRR_Type_Name,
        R.FRR_Reason,
        R.FRR_Employee_ID,
        R.FRR_Employee_Name,
        R.FRR_Created_By,
        CONVERT(VARCHAR, R.FRR_Created_Date, 103) + '' '' + CONVERT(VARCHAR(5), R.FRR_Created_Date, 108) AS FRR_Created_Date,
        R.FRR_Status
    FROM Formation_Rework_Rejection R
    OUTER APPLY (
        SELECT TOP 1 mv_BatteryType, mv_CKTNo
        FROM MVRLA
        WHERE mv_QRCode = R.FRR_QRCode
        ORDER BY mv_ID DESC
    ) M
    WHERE R.FRR_Status = ''A''
      AND (
          @Type IS NULL 
          OR @Type = '''' 
          OR @Type = ''All'' 
          OR R.FRR_Type_Name = @Type 
          OR CAST(R.FRR_Type_ID AS VARCHAR(50)) = @Type
      )
      AND (
          @FromDate IS NULL 
          OR LTRIM(RTRIM(@FromDate)) = '''' 
          OR CAST(R.FRR_Created_Date AS DATE) >= TRY_CAST(@FromDate AS DATE)
      )
      AND (
          @ToDate IS NULL 
          OR LTRIM(RTRIM(@ToDate)) = '''' 
          OR CAST(R.FRR_Created_Date AS DATE) <= TRY_CAST(@ToDate AS DATE)
      )
    ORDER BY R.FRR_ID DESC;
END';

    -- ADDED: [dbo].[pr_fetch_Rework_Rejection_Types]
    EXEC sp_executesql N'CREATE OR ALTER PROC pr_fetch_Rework_Rejection_Types
AS
BEGIN
    SELECT 
        RG_iID, 
        RG_vCode, 
        RG_vDescription 
    FROM Reference_Group 
    WHERE RG_vStatus = ''A'' 
      AND (RG_vCode IN (''Rework'', ''Rejection'') OR RG_vDescription IN (''Rework'', ''Rejection''))
    ORDER BY RG_vDescription ASC
END';

    -- MODIFIED: [dbo].[Pr_Fetch_SummaryReport]
    EXEC sp_executesql N'CREATE OR ALTER proc Pr_Fetch_SummaryReport  --  Pr_Fetch_SummaryReport ''20240501'',''20240731'' 
@from varchar(50), @to varchar(50)  
as    
begin    
select * from MVRLA where mv_Status=''A'' and  
convert(varchar,[mv_CreatedDate],112)>=@from and convert(varchar,[mv_CreatedDate],112)<=@to   
end';

    -- MODIFIED: [dbo].[Pr_Fetch_T_Setting_UserMaster_Details]
    EXEC sp_executesql N'CREATE OR ALTER Proc Pr_Fetch_T_Setting_UserMaster_Details      
as      
Begin      
select UM_iid as U_ID,      
UM_vUser_Name as U_UserName,      
UM_vPassword as U_Password,      
dbo.fget_reference_value(UM_iRightst) as UM_iRightst,      
UM_vCreatedBy, UM_vUserCode as U_Rights,     
convert(varchar,UM_dCreatedDate,103) as UM_dCreatedDate,      
UM_vStatus      
from  T_Setting_UserMaster      
where  UM_vStatus=''A''      
--Generated by vino 11-11-2017 12:51:37 PM      
End 
';

    -- ADDED: [dbo].[pr_fetch_Today_Rejection_Chart]
    EXEC sp_executesql N'CREATE OR ALTER PROCEDURE pr_fetch_Today_Rejection_Chart AS BEGIN SET NOCOUNT ON; DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME); DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today); SELECT ISNULL(FRR_Reason, ''Unknown'') AS Reason, COUNT(1) AS Qty FROM Formation_Rework_Rejection WHERE FRR_Status = ''A'' AND (FRR_Type_Name = ''Rejection'' OR FRR_Type_ID = 13) AND FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow GROUP BY FRR_Reason ORDER BY Qty DESC; END';

    -- ADDED: [dbo].[pr_fetch_Today_Rework_Chart]
    EXEC sp_executesql N'CREATE OR ALTER PROCEDURE pr_fetch_Today_Rework_Chart AS BEGIN SET NOCOUNT ON; DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME); DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today); SELECT ISNULL(FRR_Reason, ''Unknown'') AS Reason, COUNT(1) AS Qty FROM Formation_Rework_Rejection WHERE FRR_Status = ''A'' AND (FRR_Type_Name = ''Rework'' OR FRR_Type_ID = 12) AND FRR_Created_Date >= @Today AND FRR_Created_Date < @Tomorrow GROUP BY FRR_Reason ORDER BY Qty DESC; END';

    -- ADDED: [dbo].[pr_fetch_Today_Rework_Rejection_Counts]
    EXEC sp_executesql N'CREATE OR ALTER PROCEDURE pr_fetch_Today_Rework_Rejection_Counts
    @Type VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
    DECLARE @Tomorrow DATETIME = DATEADD(DAY, 1, @Today);

    IF (@Type = ''Rework'')
    BEGIN
        SELECT ISNULL(COUNT(*), 0) AS Rework_Today
        FROM Formation_Rework_Rejection
        WHERE FRR_Status = ''A''
          AND (FRR_Type_Name = ''Rework'' OR FRR_Type_ID = 12)
          AND FRR_Created_Date >= @Today 
          AND FRR_Created_Date < @Tomorrow;
        RETURN;
    END

    IF (@Type = ''Rejection'')
    BEGIN
        SELECT ISNULL(COUNT(*), 0) AS Rejection_Today
        FROM Formation_Rework_Rejection
        WHERE FRR_Status = ''A''
          AND (FRR_Type_Name = ''Rejection'' OR FRR_Type_ID = 13)
          AND FRR_Created_Date >= @Today 
          AND FRR_Created_Date < @Tomorrow;
        RETURN;
    END

    SELECT 
        ISNULL(COUNT(CASE WHEN (FRR_Type_Name = ''Rework'' OR FRR_Type_ID = 12) THEN 1 END), 0) AS Rework_Today,
        ISNULL(COUNT(CASE WHEN (FRR_Type_Name = ''Rejection'' OR FRR_Type_ID = 13) THEN 1 END), 0) AS Rejection_Today
    FROM Formation_Rework_Rejection
    WHERE FRR_Status = ''A''
      AND FRR_Created_Date >= @Today 
      AND FRR_Created_Date < @Tomorrow;
END';

    -- ADDED: [dbo].[PR_GET_QR_DETAILS]
    EXEC sp_executesql N'CREATE OR ALTER PROC PR_GET_QR_DETAILS  --PR_GET_QR_DETAILS ''636A339222''
@QR VARCHAR(55)
AS
BEGIN
SELECT
mv_QRCode,
mv_BatteryType,
convert(varchar,mv_CreatedDate,105)
FROM MVRLA
WHERE mv_Status=''A'' AND mv_QRCode=@QR
END';

    -- MODIFIED: [dbo].[pr_getLogin]
    EXEC sp_executesql N'CREATE OR ALTER proc pr_getLogin
@user_Name varchar(33),                    
@Password varchar(33)                    
as                    
begin                    
    select *,      
    isnull((select sum(UP_iPoints_Cr) from User_Points where UP_iUser_ID=UM_iID),0)      
    -      
    isnull((select sum(UP_iPoints_Cr) from User_Points where UP_vCredit_by=UM_vUser_ID),0) as Points      
    from User_Master1              
    left outer join Refernce_Group_Value on UM_iUser_Type=RGV_iID              
    where (UM_vUser_Name = @user_Name COLLATE SQL_Latin1_General_CP1_CI_AS or UM_vPhone_Number = @user_Name) 
      and UM_vPassword   = @Password  COLLATE SQL_Latin1_General_CP1_CI_AS
end';

    -- MODIFIED: [dbo].[pr_getlogin1]
    EXEC sp_executesql N'CREATE OR ALTER proc pr_getlogin1   --pr_getlogin ''9597436220'',''admin''                 
@user_Name varchar(33),                    
@Password varchar(33)                    
as                    
begin                    
select *,      
isnull((select sum(UP_iPoints_Cr) from User_Points where UP_iUser_ID=UM_iID),0)      
-      
isnull((select sum(UP_iPoints_Cr) from User_Points where UP_vCredit_by=UM_vUser_ID),0) as Points      
from User_Master1              
left outer join Refernce_Group_Value on UM_iUser_Type=RGV_iID              
where UM_vUser_Name = @user_Name COLLATE SQL_Latin1_General_CP1_CI_AS 
  and UM_vPassword  = @Password  COLLATE SQL_Latin1_General_CP1_CI_AS
end';

    -- MODIFIED: [dbo].[pr_getMenu_listNew]
    EXEC sp_executesql N'CREATE OR ALTER proc pr_getMenu_listNew
@RA_iRole int,            
@MM_vModule int                  
as                      
begin                      
select *,                  
case when (select count(*) from T_Setting_Menu_Master x where x.MM_iParentID=y.MM_iId)>0 then ''dropdown-toggle'' else'''' end as [Type]                  
from dbo.T_Setting_Menu_Master y                    
left outer join T_Setting_Role_Assignment on RA_iMenuID=MM_iId                
where MM_iParentID=0 and RA_iVisibility=1 and RA_iRole=@RA_iRole               
and MM_vModule = @MM_vModule and MM_vStatus=''A''        
order by MM_iMenu_Order                 
end
';

    -- ADDED: [dbo].[pr_insert_Formation_Rework_Rejection]
    EXEC sp_executesql N'CREATE OR ALTER PROC pr_insert_Formation_Rework_Rejection
    @FRR_Formation_ID  INT = 68,
    @FRR_Type_ID       INT,
    @FRR_Type_Name     VARCHAR(50),
    @FRR_Reason        VARCHAR(500),
    @FRR_Employee_ID   INT,
    @FRR_Employee_Name VARCHAR(100) = NULL,
    @FRR_Created_By    VARCHAR(50)  = ''System'',
    @FRR_QRCode        VARCHAR(50)  = NULL,
    @FRR_BatteryType   VARCHAR(100) = NULL,
    @FRR_CKTNo         VARCHAR(50)  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- If employee name was not supplied, lookup from Employee_Master
    IF (@FRR_Employee_Name IS NULL OR @FRR_Employee_Name = '''')
    BEGIN
        SELECT @FRR_Employee_Name = EM_Employee_Name 
        FROM Employee_Master 
        WHERE EM_ID = @FRR_Employee_ID;
    END

    -- If type name was not supplied, lookup from Reference_Group
    IF (@FRR_Type_Name IS NULL OR @FRR_Type_Name = '''')
    BEGIN
        SELECT @FRR_Type_Name = RG_vDescription 
        FROM Reference_Group 
        WHERE RG_iID = @FRR_Type_ID;
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
        FRR_CKTNo
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
        ''A'',
        @FRR_QRCode,
        @FRR_BatteryType,
        @FRR_CKTNo
    );
END';

    -- MODIFIED: [dbo].[pr_insert_MVRLA]
    EXEC sp_executesql N'CREATE OR ALTER proc pr_insert_MVRLA    
@mv_BatteryType varchar(50),@mv_FillingLine varchar(50),@mv_BankNo varchar(50),@mv_Date varchar(50),@mv_NoOfCells varchar(50),@mv_FillingStartTime varchar(50),    
@mv_FillingEndTime varchar(50),@mv_CDPassedQty varchar(50),@mv_Shift varchar(50),@mv_ReadingOperator varchar(50),    
@mv_CDFailQty varchar(50),@mv_AcidSPG varchar(50),@mv_AcidVolume varchar(50),@mv_TubNo varchar(50),@mv_CKTNo varchar(50),    
@mv_CellShortQty varchar(50),@mv_Temperature varchar(50),@mv_PRDSupervisior varchar(50),@mv_FillingOperator varchar(50),    
@mv_QAInspector varchar(50),@mv_ShiftEngineer varchar(50),@mv_User varchar(50),@mv_QRCode varchar(50)    
as    
begin    
insert into MVRLA(mv_BatteryType,mv_FillingLine,mv_BankNo,mv_Date,mv_NoOfCells,mv_FillingStartTime,mv_FillingEndTime,mv_CDPassedQty,    
mv_CDFailQty,mv_AcidSPG,mv_AcidVolume,mv_TubNo,mv_CKTNo,mv_CellShortQty,mv_Temperature,mv_PRDSupervisior,mv_FillingOperator,mv_QAInspector,    
mv_ShiftEngineer,mv_Status,mv_CreatedDate,mv_User,mv_QRCode,mv_Shift,mv_ReadingOperator)     
Values(@mv_BatteryType,@mv_FillingLine,@mv_BankNo,replace(@mv_Date,''T'','' ''),@mv_NoOfCells,replace(@mv_FillingStartTime,''T'','' ''),  
replace(@mv_FillingEndTime,''T'','' ''),@mv_CDPassedQty,    
@mv_CDFailQty,@mv_AcidSPG,@mv_AcidVolume,@mv_TubNo,@mv_CKTNo,@mv_CellShortQty,@mv_Temperature,@mv_PRDSupervisior,@mv_FillingOperator,    
@mv_QAInspector,@mv_ShiftEngineer,''A'',getdate(),@mv_User,@mv_QRCode,@mv_Shift,@mv_ReadingOperator)    
end';

    -- ADDED: [dbo].[Pr_Insert_Reference_Group]
    EXEC sp_executesql N'CREATE OR ALTER PROCEDURE Pr_Insert_Reference_Group
    @RG_vDescription VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM Reference_Group WHERE RG_vDescription = @RG_vDescription)
    BEGIN
        SELECT ''Reference Type already exists.'' AS Result;
        RETURN;
    END

    DECLARE @NewID INT;
    SELECT @NewID = ISNULL(MAX(RG_iID), 0) + 1 FROM Reference_Group;

    INSERT INTO Reference_Group (RG_iID, RG_vCode, RG_vDescription, RG_vUpdatedBy, RG_dUpdateDate, RG_vStatus)
    VALUES (@NewID, @RG_vDescription, @RG_vDescription, ''Admin'', GETDATE(), ''A'');

    SELECT ''True'' AS Result;
END';

    -- MODIFIED: [dbo].[Pr_Update_ReferenceGroup_Value]
    EXEC sp_executesql N'CREATE OR ALTER PROC [dbo].[Pr_Update_ReferenceGroup_Value]
@RGV_iID int,
@RGV_vCode varchar(50),
@RGV_vDesciption varchar(250)
as
Begin
if exists (select 1 from ReferenceGroup_Value where RGV_vDesciption=@RGV_vDesciption and RGV_vStatus=''A'' and RGV_iID!=@RGV_iID)
begin
    raiserror (''Description ''''%s'''' Already Exists ,Try New'',16,-1,@RGV_vDesciption)
    return
end
update ReferenceGroup_Value
Set
RGV_vCode=@RGV_vCode,
RGV_vDesciption=@RGV_vDesciption
where RGV_iID=@RGV_iID
End';

    COMMIT TRANSACTION;
    PRINT 'SUCCESS: Upgrade completed.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    DECLARE @ErrMsg   NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrSev   INT            = ERROR_SEVERITY();
    DECLARE @ErrState INT            = ERROR_STATE();
    PRINT 'FAILED: ' + @ErrMsg;
    RAISERROR(@ErrMsg, @ErrSev, @ErrState);
END CATCH
GO

-- ====================================================================
-- APPENDIX: Data Migration Notes
-- ====================================================================
/*
1. CHANGES THAT MAY CAUSE DATA LOSS:
   None automatically detected.

2. CHANGES REQUIRING DATA MIGRATION:
   None automatically detected.

3. UNCERTAINTIES / MANUAL STEPS:
   - IDENTITY columns cannot be altered with ALTER COLUMN; rebuild table if structure changed.
   - Programmable objects may have cross-dependencies; deploy in topological order if errors occur.
   - Removed tables/columns are commented out; validate data before dropping.
   - If SQL Server < 2016 SP1, replace CREATE OR ALTER with DROP-then-CREATE.
*/
