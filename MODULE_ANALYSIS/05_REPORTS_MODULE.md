# MODULE 5: PRODUCTION REPORTS

## 1. WORKFLOW (Old System)
```
User navigates to Reports menu → selects one of 3 active reports:
   ↓
1. Filling Report: From Date → To Date → Shift filter → Pr_Fetch_FillingReport
2. Summary Report: From Date → To Date → Pr_Fetch_SummaryReport
3. Rework/Rejection Report: Type filter → From/To dates → pr_fetch_Rework_Rejection_Report
   ↓
Data loaded into jqxGrid with sorting, filtering, pagination
   ↓
Export options: Excel export via MasterController.Export_To_Excel
```

## 2. DATABASE TABLES USED

### For Filling & Summary Reports:
- **MVRLA** (14,898 rows) — All formation process records

### For Defect Reports:
- **Formation_Rework_Rejection** (13 rows) — Defect log entries

## 3. STORED PROCEDURE LOGIC

### 3.1 Pr_Fetch_FillingReport
```sql
-- Detailed filling log filtered by date range and shift
-- Parameters: @from date, @to date, @shift varchar
SELECT mv_BatteryType, mv_FillingLine, mv_BankNo, mv_Date, mv_Shift,
  mv_NoOfCells, mv_FillingStartTime, mv_FillingEndTime,
  mv_AcidSPG, mv_AcidVolume, mv_TubNo, mv_CKTNo,
  mv_CellShortQty, mv_Temperature, mv_PRDSupervisior,
  mv_FillingOperator, mv_QAInspector, mv_ShiftEngineer
FROM MVRLA
WHERE mv_Status = 'A'
  AND CONVERT(varchar, mv_Date, 112) BETWEEN @from AND @to
  AND (@shift = '' OR mv_Shift = @shift)
ORDER BY mv_Date DESC
```

### 3.2 Pr_Fetch_FillingReport2
```sql
-- Alternative filling report filtered by tub and bank
-- Parameters: @from, @to, @tub, @bank
SELECT * FROM MVRLA
WHERE mv_Status = 'A'
  AND CONVERT(varchar, mv_Date, 112) BETWEEN @from AND @to
  AND (@tub = '' OR mv_TubNo = @tub)
  AND (@bank = '' OR mv_BankNo = @bank)
```

### 3.3 Pr_Fetch_SummaryReport
```sql
-- Aggregated formation summary by date range
-- Parameters: @from date, @to date
SELECT mv_BatteryType, mv_Shift,
  COUNT(*) AS TotalBatches,
  SUM(CAST(mv_CDPassedQty AS int)) AS TotalPassed,
  SUM(CAST(mv_CDFailQty AS int)) AS TotalFailed,
  AVG(CAST(mv_Temperature AS float)) AS AvgTemp
FROM MVRLA
WHERE mv_Status = 'A'
  AND CONVERT(varchar, mv_Date, 112) BETWEEN @from AND @to
GROUP BY mv_BatteryType, mv_Shift
```

### 3.4 pr_fetch_Rework_Rejection_Report
```sql
-- Defect audit report with date and type filters
-- Parameters: @Type ('All'/'Rework'/'Rejection'), @from date, @to date
SELECT FRR_Type_Name, FRR_Reason, FRR_Employee_Name,
  FRR_Created_By, FRR_Created_Date
FROM Formation_Rework_Rejection
WHERE (@Type = 'All' OR FRR_Type_Name = @Type)
  AND CONVERT(varchar, FRR_Created_Date, 112) BETWEEN @from AND @to
ORDER BY FRR_Created_Date DESC
```

## 4. OLD SOURCE CODE REFERENCES
- **Controller:** [ReportsController.cs L83-102](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/ReportsController.cs#L83-L102)
- **Controller:** [ReportsController.cs L65-74](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/ReportsController.cs#L65-L74)
- **Views:** [Views/Reports/Filling_Report.cshtml](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Reports/Filling_Report.cshtml), [Summary_Report.cshtml](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Reports/Summary_Report.cshtml), [Rework_Rejection.cshtml](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Reports/Rework_Rejection.cshtml)

## 5. MIGRATION ENDPOINTS (.NET 8 Web API)
| HTTP Method | Endpoint | Purpose | SP Used |
|-------------|----------|---------|---------|
| GET | `/api/reports/filling?from=&to=&shift=` | Detailed filling report | `Pr_Fetch_FillingReport` |
| GET | `/api/reports/filling2?from=&to=&tub=&bank=` | Filling report by tub/bank | `Pr_Fetch_FillingReport2` |
| GET | `/api/reports/summary?from=&to=` | Aggregated summary report | `Pr_Fetch_SummaryReport` |
| GET | `/api/reports/defects?type=&from=&to=` | Rework/Rejection audit report | `pr_fetch_Rework_Rejection_Report` |
| GET | `/api/reports/filling/export-excel` | Export filling data to XLSX | Custom Dapper query |
| GET | `/api/reports/filling/export-pdf` | Export filling data to PDF | QuestPDF generator |

## 6. REACT COMPONENTS
- `src/pages/Reports/FillingReportPage.jsx` — Date picker + shift filter + DataTable + export buttons
- `src/pages/Reports/SummaryReportPage.jsx` — Date picker + aggregated summary grid
- `src/pages/Reports/DefectReportPage.jsx` — Type filter + date range + defect audit grid
