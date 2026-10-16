# AMARA RAJA BATTERIES — MVRLA FORMATION PROJECT WORKFLOW

## COMPLETE APPLICATION FLOW DIAGRAM

```
┌─────────────────────────────────────────────────────────────────┐
│                    USER OPENS BROWSER                            │
│                  http://localhost/SBLT2/                         │
└──────────────────────┬──────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────────────────────────┐
│  MODULE 1: LOGIN (Views/Home/Index.cshtml)                       │
│  ├── Enter Username + Password                                   │
│  ├── AJAX → pr_getlogin1 → User_Master1 table                  │
│  ├── Returns: Name~RoleID~AreaID~UserID~ID                      │
│  ├── Stores in sessionStorage                                    │
│  └── Calls pr_getMenu_listNew1(RoleID=21) → Menu tree           │
└──────────────────────┬──────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────────────────────────┐
│  MODULE 2: DASHBOARD (Views/Home/Dashboard.cshtml)               │
│  ├── KPI Card 1: Today's Passed Qty → pr_fetch_passedqty_chart  │
│  ├── KPI Card 2: Today's Failed Qty → pr_fetch_failedqty_chart  │
│  ├── KPI Card 3: Today's Rework Count                           │
│  ├── KPI Card 4: Today's Rejection Count                        │
│  ├── Chart 1: Battery Type Pass/Fail Comparison                 │
│  └── Chart 2: Defect Reason Distribution (Rework vs Rejection)  │
└──────────────────────┬──────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────────────────────────┐
│  MODULE 3: MVRLA FORMATION ENTRY (Views/Transaction/MVRLA)       │
│  ├── 10 Dropdown Combos loaded from ReferenceGroup_Value         │
│  ├── QR Code Barcode Scanner Input (10-char validation)          │
│  ├── Form: BatteryType, Line, Bank, Date, Cells, AcidSPG...    │
│  ├── INSERT → pr_insert_MVRLA → MVRLA table                    │
│  ├── VIEW → Pr_Fetch_MVRLA → Today's grid                      │
│  ├── DELETE → Pr_Delete_MVRLA → Soft delete (status='D')       │
│  └── PDF → Pr_Fetch_MVRLA_PDF → Crystal Reports                │
└──────────────────────┬──────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────────────────────────┐
│  MODULE 4: REWORK & REJECTION (Views/Transaction/Rework_Rejection)│
│  ├── Type Selection: Rework (ID=12) or Rejection (ID=13)         │
│  ├── Dynamic Reason Codes from ReferenceGroup_Value              │
│  ├── Operator Selection from Employee_Master                     │
│  ├── INSERT → pr_insert_Formation_Rework_Rejection               │
│  └── VIEW → pr_fetch_Formation_Rework_Rejection_List             │
└──────────────────────┬──────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────────────────────────┐
│  MODULE 5: REPORTS                                               │
│  ├── Filling Report: Date range + Shift → Pr_Fetch_FillingReport │
│  ├── Summary Report: Date range → Pr_Fetch_SummaryReport         │
│  └── Defect Report: Type + Date → pr_fetch_Rework_Rejection_Report│
└──────────────────────┬──────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────────────────────────┐
│  MODULE 6: MASTERS                                               │
│  ├── Product Master: Product_Master_Assembly (read-only)         │
│  └── Reference Master: Reference_Group + ReferenceGroup_Value    │
└──────────────────────┬──────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────────────────────────┐
│  MODULE 7: SETTINGS                                              │
│  ├── User Master: CRUD on User_Master1                           │
│  └── Role Assignment: T_Setting_Menu_Master × T_Setting_Role_Assignment │
└──────────────────────────────────────────────────────────────────┘
```

## ACTIVE DATABASE TABLES SUMMARY (Only tables used by this project)

| # | Table Name | Rows | Module(s) Used In | Purpose |
|---|-----------|------|-------------------|---------|
| 1 | `MVRLA` | 14,898 | Formation, Dashboard, Reports | Core formation process log |
| 2 | `Formation_Rework_Rejection` | 13 | Rework, Dashboard, Reports | Defect/scrap log |
| 3 | `User_Master1` | 64 | Login, Settings | User credentials |
| 4 | `Product_Master_Assembly` | 1,276 | Masters | Battery product catalogue |
| 5 | `Employee_Master` | 21 | Rework | Factory floor operators |
| 6 | `Reference_Group` | 13 | Rework, Masters | Dropdown category definitions |
| 7 | `ReferenceGroup_Value` | 27 | Formation Combos, Rework, Masters | Dropdown option values |
| 8 | `Refernce_Group_Value` | 6 | Login (role type lookup) | User role name mapping |
| 9 | `T_Setting_Menu_Master` | 87 | Login, Settings | Menu hierarchy |
| 10 | `T_Setting_Role_Assignment` | 134 | Login, Settings | Role-menu permissions |
| 11 | `User_Points` | - | Login | Gamification points |
| 12 | `T_Setting_UserMaster` | - | Settings | Extended user settings |
| 13 | `T_Setting_Role_Master` | - | Settings | Role definitions |

## COMPLETE .NET 8 WEB API ENDPOINT CATALOG

| # | Method | Endpoint | Module | SP/Source |
|---|--------|----------|--------|-----------|
| 1 | POST | `/api/auth/login` | Auth | `pr_getlogin1` |
| 2 | GET | `/api/auth/navigation` | Auth | `pr_getMenu_listNew1` |
| 3 | GET | `/api/dashboard/kpis` | Dashboard | Multiple chart SPs |
| 4 | GET | `/api/dashboard/defect-charts` | Dashboard | Rework/Rejection chart SPs |
| 5 | GET | `/api/formation/combos` | Formation | 10x `pr_fetch_*Combos` |
| 6 | POST | `/api/formation/mvrla` | Formation | `pr_insert_MVRLA` |
| 7 | GET | `/api/formation/mvrla/today` | Formation | `Pr_Fetch_MVRLA` |
| 8 | DELETE | `/api/formation/mvrla/{id}` | Formation | `Pr_Delete_MVRLA` |
| 9 | GET | `/api/formation/mvrla/check-qr/{code}` | Formation | `pr_Fetch_QRCode_Available` |
| 10 | GET | `/api/formation/mvrla/export-pdf` | Formation | `Pr_Fetch_MVRLA_PDF` |
| 11 | GET | `/api/formation/defects/types` | Defects | `pr_fetch_Rework_Rejection_Types` |
| 12 | GET | `/api/formation/defects/reasons/{typeId}` | Defects | `pr_fetch_Rework_Rejection_Reasons` |
| 13 | GET | `/api/employees/active` | Defects | `pr_fetch_Active_Employees` |
| 14 | POST | `/api/formation/defects` | Defects | `pr_insert_Formation_Rework_Rejection` |
| 15 | GET | `/api/formation/defects` | Defects | `pr_fetch_Formation_Rework_Rejection_List` |
| 16 | GET | `/api/reports/filling` | Reports | `Pr_Fetch_FillingReport` |
| 17 | GET | `/api/reports/summary` | Reports | `Pr_Fetch_SummaryReport` |
| 18 | GET | `/api/reports/defects` | Reports | `pr_fetch_Rework_Rejection_Report` |
| 19 | GET | `/api/products/assembly` | Masters | Direct SQL |
| 20 | GET | `/api/references` | Masters | `pr_get_Reference_Values` |
| 21 | POST | `/api/references` | Masters | `pr_insert_Reference_Values` |
| 22 | GET | `/api/settings/users` | Settings | `Pr_Fetch_T_Setting_UserMaster_Details` |
| 23 | POST | `/api/settings/users` | Settings | `pr_insert_User_Master1` |
| 24 | GET | `/api/settings/roles/{roleId}/permissions` | Settings | `pr_get_T_Setting_Menu_Master` |
| 25 | POST | `/api/settings/roles/{roleId}/permissions` | Settings | `pr_insert_T_Setting_Role_Assignment` |
