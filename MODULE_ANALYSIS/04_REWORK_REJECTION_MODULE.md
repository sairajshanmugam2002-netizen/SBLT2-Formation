# MODULE 4: FORMATION REWORK & REJECTION

## 1. WORKFLOW (Old System)
```
User navigates to Formation → Rework and Rejection menu item
   ↓
TransactionController.Rework_Rejection() → Views/Transaction/Rework_Rejection.cshtml
   ↓
Page loads → fires AJAX calls:
   1. pr_fetch_Rework_Rejection_Types() → Gets defect type categories (Rework=12, Rejection=13)
   2. pr_fetch_Active_Employees()       → Active operator/employee list
   3. pr_fetch_Formation_Rework_Rejection_List() → Today's logged defects
   ↓
User selects Defect Type (Rework or Rejection)
   ↓
onChange fires: pr_fetch_Rework_Rejection_Reasons(Type_ID)
   → Loads reason codes specific to selected type
   ↓
User fills: Type, Reason, Employee (responsible operator), remarks
   ↓
AJAX POST → pr_insert_Formation_Rework_Rejection(7 parameters)
   ↓
SP inserts into Formation_Rework_Rejection table
   ↓
Grid refreshes with updated defect list
```

## 2. DATABASE TABLES USED

### 2.1 Formation_Rework_Rejection (13 rows) — DEFECT LOG
| Column | Type | Purpose |
|--------|------|---------|
| FRR_Id | int (PK, Identity) | Defect record ID |
| FRR_Formation_ID | int | Hardcoded as 68 in code (⚠️ potential bug) |
| FRR_Type_ID | int | Defect type: 12 = Rework, 13 = Rejection |
| FRR_Type_Name | varchar(50) | "Rework" or "Rejection" |
| FRR_Reason | varchar(500) | Defect reason description |
| FRR_Employee_ID | int | FK to Employee_Master.EM_iId |
| FRR_Employee_Name | varchar(100) | Operator responsible |
| FRR_Created_By | varchar(50) | Logged-in user |
| FRR_Created_Date | datetime | Auto-set by SP (GETDATE()) |

### 2.2 Reference_Group (13 categories)
- Category 12 = "Rework" reasons group
- Category 13 = "Rejection" reasons group

### 2.3 ReferenceGroup_Value (27 items) — REASON CODES
- Rework Reasons: Terminal Problem, Charging Problem, Label Problem
- Rejection Reasons: Physical Damage, Quality Failure

### 2.4 Employee_Master (21 rows) — FACTORY PERSONNEL
| Column | Type | Purpose |
|--------|------|---------|
| EM_iId | int (PK) | Employee ID |
| EM_vEmployee_ID | varchar | Employee code |
| EM_vEmployee_Name | varchar | Employee full name |
| EM_vDepartment | varchar | Department assignment |
| EM_cStatus | char(1) | 'A' = Active |

## 3. STORED PROCEDURE LOGIC

### 3.1 pr_insert_Formation_Rework_Rejection
```sql
-- Inserts a defect record
-- NOTE: FRR_Formation_ID is HARDCODED to 68 in the controller code!
INSERT INTO Formation_Rework_Rejection
  (FRR_Formation_ID, FRR_Type_ID, FRR_Type_Name, FRR_Reason,
   FRR_Employee_ID, FRR_Employee_Name, FRR_Created_By, FRR_Created_Date)
VALUES (@FRR_Formation_ID, @FRR_Type_ID, @FRR_Type_Name, @FRR_Reason,
   @FRR_Employee_ID, @FRR_Employee_Name, @FRR_Created_By, GETDATE())
```

### 3.2 pr_fetch_Rework_Rejection_Types
```sql
-- Returns defect type categories from Reference_Group
SELECT RG_iID, RG_vDescription
FROM Reference_Group
WHERE RG_iID IN (12, 13)  -- 12=Rework, 13=Rejection
```

### 3.3 pr_fetch_Rework_Rejection_Reasons
```sql
-- Returns reason codes filtered by type
-- Parameter: @Type_ID int (12 or 13)
SELECT RGV_iID, RGV_vDesciption AS Reason
FROM ReferenceGroup_Value
WHERE RGV_iRG_ID = @Type_ID AND RGV_cStatus = 'A'
```

### 3.4 pr_fetch_Active_Employees
```sql
SELECT EM_iId, EM_vEmployee_Name FROM Employee_Master WHERE EM_cStatus = 'A'
```

### 3.5 pr_fetch_Formation_Rework_Rejection_List
```sql
-- Returns today's defect logs
SELECT * FROM Formation_Rework_Rejection
WHERE CONVERT(varchar, FRR_Created_Date, 112) = CONVERT(varchar, GETDATE(), 112)
ORDER BY FRR_Id DESC
```

## 4. OLD SOURCE CODE REFERENCES
- **Controller:** [TransactionController.cs L132-164](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs#L132-L164) — `pr_insert_Formation_Rework_Rejection`
- **Controller:** [TransactionController.cs L60-88](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs#L60-L88) — Rework types & reasons
- **View:** [Views/Transaction/Rework_Rejection.cshtml](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Transaction/Rework_Rejection.cshtml)

## 5. MIGRATION ENDPOINTS (.NET 8 Web API)
| HTTP Method | Endpoint | Purpose | SP Used |
|-------------|----------|---------|---------|
| GET | `/api/formation/defects/types` | Get defect type categories | `pr_fetch_Rework_Rejection_Types` |
| GET | `/api/formation/defects/reasons/{typeId}` | Get reasons by type (12/13) | `pr_fetch_Rework_Rejection_Reasons` |
| GET | `/api/employees/active` | Get active employee list | `pr_fetch_Active_Employees` |
| POST | `/api/formation/defects` | Log a new defect | `pr_insert_Formation_Rework_Rejection` |
| GET | `/api/formation/defects` | Get today's defect list | `pr_fetch_Formation_Rework_Rejection_List` |

## 6. REACT COMPONENTS
- `src/pages/Formation/ReworkRejectionPage.jsx` — Type toggle + reason filter + operator select + defect grid
