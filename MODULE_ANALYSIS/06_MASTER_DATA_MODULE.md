# MODULE 6: MASTER DATA MANAGEMENT

## 1. WORKFLOW (Old System)
```
User navigates to Masters menu → selects:
   A) Assembly Product Master
   B) Reference Master
   ↓
A) Product Master:
   MasterController.Assembly_Product_Master() → Views/Master/Assembly_Product_Master.cshtml
   → Pr_Fetch_Product_MasterAssy() → Direct SQL query on Product_Master_Assembly
   → jqxGrid displays product catalogue

B) Reference Master:
   MasterController.Reference_Master() → Views/Master/Reference_Master.cshtml
   → pr_get_Reference_Values() → Loads Reference_Group + ReferenceGroup_Value
   → CRUD operations: Insert/Update/Delete reference values
```

## 2. DATABASE TABLES USED

### 2.1 Product_Master_Assembly (1,276 rows)
| Column | Type | Purpose |
|--------|------|---------|
| PMA_iID | int (PK) | Product ID |
| PMA_vProduct_Name | varchar | Battery product name |
| PMA_vProduct_Code | varchar | SAP/Oracle product code |
| PMA_vAH_Rating | varchar | Ampere-hour rating |
| PMA_vContainer_Type | varchar | Container specification |

### 2.2 Reference_Group (13 rows)
| Column | Type | Purpose |
|--------|------|---------|
| RG_iID | int (PK) | Category ID |
| RG_vDescription | varchar | Category name (e.g. "Battery Type", "Line", "Tub") |

### 2.3 ReferenceGroup_Value (27 rows)
| Column | Type | Purpose |
|--------|------|---------|
| RGV_iID | int (PK) | Value ID |
| RGV_iRG_ID | int | FK to Reference_Group.RG_iID |
| RGV_vDesciption | varchar | Value description |
| RGV_cStatus | char(1) | 'A' = Active |

## 3. MIGRATION ENDPOINTS (.NET 8 Web API)
| HTTP Method | Endpoint | Purpose |
|-------------|----------|---------|
| GET | `/api/products/assembly` | Get product master list |
| GET | `/api/references` | Get all reference groups + values |
| GET | `/api/references/{groupId}` | Get values for a specific group |
| POST | `/api/references` | Insert a new reference value |
| PUT | `/api/references/{id}` | Update a reference value |
| DELETE | `/api/references/{id}` | Delete a reference value |

## 4. REACT COMPONENTS
- `src/pages/Masters/ProductMasterPage.jsx` — Read-only product catalogue grid
- `src/pages/Masters/ReferenceMasterPage.jsx` — CRUD grid for reference groups/values
