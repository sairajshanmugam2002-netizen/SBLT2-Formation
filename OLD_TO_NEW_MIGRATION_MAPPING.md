# OLD TO NEW SYSTEM MIGRATION MAPPING SPECIFICATION
*Amara Raja Batteries – MVRLA Formation Traceability System*

> **Document Version:** 1.0.0  
> **Target Architecture:** .NET 8 Web API (Clean Architecture + Dapper) + React (Vite + Vanilla CSS)  
> **Database:** Existing SQL Server Database `Amaron2_Web_Assembly` (Preserved & Reused)  
> **Source Project:** ASP.NET MVC 4.8 / ADO.NET (`SBLT2`)  

---

## 1. Target Architecture Overview

The legacy ASP.NET MVC architecture (`SBLT2`) will be replaced with a decoupled, high-performance, maintainable Clean Architecture system.

```
[ React + Vite Client (Port 5173) ]
           │
           │ HTTPS / REST (JSON) + JWT Bearer Token
           ▼
[ ASP.NET Core 8 Web API Controllers ]  ──► AmaraRaja.API
           │
           ▼
[ Application Services & CQRS Handlers ] ──► AmaraRaja.Application (DTOs, FluentValidation)
           │
           ▼
[ Domain Entities & Interfaces ]        ──► AmaraRaja.Domain
           │
           ▼
[ Dapper Repositories & SqlClient ]     ──► AmaraRaja.Infrastructure
           │
           ▼
[ Microsoft SQL Server ]                ──► Database: Amaron2_Web_Assembly (Existing SPs & Tables)
```

---

## 2. Technology Replacement Matrix

| Layer / Concern | Old Implementation (SBLT2) | New Target Implementation | Justification / Improvement |
| :--- | :--- | :--- | :--- |
| **Frontend Framework** | Razor `.cshtml` + jQuery 2.1.1 | React 18+ (Vite SPA) | Component-driven UI, fast HMR, reactive state management |
| **Styling / CSS** | Materialize CSS + AdminLTE 2.0 / Bootstrap 3 | Modular Vanilla CSS | Complete design control, zero framework bloat, responsive layout |
| **Grid / Table UI** | jqxGrid (JQWidgets) | Custom React Data Table | Native React virtualized rendering, no proprietary licensing |
| **Dropdowns / Combos** | jqxDropDownList / HTML select | Custom React Select / Searchable Dropdowns | Fast keyboard navigation, accessible, smooth UX |
| **Client State / Storage** | HTML5 `sessionStorage` (`Name~rights~Area`) | React AuthContext + Secure Token Storage | Secure, observable state, no raw string splitting |
| **HTTP Client** | `$.ajax` synchronous / callbacks | Axios / Fetch with request/response interceptors | Promise-based, automatic JWT injection, unified error toast handling |
| **Backend Framework** | ASP.NET MVC 4.0 / 4.8 (.NET Framework) | ASP.NET Core 8 Web API (.NET 8 LTS) | Cross-platform, high throughput, dependency injection native |
| **Architecture** | Monolithic fat controllers + ASPX generic handler | Clean Architecture (API, Application, Domain, Infrastructure) | Strict separation of concerns, testability, enterprise maintainability |
| **Data Access Layer** | Raw ADO.NET (`SqlConnection`, `DataTable`) | Dapper ORM + `Microsoft.Data.SqlClient` | Micro-ORM speed, typed parameter mapping, zero DataTable overhead |
| **Database Access Helper**| `SriPravanTech.dbFunctions` (static helper) | Generic `IDbConnectionFactory` + Repository Pattern | Thread-safe connection pooling, transaction scope management |
| **Validation** | Client-side jQuery alert popups | FluentValidation (Backend) + React Hook Form (Frontend) | Consistent, declarative validation on both client and API |
| **Authentication** | Custom `pr_getlogin1` + session string | JWT (JSON Web Tokens) with ASP.NET Core Identity / Auth | Stateless, cryptographic, secure token expiration, standard RBAC |
| **Report Generation** | Crystal Reports (`.rpt`) via Win32 runtime | QuestPDF / PDFSharp API endpoints + React Print | Native 64-bit cross-platform PDF generation, no Crystal runtime |
| **Barcode / QR** | QRCoder + GenCode128 in Controller | QRCoder in .NET 8 / Client-side SVG QR Generator | Vector-crisp QR code rendering, thermal printer direct ESC/POS |

---

## 3. End-to-End Migration Traceability Matrix

| Module | Old Screen (.cshtml) | Old Controller Action | Stored Procedure / SQL | Target Database Table | New React Component | New Web API Endpoint | Architecture Layer Flow |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Auth** | `Home/Index.cshtml` | `HomeController.pr_getlogin1` | `pr_getlogin1` | `User_Master1`, `Refernce_Group_Value` | `LoginPage.jsx` | `POST /api/auth/login` | `AuthController` ➔ `AuthService` ➔ `AuthRepository` |
| **Auth** | `Home/Index.cshtml` | `HomeController.pr_getMenu_listNew1` | `pr_getMenu_listNew1` | `T_Setting_Menu_Master`, `T_Setting_Role_Assignment` | `SidebarNav.jsx` | `GET /api/auth/navigation` | `AuthController` ➔ `MenuService` ➔ `MenuRepository` |
| **Dashboard** | `Home/Dashboard.cshtml` | `pr_fetch_passedqty_chart`, `pr_fetch_failedqty_chart` | `pr_fetch_passedqty_chart`, `pr_fetch_failedqty_chart` | `MVRLA` | `DashboardOverview.jsx` | `GET /api/dashboard/kpis` | `DashboardController` ➔ `DashboardService` ➔ `MvrlaRepository` |
| **Dashboard** | `Home/Dashboard.cshtml` | `pr_fetch_Today_Rework_Chart`, `pr_fetch_Today_Rejection_Chart` | `pr_fetch_Today_Rework_Chart`, `pr_fetch_Today_Rejection_Chart` | `Formation_Rework_Rejection` | `DefectChart.jsx` | `GET /api/dashboard/defect-charts` | `DashboardController` ➔ `DashboardService` ➔ `ReworkRepository` |
| **Dashboard** | `Home/Dashboard.cshtml` | `pr_fetch_Today_Rework_Count`, `pr_fetch_Today_Rejection_Count` | `pr_fetch_Today_Rework_Rejection_Counts` | `Formation_Rework_Rejection` | `StatCards.jsx` | `GET /api/dashboard/defect-counts` | `DashboardController` ➔ `DashboardService` ➔ `ReworkRepository` |
| **Formation** | `Transaction/MVRLA.cshtml` | `TransactionController.pr_insert_MVRLA` | `pr_insert_MVRLA` | `MVRLA` | `MvrlaEntryPage.jsx` | `POST /api/formation/mvrla` | `MvrlaController` ➔ `MvrlaService` ➔ `MvrlaRepository` |
| **Formation** | `Transaction/MVRLA.cshtml` | `TransactionController.Pr_Fetch_MVRLA` | `Pr_Fetch_MVRLA` | `MVRLA` | `MvrlaTable.jsx` | `GET /api/formation/mvrla/today` | `MvrlaController` ➔ `MvrlaService` ➔ `MvrlaRepository` |
| **Formation** | `Transaction/MVRLA.cshtml` | `TransactionController.Pr_Delete_MVRLA` | `Pr_Delete_MVRLA` | `MVRLA` | `MvrlaTable.jsx` | `DELETE /api/formation/mvrla/{id}` | `MvrlaController` ➔ `MvrlaService` ➔ `MvrlaRepository` |
| **Formation** | `Transaction/MVRLA.cshtml` | `TransactionController.Pr_Fetch_MVRLA_PDF` | `Pr_Fetch_MVRLA_PDF` | `MVRLA` | `MvrlaPrintModal.jsx` | `GET /api/formation/mvrla/pdf` | `MvrlaController` ➔ `PdfExportService` ➔ `MvrlaRepository` |
| **Formation** | `Transaction/MVRLA.cshtml` | `TransactionController.pr_fetch_*Combos` | 10x `pr_fetch_*Combos` | `ReferenceGroup_Value` | `MvrlaCombos.jsx` | `GET /api/formation/combos` | `FormationController` ➔ `ReferenceService` ➔ `RefRepository` |
| **Defects** | `Transaction/Rework_Rejection.cshtml` | `TransactionController.pr_insert_Formation_Rework_Rejection` | `pr_insert_Formation_Rework_Rejection` | `Formation_Rework_Rejection` | `ReworkRejectionPage.jsx` | `POST /api/formation/defects` | `DefectController` ➔ `DefectService` ➔ `DefectRepository` |
| **Defects** | `Transaction/Rework_Rejection.cshtml` | `TransactionController.pr_fetch_Formation_Rework_Rejection_List` | `pr_fetch_Formation_Rework_Rejection_List` | `Formation_Rework_Rejection` | `DefectList.jsx` | `GET /api/formation/defects` | `DefectController` ➔ `DefectService` ➔ `DefectRepository` |
| **Defects** | `Transaction/Rework_Rejection.cshtml` | `TransactionController.pr_fetch_Rework_Rejection_Reasons` | `pr_fetch_Rework_Rejection_Reasons` | `ReferenceGroup_Value`, `Reference_Group` | `DefectForm.jsx` | `GET /api/formation/defect-reasons/{typeId}` | `DefectController` ➔ `ReferenceService` ➔ `RefRepository` |
| **Defects** | `Transaction/Rework_Rejection.cshtml` | `TransactionController.pr_fetch_Active_Employees` | `pr_fetch_Active_Employees` | `Employee_Master` | `DefectForm.jsx` | `GET /api/employees/active` | `EmployeeController` ➔ `EmployeeService` ➔ `EmployeeRepository` |
| **Reports** | `Reports/Filling_Report.cshtml` | `ReportsController.Pr_Fetch_FillingReport` | `Pr_Fetch_FillingReport`, `Pr_Fetch_FillingReport2` | `MVRLA` | `FillingReportPage.jsx` | `GET /api/reports/filling` | `ReportController` ➔ `ReportService` ➔ `MvrlaRepository` |
| **Reports** | `Reports/Summary_Report.cshtml` | `ReportsController.Pr_Fetch_SummaryReport` | `Pr_Fetch_SummaryReport` | `MVRLA` | `SummaryReportPage.jsx` | `GET /api/reports/summary` | `ReportController` ➔ `ReportService` ➔ `MvrlaRepository` |
| **Reports** | `Reports/Rework_Rejection.cshtml` | `ReportsController.pr_fetch_Rework_Rejection_Report` | `pr_fetch_Rework_Rejection_Report` | `Formation_Rework_Rejection` | `DefectReportPage.jsx` | `GET /api/reports/defects` | `ReportController` ➔ `ReportService` ➔ `DefectRepository` |
| **Masters** | `Master/Assembly_Product_Master.cshtml` | `MasterController.Pr_Fetch_Product_MasterAssy` | Direct SQL query on `Product_Master_Assembly` | `Product_Master_Assembly` | `ProductMasterPage.jsx` | `GET /api/products/assembly` | `ProductController` ➔ `ProductService` ➔ `ProductRepository` |
| **Masters** | `Master/Reference_Master.cshtml` | `MasterController.pr_get_Reference_Values` | `pr_get_Reference_Values` | `Reference_Group`, `ReferenceGroup_Value` | `ReferenceMasterPage.jsx` | `GET /api/references` | `ReferenceController` ➔ `ReferenceService` ➔ `RefRepository` |
| **Settings** | `Setting/Role_Assignment.cshtml` | `SettingController.pr_get_T_Setting_Menu_Master` | `pr_get_T_Setting_Menu_Master` | `T_Setting_Menu_Master`, `T_Setting_Role_Assignment` | `RoleAssignmentPage.jsx` | `GET /api/settings/roles` | `RoleController` ➔ `RoleService` ➔ `RoleRepository` |
| **Settings** | `Setting/User_Master.cshtml` | `SettingController.Pr_Fetch_T_Setting_UserMaster_Details` | `Pr_Fetch_T_Setting_UserMaster_Details` | `T_Setting_UserMaster` | `UserMasterPage.jsx` | `GET /api/settings/users` | `UserController` ➔ `UserService` ➔ `UserRepository` |

---

## 4. Retired Legacy Modules (Do Not Migrate)

The following modules exist as copied code in the old repository but their backend stored procedures **do not exist** in `Amaron2_Web_Assembly`:
1. **Warehouse Inward/Outward:** `APR_Inward`, `APR_Outward`, `CHE_Outward`, `CHN_Inward`
2. **Inventory Receipts & QA:** `Material_Receipt`, `Material_Issue`, `Quality_Inspection`, `Location_Update`
3. **Warehouse Reports:** `Summary_APR`, `Summary_CHN`, `Transit_report`, `FIFO_Display`, `Inventory_Report`, `Non_Moving_Report`, `QI_Pending_Report`
4. **Starter Boilerplate Stubs:** `Create_User_Game.cshtml`, `Dashboard_Bobbin.cshtml`, `ReportController.cs`
