# MODULE 7: SETTINGS (User Management & Role Assignment)

## 1. WORKFLOW (Old System)
```
A) User Master:
   SettingController.User_Master() → Views/Setting/User_Master.cshtml
   → Pr_Fetch_T_Setting_UserMaster_Details() → User list in jqxGrid
   → Create/Edit users (Insert/Update User_Master1)

B) Role Assignment:
   SettingController.Role_Assignment() → Views/Setting/Role_Assignment.cshtml
   → pr_get_T_Setting_Menu_Master() → Full menu tree with checkboxes
   → User selects Role → checks/unchecks menu permissions (View/Create/Edit/Delete)
   → pr_insert_T_Setting_Role_Assignment() → Saves permission matrix
```

## 2. DATABASE TABLES USED
- **User_Master1** (64 rows) — User credentials & profile
- **T_Setting_UserMaster** — Extended user settings
- **T_Setting_Menu_Master** (87 rows) — Menu hierarchy definition
- **T_Setting_Role_Assignment** (134 rows) — Role-to-menu permission mapping
- **T_Setting_Role_Master** — Role definitions

## 3. MIGRATION ENDPOINTS (.NET 8 Web API)
| HTTP Method | Endpoint | Purpose |
|-------------|----------|---------|
| GET | `/api/settings/users` | Get all users |
| POST | `/api/settings/users` | Create a new user |
| PUT | `/api/settings/users/{id}` | Update user details |
| GET | `/api/settings/roles` | Get all roles with permissions |
| GET | `/api/settings/roles/{roleId}/permissions` | Get menu permissions for a role |
| POST | `/api/settings/roles/{roleId}/permissions` | Save role permissions |

## 4. REACT COMPONENTS
- `src/pages/Settings/UserMasterPage.jsx` — User CRUD grid
- `src/pages/Settings/RoleAssignmentPage.jsx` — Menu tree with permission checkboxes
