# Schema Diff Report
**OLD**: MVRLA_Formation (localhost)  
**NEW**: Amaron2_Web_Assembly_v2 (LAPTOP-7KC3K6QQ\SA)  
**Generated**: 2026-10-23 17:52:18

## Summary

- **ADDED**: 26
- **MODIFIED**: 14

### By Object Type
- **SQL_STORED_PROCEDURE**: 25 change(s)
- **COLUMN**: 13 change(s)
- **TABLE**: 1 change(s)
- **PRIMARY KEY**: 1 change(s)

## COLUMN Changes (13)

| Object Name | Change | Old Definition | New Definition |
|-------------|--------|----------------|----------------|
| dbo.Formation_Rework_Rejection.FRR_QRCode | ADDED |  | varchar(50) NULL |
| dbo.Formation_Rework_Rejection.FRR_Status | ADDED |  | varchar(10) NULL DEFAULT ('A') |
| dbo.Formation_Rework_Rejection.FRR_Employee_ID | ADDED |  | int NOT NULL |
| dbo.Formation_Rework_Rejection.FRR_ID | ADDED |  | int NOT NULL IDENTITY |
| dbo.Formation_Rework_Rejection.FRR_BatteryType | ADDED |  | varchar(100) NULL |
| dbo.Formation_Rework_Rejection.FRR_CKTNo | ADDED |  | varchar(50) NULL |
| dbo.Formation_Rework_Rejection.FRR_Type_Name | ADDED |  | varchar(50) NOT NULL |
| dbo.Formation_Rework_Rejection.FRR_Employee_Name | ADDED |  | varchar(100) NULL |
| dbo.Formation_Rework_Rejection.FRR_Reason | ADDED |  | varchar(500) NOT NULL |
| dbo.Formation_Rework_Rejection.FRR_Formation_ID | ADDED |  | int NULL DEFAULT ((68)) |
| dbo.Formation_Rework_Rejection.FRR_Created_By | ADDED |  | varchar(50) NULL |
| dbo.Formation_Rework_Rejection.FRR_Type_ID | ADDED |  | int NOT NULL |
| dbo.Formation_Rework_Rejection.FRR_Created_Date | ADDED |  | datetime(3) NULL DEFAULT (getdate()) |

## PRIMARY KEY Changes ()

| Object Name | Change | Old Definition | New Definition |
|-------------|--------|----------------|----------------|
| dbo.Formation_Rework_Rejection | ADDED |  | FRR_ID |

## SQL_STORED_PROCEDURE Changes (25)

| Object Name | Change | Old Definition | New Definition |
|-------------|--------|----------------|----------------|
| dbo.Pr_Delete_MVRLA | MODIFIED | <old body> | <new body> |
| dbo.Pr_Delete_Reference_Master | MODIFIED | <old body> | <new body> |
| dbo.pr_fetch_Active_Employees | ADDED |  | <exists in NEW> |
| dbo.Pr_Fetch_FillingReport | MODIFIED | <old body> | <new body> |
| dbo.Pr_Fetch_FillingReport2 | MODIFIED | <old body> | <new body> |
| dbo.pr_fetch_Formation_Rework_Rejection_List | ADDED |  | <exists in NEW> |
| dbo.Pr_Fetch_MVRLA | MODIFIED | <old body> | <new body> |
| dbo.Pr_Fetch_MVRLA_PDF | MODIFIED | <old body> | <new body> |
| dbo.pr_Fetch_QRCode_Available | MODIFIED | <old body> | <new body> |
| dbo.pr_fetch_Rework_Rejection_Reasons | ADDED |  | <exists in NEW> |
| dbo.pr_fetch_Rework_Rejection_Report | ADDED |  | <exists in NEW> |
| dbo.pr_fetch_Rework_Rejection_Types | ADDED |  | <exists in NEW> |
| dbo.Pr_Fetch_SummaryReport | MODIFIED | <old body> | <new body> |
| dbo.Pr_Fetch_T_Setting_UserMaster_Details | MODIFIED | <old body> | <new body> |
| dbo.pr_fetch_Today_Rejection_Chart | ADDED |  | <exists in NEW> |
| dbo.pr_fetch_Today_Rework_Chart | ADDED |  | <exists in NEW> |
| dbo.pr_fetch_Today_Rework_Rejection_Counts | ADDED |  | <exists in NEW> |
| dbo.PR_GET_QR_DETAILS | ADDED |  | <exists in NEW> |
| dbo.pr_getLogin | MODIFIED | <old body> | <new body> |
| dbo.pr_getlogin1 | MODIFIED | <old body> | <new body> |
| dbo.pr_getMenu_listNew | MODIFIED | <old body> | <new body> |
| dbo.pr_insert_Formation_Rework_Rejection | ADDED |  | <exists in NEW> |
| dbo.pr_insert_MVRLA | MODIFIED | <old body> | <new body> |
| dbo.Pr_Insert_Reference_Group | ADDED |  | <exists in NEW> |
| dbo.Pr_Update_ReferenceGroup_Value | MODIFIED | <old body> | <new body> |

## TABLE Changes ()

| Object Name | Change | Old Definition | New Definition |
|-------------|--------|----------------|----------------|
| dbo.Formation_Rework_Rejection | ADDED |  | dbo.Formation_Rework_Rejection |

