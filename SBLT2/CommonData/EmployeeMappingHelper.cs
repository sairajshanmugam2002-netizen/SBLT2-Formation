using System;
using System.Data;
using System.Data.SqlClient;

namespace SriPravanTech
{
    /// <summary>
    /// Helper service for Employee mapping in Reference Master and Rework/Rejection.
    /// Manages Department dropdown binding, storing employees in Employee_Master upon Reference Master save,
    /// and binding mapped employees into Rework/Rejection dropdown.
    /// </summary>
    public static class EmployeeMappingHelper
    {
        /// <summary>
        /// Fetches distinct departments from Employee_Master and Employee_Master_Ass
        /// for the Department dropdown in Reference Master (/Master/Reference_Master).
        /// </summary>
        public static string GetDepartmentList()
        {
            string query = @"SELECT DISTINCT Department 
                             FROM (
                                 SELECT EM_Department AS Department FROM Employee_Master WHERE NULLIF(LTRIM(RTRIM(EM_Department)), '') IS NOT NULL
                                 UNION 
                                 SELECT EM_vDepartment AS Department FROM Employee_Master_Ass WHERE NULLIF(LTRIM(RTRIM(EM_vDepartment)), '') IS NOT NULL
                             ) d
                             ORDER BY Department ASC";

            DataTable dt = dbFunctions.getTable(query);
            return dbFunctions.GetJSONString(dt);
        }

        /// <summary>
        /// Gets the department of an existing employee by code or name.
        /// </summary>
        public static string GetEmployeeDepartment(string empCode, string empName = "")
        {
            if (string.IsNullOrEmpty(empCode) && string.IsNullOrEmpty(empName)) return "";
            try
            {
                using (SqlConnection con = new SqlConnection(dbFunctions.connectionstring))
                {
                    con.Open();
                    string sql = @"SELECT TOP 1 LTRIM(RTRIM(EM_Department)) 
                                   FROM Employee_Master 
                                   WHERE (EM_Code = @EM_Code OR EM_Employee_Name = @EM_Name) 
                                     AND NULLIF(LTRIM(RTRIM(EM_Department)), '') IS NOT NULL 
                                   ORDER BY EM_ID DESC";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@EM_Code", SqlDbType.VarChar, 50).Value = empCode ?? "";
                        cmd.Parameters.Add("@EM_Name", SqlDbType.VarChar, 50).Value = empName ?? "";
                        object val = cmd.ExecuteScalar();
                        return (val != null && val != DBNull.Value) ? val.ToString().Trim() : "";
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Checks if a ReferenceGroup_Value record corresponds to Reference Type = Employee (RG_iID = 1).
        /// </summary>
        public static bool IsEmployeeReference(string rgvId)
        {
            if (string.IsNullOrEmpty(rgvId)) return false;
            try
            {
                using (SqlConnection con = new SqlConnection(dbFunctions.connectionstring))
                {
                    con.Open();
                    string sql = @"SELECT COUNT(1) 
                                   FROM ReferenceGroup_Value rgv
                                   INNER JOIN Reference_Group rg ON rgv.RGV_iRG_ID = rg.RG_iID
                                   WHERE rgv.RGV_iID = @RGV_iID 
                                     AND (rg.RG_vDescription = 'Employee' OR rg.RG_iID = 1)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@RGV_iID", SqlDbType.Int).Value = Convert.ToInt32(rgvId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Stores or updates an employee in Employee_Master when saved through Reference Master.
        /// Inserts with next EM_ID if new, or updates name and department if already exists.
        /// </summary>
        public static void SaveEmployeeToMaster(string empCode, string empName, string department, string user = "System")
        {
            if (string.IsNullOrEmpty(empCode) && string.IsNullOrEmpty(empName)) return;

            empCode = (empCode ?? "").Trim();
            empName = (empName ?? "").Trim();
            department = (department ?? "").Trim();
            if (string.IsNullOrEmpty(user)) user = "System";

            try
            {
                using (SqlConnection con = new SqlConnection(dbFunctions.connectionstring))
                {
                    con.Open();

                    // Check if employee with this code exists
                    string checkSql = "SELECT EM_ID FROM Employee_Master WHERE EM_Code = @EM_Code";
                    object existingId = null;
                    using (SqlCommand cmd = new SqlCommand(checkSql, con))
                    {
                        cmd.Parameters.Add("@EM_Code", SqlDbType.VarChar, 50).Value = empCode;
                        existingId = cmd.ExecuteScalar();
                    }

                    if (existingId != null && existingId != DBNull.Value)
                    {
                        // Update existing employee
                        string updateSql = @"UPDATE Employee_Master 
                                             SET EM_Employee_Name = @EM_Employee_Name,
                                                 EM_Department = CASE WHEN @EM_Department <> '' THEN @EM_Department ELSE EM_Department END,
                                                 EM_Status = 'A'
                                             WHERE EM_ID = @EM_ID";
                        using (SqlCommand cmd = new SqlCommand(updateSql, con))
                        {
                            cmd.Parameters.Add("@EM_ID", SqlDbType.Int).Value = Convert.ToInt32(existingId);
                            cmd.Parameters.Add("@EM_Employee_Name", SqlDbType.VarChar, 50).Value = empName;
                            cmd.Parameters.Add("@EM_Department", SqlDbType.VarChar, 50).Value = department;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert new employee using max(EM_ID)+1
                        string insertSql = @"DECLARE @NewID INT;
                                             SELECT @NewID = ISNULL(MAX(EM_ID), 0) + 1 FROM Employee_Master;
                                             INSERT INTO Employee_Master 
                                             (EM_ID, EM_Code, EM_Employee_Name, EM_Department, EM_Status, EM_Created_by, EM_Created_Date)
                                             VALUES 
                                             (@NewID, @EM_Code, @EM_Employee_Name, @EM_Department, 'A', @EM_Created_by, GETDATE());";
                        using (SqlCommand cmd = new SqlCommand(insertSql, con))
                        {
                            cmd.Parameters.Add("@EM_Code", SqlDbType.VarChar, 50).Value = empCode;
                            cmd.Parameters.Add("@EM_Employee_Name", SqlDbType.VarChar, 50).Value = empName;
                            cmd.Parameters.Add("@EM_Department", SqlDbType.VarChar, 50).Value = department;
                            cmd.Parameters.Add("@EM_Created_by", SqlDbType.VarChar, 50).Value = user;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error saving employee to master: " + ex.Message);
            }
        }

        /// <summary>
        /// Fetches active employees from Employee_Master (legacy helper).
        /// </summary>
        public static string GetActiveEmployeesForReferenceMaster()
        {
            string query = @"SELECT 
                                EM_ID, 
                                LTRIM(RTRIM(ISNULL(EM_Code, ''))) AS EM_Code, 
                                LTRIM(RTRIM(ISNULL(EM_Employee_Name, ''))) AS EM_Employee_Name,
                                LTRIM(RTRIM(ISNULL(EM_Employee_Name, ''))) + 
                                    CASE 
                                        WHEN NULLIF(LTRIM(RTRIM(ISNULL(EM_Code, ''))), '') IS NOT NULL 
                                        THEN ' (' + LTRIM(RTRIM(EM_Code)) + ')' 
                                        ELSE '' 
                                    END AS EM_Display_Name
                             FROM Employee_Master
                             WHERE EM_Status = 'A'
                             ORDER BY EM_Employee_Name ASC";

            DataTable dt = dbFunctions.getTable(query);
            return dbFunctions.GetJSONString(dt);
        }

        /// <summary>
        /// Fetches employees mapped and maintained through Reference Master (ReferenceGroup_Value where Reference Type = Employee)
        /// for the Rework/Rejection Employee dropdown (/Transaction/Rework_Rejection).
        /// Displays Employee Name (EM_Code), e.g. Sairaj (100234), Aravinth (100002341), Sakthi (1000071), Sakthivel (1001).
        /// Eliminates duplicates and preserves correct employee IDs.
        /// </summary>
        public static string GetMappedEmployeesForReworkRejection()
        {
            string query = @";WITH MappedEmployees AS (
                SELECT 
                    ISNULL(em.EM_ID, rgv.RGV_iID) AS EM_ID,
                    LTRIM(RTRIM(ISNULL(rgv.RGV_vCode, ''))) AS EM_Code,
                    LTRIM(RTRIM(ISNULL(rgv.RGV_vDesciption, ''))) AS Raw_Name,
                    CASE 
                        WHEN NULLIF(LTRIM(RTRIM(ISNULL(rgv.RGV_vDesciption, ''))), '') IS NULL THEN ''
                        ELSE UPPER(LEFT(LTRIM(RTRIM(rgv.RGV_vDesciption)), 1)) + 
                             SUBSTRING(LTRIM(RTRIM(rgv.RGV_vDesciption)), 2, 200)
                    END AS EM_Employee_Name,
                    CASE 
                        WHEN NULLIF(LTRIM(RTRIM(ISNULL(rgv.RGV_vCode, ''))), '') IS NOT NULL 
                        THEN (UPPER(LEFT(LTRIM(RTRIM(rgv.RGV_vDesciption)), 1)) + 
                              SUBSTRING(LTRIM(RTRIM(rgv.RGV_vDesciption)), 2, 200)) + 
                             ' (' + LTRIM(RTRIM(rgv.RGV_vCode)) + ')'
                        ELSE (UPPER(LEFT(LTRIM(RTRIM(rgv.RGV_vDesciption)), 1)) + 
                              SUBSTRING(LTRIM(RTRIM(rgv.RGV_vDesciption)), 2, 200))
                    END AS EM_Display_Name,
                    ROW_NUMBER() OVER (
                        PARTITION BY LTRIM(RTRIM(ISNULL(rgv.RGV_vCode, ''))), LTRIM(RTRIM(ISNULL(rgv.RGV_vDesciption, ''))) 
                        ORDER BY rgv.RGV_iID DESC
                    ) AS rn
                FROM ReferenceGroup_Value rgv
                INNER JOIN Reference_Group rg ON rgv.RGV_iRG_ID = rg.RG_iID
                LEFT JOIN Employee_Master em ON (
                    em.EM_Code = rgv.RGV_vCode 
                    OR em.EM_Employee_Name = rgv.RGV_vDesciption
                ) AND em.EM_Status = 'A'
                WHERE (rg.RG_vDescription = 'Employee' OR rg.RG_iID = 1) 
                  AND rgv.RGV_vStatus = 'A'
            )
            SELECT EM_ID, EM_Code, EM_Employee_Name, EM_Display_Name
            FROM MappedEmployees
            WHERE rn = 1
            ORDER BY EM_Employee_Name ASC;";

            DataTable dt = dbFunctions.getTable(query);
            return dbFunctions.GetJSONString(dt);
        }
    }
}
