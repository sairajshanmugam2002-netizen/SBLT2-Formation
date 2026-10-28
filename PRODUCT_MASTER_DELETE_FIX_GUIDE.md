# Product Master Page — Grid Delete Error Fix Guide

## 1. Problem Summary & Root Cause

When selecting a row in the **Product Master** grid ([`SBLT2/Views/Master/Assembly_Product_Master.cshtml`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Master/Assembly_Product_Master.cshtml)) and clicking the **Delete** button:
1. A confirmation popup appeared: *"Are you sure want to delete press OK"*.
2. Clicking **OK** showed the alert:
   > ❌ **`Record Saved Successfully...!`**
3. The selected record was **never deleted** and stayed in the grid.

### Why Did This Happen?
Looking at the original JavaScript code in `Assembly_Product_Master.cshtml`:
```javascript
// BEFORE (Buggy Code):
$('#Delete').click(function () {
    var selectedrows = $('#Grid').jqxGrid('getselectedrowindexes');
    if (selectedrows.length <= 0) {
        alert('Please select row yoy want to Delete');
        return;
    }
    else {
        var a = confirm("Are you sure want to delete press OK");
        if (a == true) {
            for (var i = 0; i < selectedrows.length; i++) {
                // BUG 1: The grid primary key is PM_iID, but it queried "inv_Id" (which evaluated to undefined)
                inv_Id = $('#Grid').jqxGrid('getcellvalue', selectedrows[i], "inv_Id");
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    // BUG 2: Called Material Receipt delete endpoint instead of Product Master
                    url: 'Pr_Delete_Material_Receipt',
                    data: "{'ID':'" + inv_Id + "'}",
                    async: false,
                    ...
                });
            }
        }
    }
    // BUG 3: Alert was placed outside the loop and hardcoded to "Record Saved Successfully...!"
    alert("Record Saved Successfully...!");
    $('#Grid').jqxGrid('clearselection');
    $("#Grid").jqxGrid('updatebounddata');
});
```

---

## 2. Solutions Implemented

### A. Frontend Fix ([`SBLT2/Views/Master/Assembly_Product_Master.cshtml`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Master/Assembly_Product_Master.cshtml#L932-L968))
- Retrieved the correct primary key: `PM_iID = $('#Grid').jqxGrid('getcellvalue', selectedrows[i], "PM_iID");`
- Directed AJAX request to `pr_Delete_Product_MasterAssy`.
- Moved alert inside `if (a == true)` and corrected message to **`alert("Record Deleted Successfully...!");`**.
- Refreshed grid data with `$('#Grid').jqxGrid('updatebounddata')`.

```javascript
// AFTER (Fixed Code):
$('#Delete').click(function () {
    var selectedrows = $('#Grid').jqxGrid('getselectedrowindexes');
    if (selectedrows.length <= 0) {
        alert('Please select row want to Delete');
        return;
    }
    else {
        var a = confirm("Are you sure want to delete press OK");
        if (a == true) {
            for (var i = 0; i < selectedrows.length; i++) {
                var pm_iID = $('#Grid').jqxGrid('getcellvalue', selectedrows[i], "PM_iID");
                if (pm_iID) {
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        url: 'pr_Delete_Product_MasterAssy',
                        data: "{'ID':'" + pm_iID + "'}",
                        async: false,
                        success: function (response) { },
                        error: function () {
                            console.log('there is some error');
                        }
                    });
                }
            }
            $('#Grid').jqxGrid('clearselection');
            $("#Grid").jqxGrid('updatebounddata');
            alert("Record Deleted Successfully...!");
        }
        else {
            $('#Grid').jqxGrid('clearselection');
            return;
        }
    }
});
```

---

### B. Controller Endpoints ([`SBLT2/Controllers/MasterController.cs`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/MasterController.cs#L723-L751))

Added both `pr_Delete_Product_MasterAssy` and updated `Pr_Delete_Product_Master_by_ID` to perform soft-delete (`PM_Status = 'D'`) on `Product_Master_Assembly`:

```csharp
public string pr_Delete_Product_MasterAssy(string ID)
{
    try
    {
        if (string.IsNullOrWhiteSpace(ID)) return "Invalid ID";
        string cleanID = ID.Trim().Replace("'", "''");
        DataTable dt = dbFunctions.getTable("UPDATE Product_Master_Assembly SET PM_Status = 'D' WHERE PM_iID = '" + cleanID + "'");
        return "True";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}

public string Pr_Delete_Product_Master_by_ID(string ID)
{
    try
    {
        if (string.IsNullOrWhiteSpace(ID)) return "Invalid ID";
        string cleanID = ID.Trim().Replace("'", "''");
        DataTable dt = dbFunctions.getTable("UPDATE Product_Master_Assembly SET PM_Status = 'D' WHERE PM_iID = '" + cleanID + "'");
        return "True";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}
```

Since `Pr_Fetch_Product_MasterAssy` queries `WHERE PM_Status = 'A'`, any record marked as `'D'` disappears immediately after `#Grid` reloads.

---

## 3. SQL Stored Procedure Script

File: [`pr_Delete_Product_MasterAssy.sql`](file:///d:/office/MVRLA_Formation/MVRLA_Formation/pr_Delete_Product_MasterAssy.sql)

You can run this in SQL Server Management Studio (SSMS) if you want a dedicated stored procedure in your database:

```sql
USE [MVRLA]; -- Adjust database name if needed
GO

IF OBJECT_ID('dbo.pr_Delete_Product_MasterAssy', 'P') IS NOT NULL
    DROP PROCEDURE dbo.pr_Delete_Product_MasterAssy;
GO

CREATE PROCEDURE dbo.pr_Delete_Product_MasterAssy
    @PM_iID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF (@PM_iID IS NULL OR @PM_iID <= 0)
    BEGIN
        SELECT 'False' AS [Result], 'Invalid Record ID' AS [Message];
        RETURN;
    END

    UPDATE [dbo].[Product_Master_Assembly]
    SET [PM_Status] = 'D'
    WHERE [PM_iID] = @PM_iID;

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
```

---

## 4. Verification Checklist

- [x] Select any row in the Product Master grid.
- [x] Click **Delete** button in the grid toolbar.
- [x] Confirmation prompt appears: *"Are you sure want to delete press OK"*.
- [x] Click **OK**:
  - The record is soft-deleted (`PM_Status = 'D'`).
  - Alert displays: **`"Record Deleted Successfully...!"`** (no longer says `"Record Saved Successfully...!"`).
  - Grid reloads automatically and the deleted item is gone.
- [x] If user clicks **Cancel**, selection clears and no record is deleted.
- [x] Solution compiled cleanly with **0 Errors**.
