# Reference Master: Update Fix, Close Button Removal & Complete Flow

## 1. Issue Diagnosis & Root Cause

### Error Encountered
During record update on the **Reference Master** page (`/Master/Reference_Master`), the following error was displayed in a toast notification:
```
Procedure or function Pr_Update_ReferenceGroup_Value has too many arguments specified.
```

### Root Cause
- In the SQL Server database, the existing stored procedure `Pr_Update_ReferenceGroup_Value` is defined as:
  ```sql
  CREATE Proc Pr_Update_ReferenceGroup_Value      
  @RGV_iID int,      
  @RGV_vCode varchar(50),      
  @RGV_vDesciption varchar(250)
  ```
  It strictly accepts **3 parameters**: `@RGV_iID`, `@RGV_vCode`, and `@RGV_vDesciption`.

- In `SBLT2/Controllers/MasterController.cs`, method `Pr_Update_ReferenceGroup_Value` was previously passing a 4th parameter:
  ```csharp
  // Extra argument that caused SQL Server to throw "too many arguments specified"
  com.Parameters.Add("@RGV_vDefectReasonType", SqlDbType.VarChar, 100).Value = ...;
  ```
- Because the stored procedure did not define `@RGV_vDefectReasonType`, SQL Server rejected the execution.

---

## 2. Changes Implemented

### A. Fix `Pr_Update_ReferenceGroup_Value` in `MasterController.cs`
- Removed `@RGV_vDefectReasonType` from the `SqlCommand` parameters in `Pr_Update_ReferenceGroup_Value`.
- The procedure call now matches the exact 3 parameters required by `Pr_Update_ReferenceGroup_Value`:
  - `@RGV_iID`
  - `@RGV_vCode`
  - `@RGV_vDesciption`
- Maintained the dual-persistence logic: when an employee reference is updated, `EmployeeMappingHelper.SaveEmployeeToMaster(RGV_vCode, RGV_vDesciption, Department, User)` synchronizes the update with `Employee_Master`.

```csharp
public string Pr_Update_ReferenceGroup_Value(string RGV_iID, string RGV_vCode, string RGV_vDesciption, string RGV_vDefectReasonType = "", string Department = "", string User = "System")
{
    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
    try
    {
        con.Open();
        SqlCommand com = new SqlCommand();
        com.Connection = con;
        com.CommandType = CommandType.StoredProcedure;
        com.CommandText = "Pr_Update_ReferenceGroup_Value";
        com.Parameters.Add("@RGV_iID", SqlDbType.VarChar).Value = RGV_iID;
        com.Parameters.Add("@RGV_vCode", SqlDbType.VarChar).Value = RGV_vCode ?? "";
        com.Parameters.Add("@RGV_vDesciption", SqlDbType.VarChar).Value = RGV_vDesciption ?? "";
        com.ExecuteNonQuery();
        con.Close();
        
        // If updating an employee reference record, also update Employee_Master
        if (!string.IsNullOrEmpty(Department) || EmployeeMappingHelper.IsEmployeeReference(RGV_iID))
        {
            EmployeeMappingHelper.SaveEmployeeToMaster(RGV_vCode, RGV_vDesciption, Department, User);
        }
        return "True";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}
```

### B. Removal of Unused Close Button in `Reference_Master.cshtml`
- Removed the redundant `<button class="btn btn-white btn-default btn-round" data-dismiss="modal">Close</button>` button located next to `Save` and `Clear`.
- Action buttons in the header row are now strictly:
  - **Save / Update** (`#btn_Save`)
  - **Clear** (`#btn_Clear`)

---

## 3. Reference Master Flow Summary

```
                      [ Reference Type Dropdown ]
                                  |
              +-------------------+-------------------+
              |                                       |
    [ Reference Type = Employee ]            [ Other Reference Types ]
              |                                       |
   1. Shows Department dropdown              1. Hides Department dropdown
   2. Reference Code labeled as              2. Reference Code labeled as
      "Employee Code"                           "Reference Code"
   3. Description labeled as                 3. Description labeled as
      "Employee Name"                           "Defect Reason / Description"
              |                                       |
   [ Save / Update Clicked ]                 [ Save / Update Clicked ]
              |                                       |
   - Saves to ReferenceGroup_Value           - Saves to ReferenceGroup_Value
   - Saves/Updates Employee_Master
```

### Data Synchronization Details
When Reference Type is `Employee`:
1. **ReferenceGroup_Value Table**:
   - `RGV_iRG_ID` = `1` (Employee)
   - `RGV_vCode` = Employee Code (e.g., `0002`)
   - `RGV_vDesciption` = Employee Name (e.g., `sasi`)
   - `RGV_vStatus` = `'A'`
2. **Employee_Master Table**:
   - `EM_Code` = Employee Code
   - `EM_Employee_Name` = Employee Name
   - `EM_Department` = Selected Department (e.g., `Formation`)
   - `EM_Status` = `'A'`

---

## 4. Stored Procedures Status

> [!NOTE]
> As instructed, **no stored procedures in the database were altered or rewritten**. All database procedures remain in their original state.

| Stored Procedure Name | Database Status | Parameters Expected | Action in Code |
| :--- | :--- | :--- | :--- |
| `Pr_Update_ReferenceGroup_Value` | **Unmodified** | `@RGV_iID`, `@RGV_vCode`, `@RGV_vDesciption` | Controller parameters aligned to match exactly |
| `Pr_Insert_ReferenceGroup_Value` | **Unmodified** | `@RGV_iRG_ID`, `@RGV_vCode`, `@RGV_vDesciption`, `@RGV_vDefectReasonType` | Calling properly with optional `@RGV_vDefectReasonType` |
| `pr_fetch_Active_Employees` | **Unmodified** | None | Populates Rework/Rejection dropdown as `Employee Name (EM_Code)` |

---

## 5. Verification & Test Results

1. **Compilation**:
   - Built using MSBuild (`MSBuild.exe SBLT2.csproj /p:Configuration=Debug /t:Build`).
   - Result: `0 Errors, 10 Warnings (obsolete settings warnings)`. Build succeeded.

2. **HTTP POST Test for Update (`Pr_Update_ReferenceGroup_Value`)**:
   - Tested updating Employee record `2375` (Code: `111`, Description: `sai`, Department: `Formation`).
   - Response returned: `"True"` with 200 OK.
   - SQL query confirmed `ReferenceGroup_Value` and `Employee_Master` updated successfully with department `Formation`.

3. **Page UI Inspection**:
   - Tested `http://localhost:19450/Master/Reference_Master`.
   - Verified that the unused `Close` button is completely removed.
   - Verified that `Save/Update` and `Clear` buttons render cleanly.
