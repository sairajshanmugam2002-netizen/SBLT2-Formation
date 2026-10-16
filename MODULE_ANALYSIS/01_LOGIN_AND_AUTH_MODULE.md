# MODULE 1: LOGIN & AUTHENTICATION

## 1. WORKFLOW (Old System)
```
User opens browser → Views/Home/Index.cshtml
   ↓
Enters Username + Password → clicks Login button
   ↓
jQuery AJAX POST → HomeController.pr_getlogin1(user, pwd)
   ↓
Controller calls SP: pr_getlogin1 '@username','@password'
   ↓
SP validates against User_Master1 table (NOT User_Master!)
   ↓
Returns tilde-delimited string: "UM_vUser_Name~UM_iUser_Type~UM_iArea~UM_vUser_ID~UM_iId"
   ↓
Frontend stores in sessionStorage: Name, rights, user, Area, User_ID
   ↓
Immediately calls HomeController.pr_getMenu_listNew1(roleId, module)
   ↓
SP pr_getMenu_listNew1 builds authorized menu HTML from T_Setting_Menu_Master + T_Setting_Role_Assignment
   ↓
Sidebar rendered → Redirects to Dashboard
```

## 2. DATABASE TABLES USED

### 2.1 User_Master1 (64 rows) — PRIMARY AUTH TABLE
| Column | Type | Purpose |
|--------|------|---------|
| UM_iId | int (PK, Identity) | User ID |
| UM_vUser_ID | varchar | Login username |
| UM_vPassword | varchar | Plain-text password (⚠️ security risk) |
| UM_vUser_Name | varchar | Display name |
| UM_iUser_Type | int | FK to Refernce_Group_Value.RGV_iID (Role type) |
| UM_iArea | int | Factory area assignment |
| UM_cStatus | char(1) | 'A' = Active, 'D' = Deleted |

### 2.2 Refernce_Group_Value (6 rows) — ROLE TYPE LOOKUP
| Column | Type | Purpose |
|--------|------|---------|
| RGV_iID | int (PK) | Reference value ID |
| RGV_iRG_ID | int | FK to Reference_Group |
| RGV_vDesciption | varchar | Role name (e.g. "Formation", "Admin") |

### 2.3 User_Points — GAMIFICATION POINTS
| Column | Type | Purpose |
|--------|------|---------|
| UP_User_ID | int | FK to User_Master1.UM_iId |
| UP_Points | int | Net points earned |

### 2.4 T_Setting_Menu_Master (87 rows) — MENU HIERARCHY
| Column | Type | Purpose |
|--------|------|---------|
| MM_iID | int (PK) | Menu item ID |
| MM_vMenu_Name | varchar | Display text |
| MM_iParent_ID | int | Parent menu (0 = root) |
| MM_vURL | varchar | MVC route path |
| MM_vIcon | varchar | FontAwesome icon class |
| MM_iOrder | int | Sort order |
| MM_iModule_ID | int | Module category |

### 2.5 T_Setting_Role_Assignment (134 rows) — RBAC PERMISSIONS
| Column | Type | Purpose |
|--------|------|---------|
| RA_iID | int (PK) | Assignment ID |
| RA_iRole_ID | int | Role ID (21 = Formation) |
| RA_iMenu_ID | int | FK to MM_iID |
| RA_bView | bit | View permission |
| RA_bCreate | bit | Create permission |
| RA_bEdit | bit | Edit permission |
| RA_bDelete | bit | Delete permission |

## 3. STORED PROCEDURE LOGIC

### 3.1 pr_getlogin1
```sql
-- Authenticates user, returns user info as single row
-- Parameters: @user varchar, @pwd varchar
-- Logic:
SELECT UM_vUser_Name, UM_iUser_Type, UM_iArea, UM_vUser_ID, UM_iId
FROM User_Master1
WHERE UM_vUser_ID = @user
  AND UM_vPassword = @pwd COLLATE SQL_Latin1_General_CP1_CS_AS  -- Case-sensitive!
  AND UM_cStatus = 'A'
-- If match found: returns 1 row
-- If no match: returns 0 rows → Controller returns "false"
```
**Key Business Rules:**
- Password comparison is CASE-SENSITIVE (COLLATE SQL_Latin1_General_CP1_CS_AS)
- Only ACTIVE users (UM_cStatus = 'A') can login
- User type ID maps to Refernce_Group_Value for role name

### 3.2 pr_getMenu_listNew1
```sql
-- Builds menu tree for a specific role and module
-- Parameters: @Role_ID int, @Module_ID int
-- Logic:
SELECT MM_iID, MM_vMenu_Name, MM_vURL, MM_vIcon, MM_iOrder, MM_iParent_ID
FROM T_Setting_Menu_Master MM
INNER JOIN T_Setting_Role_Assignment RA ON MM.MM_iID = RA.RA_iMenu_ID
WHERE RA.RA_iRole_ID = @Role_ID
  AND RA.RA_bView = 1
  AND MM.MM_iModule_ID = @Module_ID
ORDER BY MM.MM_iOrder
```

## 4. OLD SOURCE CODE REFERENCES
- **Controller:** [HomeController.cs L137-151](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/HomeController.cs#L137-L151) — `pr_getlogin1` action
- **Controller:** [HomeController.cs L108-113](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Controllers/HomeController.cs#L108-L113) — `pr_getMenu_listNew1` action
- **View:** [Views/Home/Index.cshtml](file:///d:/office/MVRLA_Formation/MVRLA_Formation/SBLT2/Views/Home/Index.cshtml) — Login form

## 5. MIGRATION ENDPOINTS (.NET 8 Web API)
| HTTP Method | Endpoint | Purpose | SP Used |
|-------------|----------|---------|---------|
| POST | `/api/auth/login` | Authenticate user, return JWT token | `pr_getlogin1` |
| GET | `/api/auth/navigation` | Get authorized menu tree for logged-in user | `pr_getMenu_listNew1` |
| GET | `/api/auth/points` | Get user's gamification points | `pr_Points` |

## 6. REACT COMPONENTS
- `src/pages/Auth/LoginPage.jsx` — Modern branded login form
- `src/context/AuthContext.jsx` — JWT token storage + user state
- `src/components/Layout/Sidebar.jsx` — Dynamic menu from /api/auth/navigation
