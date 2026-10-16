# MODULE 3: MVRLA FORMATION ENTRY (Core Transaction)

## 1. WORKFLOW (Old System)
```
User navigates to Formation → MVRLA menu item
   ↓
TransactionController.MVRLA() → Views/Transaction/MVRLA.cshtml loads
   ↓
Page loads → fires 10 parallel AJAX calls to populate dropdown combos:
   1. pr_fetch_BatteryTypeCombos  → Battery type dropdown (26ah, 45ah, etc.)
   2. pr_fetch_LineCombos         → Filling line dropdown (1, 2, etc.)
   3. pr_fetch_BankNoCombos       → Bank number dropdown (C, etc.)
   4. pr_fetch_TubCombos          → Tub number dropdown (A19, A15, etc.)
   5. pr_fetch_CKTCombos          → Circuit number dropdown
   6. pr_fetch_SupervisorCombos   → PRD Supervisor dropdown
   7. pr_fetch_OperatorCombos     → Filling Operator dropdown
   8. pr_fetch_InspectorCombos    → QA Inspector dropdown
   9. pr_fetch_ShiftEngineerCombos → Shift Engineer dropdown
  10. pr_fetch_ReadingOperatorCombos → Reading Operator dropdown
   ↓
Also loads today's grid data → Pr_Fetch_MVRLA()
   ↓
User fills form fields:
   - Battery Type (combo)    - Filling Line (combo)    - Bank No (combo)
   - Date (datetime-local)   - No Of Cells (text)      - Filling Start/End Time
   - Acid SPG (text)         - Acid Volume (text)      - Tub No (combo)
   - CKT No (combo)         - Cell Short Qty (text)    - Temperature (text)
   - PRD Supervisor (combo)  - QA Inspector (combo)    - Filling Operator (combo)
   - Shift Engineer (combo)  - Shift (A/B/C combo)     - Reading Operator (combo)
   - QR Code (text → auto-scans via Enter key with barcode scanner)
   ↓
QR Code validation: checks if QR code already scanned (pr_Fetch_QRCode_Available)
   ↓ (if QR code is unique)
AJAX POST → TransactionController.pr_insert_MVRLA(23 parameters)
   ↓
SP pr_insert_MVRLA inserts record into MVRLA table
   ↓
Grid refreshes to show today's records
   ↓
Delete: User selects rows → clicks Delete → Pr_Delete_MVRLA (soft delete mv_Status='D')
   ↓
Print PDF: User selects row → Pr_Fetch_MVRLA_PDF → Crystal Reports → Browser PDF
```

## 2. DATABASE TABLES USED

### 2.1 MVRLA (14,898 rows) — PRIMARY FORMATION LOG
| Column | Type | Purpose |
|--------|------|---------|
| mv_ID | int (PK, Identity) | Record ID |
| mv_BatteryType | varchar | Battery type name (from combo) |
| mv_FillingLine | varchar | Production line number |
| mv_BankNo | varchar | Bank identifier |
| mv_Date | datetime | Formation date/time |
| mv_NoOfCells | varchar | Number of cells in batch |
| mv_FillingStartTime | datetime | Filling start timestamp |
| mv_FillingEndTime | datetime | Filling end timestamp |
| mv_CDPassedQty | varchar | CD Passed quantity (currently sent as empty "") |
| mv_CDFailQty | varchar | CD Failed quantity (currently sent as empty "") |
| mv_AcidSPG | varchar | Acid specific gravity reading |
| mv_AcidVolume | varchar | Acid volume in ml |
| mv_TubNo | varchar | Tub identifier |
| mv_CKTNo | varchar | Circuit number |
| mv_CellShortQty | varchar | Cell short circuit quantity |
| mv_Temperature | varchar | Process temperature |
| mv_PRDSupervisior | varchar | Supervisor name |
| mv_FillingOperator | varchar | Filling operator name |
| mv_QAInspector | varchar | QA Inspector name |
| mv_ShiftEngineer | varchar | Shift engineer name |
| mv_ReadingOperator | varchar | Reading operator name |
| mv_QRCode | varchar | Scanned QR/barcode (10 chars) |
| mv_Shift | varchar | Shift identifier (A/B/C) |
| mv_User | varchar | Created by user |
| mv_Status | char(1) | 'A' = Active, 'D' = Deleted (soft delete) |
| mv_CreatedDate | datetime | Record creation timestamp |

### 2.2 ReferenceGroup_Value (27 rows) — DROPDOWN SOURCE
- All 10 combo SPs query this table filtered by different `RGV_iRG_ID` values
- Each RG category maps to a dropdown: BatteryType, Line, Bank, Tub, CKT, Supervisor, Operator, Inspector, ShiftEngineer, ReadingOperator

## 3. STORED PROCEDURE LOGIC

### 3.1 pr_insert_MVRLA (23 parameters)
```sql
-- Inserts a new MVRLA formation process record
-- All 23 parameters are varchar type
INSERT INTO MVRLA (mv_BatteryType, mv_FillingLine, mv_BankNo, mv_Date,
  mv_NoOfCells, mv_FillingStartTime, mv_FillingEndTime, mv_CDPassedQty,
  mv_CDFailQty, mv_AcidSPG, mv_AcidVolume, mv_TubNo, mv_CKTNo,
  mv_CellShortQty, mv_Temperature, mv_PRDSupervisior, mv_FillingOperator,
  mv_QAInspector, mv_ShiftEngineer, mv_QRCode, mv_User, mv_Shift,
  mv_ReadingOperator, mv_Status, mv_CreatedDate)
VALUES (@mv_BatteryType, @mv_FillingLine, @mv_BankNo, @mv_Date, ...,
  'A', GETDATE())
```

### 3.2 Pr_Fetch_MVRLA
```sql
-- Returns today's active MVRLA records for the grid
SELECT * FROM MVRLA
WHERE mv_Status = 'A'
  AND CONVERT(varchar, mv_CreatedDate, 112) = CONVERT(varchar, GETDATE(), 112)
ORDER BY mv_ID DESC
```

### 3.3 Pr_Delete_MVRLA
```sql
-- Soft deletes an MVRLA record
-- Parameter: @ID int
UPDATE MVRLA SET mv_Status = 'D' WHERE mv_ID = @ID
```

### 3.4 pr_Fetch_QRCode_Available
```sql
-- Checks if QR code already exists (duplicate prevention)
-- Parameter: @ID varchar (10 chars)
SELECT mv_QRCode FROM MVRLA WHERE mv_QRCode = @ID AND mv_Status = 'A'
-- Returns row if exists (duplicate), empty if unique
```

### 3.5 pr_fetch_*Combos (10 SPs — all same pattern)
```sql
-- Each combo SP queries ReferenceGroup_Value filtered by category ID
-- Example: pr_fetch_BatteryTypeCombos
SELECT RGV_iRG_ID, RGV_vDesciption
FROM ReferenceGroup_Value
WHERE RGV_iRG_ID = <category_id>   -- Different ID for each combo
  AND RGV_cStatus = 'A'
ORDER BY RGV_vDesciption
```
**Category IDs:** BatteryType=1, Line=2, Bank=3, Tub=4, CKT=5, Supervisor=6, Operator=7, Inspector=8, ShiftEngineer=9, ReadingOperator=10

### 3.6 Pr_Fetch_MVRLA_PDF
```sql
-- Returns filtered MVRLA data for Crystal Reports PDF export
-- Parameters: @mv_QRCode, @from date, @to date, @tub
SELECT * FROM MVRLA
WHERE mv_Status = 'A'
  AND (@mv_QRCode = '' OR mv_QRCode = @mv_QRCode)
  AND CONVERT(varchar, mv_Date, 112) BETWEEN @from AND @to
  AND (@tub = '' OR mv_TubNo = @tub)
```

## 4. OLD SOURCE CODE REFERENCES
- **Controller:** [TransactionController.cs L950-995](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs#L950-L995) — `pr_insert_MVRLA`
- **Controller:** [TransactionController.cs L201-290](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs#L201-L290) — 10 combo actions
- **Controller:** [TransactionController.cs L264-269](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs#L264-L269) — `Pr_Fetch_MVRLA`
- **Controller:** [TransactionController.cs L442-446](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs#L442-L446) — `Pr_Delete_MVRLA`
- **Controller:** [TransactionController.cs L645-676](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs#L645-L676) — `Pr_Fetch_MVRLA_PDF`
- **View:** [Views/Transaction/MVRLA.cshtml](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Transaction/MVRLA.cshtml) — 860 lines

## 5. MIGRATION ENDPOINTS (.NET 8 Web API)
| HTTP Method | Endpoint | Purpose | SP Used |
|-------------|----------|---------|---------|
| GET | `/api/formation/combos` | All 10 dropdowns in one call | 10x `pr_fetch_*Combos` |
| POST | `/api/formation/mvrla` | Insert new formation record | `pr_insert_MVRLA` |
| GET | `/api/formation/mvrla/today` | Get today's formation records | `Pr_Fetch_MVRLA` |
| DELETE | `/api/formation/mvrla/{id}` | Soft delete a formation record | `Pr_Delete_MVRLA` |
| GET | `/api/formation/mvrla/check-qr/{code}` | Check QR code uniqueness | `pr_Fetch_QRCode_Available` |
| GET | `/api/formation/mvrla/export-pdf` | Export filtered data as PDF | `Pr_Fetch_MVRLA_PDF` |

## 6. REACT COMPONENTS
- `src/pages/Formation/MvrlaEntryPage.jsx` — Form + grid + QR scanner
- `src/components/Common/SearchableSelect.jsx` — Dropdown replacement for jqxComboBox
