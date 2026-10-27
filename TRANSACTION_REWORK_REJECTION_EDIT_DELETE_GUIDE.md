# Transaction Controller — Rework & Rejection Page: Delete & Edit Guide

This document details the configuration for the **Transaction / Rework & Rejection** page ([`SBLT2/Views/Transaction/Rework_Rejection.cshtml`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Transaction/Rework_Rejection.cshtml)), styled after the **MVRLA Formation** screen model:

1. **Header Action Button**: A red `Delete Selected` button (`#Delete`) in the card header.
2. **Multi-Select Checkboxes**: Leftmost checkbox column (`selectionmode: 'checkbox'`) for selecting rows.
3. **Clean Grid**: No in-grid `EDIT` or `DEL` icon columns (clean data layout starting with `S.No`, `QR Code`, `Battery Type`, etc.).
4. **Row Editing**: Double-clicking any row loads its data into the entry form and switches the button label to **"Update Record"**.
5. **Batch Deletion**: Checking one or more rows and clicking `Delete Selected` prompts confirmation and soft-deletes the selected records (`FRR_Status = 'D'`).
6. **Backend Controller**: Stored procedure endpoints in [`SBLT2/Controllers/TransactionController.cs`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs).
7. **Database Stored Procedures**: `pr_delete_Formation_Rework_Rejection` and `pr_update_Formation_Rework_Rejection`.

---

## 1. SQL Stored Procedures

Execute this script in SQL Server Management Studio (SSMS) against your database (e.g., `MVRLA`).

```sql
-- =========================================================================
-- SCRIPT: pr_Rework_Rejection_Edit_Delete_Procedures.sql
-- DESCRIPTION: Stored procedures for Soft-Delete and Update operations
--              on the Formation_Rework_Rejection table.
-- =========================================================================


USE [MVRLA]; -- Adjust database name if needed
GO

-- =========================================================================
-- 1. STORED PROCEDURE: pr_delete_Formation_Rework_Rejection
-- Description: Performs soft deletion by marking FRR_Status = 'D'.
-- =========================================================================
IF OBJECT_ID('dbo.pr_delete_Formation_Rework_Rejection', 'P') IS NOT NULL
    DROP PROCEDURE dbo.pr_delete_Formation_Rework_Rejection;
GO

CREATE PROCEDURE dbo.pr_delete_Formation_Rework_Rejection
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
END;
GO

-- =========================================================================
-- 2. STORED PROCEDURE: pr_update_Formation_Rework_Rejection
-- Description: Updates defect reasons, classifications, employee, and cells
--              for an active Formation_Rework_Rejection record.
-- =========================================================================
IF OBJECT_ID('dbo.pr_update_Formation_Rework_Rejection', 'P') IS NOT NULL
    DROP PROCEDURE dbo.pr_update_Formation_Rework_Rejection;
GO

CREATE PROCEDURE dbo.pr_update_Formation_Rework_Rejection
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
END;
GO
```

---

## 2. Controller Methods in `TransactionController.cs`

File: [`SBLT2/Controllers/TransactionController.cs`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs#L528-L599)

### Soft Delete Endpoint (`Pr_Delete_Formation_Rework_Rejection`)
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
```

### Update Endpoint (`Pr_Update_Formation_Rework_Rejection`)
```csharp
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

## 3. View UI & JavaScript Integration

File: [`SBLT2/Views/Transaction/Rework_Rejection.cshtml`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Transaction/Rework_Rejection.cshtml)

### A. Grid Header with `Delete Selected` Button
```html
<div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 12px;">
    <h4 style="font-size: 14px; font-weight: 700; color: var(--text-primary); margin: 0;">
        <i class="fa fa-list text-primary"></i> Logged Rework / Rejection Records
    </h4>
    <button type="button" id="Delete" class="btn btn-danger btn-sm" style="min-height: 32px; height: 32px; padding: 4px 14px;">
        <i class="fa fa-trash"></i> Delete Selected
    </button>
</div>
<div id="Grid"></div>
```

### B. jqxGrid Setup (Clean Grid with Checkbox Column, No In-Grid Buttons)
```javascript
var gridAdapter = new $.jqx.dataAdapter(gridSource);
$("#Grid").jqxGrid({
    theme: 'ui-sunny',
    width: '100%',
    height: '420px',
    source: gridAdapter,
    selectionmode: 'checkbox',
    pageable: true,
    pagermode: 'simple',
    pagesize: 14,
    showfilterrow: true,
    filterable: true,
    sortable: true,
    columnsresize: true,
    enabletooltips: true,
    columnsheight: 34,
    rowsheight: 30,
    altrows: true,
    columns: [
        { text: 'S.No', datafield: 'SNo', width: 50, align: 'center', cellsalign: 'center' },
        { text: 'QR Code', datafield: 'FRR_QRCode', width: 120 },
        { text: 'Battery Type', datafield: 'FRR_BatteryType', width: 100 },
        { text: 'CKT No', datafield: 'FRR_CKTNo', width: 80 },
        { text: 'Type', datafield: 'FRR_Type_Name', width: 85 },
        { text: 'Defect Reason', datafield: 'FRR_Reason', width: 140 },
        { text: 'Defect Code Reason', datafield: 'FRR_DefectReason', width: 150 },
        { text: 'Employee', datafield: 'FRR_Employee_Name', width: 110 },
        { text: 'Defect Cell', datafield: 'FRR_DefectCell', width: 80, align: 'center', cellsalign: 'center' },
        { text: 'OCV', datafield: 'FRR_OCV', width: 75, align: 'center', cellsalign: 'center' },
        { text: 'Plate No', datafield: 'FRR_PlateNo', width: 75, align: 'center', cellsalign: 'center' },
        { text: 'Location', datafield: 'FRR_Location', width: 75, align: 'center', cellsalign: 'center' },
        { text: 'Plate Type', datafield: 'FRR_PlateType', width: 110 },
        { text: 'Cell 1', datafield: 'FRR_Cell1', width: 60, align: 'center', cellsalign: 'center' },
        { text: 'Cell 2', datafield: 'FRR_Cell2', width: 60, align: 'center', cellsalign: 'center' },
        { text: 'Cell 3', datafield: 'FRR_Cell3', width: 60, align: 'center', cellsalign: 'center' },
        { text: 'Cell 4', datafield: 'FRR_Cell4', width: 60, align: 'center', cellsalign: 'center' },
        { text: 'Cell 5', datafield: 'FRR_Cell5', width: 60, align: 'center', cellsalign: 'center' },
        { text: 'Cell 6', datafield: 'FRR_Cell6', width: 60, align: 'center', cellsalign: 'center' },
        { text: 'Created By', datafield: 'FRR_Created_By', width: 80 },
        { text: 'Date & Time', datafield: 'FRR_Created_Date', width: 120 }
    ]
});
```

### C. Double-Click Row to Edit (`rowdoubleclick`)
```javascript
$("#Grid").on("rowdoubleclick", function (event) {
    var rowindex = event.args.rowindex;
    var dataRecord = $("#Grid").jqxGrid('getrowdata', rowindex);
    if (dataRecord) {
        populateFormForEdit(dataRecord);
    }
});
```

### D. Delete Selected Rows (`#Delete`)
```javascript
$('#Delete').click(function () {
    var selectedrows = $('#Grid').jqxGrid('getselectedrowindexes');
    if (selectedrows.length <= 0) {
        alert('Please select row want to Delete');
        return;
    }
    var a = confirm("Are you sure want to delete press OK");
    if (a === true) {
        for (var i = 0; i < selectedrows.length; i++) {
            var frrId = $('#Grid').jqxGrid('getcellvalue', selectedrows[i], "FRR_ID");
            if (frrId) {
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: '../../Transaction/Pr_Delete_Formation_Rework_Rejection',
                    data: JSON.stringify({ ID: frrId }),
                    async: false,
                    success: function (response) { }
                });
            }
        }
        getGrid();
        alert('Record Deleted Successfully');
    }
});
```

---

## 4. Summary of Final Behavior

| Action | How Triggered | Result |
| :--- | :--- | :--- |
| **Delete Records** | Select row checkboxes ➔ Click red **Delete Selected** button | Confirms and soft-deletes selected records (`FRR_Status = 'D'`), reloads grid |
| **Edit Record** | Double-click any row in the grid | Loads all record details into entry form; button switches to **"Update Record"** |
| **Grid Display** | Automatic on page load / after save / delete | Clean columns starting with `S.No`, `QR Code`, etc., matching MVRLA Formation |
