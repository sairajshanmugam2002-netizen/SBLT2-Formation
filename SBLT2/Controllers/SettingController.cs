using SriPravanTech;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBLT2.Controllers
{
    public class SettingController : Controller
    {
        //
        // GET: /Setting/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Modules()
        {
            return View();
        }

        public ActionResult User_Master()
        {
            return View();
        }

        public ActionResult Menus()
        {
            return View();
        }


        public ActionResult Notice()
        {
            return View();
        }

        public ActionResult Module_Assignment()
        {
            return View();
        }

        public ActionResult Role_Assignment()
        {
            return View();
        }

        public ActionResult Role_Category()
        {
            return View();
        }


        public ActionResult Backup()
        {
            return View();
        }





        public string pr_create_Backup()
        {
            DataTable dt = dbFunctions.getTable("pr_create_Backup");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }




        public string pr_get_T_Setting_Menu_Master(string Role, string module)
        {
            int roleId = 0, moduleId = 1;
            int.TryParse(Role, out roleId);
            int.TryParse(module, out moduleId);
            if (moduleId == 6 || moduleId == 0) moduleId = 1;

            DataTable dt = dbFunctions.getTable("pr_get_T_Setting_Menu_Master " + roleId + "," + moduleId + ",1");
            if (dt == null || dt.Rows.Count == 0)
            {
                return "{\"record\":[]}";
            }
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }




        public string Pr_Load_MM_vMenu_Name(string ID, string module)
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_MM_vMenu_Name " + ID + "," + module);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Load_Reference_value(string Group_Name)
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_Reference_value  '" + Group_Name + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Load_MD_vModuleName()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_MD_vModuleName");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_T_Settings_Modules_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Settings_Modules_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_T_Settings_Modules_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Settings_Modules_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_T_Settings_Modules(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_T_Settings_Modules '" + ID + "'");
            return "True";
        }


        public string Pr_Insert_T_Settings_Modules(
        string MD_vModuleName,
        string MD_vModuleDescription,
        string MD_vIcon,
        string MD_vMenuID,
        string MD_iOrganization,
        string MD_vUpdatedBy
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_T_Settings_Modules";
                com.Parameters.Add("@MD_vModuleName", SqlDbType.VarChar).Value = MD_vModuleName;
                com.Parameters.Add("@MD_vModuleDescription", SqlDbType.VarChar).Value = MD_vModuleDescription;
                com.Parameters.Add("@MD_vIcon", SqlDbType.VarChar).Value = MD_vIcon;
                com.Parameters.Add("@MD_vMenuID", SqlDbType.VarChar).Value = MD_vMenuID;
                com.Parameters.Add("@MD_iOrganization", SqlDbType.VarChar).Value = MD_iOrganization;
                com.Parameters.Add("@MD_vUpdatedBy", SqlDbType.VarChar).Value = MD_vUpdatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Update_T_Settings_Modules(
        string MD_iid,
        string MD_vModuleName,
        string MD_vModuleDescription,
        string MD_vIcon,
        string MD_vMenuID,
        string MD_iOrganization,
        string MD_vUser
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_T_Settings_Modules";
                com.Parameters.Add("@MD_iid", SqlDbType.VarChar).Value = MD_iid;
                com.Parameters.Add("@MD_vModuleName", SqlDbType.VarChar).Value = MD_vModuleName;
                com.Parameters.Add("@MD_vModuleDescription", SqlDbType.VarChar).Value = MD_vModuleDescription;
                com.Parameters.Add("@MD_vIcon", SqlDbType.VarChar).Value = MD_vIcon;
                com.Parameters.Add("@MD_vMenuID", SqlDbType.VarChar).Value = MD_vMenuID;
                com.Parameters.Add("@MD_iOrganization", SqlDbType.VarChar).Value = MD_iOrganization;
                com.Parameters.Add("@MD_vUser", SqlDbType.VarChar).Value = MD_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Fetch_T_Setting_Menu_Master_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Setting_Menu_Master_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_T_Setting_Menu_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Setting_Menu_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_T_Setting_Menu_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_T_Setting_Menu_Master '" + ID + "'");
            return "True";
        }


        public string Pr_Fetch_T_Settings_Rolewise_Category_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Settings_Rolewise_Category_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_T_Settings_Rolewise_Category_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Settings_Rolewise_Category_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_T_Settings_Rolewise_Category(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_T_Settings_Rolewise_Category '" + ID + "'");
            return "True";
        }


        public string Pr_Insert_T_Settings_Rolewise_Category(
        string RWC_iRoleID,
        string RWC_vCategory,
        string RWC_iCategory_ID,
        string RWC_iOrganiation,
        string RWC_dCreatedDate
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_T_Settings_Rolewise_Category";
                com.Parameters.Add("@RWC_iRoleID", SqlDbType.VarChar).Value = RWC_iRoleID;
                com.Parameters.Add("@RWC_vCategory", SqlDbType.VarChar).Value = RWC_vCategory;
                com.Parameters.Add("@RWC_iCategory_ID", SqlDbType.VarChar).Value = RWC_iCategory_ID;
                com.Parameters.Add("@RWC_iOrganiation", SqlDbType.VarChar).Value = RWC_iOrganiation;
                com.Parameters.Add("@RWC_vCreatedby", SqlDbType.VarChar).Value = RWC_dCreatedDate;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Update_T_Settings_Rolewise_Category(
        string RWC_iID,
        string RWC_iRoleID,
        string RWC_vCategory,
        string RWC_iCategory_ID,
        string RWC_iOrganiation,
        string RWC_dUpdatedDate
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_T_Settings_Rolewise_Category";
                com.Parameters.Add("@RWC_iID", SqlDbType.VarChar).Value = RWC_iID;
                com.Parameters.Add("@RWC_iRoleID", SqlDbType.VarChar).Value = RWC_iRoleID;
                com.Parameters.Add("@RWC_vCategory", SqlDbType.VarChar).Value = RWC_vCategory;
                com.Parameters.Add("@RWC_iCategory_ID", SqlDbType.VarChar).Value = RWC_iCategory_ID;
                com.Parameters.Add("@RWC_iOrganiation", SqlDbType.VarChar).Value = RWC_iOrganiation;

                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Insert_T_Setting_Menu_Master(
        string MM_vModule,
        string MM_vMenu_Name,
        string MM_iMenu_Order,
        string MM_iMenuLevel,
        string MM_iParentID,
        string MM_vURL,
        string MM_vIcon,
        string MM_iOrganization,
        string MM_vCreatedby
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_T_Setting_Menu_Master";
                com.Parameters.Add("@MM_vModule", SqlDbType.VarChar).Value = MM_vModule;
                com.Parameters.Add("@MM_vMenu_Name", SqlDbType.VarChar).Value = MM_vMenu_Name;
                com.Parameters.Add("@MM_iMenu_Order", SqlDbType.VarChar).Value = MM_iMenu_Order;
                com.Parameters.Add("@MM_iMenuLevel", SqlDbType.VarChar).Value = MM_iMenuLevel;
                com.Parameters.Add("@MM_iParentID", SqlDbType.VarChar).Value = MM_iParentID;
                com.Parameters.Add("@MM_vURL", SqlDbType.VarChar).Value = MM_vURL;
                com.Parameters.Add("@MM_vIcon", SqlDbType.VarChar).Value = MM_vIcon;
                com.Parameters.Add("@MM_iOrganization", SqlDbType.VarChar).Value = MM_iOrganization;
                com.Parameters.Add("@MM_vCreatedby", SqlDbType.VarChar).Value = MM_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Update_T_Setting_Menu_Master(
        string MM_iId,
        string MM_vModule,
        string MM_vMenu_Name,
        string MM_iMenu_Order,
        string MM_iMenuLevel,
        string MM_iParentID,
        string MM_vURL,
        string MM_vIcon,
        string MM_iOrganization,
        string MM_vUpdatedBy
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_T_Setting_Menu_Master";
                com.Parameters.Add("@MM_iId", SqlDbType.VarChar).Value = MM_iId;
                com.Parameters.Add("@MM_vModule", SqlDbType.VarChar).Value = MM_vModule;
                com.Parameters.Add("@MM_vMenu_Name", SqlDbType.VarChar).Value = MM_vMenu_Name;
                com.Parameters.Add("@MM_iMenu_Order", SqlDbType.VarChar).Value = MM_iMenu_Order;
                com.Parameters.Add("@MM_iMenuLevel", SqlDbType.VarChar).Value = MM_iMenuLevel;
                com.Parameters.Add("@MM_iParentID", SqlDbType.VarChar).Value = MM_iParentID;
                com.Parameters.Add("@MM_vURL", SqlDbType.VarChar).Value = MM_vURL;
                com.Parameters.Add("@MM_vIcon", SqlDbType.VarChar).Value = MM_vIcon;
                com.Parameters.Add("@MM_iOrganization", SqlDbType.VarChar).Value = MM_iOrganization;
                com.Parameters.Add("@MM_vUpdatedBy", SqlDbType.VarChar).Value = MM_vUpdatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Fetch_T_Setting_UserMaster_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Setting_UserMaster_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_Notice_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Notice_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_Notice_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Notice_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_Notice(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Notice '" + ID + "'");
            return "True";
        }


        public string Pr_Insert_Notice(
        string N_Notice_Details,
        string N_Created_By
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Notice";
                com.Parameters.Add("@N_Notice_Details", SqlDbType.NVarChar).Value = N_Notice_Details;
                com.Parameters.Add("@N_Created_By", SqlDbType.VarChar).Value = N_Created_By;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Update_Notice(
        string N_iid,
        string N_Notice_Details,
        string N_Created_By
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Notice";
                com.Parameters.Add("@N_iid", SqlDbType.VarChar).Value = N_iid;
                com.Parameters.Add("@N_Notice_Details", SqlDbType.NVarChar).Value = N_Notice_Details;
                com.Parameters.Add("@N_Created_By", SqlDbType.VarChar).Value = N_Created_By;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        public string Pr_Fetch_T_Setting_UserMaster_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Setting_UserMaster_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_T_Setting_UserMaster(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_T_Setting_UserMaster '" + ID + "'");
            return "True";
        }


        public string Pr_Insert_T_Setting_UserMaster(
        string U_UserName,
        string U_Password,
        string U_Rights,
        string U_CreatedDBy,
        string U_LodgeID
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_T_Setting_UserMaster";
                com.Parameters.Add("@U_UserName", SqlDbType.VarChar).Value = U_UserName;
                com.Parameters.Add("@U_Password", SqlDbType.VarChar).Value = U_Password;
                com.Parameters.Add("@U_LodgeID", SqlDbType.VarChar).Value = U_LodgeID;
                com.Parameters.Add("@U_Rights", SqlDbType.VarChar).Value = U_Rights;
                com.Parameters.Add("@U_CreatedDBy", SqlDbType.VarChar).Value = U_CreatedDBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Update_T_Setting_UserMaster(
        string U_ID,
        string U_UserName,
        string U_Password,
        string U_Rights,
       // string U_CreatedDBy,
        string U_LodgeID
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_T_Setting_UserMaster";
                com.Parameters.Add("@U_ID", SqlDbType.VarChar).Value = U_ID;
                com.Parameters.Add("@U_UserName", SqlDbType.VarChar).Value = U_UserName;
                com.Parameters.Add("@U_LodgeID", SqlDbType.VarChar).Value = U_LodgeID;

                com.Parameters.Add("@U_Password", SqlDbType.VarChar).Value = U_Password;
                com.Parameters.Add("@U_Rights", SqlDbType.VarChar).Value = U_Rights;
               // com.Parameters.Add("@U_CreatedDBy", SqlDbType.VarChar).Value = U_CreatedDBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Fetch_T_Setting_Module_Assignment_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Setting_Module_Assignment_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_T_Setting_Module_Assignment_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_T_Setting_Module_Assignment_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_T_Setting_Module_Assignment(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_T_Setting_Module_Assignment '" + ID + "'");
            return "True";
        }


        public string Pr_Insert_T_Setting_Module_Assignment(
        string MA_vModuleName,
        string MA_iMenuID,
        string MA_iOrganization,
        string MA_vCreatedby
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_T_Setting_Module_Assignment";
                com.Parameters.Add("@MA_vModuleName", SqlDbType.VarChar).Value = MA_vModuleName;
                com.Parameters.Add("@MA_iMenuID", SqlDbType.VarChar).Value = MA_iMenuID;
                com.Parameters.Add("@MA_iOrganization", SqlDbType.VarChar).Value = MA_iOrganization;
                com.Parameters.Add("@MA_vCreatedby", SqlDbType.VarChar).Value = MA_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Update_T_Setting_Module_Assignment(
        string MA_iid,
        string MA_vModuleName,
        string MA_iMenuID,
        string MA_iOrganization,
        string MA_vUpdatedBy
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_T_Setting_Module_Assignment";
                com.Parameters.Add("@MA_iid", SqlDbType.VarChar).Value = MA_iid;
                com.Parameters.Add("@MA_vModuleName", SqlDbType.VarChar).Value = MA_vModuleName;
                com.Parameters.Add("@MA_iMenuID", SqlDbType.VarChar).Value = MA_iMenuID;
                com.Parameters.Add("@MA_iOrganization", SqlDbType.VarChar).Value = MA_iOrganization;
                com.Parameters.Add("@MA_vUpdatedBy", SqlDbType.VarChar).Value = MA_vUpdatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        public string Pr_Insert_T_Setting_Role_Assignment(
        string RA_iRole,
        string RA_iMenuID,
        string RA_iVisibility,
        string RA_iView,
        string RA_iAdd,
        string RA_iEdit,
        string RA_iDelete,
        string RA_iOrganization,
        string RA_vCreatedby
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_T_Setting_Role_Assignment";
                com.Parameters.Add("@RA_iRole", SqlDbType.VarChar).Value = RA_iRole;
                com.Parameters.Add("@RA_iMenuID", SqlDbType.VarChar).Value = RA_iMenuID;
                com.Parameters.Add("@RA_iVisibility", SqlDbType.VarChar).Value = RA_iVisibility;
                com.Parameters.Add("@RA_iView", SqlDbType.VarChar).Value = RA_iView;
                com.Parameters.Add("@RA_iAdd", SqlDbType.VarChar).Value = RA_iAdd;
                com.Parameters.Add("@RA_iEdit", SqlDbType.VarChar).Value = RA_iEdit;
                com.Parameters.Add("@RA_iDelete", SqlDbType.VarChar).Value = RA_iDelete;
                com.Parameters.Add("@RA_iOrganization", SqlDbType.VarChar).Value = RA_iOrganization;
                com.Parameters.Add("@RA_vCreatedby", SqlDbType.VarChar).Value = RA_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




    }
}
