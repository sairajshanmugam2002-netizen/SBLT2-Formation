# MODULE 2: DASHBOARD (KPI & CHARTS)

## 1. WORKFLOW (Old System)
```
After login → HomeController.Dashboard() → Views/Home/Dashboard.cshtml
   ↓
Page loads → fires 6 parallel AJAX calls:
   1. HomeController.pr_fetch_passedqty_chart()     → Battery type pass counts
   2. HomeController.pr_fetch_failedqty_chart()     → Battery type fail counts
   3. HomeController.pr_fetch_Today_Rework_Count()  → Today's rework count
   4. HomeController.pr_fetch_Today_Rejection_Count() → Today's rejection count
   5. HomeController.pr_fetch_Today_Rework_Chart()  → Rework reasons breakdown
   6. HomeController.pr_fetch_Today_Rejection_Chart() → Rejection reasons breakdown
   ↓
Data rendered using CanvasJS / jqxChart widgets
```

## 2. DATABASE TABLES USED

### 2.1 MVRLA (14,898 rows) — READ for aggregation
- Queries today's records grouped by `mv_BatteryType`
- Sums `mv_CDPassedQty` and `mv_CDFailQty` per battery type

### 2.2 Formation_Rework_Rejection (13 rows) — READ for defect counts
- Counts today's records filtered by `FRR_Type_Name` = 'Rework' or 'Rejection'
- Groups by `FRR_Reason` for chart breakdown

## 3. STORED PROCEDURE LOGIC

### 3.1 pr_fetch_passedqty_chart
```sql
-- Returns passed quantity aggregated by battery type for today
SELECT mv_BatteryType AS label, SUM(CAST(mv_CDPassedQty AS int)) AS y
FROM MVRLA
WHERE CONVERT(varchar, mv_Date, 112) = CONVERT(varchar, GETDATE(), 112)
  AND mv_Status = 'A'
GROUP BY mv_BatteryType
```

### 3.2 pr_fetch_failedqty_chart
```sql
-- Returns failed quantity aggregated by battery type for today
SELECT mv_BatteryType AS label, SUM(CAST(mv_CDFailQty AS int)) AS y
FROM MVRLA
WHERE CONVERT(varchar, mv_Date, 112) = CONVERT(varchar, GETDATE(), 112)
  AND mv_Status = 'A'
GROUP BY mv_BatteryType
```

### 3.3 pr_fetch_Today_Rework_Rejection_Counts
```sql
-- Returns count of rework/rejection entries for today
-- Parameters: @Type varchar ('Rework' or 'Rejection')
SELECT COUNT(*) AS Cnt
FROM Formation_Rework_Rejection
WHERE FRR_Type_Name = @Type
  AND CONVERT(varchar, FRR_Created_Date, 112) = CONVERT(varchar, GETDATE(), 112)
```

### 3.4 pr_fetch_Today_Rework_Chart
```sql
-- Returns defect reason breakdown for rework entries today
SELECT FRR_Reason AS label, COUNT(*) AS y
FROM Formation_Rework_Rejection
WHERE FRR_Type_Name = 'Rework'
  AND CONVERT(varchar, FRR_Created_Date, 112) = CONVERT(varchar, GETDATE(), 112)
GROUP BY FRR_Reason
```

### 3.5 pr_fetch_Today_Rejection_Chart
```sql
-- Same structure as above but for 'Rejection' type
```

## 4. OLD SOURCE CODE REFERENCES
- **Controller:** [HomeController.cs L73-107](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/HomeController.cs#L73-L107)
- **View:** [Views/Home/Dashboard.cshtml](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Home/Dashboard.cshtml)

## 5. MIGRATION ENDPOINTS (.NET 8 Web API)
| HTTP Method | Endpoint | Purpose | SPs Used |
|-------------|----------|---------|----------|
| GET | `/api/dashboard/kpis` | All KPI stats (pass/fail/rework/rejection) | `pr_fetch_passedqty_chart`, `pr_fetch_failedqty_chart`, `pr_fetch_Today_Rework_Rejection_Counts` |
| GET | `/api/dashboard/defect-charts` | Rework & Rejection reason breakdown | `pr_fetch_Today_Rework_Chart`, `pr_fetch_Today_Rejection_Chart` |

## 6. REACT COMPONENTS
- `src/pages/Dashboard/DashboardPage.jsx` — Main dashboard with KPI cards + charts
- `src/components/Charts/BarChart.jsx` — Pass/Fail battery type comparison
- `src/components/Charts/DonutChart.jsx` — Defect reason distribution
- `src/components/Common/StatCard.jsx` — KPI stat card with count & trend
