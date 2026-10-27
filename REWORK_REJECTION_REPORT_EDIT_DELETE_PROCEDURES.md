# Formation Rework & Rejection — Edit & Delete Specification & Stored Procedures

## 1. Overview & Applied Changes

This document contains the complete specification, SQL stored procedures, and controller integration for the **Edit** and **Delete** functionality implemented on both Rework & Rejection pages, styled exactly after the **MVRLA Formation** screen:

1. **Transaction Page (`Transaction / Rework & Rejection`):**
   - File: [`SBLT2/Views/Transaction/Rework_Rejection.cshtml`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Transaction/Rework_Rejection.cshtml)
   - Added `Delete Selected` red button in the grid header (`#Delete`) matching the MVRLA sample image model.
   - Added `selectionmode: 'checkbox'` enabling multi-row selection checkboxes.
   - Removed in-grid `EDIT` and `DEL` columns; grid starts cleanly with `S.No`, `QR Code`, etc.
   - Rows can be double-clicked to load data into the entry form for editing.
   - Clicking `Delete Selected` prompts confirmation (`confirm("Are you sure want to delete press OK")`), executes soft-deletion, and refreshes the grid.

2. **Report Page (`Reports / Rework & Rejection`):**
   - File: [`SBLT2/Views/Reports/Rework_Rejection.cshtml`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Reports/Rework_Rejection.cshtml)
   - Removed in-grid Edit and Delete action columns. Grid starts cleanly with `S.No`, `QR CODE`, `BATTERY TYPE`, etc.
   - Modified `Type` column pill badges (`Rework` & `Rejection`) to have the exact same uniform size (`width: 72px; padding: 3px 0; border-radius: 12px;`).

3. **Controllers Updated:**
   - [`SBLT2/Controllers/TransactionController.cs`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs) — Added `Pr_Delete_Formation_Rework_Rejection` and `Pr_Update_Formation_Rework_Rejection`.

---

## 2. SQL Stored Procedure: Soft Delete (`pr_delete_Formation_Rework_Rejection`)

Execute this script in SQL Server Management Studio (SSMS) against your database. It sets `FRR_Status = 'D'`, matching the behavior of `Pr_Delete_MVRLA` (`mv_Status = 'D'`).

```sql
-- =========================================================================
-- STORED PROCEDURE: pr_delete_Formation_Rework_Rejection
-- Description: Performs soft deletion by marking FRR_Status = 'D'.
-- =========================================================================
CREATE OR ALTER PROCEDURE dbo.pr_delete_Formation_Rework_Rejection
    @ID VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF (@ID IS NULL OR LTRIM(RTRIM(@ID)) = '')
    BEGIN
        SELECT 'False' AS [Result], 'Invalid or empty ID parameter' AS [Message];
        RETURN;
    END

    UPDATE [dbo].[Formation_Rework_Rejection]
    SET [FRR_Status] = 'D'
    WHERE [FRR_ID] = CAST(@ID AS INT);

    IF @@ROWCOUNT > 0
    BEGIN
        SELECT 'True' AS [Result], 'Record Deleted Successfully' AS [Message];
    END
    ELSE
    BEGIN
        SELECT 'False' AS [Result], 'Record not found or already deleted' AS [Message];
    END
END
GO



```

---

## 3. SQL Stored Procedure: Update (`pr_update_Formation_Rework_Rejection`)

Execute this script in SSMS to update edited defect records while preserving the original QR code, battery type, circuit number, creation timestamps, and audit trail.

```sql
-- =========================================================================
-- STORED PROCEDURE: pr_update_Formation_Rework_Rejection
-- Description: Updates defect reasons, classifications, and measurements
--              for an active Formation_Rework_Rejection record.
-- =========================================================================
CREATE OR ALTER PROCEDURE dbo.pr_update_Formation_Rework_Rejection
    @FRR_ID INT,
    @FRR_Type_Name VARCHAR(50) = NULL,
    @FRR_Reason VARCHAR(500) = NULL,
    @FRR_DefectReasonType VARCHAR(100) = NULL,
    @FRR_DefectReason VARCHAR(500) = NULL,
    @FRR_Employee_Name VARCHAR(100) = NULL,
    @FRR_DefectCell VARCHAR(50) = NULL,
    @FRR_OCV VARCHAR(50) = NULL,
    @FRR_PlateNo VARCHAR(50) = NULL,
    @FRR_Location VARCHAR(50) = NULL,
    @FRR_PlateType VARCHAR(50) = NULL,
    @FRR_Cell1 VARCHAR(50) = NULL,
    @FRR_Cell2 VARCHAR(50) = NULL,
    @FRR_Cell3 VARCHAR(50) = NULL,
    @FRR_Cell4 VARCHAR(50) = NULL,
    @FRR_Cell5 VARCHAR(50) = NULL,
    @FRR_Cell6 VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF (@FRR_ID IS NULL OR @FRR_ID <= 0)
    BEGIN
        SELECT 'False' AS [Result], 'Invalid Record ID' AS [Message];
        RETURN;
    END

    -- Determine Type_ID corresponding to Type_Name (12 = Rework, 13 = Rejection)
    DECLARE @CalculatedTypeID INT = NULL;
    IF (@FRR_Type_Name = 'Rework')
        SET @CalculatedTypeID = 12;
    ELSE IF (@FRR_Type_Name = 'Rejection')
        SET @CalculatedTypeID = 13;

    UPDATE [dbo].[Formation_Rework_Rejection]
    SET
        [FRR_Type_ID]          = ISNULL(@CalculatedTypeID, [FRR_Type_ID]),
        [FRR_Type_Name]        = ISNULL(NULLIF(@FRR_Type_Name, ''), [FRR_Type_Name]),
        [FRR_Reason]           = ISNULL(NULLIF(@FRR_Reason, ''), [FRR_Reason]),
        [FRR_DefectReasonType] = ISNULL(@FRR_DefectReasonType, [FRR_DefectReasonType]),
        [FRR_DefectReason]     = ISNULL(NULLIF(@FRR_DefectReason, ''), [FRR_DefectReason]),
        [FRR_Employee_Name]    = ISNULL(NULLIF(@FRR_Employee_Name, ''), [FRR_Employee_Name]),
        [FRR_DefectCell]       = ISNULL(@FRR_DefectCell, [FRR_DefectCell]),
        [FRR_OCV]              = ISNULL(@FRR_OCV, [FRR_OCV]),
        [FRR_PlateNo]          = ISNULL(@FRR_PlateNo, [FRR_PlateNo]),
        [FRR_Location]         = ISNULL(@FRR_Location, [FRR_Location]),
        [FRR_PlateType]        = ISNULL(@FRR_PlateType, [FRR_PlateType]),
        [FRR_Cell1]            = ISNULL(@FRR_Cell1, [FRR_Cell1]),
        [FRR_Cell2]            = ISNULL(@FRR_Cell2, [FRR_Cell2]),
        [FRR_Cell3]            = ISNULL(@FRR_Cell3, [FRR_Cell3]),
        [FRR_Cell4]            = ISNULL(@FRR_Cell4, [FRR_Cell4]),
        [FRR_Cell5]            = ISNULL(@FRR_Cell5, [FRR_Cell5]),
        [FRR_Cell6]            = ISNULL(@FRR_Cell6, [FRR_Cell6])
    WHERE [FRR_ID] = @FRR_ID
      AND [FRR_Status] = 'A';

    IF @@ROWCOUNT > 0
    BEGIN
        SELECT 'True' AS [Result], 'Record Updated Successfully' AS [Message];
    END
    ELSE
    BEGIN
        SELECT 'False' AS [Result], 'Record not found or inactive' AS [Message];
    END
END
GO
```

---

## 4. Backend C# Actions (`TransactionController.cs`)

These actions have been added to [`SBLT2/Controllers/TransactionController.cs`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs):

```csharp
public string Pr_Delete_Formation_Rework_Rejection(string ID)
{
    try
    {
        string cleanID = (ID ?? "").Replace("'", "''");
        DataTable dt = dbFunctions.getTable("pr_delete_Formation_Rework_Rejection '" + cleanID + "'");
        return "True";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}

public string Pr_Update_Formation_Rework_Rejection(
    string FRR_ID,
    string FRR_Type_ID = "",
    string FRR_Type_Name = "",
    string FRR_Reason = "",
    string FRR_Employee_ID = "",
    string FRR_Employee_Name = "",
    string FRR_Created_By = "",
    string FRR_QRCode = "",
    string FRR_BatteryType = "",
    string FRR_CKTNo = "",
    string FRR_Cell1 = "",
    string FRR_Cell2 = "",
    string FRR_Cell3 = "",
    string FRR_Cell4 = "",
    string FRR_Cell5 = "",
    string FRR_Cell6 = "",
    string FRR_DefectCell = "",
    string FRR_OCV = "",
    string FRR_PlateNo = "",
    string FRR_Location = "",
    string FRR_PlateType = "",
    string FRR_DefectReasonType = "",
    string FRR_DefectReason = ""
)
{
    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
    try
    {
        con.Open();
        SqlCommand com = new SqlCommand("pr_update_Formation_Rework_Rejection", con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@FRR_ID", SqlDbType.Int).Value = Convert.ToInt32(FRR_ID);
        com.Parameters.Add("@FRR_Type_Name", SqlDbType.VarChar, 50).Value = FRR_Type_Name ?? "";
        com.Parameters.Add("@FRR_Reason", SqlDbType.VarChar, 500).Value = FRR_Reason ?? "";
        com.Parameters.Add("@FRR_DefectReasonType", SqlDbType.VarChar, 100).Value = string.IsNullOrEmpty(FRR_DefectReasonType) ? (object)DBNull.Value : FRR_DefectReasonType;
        com.Parameters.Add("@FRR_DefectReason", SqlDbType.VarChar, 500).Value = string.IsNullOrEmpty(FRR_DefectReason) ? (object)DBNull.Value : FRR_DefectReason;
        com.Parameters.Add("@FRR_Employee_Name", SqlDbType.VarChar, 100).Value = FRR_Employee_Name ?? "";
        com.Parameters.Add("@FRR_DefectCell", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_DefectCell) ? (object)DBNull.Value : FRR_DefectCell;
        com.Parameters.Add("@FRR_OCV", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_OCV) ? (object)DBNull.Value : FRR_OCV;
        com.Parameters.Add("@FRR_PlateNo", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_PlateNo) ? (object)DBNull.Value : FRR_PlateNo;
        com.Parameters.Add("@FRR_Location", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_Location) ? (object)DBNull.Value : FRR_Location;
        com.Parameters.Add("@FRR_PlateType", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_PlateType) ? (object)DBNull.Value : FRR_PlateType;
        com.Parameters.Add("@FRR_Cell1", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_Cell1) ? (object)DBNull.Value : FRR_Cell1;
        com.Parameters.Add("@FRR_Cell2", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_Cell2) ? (object)DBNull.Value : FRR_Cell2;
        com.Parameters.Add("@FRR_Cell3", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_Cell3) ? (object)DBNull.Value : FRR_Cell3;
        com.Parameters.Add("@FRR_Cell4", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_Cell4) ? (object)DBNull.Value : FRR_Cell4;
        com.Parameters.Add("@FRR_Cell5", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_Cell5) ? (object)DBNull.Value : FRR_Cell5;
        com.Parameters.Add("@FRR_Cell6", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_Cell6) ? (object)DBNull.Value : FRR_Cell6;
        com.ExecuteNonQuery();
        con.Close();
        return "True";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}
```

---

## 5. UI Grid Layout on Transaction Page

| Column Header | Datafield | Width | Properties | Action / Render |
| :--- | :--- | :---: | :---: | :--- |
| *(Checkbox)* | `[selection]` | auto | `selectionmode: 'checkbox'` | Select one or multiple rows for bulk deletion |
| **Edit** | `Edit` | 45px | Centered, pointer cursor | Clicking populates the form above and enters Update mode |
| **Delete** | `Delete` | 50px | Centered, pointer cursor | Prompts confirmation and deletes this single record |
| **S.No** | `SNo` | 50px | Centered | Row number |
| **QR Code** | `FRR_QRCode` | 120px | Left | Battery QR code |
| **Battery Type** | `FRR_BatteryType` | 100px | Left | Model / Battery Type |
| **CKT No** | `FRR_CKTNo` | 80px | Left | Circuit number |
| **Type** | `FRR_Type_Name` | 85px | Left | Rework or Rejection |
| **Defect Reason** | `FRR_Reason` | 140px | Left | Process Reason |
| **Defect Code Reason** | `FRR_DefectReason` | 150px | Left | Root Cause Defect Reason |
| **Employee** | `FRR_Employee_Name` | 110px | Left | Operator Name |
| **Defect Cell** | `FRR_DefectCell` | 80px | Centered | Defect Cell (e.g. C-1) |
| **OCV** | `FRR_OCV` | 75px | Centered | OCV Voltage |
| **Plate No** | `FRR_PlateNo` | 75px | Centered | Plate Number |
| **Location** | `FRR_Location` | 75px | Centered | Lug / Bottom / Top |
| **Plate Type** | `FRR_PlateType` | 110px | Left | Positive / Negative |
| **Cell 1 – 6** | `FRR_Cell1` – `6` | 60px each | Centered | Cell Readings |
| **Created By** | `FRR_Created_By` | 80px | Left | User ID |
| **Date & Time** | `FRR_Created_Date` | 120px | Left | Timestamp |
