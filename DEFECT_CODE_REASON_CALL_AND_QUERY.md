# Defect Code Reason - List Call Location & Queries

**Target Page:** [Transaction / Rework & Rejection](http://localhost:19450/Transaction/Rework_Rejection)  
**Database:** `MVRLA_Formation`

---

## 1. Which Place to Call

### A. Frontend UI Location
* **File:** [SBLT2/Views/Transaction/Rework_Rejection.cshtml](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Transaction/Rework_Rejection.cshtml#L573-L604)
* **HTML Element:**
  ```html
  <div id="ddlDefectCodeReason"></div>
  ```
* **JavaScript Function:**
  ```javascript
  function loadDefectCodeReasons(refType) {
      var url = "../../Transaction/pr_fetch_Rework_Rejection_Reasons?Type_ID=17" +
          (refType ? ("&ReferenceType=" + encodeURIComponent(refType)) : "");

      var defectCodeReasonSource = {
          datatype: "json",
          datafields: [
              { name: 'RGV_iID', type: 'string' },
              { name: 'RGV_vDesciption', type: 'string' },
              { name: 'RGV_vCode', type: 'string' },
              { name: 'Reference_Type', type: 'string' },
              { name: 'DefectReasonType', type: 'string' }
          ],
          id: 'RGV_iID',
          url: url,
          async: false
      };
      var defectCodeReasonAdapter = new $.jqx.dataAdapter(defectCodeReasonSource);
      var placeholder = refType ? ("Select " + refType + " Reason") : "Select Defect Code Reason";

      $("#ddlDefectCodeReason").jqxDropDownList({
          selectedIndex: -1,
          source: defectCodeReasonAdapter,
          displayMember: "RGV_vDesciption",
          valueMember: "RGV_vDesciption",
          placeHolder: placeholder,
          width: '100%',
          height: '34px',
          filterable: true,
          filterPlaceHolder: "Search Reason...",
          autoDropDownHeight: true
      });
      $('#ddlDefectCodeReason').jqxDropDownList('clearSelection');
  }
  ```

* **Where it is Triggered:**
  1. **When Defect Reason is selected (`#ddlReason.on('select')`):**
     ```javascript
     $('#ddlReason').on('select', function (event) {
         if (event.args && event.args.item && event.args.item.originalItem) {
             var orig = event.args.item.originalItem;
             var mappedRefType = orig.DefectReasonType || orig.Reference_Type || '';
             if (mappedRefType && mappedRefType !== 'Rework' && mappedRefType !== 'Rejection') {
                 loadDefectCodeReasons(mappedRefType);
             } else {
                 loadDefectCodeReasons(null);
             }
         } else {
             loadDefectCodeReasons(null);
         }
     });
     ```
  2. **On Page Load:**
     ```javascript
     loadDefectCodeReasons(null);
     ```
  3. **On Form Clear:**
     ```javascript
     loadDefectCodeReasons(null);
     ```

---

### B. Backend Controller Location
* **File:** [SBLT2/Controllers/TransactionController.cs](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/TransactionController.cs#L74-L105)
* **Controller Action:**
  ```csharp
  public string pr_fetch_Rework_Rejection_Reasons(string Type_ID = null, string ID = null, string ReferenceType = null)
  {
      string selectedType = !string.IsNullOrEmpty(Type_ID) ? Type_ID : ID;
      string cleanType = !string.IsNullOrEmpty(selectedType) ? selectedType.Replace("'", "''") : null;
      string cleanRefType = !string.IsNullOrEmpty(ReferenceType) ? ReferenceType.Replace("'", "''") : null;

      string query;
      if (!string.IsNullOrEmpty(cleanType) && !string.IsNullOrEmpty(cleanRefType))
      {
          query = "EXEC dbo.pr_fetch_Rework_Rejection_Reasons @Type_ID = '" + cleanType + "', @ReferenceType = '" + cleanRefType + "'";
      }
      else if (!string.IsNullOrEmpty(cleanType))
      {
          query = "EXEC dbo.pr_fetch_Rework_Rejection_Reasons @Type_ID = '" + cleanType + "'";
      }
      else if (!string.IsNullOrEmpty(cleanRefType))
      {
          query = "EXEC dbo.pr_fetch_Rework_Rejection_Reasons @ReferenceType = '" + cleanRefType + "'";
      }
      else
      {
          query = "EXEC dbo.pr_fetch_Rework_Rejection_Reasons";
      }

      DataTable dt = dbFunctions.getTable(query);
      if (dt == null || dt.Rows.Count == 0)
      {
          return "[]";
      }
      return dbFunctions.GetJSONString(dt);
  }
  ```

---

## 2. Queries for Defect Code Reason List

### A. Stored Procedure Calls (Execution Queries)

```sql
-- 1. Get Defect Code Reasons for Electrical:
EXEC pr_fetch_Rework_Rejection_Reasons @Type_ID = '17', @ReferenceType = 'Electrical';

-- 2. Get Defect Code Reasons for Mechanical:
EXEC pr_fetch_Rework_Rejection_Reasons @Type_ID = '17', @ReferenceType = 'Mechanical';

-- 3. Get Defect Code Reasons for Quality:
EXEC pr_fetch_Rework_Rejection_Reasons @Type_ID = '17', @ReferenceType = 'Quality';

-- 4. Get All Defect Code Reasons (Unfiltered):
EXEC pr_fetch_Rework_Rejection_Reasons @Type_ID = '17';
```

---

### B. Direct Raw SQL Query (Behind the Stored Procedure)

Run this directly in SQL Server Management Studio (SSMS) to get the Defect Code Reason list:

```sql
USE [MVRLA_Formation];
GO

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
  AND v.RGV_iRG_ID  = 17   -- Group 17 = Defect Reason
  -- Optional: filter by Reference Type
  -- AND LTRIM(RTRIM(v.RGV_vDefectReasonType)) = 'Electrical'
ORDER BY v.RGV_vDesciption ASC;
GO
```
