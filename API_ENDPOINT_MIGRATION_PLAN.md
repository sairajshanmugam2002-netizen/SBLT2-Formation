# .NET 8 WEB API ENDPOINT MIGRATION PLAN
*Clean Architecture Backend Specification for Amara Raja MVRLA Formation*

> **Target Platform:** ASP.NET Core 8 Web API (.NET 8 LTS)  
> **Data Access:** Dapper 2.1 + Microsoft.Data.SqlClient  
> **Database:** Amaron2_Web_Assembly (SQL Server)  

---

## 1. Authentication & Navigation Endpoints

### 1.1 POST /api/auth/login
- **Purpose:** Authenticates user credentials and returns JWT bearer token + permissions.
- **Old Implementation:** `HomeController.pr_getlogin1` ➔ SP `pr_getlogin1`
- **Database Tables:** `User_Master1`, `Refernce_Group_Value`, `User_Points`
- **Request DTO:**
```json
{
  "userName": "admin",
  "password": "sai@123"
}
```
- **Response DTO:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiration": "2026-10-15T20:00:00Z",
  "user": {
    "userId": 2,
    "userName": "admin",
    "roleId": 21,
    "roleName": "Formation",
    "areaId": 2,
    "points": 0
  }
}
```

### 1.2 GET /api/auth/navigation
- **Purpose:** Returns authorized menu hierarchy for current authenticated user.
- **Old Implementation:** `HomeController.pr_getMenu_listNew1` ➔ SP `pr_getMenu_listNew1`
- **Database Tables:** `T_Setting_Menu_Master`, `T_Setting_Role_Assignment`
- **Response DTO:**
```json
[
  {
    "menuId": 97,
    "menuName": "Dashboard",
    "url": "/dashboard",
    "icon": "fa-dashboard",
    "children": [
      { "menuId": 106, "menuName": "Landing Dashboard", "url": "/dashboard" }
    ]
  },
  {
    "menuId": 68,
    "menuName": "Formation",
    "url": "/formation",
    "icon": "fa-battery-full",
    "children": [
      { "menuId": 74, "menuName": "MVRLA", "url": "/formation/mvrla" },
      { "menuId": 1112, "menuName": "Rework and Rejection", "url": "/formation/rework-rejection" }
    ]
  }
]
```

---

## 2. Dashboard Endpoints

### 2.1 GET /api/dashboard/kpis
- **Purpose:** Fetches production pass/fail aggregates and defect counts for today.
- **Old Implementation:** `HomeController.pr_fetch_passedqty_chart`, `pr_fetch_failedqty_chart`, `pr_fetch_Today_Rework_Count`
- **Response DTO:**
```json
{
  "totalPassedQty": 450,
  "totalFailedQty": 12,
  "reworkTodayCount": 5,
  "rejectionTodayCount": 2,
  "batteryTypeBreakdown": [
    { "batteryType": "26ah", "passedQty": 250, "failedQty": 8 },
    { "batteryType": "45ah", "passedQty": 200, "failedQty": 4 }
  ]
}
```

### 2.2 GET /api/dashboard/defect-charts
- **Purpose:** Fetches reason breakdown for Rework and Rejection charts.
- **Old Implementation:** `HomeController.pr_fetch_Today_Rework_Chart`, `pr_fetch_Today_Rejection_Chart`
- **Response DTO:**
```json
{
  "reworkReasons": [
    { "reason": "Terminal Problem", "count": 3 },
    { "reason": "Label Problem", "count": 2 }
  ],
  "rejectionReasons": [
    { "reason": "Physical Damage", "count": 1 },
    { "reason": "Quality Failure", "count": 1 }
  ]
}
```

---

## 3. MVRLA Formation Endpoints

### 3.1 GET /api/formation/combos
- **Purpose:** Consolidates all 10 dropdown lookups for MVRLA entry in a single HTTP request.
- **Old Implementation:** 10 separate calls to `pr_fetch_*Combos`
- **Underlying SPs:** `pr_fetch_BankNoCombos`, `pr_fetch_BatteryTypeCombos`, `pr_fetch_ReadingOperatorCombos`, `pr_fetch_OperatorCombos`, `pr_fetch_LineCombos`, `pr_fetch_ShiftEngineerCombos`, `pr_fetch_InspectorCombos`, `pr_fetch_SupervisorCombos`, `pr_fetch_TubCombos`, `pr_fetch_CKTCombos`
- **Response DTO:**
```json
{
  "batteryTypes": [{ "id": 11, "name": "26ah" }, { "id": 12, "name": "45ah" }],
  "lines": [{ "id": 13, "name": "1" }, { "id": 4, "name": "2" }],
  "banks": [{ "id": 9, "name": "C" }],
  "tubs": [{ "id": 3, "name": "A19" }, { "id": 14, "name": "A15" }],
  "circuits": [{ "id": 10, "name": "15A-56C" }, { "id": 15, "name": "16A" }],
  "supervisors": [{ "id": 5, "name": "Mogili Reddy I" }],
  "operators": [{ "id": 6, "name": "Vihari B" }],
  "inspectors": [{ "id": 7, "name": "Hari Prasa" }],
  "shiftEngineers": [{ "id": 8, "name": "Mohan Babu" }],
  "readingOperators": []
}
```

### 3.2 POST /api/formation/mvrla
- **Purpose:** Inserts a new MVRLA battery formation process record.
- **Old Implementation:** `TransactionController.pr_insert_MVRLA` ➔ SP `pr_insert_MVRLA`
- **Target Table:** `MVRLA`
- **Request DTO:**
```json
{
  "batteryType": "26ah",
  "fillingLine": "1",
  "bankNo": "C",
  "date": "2026-10-15",
  "noOfCells": "6",
  "fillingStartTime": "2026-10-15 08:00:00",
  "fillingEndTime": "2026-10-15 16:00:00",
  "cdPassedQty": 50,
  "cdFailQty": 1,
  "acidSpg": "1.280",
  "acidVolume": "500",
  "tubNo": "A19",
  "cktNo": "15A-56C",
  "cellShortQty": 0,
  "temperature": "32",
  "prdSupervisor": "Mogili Reddy I",
  "fillingOperator": "Vihari B",
  "qaInspector": "Hari Prasa",
  "shiftEngineer": "Mohan Babu",
  "readingOperator": "Operator 1",
  "shift": "A",
  "qrCode": "QR180-503-00338",
  "remarks": "Batch ok"
}
```

### 3.3 GET /api/formation/mvrla/today
- **Purpose:** Retrieves today's active MVRLA formation records.
- **Old Implementation:** `TransactionController.Pr_Fetch_MVRLA` ➔ SP `Pr_Fetch_MVRLA`
- **Target Table:** `MVRLA`

### 3.4 DELETE /api/formation/mvrla/{id}
- **Purpose:** Soft-deletes an MVRLA formation record (`mv_Status = 'D'`).
- **Old Implementation:** `TransactionController.Pr_Delete_MVRLA` ➔ SP `Pr_Delete_MVRLA`

### 3.5 GET /api/formation/mvrla/export-pdf
- **Purpose:** Exports filtered MVRLA data to PDF format.
- **Old Implementation:** `TransactionController.Pr_Fetch_MVRLA_PDF` ➔ Crystal Reports runtime
- **New Implementation:** Dapper query via `Pr_Fetch_MVRLA_PDF` ➔ QuestPDF generator ➔ Streams `application/pdf`

---

## 4. Formation Rework & Rejection Endpoints

### 4.1 GET /api/formation/defects/types
- **Purpose:** Returns defect types (Rework ID 12, Rejection ID 13).
- **Old Implementation:** SP `pr_fetch_Rework_Rejection_Types` ➔ Table `Reference_Group`

### 4.2 GET /api/formation/defects/reasons/{typeId}
- **Purpose:** Returns reason codes filtered by defect type.
- **Old Implementation:** SP `pr_fetch_Rework_Rejection_Reasons` ➔ Table `ReferenceGroup_Value`

### 4.3 GET /api/employees/active
- **Purpose:** Returns active factory floor operators/inspectors for selection.
- **Old Implementation:** SP `pr_fetch_Active_Employees` ➔ Table `Employee_Master`

### 4.4 POST /api/formation/defects
- **Purpose:** Inserts a Rework or Rejection record.
- **Old Implementation:** SP `pr_insert_Formation_Rework_Rejection`
- **Target Table:** `Formation_Rework_Rejection`
- **Request DTO:**
```json
{
  "formationId": 68,
  "typeId": 12,
  "typeName": "Rework",
  "reason": "Terminal Problem",
  "employeeId": 2,
  "employeeName": "Hari. L"
}
```

### 4.5 GET /api/formation/defects
- **Purpose:** Fetches recent defect log entries.
- **Old Implementation:** SP `pr_fetch_Formation_Rework_Rejection_List`

---

## 5. Production Reports Endpoints

### 5.1 GET /api/reports/filling
- **Parameters:** `from` (string yyyyMMdd), `to` (string yyyyMMdd), `shift` (optional), `tub` (optional), `bank` (optional)
- **Old Implementation:** SP `Pr_Fetch_FillingReport`, `Pr_Fetch_FillingReport2`

### 5.2 GET /api/reports/summary
- **Parameters:** `from` (string yyyyMMdd), `to` (string yyyyMMdd)
- **Old Implementation:** SP `Pr_Fetch_SummaryReport`

### 5.3 GET /api/reports/defects
- **Parameters:** `type` ('All', 'Rework', 'Rejection'), `fromDate` (yyyy-MM-dd), `toDate` (yyyy-MM-dd)
- **Old Implementation:** SP `pr_fetch_Rework_Rejection_Report`
