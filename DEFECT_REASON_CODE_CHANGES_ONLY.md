# Defect Reason & Defect Code Reason - Changes Only

**Target Page:** [Transaction / Rework & Rejection](http://localhost:19450/Transaction/Rework_Rejection)  
**Database:** `MVRLA_Formation`  
**Scope:** Strictly **Defect Reason** & **Defect Code Reason** queries, table alters, stored procedures, and label changes only.

---

## 1. Table Schema Changes (`ALTER TABLE`)

Only the columns specifically created for Defect Reason and Defect Code Reason:

### 1.1. Table: `ReferenceGroup_Value`
Adds the mapping column to associate each reason with its category/type (e.g. `Electrical`, `Mechanical`, `Quality`).

```sql
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'ReferenceGroup_Value' 
      AND COLUMN_NAME = 'RGV_vDefectReasonType'
)
BEGIN
    ALTER TABLE ReferenceGroup_Value ADD RGV_vDefectReasonType VARCHAR(100) NULL;
END
GO
```

### 1.2. Table: `Formation_Rework_Rejection`
Adds the Defect Code Reason storage columns.

```sql
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_DefectReasonType')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_DefectReasonType VARCHAR(100) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Formation_Rework_Rejection') AND name = 'FRR_DefectReason')
BEGIN
    ALTER TABLE Formation_Rework_Rejection ADD FRR_DefectReason VARCHAR(500) NULL;
END
GO
```

---

## 2. Seed & Configuration Queries

### 2.1. Reference Groups Setup
Registers `Defect Reason Type` and `Defect Reason` groups in `Reference_Group`:

```sql
-- Defect Reason Type Group (DRT)
IF NOT EXISTS (SELECT 1 FROM Reference_Group WHERE RG_vDescription = 'Defect Reason Type')
BEGIN
    DECLARE @NewRGID INT = (SELECT ISNULL(MAX(RG_iID), 0) + 1 FROM Reference_Group);
    INSERT INTO Reference_Group (RG_iID, RG_vCode, RG_vDescription, RG_vStatus)
    VALUES (@NewRGID, 'DRT', 'Defect Reason Type', 'A');
END

-- Defect Reason Group (DR)
IF NOT EXISTS (SELECT 1 FROM Reference_Group WHERE RG_vDescription = 'Defect Reason')
BEGIN
    DECLARE @NewRGID2 INT = (SELECT ISNULL(MAX(RG_iID), 0) + 1 FROM Reference_Group);
    INSERT INTO Reference_Group (RG_iID, RG_vCode, RG_vDescription, RG_vStatus)
    VALUES (@NewRGID2, 'DR', 'Defect Reason', 'A');
END
GO
```

### 2.2. Mapping Defect Reasons to Defect Reason Type
Maps specific defect reasons to their corresponding type (`Electrical`, `Mechanical`, `Quality`):

```sql
-- Electrical Defect Reasons
UPDATE ReferenceGroup_Value 
SET RGV_vDefectReasonType = 'Electrical' 
WHERE RGV_vDesciption IN ('OCV High', 'OCV Low', 'Cell Voltage Issue');

-- Mechanical Defect Reasons
UPDATE ReferenceGroup_Value 
SET RGV_vDefectReasonType = 'Mechanical' 
WHERE RGV_vDesciption IN ('Plate Crack', 'Plate Damage');

-- Quality Defect Reasons
UPDATE ReferenceGroup_Value 
SET RGV_vDefectReasonType = 'Quality' 
WHERE RGV_vDesciption IN ('Wrong Plate');
GO
```

---

## 3. Stored Procedure Changes

### 3.1. `pr_fetch_Rework_Rejection_Reasons`
Altered to accept `@ReferenceType` so selecting a type (Electrical, Mechanical, Quality) returns only matching Defect Code Reasons:

```sql
CREATE OR ALTER PROCEDURE pr_fetch_Rework_Rejection_Reasons
    @Type_ID       VARCHAR(100) = NULL,
    @ReferenceType VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @Type_ID = NULLIF(LTRIM(RTRIM(@Type_ID)), '');
    SET @ReferenceType = NULLIF(LTRIM(RTRIM(@ReferenceType)), '');

    IF (@Type_ID = 'ALL') SET @Type_ID = NULL;
    IF (@ReferenceType = 'ALL') SET @ReferenceType = NULL;

    -- Route single ReferenceType passed into @Type_ID to Defect Reasons group (17)
    IF (@ReferenceType IS NULL AND @Type_ID IS NOT NULL
        AND EXISTS (
            SELECT 1 FROM ReferenceGroup_Value 
            WHERE RGV_vStatus = 'A' 
              AND LTRIM(RTRIM(RGV_vDefectReasonType)) = @Type_ID
        )
        AND @Type_ID NOT IN ('12', '13', 'Rework', 'Rejection')
    )
    BEGIN
        SET @ReferenceType = @Type_ID;
        SET @Type_ID = '17';
    END

    DECLARE @ParsedID INT = TRY_CAST(@Type_ID AS INT);

    SELECT DISTINCT
        v.RGV_iID,
        v.RGV_vDesciption,
        v.RGV_vCode,
        v.RGV_iRG_ID,
        ISNULL(NULLIF(LTRIM(RTRIM(v.RGV_vDefectReasonType)), ''), g.RG_vDescription) AS Reference_Type,
        ISNULL(v.RGV_vDefectReasonType, '') AS DefectReasonType
    FROM ReferenceGroup_Value v
    INNER JOIN Reference_Group g ON v.RGV_iRG_ID = g.RG_iID
    WHERE v.RGV_vStatus = 'A'
      AND g.RG_vStatus  = 'A'
      AND (
          @ReferenceType IS NULL
          OR LTRIM(RTRIM(v.RGV_vDefectReasonType)) = @ReferenceType
          OR LTRIM(RTRIM(g.RG_vDescription)) = @ReferenceType
          OR LTRIM(RTRIM(g.RG_vCode)) = @ReferenceType
      )
      AND (
          (@ParsedID IS NOT NULL AND v.RGV_iRG_ID = @ParsedID)
          OR (@ParsedID IS NULL AND @Type_ID IS NOT NULL
              AND (g.RG_vCode = @Type_ID OR g.RG_vDescription = @Type_ID))
          OR (@Type_ID IS NULL AND @ReferenceType IS NOT NULL
              AND (v.RGV_iRG_ID = 17 OR LTRIM(RTRIM(v.RGV_vDefectReasonType)) = @ReferenceType))
          OR (@Type_ID IS NULL AND @ReferenceType IS NULL
              AND v.RGV_iRG_ID IN (12, 13, 17))
      )
    ORDER BY v.RGV_vDesciption ASC;
END;
GO
```

---

### 3.2. `pr_insert_Formation_Rework_Rejection` (Defect Reason parameters)
Saves both `FRR_Reason` and `FRR_DefectReason` / `FRR_DefectReasonType`:

```sql
-- Parameters added for Defect Reason:
--   @FRR_DefectReasonType VARCHAR(100) = NULL
--   @FRR_DefectReason     VARCHAR(500) = NULL

-- Insert logic handles reason synchronization:
IF (@FRR_Reason IS NULL OR @FRR_Reason = '')
    SET @FRR_Reason = @FRR_DefectReason;
IF (@FRR_DefectReason IS NULL OR @FRR_DefectReason = '')
    SET @FRR_DefectReason = @FRR_Reason;

-- Insert statement includes:
--   FRR_Reason, FRR_DefectReasonType, FRR_DefectReason
```

---

### 3.3. `pr_fetch_Formation_Rework_Rejection_List`
Returns both reason columns for display:

```sql
SELECT 
    ROW_NUMBER() OVER(ORDER BY FRR_ID DESC) AS [SNo],
    FRR_ID,
    FRR_Type_Name,
    FRR_Reason,                                            -- Primary Defect Reason
    ISNULL(FRR_DefectReasonType, '') AS FRR_DefectReasonType,
    ISNULL(FRR_DefectReason, ISNULL(FRR_Reason, '')) AS FRR_DefectReason, -- Defect Code Reason
    ...
FROM Formation_Rework_Rejection
WHERE FRR_Status = 'A'
ORDER BY FRR_ID DESC;
```

---

## 4. Defect Reason & Defect Code Reason - Label Changes Only

These are the exact label changes made in `SBLT2/Views/Transaction/Rework_Rejection.cshtml`:

### 4.1. Form Control Labels
| Control | Old Label | New Label | Note |
|---|---|---|---|
| Dropdown `#ddlReason` | `<label>* 1. Reason</label>` | `<label><span class="required-star">*</span> 1. Defect Reason</label>` | Changed from **`Reason`** to **`Defect Reason`** |
| Dropdown `#ddlDefectCodeReason` | *(None)* | `<label>Defect Code Reason</label>` | Added label for **`Defect Code Reason`** |

### 4.2. Grid Column Header Labels
| Column Datafield | Old Grid Header | New Grid Header | Note |
|---|---|---|---|
| `FRR_Reason` | `{ text: 'Reason', ... }` | `{ text: 'Defect Reason', datafield: 'FRR_Reason', width: 140 }` | Changed from **`Reason`** to **`Defect Reason`** |
| `FRR_DefectReason` | *(None)* | `{ text: 'Defect Code Reason', datafield: 'FRR_DefectReason', width: 150 }` | Added new header **`Defect Code Reason`** |
