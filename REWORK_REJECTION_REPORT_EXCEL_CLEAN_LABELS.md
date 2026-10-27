# Remove FRR_ Prefix From Excel Export Only

## 1. Overview

In the **Rework / Rejection Report Excel download/export**, the `FRR_` prefix is now removed from the Excel column headers dynamically upon export.

All existing application code, database column names, stored procedures, and report grid UI `datafield` names remain completely unchanged.

---

## 2. Before vs Expected Excel Column Headers

| # | Current Query Column | Exported Excel Header |
|---|---|---|
| 1 | `SNo` | **SNo** |
| 2 | `FRR_ID` | **ID** |
| 3 | `Formation_ID` | **Formation_ID** |
| 4 | `FRR_QRCode` | **QRCode** |
| 5 | `FRR_BatteryType` | **BatteryType** |
| 6 | `FRR_CKTNo` | **CKTNo** |
| 7 | `FRR_Formation_ID` | **Formation_ID** |
| 8 | `FRR_Type_ID` | **Type_ID** |
| 9 | `FRR_Type_Name` | **Type_Name** |
| 10 | `FRR_Reason` | **Reason** |
| 11 | `FRR_DefectReasonType` | **DefectReasonType** |
| 12 | `FRR_DefectReason` | **DefectReason** |
| 13 | `FRR_Employee_ID` | **Employee_ID** |
| 14 | `FRR_Employee_Name` | **Employee_Name** |
| 15 | `FRR_Created_By` | **Created_By** |
| 16 | `FRR_Created_Date` | **Created_Date** |
| 17 | `FRR_Status` | **Status** |
| 18 | `FRR_Cell1` | **Cell1** |
| 19 | `FRR_Cell2` | **Cell2** |
| 20 | `FRR_Cell3` | **Cell3** |
| 21 | `FRR_Cell4` | **Cell4** |
| 22 | `FRR_Cell5` | **Cell5** |
| 23 | `FRR_Cell6` | **Cell6** |
| 24 | `FRR_DefectCell` | **DefectCell** |
| 25 | `FRR_OCV` | **OCV** |
| 26 | `FRR_PlateNo` | **PlateNo** |
| 27 | `FRR_Location` | **Location** |
| 28 | `FRR_PlateType` | **PlateType** |

---

## 3. Implementation Details

### File: `SBLT2/Controllers/MasterController.cs`
In `Export_To_Excel(string FileName, string Heading, string Data)`:

```csharp
DataTable dt = dbFunctions.getTable(Data);
if (dt != null && dt.Columns.Count > 0)
{
    int columnscount = dt.Columns.Count;
    for (int j = 0; j < columnscount; j++)
    {      
        Response.Write("<Td>");
        Response.Write("<B>");
        string header = dt.Columns[j].ColumnName.ToString();
        
        // Strip only the leading FRR_ prefix for clean Excel headers
        if (header.StartsWith("FRR_"))
        {
            header = header.Substring(4);
        }
        
        Response.Write(header);
        Response.Write("</B>");
        Response.Write("</Td>");
    }
    Response.Write("</TR>");
    for (int j = 0; j < dt.Rows.Count; j++)
    {
        Response.Write("<TR style='font-size:11.0pt; font-family:Times New Roman; background:white;'>");
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            Response.Write("<Td>");
            Response.Write(dt.Rows[j][i].ToString());
            Response.Write("</Td>");
        }
        Response.Write("</TR>");
    }
}
```

### Unchanged Components (Preserved as Requested)
- **Database column names**: Unchanged (`FRR_QRCode`, `FRR_BatteryType`, etc.).
- **Stored procedures**: Existing `pr_fetch_Rework_Rejection_Report` used directly without alterations.
- **Report grid UI**: Kept with exact `datafield` names (`{ name: 'FRR_QRCode', type: 'string' }`, `{ text: 'QR Code', datafield: 'FRR_QRCode' }`).
- **Data fetch & update logic**: Preserved.
