# REACT + VITE FRONTEND PAGE MIGRATION PLAN
*Modern SPA Architecture for Amara Raja MVRLA Formation*

> **Framework:** React 18+ (Vite)  
> **Language:** JavaScript (ESNext / JSX)  
> **Styling:** Modular Vanilla CSS (Modern CSS3 Tokens & Glassmorphism/Dark Theme)  
> **State Management:** React Context (AuthContext) + TanStack React Query (Server Cache)  
> **Icons:** Lucide React / FontAwesome 6  
> **Charts:** Chart.js / Recharts  

---

## 1. Frontend Directory Structure

```text
frontend/
 ├── index.html
 ├── vite.config.js
 ├── package.json
 └── src/
      ├── main.jsx                     # Application bootstrap
      ├── App.jsx                      # App root with Router & Auth Provider
      ├── index.css                    # Design tokens, variables, typography, reset
      │
      ├── assets/                      # Brand assets, Amara Raja logos
      │
      ├── api/                         # Centralized Axios API client & interceptors
      │    ├── apiClient.js            # Base Axios instance with Bearer auth header
      │    ├── authApi.js              # Login & navigation endpoints
      │    ├── dashboardApi.js         # KPI & defect charts endpoints
      │    ├── formationApi.js         # MVRLA & defect logging endpoints
      │    ├── reportApi.js            # Filling, summary, defect reports endpoints
      │    └── masterApi.js            # Product, reference, employee master endpoints
      │
      ├── context/                     # Application state
      │    ├── AuthContext.jsx         # User credentials, JWT token, role permissions
      │    └── ToastContext.jsx        # Global notification toast messages
      │
      ├── components/                  # Reusable UI component library (Vanilla CSS)
      │    ├── Layout/
      │    │    ├── AppLayout.jsx      # Main application shell (Header + Sidebar + Body)
      │    │    ├── Header.jsx         # User avatar, plant indicator, logout action
      │    │    └── Sidebar.jsx        # Dynamic role-based navigation sidebar
      │    ├── Common/
      │    │    ├── DataTable.jsx      # Sortable, paginated, filterable data grid
      │    │    ├── Modal.jsx          # Accessible popups/dialogs
      │    │    ├── StatCard.jsx       # KPI card with trends and status badges
      │    │    ├── SearchableSelect.jsx# Dropdown combo replacement for jqWidgets
      │    │    ├── DateRangePicker.jsx# Clean date filter component
      │    │    ├── Button.jsx         # Primary, secondary, danger, icon buttons
      │    │    └── LoadingSpinner.jsx # Smooth async operation indicator
      │    └── Charts/
      │         ├── BarChart.jsx       # Pass/Fail production comparison
      │         └── DonutChart.jsx     # Rework/Rejection defect reason breakdown
      │
      ├── pages/                       # Application Views (Replacements for .cshtml)
      │    ├── Auth/
      │    │    └── LoginPage.jsx      # Replacement for Views/Home/Index.cshtml
      │    ├── Dashboard/
      │    │    └── DashboardPage.jsx  # Replacement for Views/Home/Dashboard.cshtml
      │    ├── Formation/
      │    │    ├── MvrlaEntryPage.jsx # Replacement for Views/Transaction/MVRLA.cshtml
      │    │    └── ReworkRejectionPage.jsx # Replacement for Views/Transaction/Rework_Rejection.cshtml
      │    ├── Reports/
      │    │    ├── FillingReportPage.jsx   # Replacement for Views/Reports/Filling_Report.cshtml
      │    │    ├── SummaryReportPage.jsx   # Replacement for Views/Reports/Summary_Report.cshtml
      │    │    └── DefectReportPage.jsx    # Replacement for Views/Reports/Rework_Rejection.cshtml
      │    ├── Masters/
      │    │    ├── ProductMasterPage.jsx   # Replacement for Views/Master/Assembly_Product_Master.cshtml
      │    │    └── ReferenceMasterPage.jsx # Replacement for Views/Master/Reference_Master.cshtml
      │    └── Settings/
      │         ├── UserMasterPage.jsx      # Replacement for Views/Setting/User_Master.cshtml
      │         └── RoleAssignmentPage.jsx  # Replacement for Views/Setting/Role_Assignment.cshtml
      │
      └── utils/                       # Helpers & utilities
           ├── formatters.js           # Date formatters (dd/MM/yyyy), numbers
           └── validators.js           # Form validation rules
```

---

## 2. Core Page Transformation Specifications

### 2.1 Login Page (`src/pages/Auth/LoginPage.jsx`)
- **Replaces:** `Views/Home/Index.cshtml`
- **Key Enhancements:**
  - Modern card container with Amara Raja branding.
  - Controlled inputs for `username` and `password`.
  - Enter key submission.
  - Calls `authApi.login({ userName, password })`.
  - On 200 OK: stores JWT token in memory/secure storage, populates `AuthContext`, and navigates to `/dashboard`.
  - Replaces clumsy alert popups with smooth inline validation and toast alerts.

### 2.2 Dashboard Page (`src/pages/Dashboard/DashboardPage.jsx`)
- **Replaces:** `Views/Home/Dashboard.cshtml`
- **Key Enhancements:**
  - Top KPI Stat Cards: Today's Passed Qty, Today's Failed Qty, Today's Rework Units, Today's Rejection Units.
  - Chart 1: Battery Type Production Pass vs Fail (replaces CanvasJS/jqxChart).
  - Chart 2: Defect Reason Pareto Distribution (Rework vs Rejection reasons).
  - Auto-refresh interval (every 60 seconds) using React Query.

### 2.3 MVRLA Formation Entry (`src/pages/Formation/MvrlaEntryPage.jsx`)
- **Replaces:** `Views/Transaction/MVRLA.cshtml`
- **Key Enhancements:**
  - Unified responsive 2-column or 3-column grid form.
  - Auto-loads all 10 dropdown lookups in parallel via `useFormationCombos()` hook.
  - Real-time calculations: Total Units = Passed + Failed; Cell Short % alert.
  - Interactive data table showing today's logged entries with delete action and PDF print.
  - Barcode / QR scanner focus-trap field for fast physical scanning.

### 2.4 Rework & Rejection Page (`src/pages/Formation/ReworkRejectionPage.jsx`)
- **Replaces:** `Views/Transaction/Rework_Rejection.cshtml`
- **Key Enhancements:**
  - Type toggle: **Rework** (Type ID 12) vs **Rejection** (Type ID 13).
  - Dynamic reason filtering: selecting Rework auto-filters reasons to Terminal, Charging, or Label problems.
  - Active operator dropdown populated from `Employee_Master`.
  - Live log of defects logged today with status badges.

### 2.5 Reports Suite (`src/pages/Reports/*`)
- **Replaces:** `Views/Reports/Filling_Report.cshtml`, `Summary_Report.cshtml`, `Rework_Rejection.cshtml`
- **Key Enhancements:**
  - Flexible date range picker (From Date / To Date) with presets (Today, Yesterday, Last 7 Days, Month-to-date).
  - Shift filter (All, Shift A, Shift B, Shift C), Tub filter, Bank filter.
  - Client-side and server-side export: **Export to Excel (XLSX)** and **Export to PDF**.
  - Sortable column headers and quick global text search.
