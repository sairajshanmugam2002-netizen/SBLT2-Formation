# Reference Master: Defect Reason List Update Query

**Page:** [Master / Reference Master](http://localhost:19450/Master/Reference_Master) -> Type: **Defect Reason**  
**Target Dropdown:** [Transaction / Rework & Rejection](http://localhost:19450/Transaction/Rework_Rejection) -> **Select Defect Code Reason**  
**SQL File:** [UPDATE_DEFECT_CODE_REASON_LIST.sql](file:///d:/office/MVRLA_Formation/MVRLA_Formation/UPDATE_DEFECT_CODE_REASON_LIST.sql)

---

## 1. What This Query Does

In **Reference Master**, when you select:
* **Reference Type (`RG_vDescription`)**: `Defect Reason` (Group ID = 17)

This query updates the `ReferenceGroup_Value` table to map each Defect Reason description to its correct category (`Electrical`, `Mechanical`, `Quality`) under column `RGV_vDefectReasonType`.

Once updated, the dropdown **Select Defect Code Reason** on the Rework/Rejection page fetches the exact matching reasons based on the selected defect type.

---

## 2. The Exact Update Query

```sql
USE [MVRLA_Formation];
GO

-- 1. Get Group ID for 'Defect Reason' (Group 17)
DECLARE @DR_ID INT;
SELECT @DR_ID = RG_iID FROM Reference_Group WHERE RG_vDescription = 'Defect Reason' OR RG_vCode = 'DR';
IF @DR_ID IS NULL SET @DR_ID = 17;

-- 2. Update Electrical Defect Reasons
UPDATE ReferenceGroup_Value
SET 
    RGV_iRG_ID = @DR_ID,
    RGV_vDefectReasonType = 'Electrical',
    RGV_dUpdateDate = GETDATE(),
    RGV_vUpdatedBy = 'Admin'
WHERE RGV_vDesciption IN (
    'OCV High', 
    'OCV Low', 
    'Cell Voltage Issue', 
    'Cell Reverse', 
    'Short Circuit', 
    'High Resistance',
    'Low Voltage',
    'Open Circuit'
);

-- 3. Update Mechanical Defect Reasons
UPDATE ReferenceGroup_Value
SET 
    RGV_iRG_ID = @DR_ID,
    RGV_vDefectReasonType = 'Mechanical',
    RGV_dUpdateDate = GETDATE(),
    RGV_vUpdatedBy = 'Admin'
WHERE RGV_vDesciption IN (
    'Plate Crack', 
    'Plate Damage', 
    'Terminal Damage', 
    'Container Damage', 
    'Cover Damage', 
    'Post Damage',
    'Intercell Weld Defect',
    'Separator Damage'
);

-- 4. Update Quality Defect Reasons
UPDATE ReferenceGroup_Value
SET 
    RGV_iRG_ID = @DR_ID,
    RGV_vDefectReasonType = 'Quality',
    RGV_dUpdateDate = GETDATE(),
    RGV_vUpdatedBy = 'Admin'
WHERE RGV_vDesciption IN (
    'Wrong Plate', 
    'Acid Leakage', 
    'Specific Gravity Low', 
    'Specific Gravity High', 
    'Wrong Product',
    'Color Mismatch',
    'Visual Defect'
);
GO
```

---

## 3. Reference Master UI Update Stored Procedure

When you update a row in the **Reference Master** UI grid, the controller executes:

```sql
EXEC Pr_Update_ReferenceGroup_Value 
    @RGV_iID = @ID,
    @RGV_vCode = @Code,
    @RGV_vDesciption = @Description,
    @RGV_vDefectReasonType = @DefectReasonType;
```

---

## 4. Query to Verify the Updated List

Run this query to see all updated Defect Code Reasons:

```sql
SELECT 
    v.RGV_iID                AS [ID],
    g.RG_vDescription        AS [Reference_Group_Type],
    v.RGV_vCode              AS [Defect_Code],
    v.RGV_vDesciption        AS [Defect_Code_Reason],
    v.RGV_vDefectReasonType  AS [Mapped_Type],
    v.RGV_vStatus            AS [Status],
    v.RGV_dUpdateDate        AS [Updated_Date]
FROM ReferenceGroup_Value v
INNER JOIN Reference_Group g ON v.RGV_iRG_ID = g.RG_iID
WHERE g.RG_vDescription = 'Defect Reason' OR v.RGV_iRG_ID = 17
ORDER BY v.RGV_vDefectReasonType, v.RGV_vDesciption;
```
