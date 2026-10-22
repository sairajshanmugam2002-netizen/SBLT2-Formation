# Product Master Table & Field Analysis
**Amara Raja MVRLA Formation Plant — Traceability & Quality Console**  
**Document Type:** Technical Verification & Schema Analysis Report (Check Only — No Modifications Made)  
**Target View:** `SBLT2/Views/Master/Assembly_Product_Master.cshtml` (UI Title: "Product Master")  
**Controller:** `SBLT2/Controllers/MasterController.cs`  
**Database Table:** `Product_Master_Assembly` (1,276 Active Production Rows, 17 Columns)  
**Reference Excel Template:** `Product_Master.xlsx` (14 Mapping Columns)

---

## 1. Executive Summary

A comprehensive, read-only audit of the **Product Master** screen, database table `Product_Master_Assembly`, backend controller endpoints, and data insertion pipeline was performed against the UI image provided.

### Key Findings
1. **Grid Discrepancy (8 Visible Columns vs 14 Form Fields):**
   - The UI entry form exposes **14 input fields** (5 marked with mandatory red asterisks `*`).
   - The bottom grid (`#Grid`) only defines **8 visible data columns** (`Edit`, `Product Code`, `Product Name`, `Variant Code`, `Compare Code`, `Skid Qty`, `MOQ Qty`, `Inv Qty`) plus the multi-select checkbox column.
   - **Crucial mandatory fields** (`Mold`, `Sector`, `Start Variant Code`, `End Variant Code`) along with optional fields (`Start Serial No`, `End Serial No`, `Charge Hrs`) are **completely omitted from the Grid view**.
2. **Database Schema Alignment (17 Columns):**
   - The database table `Product_Master_Assembly` contains **17 physical columns**.
   - The INSERT query in `MasterController.pr_Insert_Product_MasterAssy` maps 13 form parameters + 1 auto-increment ID (`PM_iID`), 1 user stamp (`PM_Created_by`), 1 date stamp (`PM_Created_Date = getdate()`), and 1 status flag (`PM_Status = 'A'`).
3. **Orphan Field Discovery (`inv_Qty` / "Bosh Code"):**
   - In the form, input `#inv_Qty` is labeled **"Bosh Code"**.
   - However, `#inv_Qty` is **NOT passed** into the backend INSERT (`pr_Insert_Product_MasterAssy`) or UPDATE (`pr_Update_Product_MasterAssy`) queries.
   - The database table `Product_Master_Assembly` **does not have a column** for "Bosh Code" or `inv_Qty`.
4. **Validation Discrepancies:**
   - The UI shows red asterisks `*` on: `Product Code`, `Mold`, `Sector`, `Start Variant Code`, and `End Variant Code`.
   - The JavaScript `jqxValidator` only checks: `#PM_Item_Code`, `#VStart`, and `#VEnd`, leaving `PM_Mold` and `PM_Sector` unvalidated before submission.
   - The validation error messages were copy-pasted from Chennai Inward transactions (e.g. `#VStart` outputs *"Invoice No Required..!"*, `#VEnd` outputs *"Vehicle No Required..!"*, `#PM_Item_Code` outputs *"QR Code Required..!"*).
5. **Data Fetch Query Mismatch:**
   - `MasterController.Pr_Fetch_Product_MasterAssy()` executes:
     `SELECT PM_iID as iID, PM_Item_Code as Part_No, PM_Description as Description, PM_Skid_Qty as Skid_Qty, chargeing_Dub_Hrs as [Charge Hrs], PM_Created_by as Created_by FROM Product_Master_Assembly WHERE PM_Status='A'`
   - The Grid data adapter expects: `PM_iID`, `PM_Item_Code`, `PM_Description`, `PM_Variant_Code`, `compare_code`, `PM_Skid_Qty`, `PM_MOQ_Qty`, `inv_Qty`, `inv_LabelCount`.
   - Column aliases in the query (`Part_No`, `Description`, `iID`) do not match the grid's expected datafields (`PM_Item_Code`, `PM_Description`, `PM_iID`), explaining why grid data rows may appear empty or out of sync.

---

## 2. Complete Field-by-Field Analysis Matrix

The table below details every field in the Product Master UI, how it is handled in JavaScript, how it maps to backend controller parameters, and how it is stored in `Product_Master_Assembly`:

| # | UI Form Label | UI Input ID | UI Asterisk (*) | JS Validator Rule | Ajax Param (`pr_Insert_Product_MasterAssy`) | Controller Method Parameter | DB Column (`Product_Master_Assembly`) | DB Data Type | Grid Column Status | In Excel Template? |
|---|---------------|-------------|-----------------|-------------------|---------------------------------------------|-----------------------------|----------------------------------------|--------------|--------------------|--------------------|
| 1 | **Product Code** | `#PM_Item_Code` | **Yes (*)** | Required ("QR Code Required..!") | `PM_Item_Code` | `string PM_Item_Code` | `PM_Item_Code` | `VARCHAR(50)` | **Visible** (`Product Code`) | Yes (`Item_Code`) |
| 2 | **Mold** | `#PM_Mold` | **Yes (*)** | *None* | `PM_Mold` | `string PM_Mold` | `PM_Mold` | `VARCHAR(50)` | **NOT IN GRID** | Yes (`Mold`) |
| 3 | **Sector** | `#PM_Sector` | **Yes (*)** | *None* | `PM_Sector` | `string PM_Sector` | `PM_Sector` | `VARCHAR(50)` | **NOT IN GRID** | Yes (`Sector`) |
| 4 | **Description** | `#PM_Description` | No | *None* | `PM_Description` | `string PM_Description` | `PM_Description` | `VARCHAR(200)` | **Visible** (`Product Name`) | Yes (`Description`) |
| 5 | **Variant Code** | `#PM_Variant_Code` | No | *None* | `PM_Variant_Code` | `string PM_Variant_Code` | `PM_Variant_Code` | `VARCHAR(50)` | **Visible** (`Variant Code`) | Yes (`Variant_Code`) |
| 6 | **Compare Code** | `#compare_code` | No | *None* | `compare_code` | `string compare_code` | `compare_code` | `VARCHAR(50)` | **Visible** (`Compare Code`) | Yes (`compare_code`) |
| 7 | **Skid Qty** | `#PM_Skid_Qty` | No | *None* | `PM_Skid_Qty` | `string PM_Skid_Qty` | `PM_Skid_Qty` | `VARCHAR(50)` / `INT` | **Visible** (`Skid Qty`) | Yes (`Skid_Qty`) |
| 8 | **Qty** | `#PM_MOQ_Qty` | No | *None* | `PM_MOQ_Qty` | `string PM_MOQ_Qty` | `PM_MOQ_Qty` | `VARCHAR(50)` / `INT` | **Visible** (`MOQ Qty`) | Yes (`MOQ_Qty`) |
| 9 | **Bosh Code** | `#inv_Qty` | No | *None* | **NOT SENT** (Orphan field) | *Not accepted* | **NO DB COLUMN** | *N/A* | **Visible** (`Inv Qty`) | No |
| 10 | **Start Variant Code** | `#VStart` | **Yes (*)** | Required ("Invoice No Required..!") | `VStart` | `string VStart` | `VStart` | `VARCHAR(50)` | **NOT IN GRID** | Yes (`VStart`) |
| 11 | **End Variant Code** | `#VEnd` | **Yes (*)** | Required ("Vehicle No Required..!") | `VEnd` | `string VEnd` | `VEnd` | `VARCHAR(50)` | **NOT IN GRID** | Yes (`VEnd`) |
| 12 | **Start Serial No** | `#SerialStart` | No | *None* | `SerialStart` | `string SerialStart` | `SerialStart` | `VARCHAR(50)` | **NOT IN GRID** | Yes (`SerialStart`) |
| 13 | **End Serial No** | `#SerialEnd` | No | *None* | `SerialEnd` | `string SerialEnd` | `SerialEnd` | `VARCHAR(50)` | **NOT IN GRID** | Yes (`SerialEnd`) |
| 14 | **Charge Hrs** | `#chargeing_Dub_Hrs` | No | *None* | `chargeing_Dub_Hrs` | `string chargeing_Dub_Hrs` | `chargeing_Dub_Hrs` | `VARCHAR(50)` / `FLOAT` | **NOT IN GRID** | Yes (`chargeing_Dub_Hrs`) |

---

## 3. System-Generated & Audit Columns (No Form Inputs)

The following columns in `Product_Master_Assembly` are populated automatically by backend logic during INSERT:

| # | DB Column | Data Type | Value Assigned on INSERT | Source / Logic |
|---|-----------|-----------|--------------------------|----------------|
| 15 | `PM_iID` | `INT` (PK) | `@PM_iID` | `isnull(max(PM_iID), 0) + 1 from Product_Master_Assembly` |
| 16 | `PM_Created_by` | `VARCHAR(50)` | `@PM_Created_by` | Taken from logged-in user session (`sessionStorage.getItem('user')`) |
| 17 | `PM_Created_Date` | `DATETIME` | `getdate()` | SQL Server system timestamp |
| 18 | `PM_Status` | `CHAR(1)` | `'A'` | Hardcoded 'A' (Active) for soft-delete support |

---

## 4. Detailed Analysis of Data Insertion Flow

### 4.1 Client-Side Save Trigger (`Assembly_Product_Master.cshtml`)
When the operator clicks the **Save** button (`#btn_Save`):
1. Client triggers form validation: `$('#form1').jqxValidator('validate')`.
2. If validation passes (`flag == true`), it calls the `save()` function.
3. `save()` constructs a GET/POST query string with the input values:
   ```javascript
   url: "pr_Insert_Product_MasterAssy?PM_Item_Code=" + $('#PM_Item_Code').val() +
        "&PM_Mold=" + $('#PM_Mold').val() +
        "&PM_Sector=" + $('#PM_Sector').val() +
        "&PM_Description=" + $('#PM_Description').val() +
        "&PM_Variant_Code=" + $('#PM_Variant_Code').val() +
        "&PM_Skid_Qty=" + $('#PM_Skid_Qty').val() +
        "&PM_MOQ_Qty=" + $('#PM_MOQ_Qty').val() +
        "&SerialStart=" + $('#SerialStart').val() +
        "&SerialEnd=" + $('#SerialEnd').val() +
        "&VStart=" + $('#VStart').val() +
        "&VEnd=" + $('#VEnd').val() +
        "&compare_code=" + $('#compare_code').val() +
        "&chargeing_Dub_Hrs=" + $('#chargeing_Dub_Hrs').val() +
        "&PM_Created_by=" + User
   ```

### 4.2 Backend Execution (`MasterController.cs`)
Method signature:
```csharp
public string pr_Insert_Product_MasterAssy(
    string PM_Item_Code, string PM_Mold, string PM_Sector, string PM_Description, 
    string PM_Variant_Code, string PM_Skid_Qty, string PM_MOQ_Qty, string SerialStart, 
    string SerialEnd, string VStart, string VEnd, string compare_code, 
    string chargeing_Dub_Hrs, string PM_Created_by
)
```

Direct SQL executed by SqlCommand:
```sql
declare @PM_iID int 
select @PM_iID = isnull(max(PM_iID), 0) + 1 from Product_Master_Assembly 

insert into Product_Master_Assembly (
    PM_iID, 
    PM_Item_Code, 
    PM_Mold, 
    PM_Sector, 
    PM_Description, 
    PM_Variant_Code, 
    PM_Skid_Qty, 
    PM_MOQ_Qty, 
    PM_Created_by, 
    PM_Created_Date, 
    PM_Status, 
    SerialStart, 
    SerialEnd, 
    VStart, 
    VEnd, 
    compare_code, 
    chargeing_Dub_Hrs
) 
Values (
    @PM_iID, 
    @PM_Item_Code, 
    @PM_Mold, 
    @PM_Sector, 
    @PM_Description, 
    @PM_Variant_Code, 
    @PM_Skid_Qty, 
    @PM_MOQ_Qty, 
    @PM_Created_by, 
    getdate(), 
    'A', 
    @SerialStart, 
    @SerialEnd, 
    @VStart, 
    @VEnd, 
    @compare_code, 
    @chargeing_Dub_Hrs
)
```

---

## 5. Summary of Why Fields are Missing in the Grid

### Mandatory Fields Missing in Grid:
1. **Mold (`PM_Mold`):** Marked mandatory `*` in the form. Saved to `Product_Master_Assembly.PM_Mold`. **Not present in the Grid columns.**
2. **Sector (`PM_Sector`):** Marked mandatory `*` in the form. Saved to `Product_Master_Assembly.PM_Sector`. **Not present in the Grid columns.**
3. **Start Variant Code (`VStart`):** Marked mandatory `*` in the form. Saved to `Product_Master_Assembly.VStart`. **Not present in the Grid columns.**
4. **End Variant Code (`VEnd`):** Marked mandatory `*` in the form. Saved to `Product_Master_Assembly.VEnd`. **Not present in the Grid columns.**

### Optional Fields Missing in Grid:
5. **Start Serial No (`SerialStart`):** Form field exists and inserts to database. **Not present in the Grid columns.**
6. **End Serial No (`SerialEnd`):** Form field exists and inserts to database. **Not present in the Grid columns.**
7. **Charge Hrs (`chargeing_Dub_Hrs`):** Form field exists and inserts to database. **Not present in the Grid columns.**

### Misaligned / Extra Fields in Grid:
- **`Inv Qty` (`inv_Qty`):** Present in the Grid columns, but binds to no valid column in `Product_Master_Assembly`. The corresponding form input is titled "Bosh Code" and is completely ignored during INSERT/UPDATE.

---

## 6. Verification Status

| Item | Status | Verification Note |
|------|--------|-------------------|
| Codebase Modified | **NO** | Zero modifications made to code, views, controllers, or database per strict user instruction. |
| Fields Added | **NO** | No extra fields were added. |
| Database Verified | **YES** | Confirmed 17 columns in `Product_Master_Assembly` via INSERT query, migration specification, and `Product_Master.xlsx`. |
| Grid Verified | **YES** | Confirmed exactly 8 visible columns defined in `Assembly_Product_Master.cshtml` line 219–277. |
