---
name: mvrla-migration
description: Complete reference for migrating the Amara Raja MVRLA Formation project from ASP.NET MVC to React+Vite frontend and .NET 8 Web API backend. Contains database schema, stored procedure mappings, API endpoint specs, and React component plans.
---

# MVRLA Formation Migration Skill

## Purpose
This skill provides all the context needed to build the new React + Vite frontend and .NET 8 Clean Architecture Web API backend for the Amara Raja MVRLA Formation Traceability system.

## Key Facts
- **Database:** `Amaron2_Web_Assembly` on `LAPTOP-7KC3K6QQ\SA` (SQL Server, Windows Auth)
- **Existing SPs must be REUSED** — Do NOT create new stored procedures
- **13 Active DB Tables** power all 7 modules
- **25 REST API endpoints** replace 378 legacy MVC controller actions
- **Zero physical foreign keys** — all referential integrity is in SP/App layer

## Active Modules (7 Total)
1. **Auth** — Login + Menu navigation (JWT tokens)
2. **Dashboard** — KPI cards + production charts
3. **MVRLA Formation** — Core battery formation data entry with QR scanning
4. **Rework & Rejection** — Defect logging (Type 12=Rework, 13=Rejection)
5. **Reports** — Filling, Summary, and Defect reports with export
6. **Masters** — Product catalogue + Reference value management
7. **Settings** — User management + Role-based menu permissions

## Core Tables
| Table | Rows | Primary Use |
|-------|------|-------------|
| MVRLA | 14,898 | Formation process log |
| Formation_Rework_Rejection | 13 | Defect/scrap tracking |
| User_Master1 | 64 | User credentials (NOT User_Master!) |
| Product_Master_Assembly | 1,276 | Battery product catalogue |
| Employee_Master | 21 | Factory personnel |
| Reference_Group | 13 | Dropdown category definitions |
| ReferenceGroup_Value | 27 | Dropdown option values |
| T_Setting_Menu_Master | 87 | Menu hierarchy |
| T_Setting_Role_Assignment | 134 | Role permissions |

## Technology Stack (New)
- **Frontend:** React 18+ (Vite), JavaScript/JSX, Modular Vanilla CSS, Axios, React Query
- **Backend:** .NET 8 Web API, Clean Architecture (API/Application/Domain/Infrastructure), Dapper, Microsoft.Data.SqlClient
- **Auth:** JWT Bearer Tokens
- **PDF Export:** QuestPDF (replaces Crystal Reports)
- **Charts:** Chart.js or Recharts

## Key Migration Rules
1. Reuse ALL existing stored procedures — connect via Dapper `CommandType.StoredProcedure`
2. Consolidate 10 combo AJAX calls into single `GET /api/formation/combos` endpoint
3. Replace `sessionStorage` with `AuthContext` + JWT token in Axios interceptor
4. Replace `jqxGrid` with custom React `DataTable` component
5. Replace `jqxComboBox` with custom React `SearchableSelect` component
6. Replace Crystal Reports with QuestPDF streaming endpoints
7. Replace `alert()` popups with toast notifications
8. Add FluentValidation for all API request DTOs

## Reference Documents
- [Full Analysis](file:///d:/office/MVRLA_Formation/MVRLA_Formation/COMPLETE_ANALYSIS_AND_MIGRATION_MASTER_SPECIFICATION.md)
- [Module 1: Login & Auth](file:///d:/office/MVRLA_Formation/MVRLA_Formation/MODULE_ANALYSIS/01_LOGIN_AND_AUTH_MODULE.md)
- [Module 2: Dashboard](file:///d:/office/MVRLA_Formation/MVRLA_Formation/MODULE_ANALYSIS/02_DASHBOARD_MODULE.md)
- [Module 3: MVRLA Formation](file:///d:/office/MVRLA_Formation/MVRLA_Formation/MODULE_ANALYSIS/03_MVRLA_FORMATION_ENTRY_MODULE.md)
- [Module 4: Rework & Rejection](file:///d:/office/MVRLA_Formation/MVRLA_Formation/MODULE_ANALYSIS/04_REWORK_REJECTION_MODULE.md)
- [Module 5: Reports](file:///d:/office/MVRLA_Formation/MVRLA_Formation/MODULE_ANALYSIS/05_REPORTS_MODULE.md)
- [Module 6: Masters](file:///d:/office/MVRLA_Formation/MVRLA_Formation/MODULE_ANALYSIS/06_MASTER_DATA_MODULE.md)
- [Module 7: Settings](file:///d:/office/MVRLA_Formation/MVRLA_Formation/MODULE_ANALYSIS/07_SETTINGS_MODULE.md)
