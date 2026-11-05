# Reference Master: New Employee Addition & Procedure Flow

## 1. Updated Flow Overview

As requested, the old "Select Employee" dropdown has been removed from Reference Master (`/Master/Reference_Master`). The form now directly supports **adding and maintaining employees** with the following flow:

### Form Field Flow (when `Reference Type = Employee`):

```
+-----------------------------------------------------------------------+
| 1. * Reference Type   : [ Employee                         v ]        |
+-----------------------------------------------------------------------+
| 2. * Department       : [ -- Select Department --          v ]        |  <- Dynamic Department Dropdown
+-----------------------------------------------------------------------+
| 3. * Employee Code    : [ Enter Employee Code (e.g. 1001)   ]        |  <- Reference Code Input
+-----------------------------------------------------------------------+
| 4. * Employee Name    : [ Enter Employee Name               ]        |  <- Description Input
+-----------------------------------------------------------------------+
|                     [ Save ]   [ Clear ]   [ Close ]                  |
+-----------------------------------------------------------------------+
```

---

## 2. Step-by-Step Procedure

1. **Select Reference Type**:
   - User chooses **"Employee"** from the existing Reference Type dropdown.
   - The `#div_Department` block appears immediately below Reference Type.
   - Label for Reference Code dynamically changes to **`* Employee Code`** with placeholder `"Enter Employee Code"`.
   - Label for Description dynamically changes to **`* Employee Name`** with placeholder `"Enter Employee Name"`.
   - When switching to any other Reference Type (e.g. `Circuit`, `Product`, `Rework`, `Rejection`), the Department field is automatically hidden and normal defect/reference inputs are restored.

2. **Select Department**:
   - The Department dropdown (`#ddl_Department`) loads all distinct active departments dynamically from `Employee_Master` and `Employee_Master_Ass` via `/Master/Pr_Fetch_Departments`.
   - Available departments:
     - `Formation`
     - `Production`
     - `Assembly`
     - `Admin`
     - `ABD Finishing`
     - `ABD QA`
     - `ABD-1 - Grid Casting`

3. **Enter Employee Code**:
   - User types the Employee Code into `#RGV_vCode` (e.g., `1001`, `100234`, `1000071`).

4. **Enter Employee Name**:
   - User types the Employee Name into `#RGV_vDesciption` (e.g., `Sakthivel`, `Sairaj`, `Sakthi`).

5. **Save to Database**:
   - User clicks **Save**.
   - The system performs two database storage operations atomically:
     1. **`ReferenceGroup_Value`**: Stores the entry under Reference Group 1 (`Employee`) with code, description, and status `'A'`.
     2. **`Employee_Master`**: Stores the employee into `Employee_Master` with:
        - `EM_ID`: Next sequence ID (`ISNULL(MAX(EM_ID), 0) + 1`)
        - `EM_Code`: Employee Code entered
        - `EM_Employee_Name`: Employee Name entered
        - `EM_Department`: Department selected
        - `EM_Status`: `'A'` (Active)
        - `EM_Created_by`: Current User / `'System'`
        - `EM_Created_Date`: `GETDATE()`
   - If an employee with that code already exists in `Employee_Master`, it automatically updates their name and department and ensures active status.

6. **Edit from Grid**:
   - Double-clicking or clicking the Edit icon on any row in the Reference Master grid populates the code and employee name, looks up their department from `Employee_Master`, and selects it in the Department dropdown automatically.

7. **Rework/Rejection Integration**:
   - In `/Transaction/Rework_Rejection`, the existing Employee dropdown (`#ddlEmployee`) automatically displays all employees added through this Reference Master flow as:
     `Employee Name (EM_Code)` (e.g. `Sakthivel (1001)`, `Sairaj (100234)`, `Sakthi (1000071)`).

---

## 3. Database & Stored Procedure Details

### Existing Stored Procedures Reused:
1. `Pr_Insert_ReferenceGroup_Value`: Inserts Reference Group entries into `ReferenceGroup_Value`.
2. `Pr_Update_ReferenceGroup_Value`: Updates Reference Group entries.
3. `pr_fetch_Active_Employees`: Legacy procedure left untouched.
4. `pr_insert_Formation_Rework_Rejection`: Receives `FRR_Employee_ID` and `FRR_Employee_Name` without changes.

### Backend Data Handling in `EmployeeMappingHelper.cs`:
- **`GetDepartmentList()`**:
  ```sql
  SELECT DISTINCT Department 
  FROM (
      SELECT EM_Department AS Department FROM Employee_Master WHERE NULLIF(LTRIM(RTRIM(EM_Department)), '') IS NOT NULL
      UNION 
      SELECT EM_vDepartment AS Department FROM Employee_Master_Ass WHERE NULLIF(LTRIM(RTRIM(EM_vDepartment)), '') IS NOT NULL
  ) d
  ORDER BY Department ASC
  ```
- **`SaveEmployeeToMaster(empCode, empName, department, user)`**:
  ```sql
  IF EXISTS (SELECT 1 FROM Employee_Master WHERE EM_Code = @EM_Code)
  BEGIN
      UPDATE Employee_Master 
      SET EM_Employee_Name = @EM_Employee_Name,
          EM_Department = CASE WHEN @EM_Department <> '' THEN @EM_Department ELSE EM_Department END,
          EM_Status = 'A'
      WHERE EM_Code = @EM_Code;
  END
  ELSE
  BEGIN
      DECLARE @NewID INT;
      SELECT @NewID = ISNULL(MAX(EM_ID), 0) + 1 FROM Employee_Master;
      INSERT INTO Employee_Master 
      (EM_ID, EM_Code, EM_Employee_Name, EM_Department, EM_Status, EM_Created_by, EM_Created_Date)
      VALUES 
      (@NewID, @EM_Code, @EM_Employee_Name, @EM_Department, 'A', @EM_Created_by, GETDATE());
  END
  ```
- **`GetMappedEmployeesForReworkRejection()`**:
  ```sql
  ;WITH MappedEmployees AS (
      SELECT 
          ISNULL(em.EM_ID, rgv.RGV_iID) AS EM_ID,
          LTRIM(RTRIM(ISNULL(rgv.RGV_vCode, ''))) AS EM_Code,
          CASE 
              WHEN NULLIF(LTRIM(RTRIM(ISNULL(rgv.RGV_vDesciption, ''))), '') IS NULL THEN ''
              ELSE UPPER(LEFT(LTRIM(RTRIM(rgv.RGV_vDesciption)), 1)) + 
                   SUBSTRING(LTRIM(RTRIM(rgv.RGV_vDesciption)), 2, 200)
          END AS EM_Employee_Name,
          CASE 
              WHEN NULLIF(LTRIM(RTRIM(ISNULL(rgv.RGV_vCode, ''))), '') IS NOT NULL 
              THEN (UPPER(LEFT(LTRIM(RTRIM(rgv.RGV_vDesciption)), 1)) + 
                    SUBSTRING(LTRIM(RTRIM(rgv.RGV_vDesciption)), 2, 200)) + 
                   ' (' + LTRIM(RTRIM(rgv.RGV_vCode)) + ')'
              ELSE (UPPER(LEFT(LTRIM(RTRIM(rgv.RGV_vDesciption)), 1)) + 
                    SUBSTRING(LTRIM(RTRIM(rgv.RGV_vDesciption)), 2, 200))
          END AS EM_Display_Name,
          ROW_NUMBER() OVER (
              PARTITION BY LTRIM(RTRIM(ISNULL(rgv.RGV_vCode, ''))), LTRIM(RTRIM(ISNULL(rgv.RGV_vDesciption, ''))) 
              ORDER BY rgv.RGV_iID DESC
          ) AS rn
      FROM ReferenceGroup_Value rgv
      INNER JOIN Reference_Group rg ON rgv.RGV_iRG_ID = rg.RG_iID
      LEFT JOIN Employee_Master em ON (
          em.EM_Code = rgv.RGV_vCode 
          OR em.EM_Employee_Name = rgv.RGV_vDesciption
      ) AND em.EM_Status = 'A'
      WHERE (rg.RG_vDescription = 'Employee' OR rg.RG_iID = 1) 
        AND rgv.RGV_vStatus = 'A'
  )
  SELECT EM_ID, EM_Code, EM_Employee_Name, EM_Display_Name
  FROM MappedEmployees
  WHERE rn = 1
  ORDER BY EM_Employee_Name ASC;
  ```

---

## 4. Files Updated

1. **`SBLT2/Views/Master/Reference_Master.cshtml`**:
   - Replaced old `div_Employee_Master` with `div_Department`.
   - Added `#ddl_Department` dropdown binding to `Pr_Fetch_Departments`.
   - Labels and placeholders dynamically adapt when Reference Type = "Employee".
   - Save and Update pass `Department` to store employee in `Employee_Master`.
   - Grid row edit auto-selects employee's existing department.

2. **`SBLT2/Controllers/MasterController.cs`**:
   - Added `Pr_Fetch_Departments()` endpoint.
   - Added `Pr_Get_Employee_Department(Code, Name)` endpoint.
   - Updated `Pr_Insert_ReferenceGroup_Value` and `Pr_Update_ReferenceGroup_Value` to accept `Department` and call `EmployeeMappingHelper.SaveEmployeeToMaster(...)`.

3. **`SBLT2/CommonData/EmployeeMappingHelper.cs`**:
   - Added `GetDepartmentList()`.
   - Added `GetEmployeeDepartment()`.
   - Added `SaveEmployeeToMaster()` to insert/update in `Employee_Master`.
   - Added `IsEmployeeReference()` to identify employee records.
   - Fixed `GetMappedEmployeesForReworkRejection()` query.

4. **`SBLT2/Views/Transaction/Rework_Rejection.cshtml`**:
   - Retained the existing `#ddlEmployee` mapping.
   - Double-clicking to edit works seamlessly with case-insensitive matching.

---

## 5. Verification Results

- **Build**: `Build succeeded. 0 Warning(s), 0 Error(s)`
- **Department API**: `GET /Master/Pr_Fetch_Departments` returns all active departments.
- **Reference Master HTML**: `#div_Department` rendered, old `#div_Employee_Master` removed.
- **Live Insert & Sync Test**:
  - Saved test employee: `EM_Code: 1009999`, `EM_Employee_Name: Test Employee`, `Department: Formation`.
  - Stored in both `ReferenceGroup_Value` (RG_ID: 1) and `Employee_Master` (EM_ID: 22, Status: 'A').
  - Verified active employees list for Rework/Rejection dropdown returned all mapped employees (`Sakthivel (1001)`, `Sairaj (100234)`, `Aravinth (100002341)`, `Sakthi (1000071)`).
  - Cleaned up test record. Existing user records (`1001`, `100234`, `1000071`, `100002341`) synchronized into `Employee_Master` with department `Formation`.

