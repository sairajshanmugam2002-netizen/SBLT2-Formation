# Employee Mapping in Reference Master and Rework/Rejection

## 1. Requirement Summary

- **Existing Project**: MVRLA Formation Traceability System (SBLT2).
- **Target Pages**:
  1. **Reference Master**: `/Master/Reference_Master`
  2. **Rework/Rejection**: `/Transaction/Rework_Rejection`

### Requirements Implemented:
1. **Reference Master Changes**:
   - Reused existing Reference Master page and Reference Type dropdown (`#RG_vDescription`).
   - Retained all existing Reference Types and existing functionality.
   - When **Reference Type = "Employee"**:
     - Displays an Employee selection dropdown (`#ddl_Employee_Master`) populated with active employee data from `Employee_Master`.
     - Displays employees using `Employee Name (Employee Code)`.
     - When an employee is selected, automatically populates the Reference Code (`#RGV_vCode`) and Employee Description (`#RGV_vDesciption`).
   - When any other Reference Type is selected:
     - Employee dropdown is hidden.
     - Existing Reference Code and Description input behavior is retained.
   - Existing Reference Master records and save/update/delete flows continue to work uninterrupted.

2. **Rework/Rejection Employee Dropdown**:
   - Reused the existing Employee dropdown (`#ddlEmployee`) on `/Transaction/Rework_Rejection`.
   - Mapped it directly to the Employee entries maintained through Reference Master (`ReferenceGroup_Value` where Reference Type = Employee).
   - Display format: `Employee Name (EM_Code)`.
     - Example:
       - `Sairaj (100234)`
       - `Aravinth (100002341)`
       - `Sakthi (1000071)`
   - Saves the correct employee ID/value when an employee is selected.
   - Correctly passes the selected employee to the existing save procedure (`pr_insert_Formation_Rework_Rejection`).
   - Eliminates duplicate employee entries (`PARTITION BY LTRIM(RTRIM(rgv.RGV_vCode)), LTRIM(RTRIM(rgv.RGV_vDesciption))`).

3. **Database and Stored Procedure Protection**:
   - **Zero stored procedures were modified**. All existing stored procedures remain 100% untouched.
   - **Zero database tables were modified or created**. Existing database tables (`Reference_Group`, `ReferenceGroup_Value`, `Employee_Master`, `Formation_Rework_Rejection`) are reused as-is.
   - No hardcoded employee names or codes.
   - All mapping logic is encapsulated cleanly in the new helper file `EmployeeMappingHelper.cs`.

---

## 2. Files Modified and Created

| File | Status | Description |
|---|---|---|
| `SBLT2/CommonData/EmployeeMappingHelper.cs` | **New File** | Encapsulates SQL queries and JSON serialization for active employees (Reference Master) and mapped employees (Rework/Rejection). |
| `SBLT2/SBLT2.csproj` | Modified | Registered `CommonData\EmployeeMappingHelper.cs` in project compilation list. |
| `SBLT2/Controllers/MasterController.cs` | Modified | Added `Pr_Fetch_Employee_Master_For_Mapping()` action method delegating to `EmployeeMappingHelper`. |
| `SBLT2/Controllers/TransactionController.cs` | Modified | Updated `pr_fetch_Active_Employees()` action method to return Reference Master mapped employees via `EmployeeMappingHelper`. |
| `SBLT2/Views/Master/Reference_Master.cshtml` | Modified | Added `#div_Employee_Master` and `#ddl_Employee_Master` combobox; toggles visibility based on Reference Type; auto-populates code and description; syncs on grid row edit. |
| `SBLT2/Views/Transaction/Rework_Rejection.cshtml` | Modified | Updated `populateFormForEdit` to handle case-insensitive and code-matched employee selection, and dynamically include historical employee items when editing older records. |

---

## 3. Implementation Details

### A. New File: `SBLT2/CommonData/EmployeeMappingHelper.cs`

Provides two dedicated, reusable methods:

1. **`GetActiveEmployeesForReferenceMaster()`**:
   - Queries `Employee_Master` where `EM_Status = 'A'`.
   - Returns:
     - `EM_ID`: Integer ID
     - `EM_Code`: Employee Code
     - `EM_Employee_Name`: Employee Name
     - `EM_Display_Name`: `Employee Name (EM_Code)`

2. **`GetMappedEmployeesForReworkRejection()`**:
   - Queries `ReferenceGroup_Value` joined with `Reference_Group` where `rg.RG_vDescription = 'Employee'` (or `rg.RG_iID = 1`) and `rgv.RGV_vStatus = 'A'`.
   - Matches with `Employee_Master` (if exists) to retrieve `em.EM_ID` or falls back to `rgv.RGV_iID`.
   - Formats `EM_Display_Name` with capitalized employee name followed by code in parentheses: `Employee Name (EM_Code)`.
   - Deduplicates entries using `ROW_NUMBER() OVER (PARTITION BY LTRIM(RTRIM(rgv.RGV_vCode)), LTRIM(RTRIM(rgv.RGV_vDesciption)) ORDER BY rgv.RGV_iID DESC)`.

### B. Controller Integration

1. **`MasterController.cs`**:
   ```csharp
   public string Pr_Fetch_Employee_Master_For_Mapping()
   {
       return EmployeeMappingHelper.GetActiveEmployeesForReferenceMaster();
   }
   ```

2. **`TransactionController.cs`**:
   ```csharp
   public string pr_fetch_Active_Employees()
   {
       return EmployeeMappingHelper.GetMappedEmployeesForReworkRejection();
   }
   ```

### C. Reference Master UI (`Reference_Master.cshtml`)

- `#div_Employee_Master` container:
  ```html
  <div class="form-group" id="div_Employee_Master" style="display: none;">
      <label class="col-sm-4 control-label no-padding-right" for="ddl_Employee_Master">
          <span class="star">*</span> Select Employee
      </label>
      <div class="col-sm-7">
          <div id="ddl_Employee_Master"></div>
      </div>
  </div>
  ```
- Toggle logic on `#RG_vDescription` select event:
  - When selected Reference Type is "Employee", calls `setEmployeeMode(true)`:
    - `#div_Employee_Master.show()`
    - Label and placeholder updated to Employee Name & Code.
  - When another type is selected, calls `setEmployeeMode(false)`:
    - `#div_Employee_Master.hide()`
    - Clears `#ddl_Employee_Master`
    - Restores standard Defect Reason / Description label.
- On selecting an employee from `#ddl_Employee_Master`:
  - Automatically sets `$('#RGV_vCode').val(emp.EM_Code)`
  - Automatically sets `$('#RGV_vDesciption').val(emp.EM_Employee_Name)`
- Grid row editing:
  - If Reference Type is Employee, selects the corresponding employee in `#ddl_Employee_Master`.
- Clear button:
  - Resets `#ddl_Employee_Master` selection alongside other form inputs.

### D. Rework/Rejection UI (`Rework_Rejection.cshtml`)

- `#ddlEmployee` dropdown binds to `/Transaction/pr_fetch_Active_Employees`.
- Display member: `EM_Display_Name` (`Employee Name (EM_Code)`).
- Value member: `EM_ID`.
- Save validation and payload:
  - Selected employee `value` (`FRR_Employee_ID`) and `label` (`FRR_Employee_Name`) are passed to `pr_insert_Formation_Rework_Rejection`.
- Row editing (`populateFormForEdit`):
  - Case-insensitive comparison between `dataRecord.FRR_Employee_Name` and dropdown item labels.
  - If editing a historical record saved before mapping, automatically adds fallback item to prevent blank dropdown.

---

## 4. Verification & Testing

1. **MSBuild Compilation**:
   - `MSBuild.exe "SBLT2\SBLT2.csproj" /p:Configuration=Debug /t:Build`
   - **Result**: `Build succeeded. 0 Warning(s), 0 Error(s)`.

2. **Endpoint Testing (HTTP)**:
   - `GET /Master/Pr_Fetch_Employee_Master_For_Mapping`:
     - Returned active employees from `Employee_Master` with `EM_ID`, `EM_Code`, `EM_Employee_Name`, `EM_Display_Name`.
     - HTTP Status: `200 OK`.
   - `GET /Transaction/pr_fetch_Active_Employees`:
     - Returned mapped employees from `ReferenceGroup_Value`:
       - `Aravinth (100002341)`
       - `Sairaj (100234)`
       - `Sakthi (1000071)`
       - `SS (SS)`
     - HTTP Status: `200 OK`.

3. **Page Rendering (HTTP)**:
   - `GET /Master/Reference_Master`: Status `200 OK`, HTML rendered with `#div_Employee_Master` and `#ddl_Employee_Master`.
   - `GET /Transaction/Rework_Rejection`: Status `200 OK`, HTML rendered.

4. **Save Transaction Test**:
   - Simulated saving a rework record with `FRR_Employee_ID = 2368` and `FRR_Employee_Name = 'Sairaj (100234)'` via `/Transaction/pr_insert_Formation_Rework_Rejection`.
   - Result: `True` (Record inserted successfully).
   - Verified in database: Record 11 was logged with employee details.
   - Cleaned up test record immediately.

5. **Database Integrity Verification**:
   - All existing tables and schema unchanged.
   - All stored procedures completely unchanged.

