using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;



using System.Text;
using System.Data.OleDb;

using System.Security.Cryptography;
using System.Web.Script.Services;


namespace SriPravanTech.CommonData
{
    public partial class getdata : System.Web.UI.Page
    {
        public static string connectionstring = System.Configuration.ConfigurationSettings.AppSettings["ConStr"];
        protected void Page_Load(object sender, EventArgs e)
        {
             Response.ContentType = "application/json";
             string data = Request.QueryString["str"].ToString();


             if (data.Equals("pr_Display_User_Master"))
             {
                 DataTable dt = dbFunctions.getTable("pr_Display_User_Master");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Load_CT_vCity_Name"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_CT_vCity_Name");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Fetch_Modules_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Modules_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Load_MM_vMenu_Namebymould"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Load_MM_vMenu_Namebymould +'" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
            


             if (data.Equals("Pr_Fetch_Modules_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Modules_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Load_CNT_vCountry"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_CNT_vCountry");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Load_MD_vModuleName"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_MD_vModuleName");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Mould_Assignment_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Mould_Assignment_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Module_Assignment_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Module_Assignment_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Module_Assignment_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Module_Assignment_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Load_MM_vMenu_Name1"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_MM_vMenu_Name1");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }





             if (data.Equals("Pr_Fetch_Mould_Assignment_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Mould_Assignment_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }



             if (data.Equals("Pr_Fetch_Preventive_Check_Sheet_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Preventive_Check_Sheet_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Preventive_Check_Sheet_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Preventive_Check_Sheet_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("pr_get_User_Masterbyid"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_get_User_Masterbyid '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Defect_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Defect_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Fetch_Category_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Category_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Category_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Category_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Rejection_Details_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Rejection_Details_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Rejection_Details_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Rejection_Details_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Load_DF_vDefect_Type"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_DF_vDefect_Type");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Fetch_Defect_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Defect_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_item_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_item_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_item_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_item_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }



             if (data.Equals("Pr_Fetch_Machine_Type_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Machine_Type_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Contributor_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Contributor_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Contributor_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Contributor_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Machine_Type_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Machine_Type_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Country_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Country_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Country_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Country_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Group_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Group_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Fetch_ItemMaster_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_ItemMaster_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_ItemMaster_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_ItemMaster_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Group_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Group_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Support_Ticket_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Support_Ticket_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Support_Ticket_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Support_Ticket_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Role_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Role_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Role_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Role_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("pr_getRightsDetails"))
             {
                 string ID = Request.QueryString["id"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_getRightsDetails '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
            

             if (data.Equals("Pr_Load_MM_vMenu_Name"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Load_MM_vMenu_Name  "+ID);
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }



             if (data.Equals("Pr_Load_MT_vTypeName"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_MT_vTypeName");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Load_MM_vName"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_MM_vName");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Idle_Hours_Details_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Idle_Hours_Details_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Idle_Hours_Details_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Idle_Hours_Details_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Item_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Item_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Item_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Item_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Shift_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Shift_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Load_MM_vDescription"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_MM_vDescription");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }




             if (data.Equals("Pr_Fetch_Shift_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Shift_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Load_MT_vTypeName1"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_MT_vTypeName1");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Shift_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Shift_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Machine_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Machine_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Fetch_UOM_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_UOM_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Load_IM_vInternal_PartNo"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_IM_vInternal_PartNo");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }




             if (data.Equals("Pr_Fetch_UOM_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_UOM_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Load_MM_vCode"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_MM_vCode");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Load_ITM_vCode"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_ITM_vCode");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("pr_get_HelpDetails"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_get_HelpDetails '"+ID+"'");
                 string s = GetJSONString(dt);
                 Response.Write(s.Replace("\n","<br>"));
             }
            


             if (data.Equals("Pr_Fetch_Plan_Entry_Details"))
             {
                 string frmdt = Request.QueryString["from"].ToString();
                 string todt = Request.QueryString["to"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Plan_Entry_Details '" + frmdt+"','"+todt+"'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Company_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Company_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Company_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Company_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("pr_get_inspectionDetails"))
             {
                 string frmdt = Request.QueryString["from"].ToString();
                 string todt = Request.QueryString["to"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_get_inspectionDetails '" + frmdt + "','" + todt + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Load_CA_vCategoryName"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_CA_vCategoryName");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Load_UM_vUOM_Name"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_UM_vUOM_Name");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }



             if (data.Equals("Pr_Fetch_Daily_Check_Sheet_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Daily_Check_Sheet_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Daily_Check_Sheet_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Daily_Check_Sheet_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Plan_Entry_Detailnew"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Plan_Entry_Detailnew ");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Help_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Help_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Help_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Help_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
             if (data.Equals("Pr_Fetch_CityMaster_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_CityMaster_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_CityMaster_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_CityMaster_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }
            


             if (data.Equals("Pr_Fetch_Plan_Entry_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Plan_Entry_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             


             if (data.Equals("Pr_Fetch_Machine_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Machine_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Load_SM_vCode"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_SM_vCode");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Inspection_Details_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Inspection_Details_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Inspection_Details_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Inspection_Details_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_MenuMaster_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_MenuMaster_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_MenuMaster_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_MenuMaster_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Idle_Type_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Idle_Type_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Idle_Type_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Idle_Type_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("pr_get_Current_Machine_Status"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_get_Current_Machine_Status '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("pr_get_Current_Machine_StatusList"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_get_Current_Machine_StatusList '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("pr_getMenu_list"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_getMenu_list '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("pr_getMenu_listnew"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 string MID = Request.QueryString["MID"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_getMenu_listnew '" + ID + "','" + MID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("pr_getLogin"))
             {
                 string user = Request.QueryString["user"].ToString();
                 string pwd = Request.QueryString["pwd"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_getLogin '"+ user + "','"+pwd+"'");
                
                 if (dt.Rows.Count > 0)
                 {
                     Response.Write(dt.Rows[0][0].ToString());
                     Session["User"] = user;
                 }
                 else
                 {
                     Response.Write("false");
                 }
             }


             if (data.Equals("pr_getMenu_listbyParent"))
             {
                 string ID = Request.QueryString["User"].ToString();
                 string Parent = Request.QueryString["Parent"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_getMenu_listbyParent '" + ID + "','" + Parent + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("pr_getMenu_listbyParentnew"))
             {
                 string ID = Request.QueryString["User"].ToString();
                 string Parent = Request.QueryString["Parent"].ToString();
                 string MID = Request.QueryString["MID"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_getMenu_listbyParentnew '" + ID + "','" + Parent + "','" + MID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

            
            

             if (data.Equals("Pr_Fetch_Mould_Master_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Mould_Master_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("pr_get_ProductionSummary"))
             {
                 string from  = Request.QueryString["from"].ToString();
                 string to = Request.QueryString["to"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_get_ProductionSummary '" + from + "','"+to+"'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Mould_Master_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Mould_Master_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }



             if (data.Equals("Pr_Load_RM_vCode"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_RM_vCode");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Load_IM_vPartName"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_IM_vPartName");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Load_GP_vGroupName"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Load_GP_vGroupName");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }



             if (data.Equals("pr_get_Inspection_Details"))
             {
                 //string ID = Request.QueryString["id"].ToString();
                 DataTable dt = dbFunctions.getTable("pr_get_Inspection_Details '20-OCt-2016','A',1");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("pr_Plan_Entry"))
             {
                 DataTable dt = dbFunctions.getTable("pr_Plan_Entry");
                 Response.Write(dt.Rows[0][0].ToString());
             }

             if (data.Equals("Pr_Fetch_FeedBack_Details"))
             {
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_FeedBack_Details");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_FeedBack_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_FeedBack_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }



             if (data.Equals("Pr_Fetch_Mould_Usage_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Mould_Usage_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }

             if (data.Equals("Pr_Fetch_Inspection_Entry_Details"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 string shift = Request.QueryString["shift"].ToString();
                 string date = Request.QueryString["date"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Inspection_Entry_Details '" + ID + "','" + shift + "','" + date + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }


             if (data.Equals("Pr_Fetch_Inspection_Entry_ByID"))
             {
                 string ID = Request.QueryString["ID"].ToString();
                 DataTable dt = dbFunctions.getTable("Pr_Fetch_Inspection_Entry_ByID '" + ID + "'");
                 string s = GetJSONString(dt);
                 Response.Write(s);
             }



            }




        [WebMethod]
        public static string Pr_Delete_Inspection_Entry(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Inspection_Entry '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Inspection_Entry(
       string IE_iItem_ID,
       string IE_vShift,
       string IE_vInspectionDate,
       string IE_InspectionID,
               string IE_vMachineID,
       string IE_vReference,
       string IE_iSNo,
       string IE_vPoints,
       string IE_vControl,
       string IE_vX1,
       string IE_vX2,
       string IE_vX3,
       string IE_vX11,
       string IE_vX12,
       string IE_vX13,
       string IE_vX4,
       string IE_vX5,
       string IE_vX6,
       string IE_vX7,
       string IE_vX8,
       string IE_vX9,
       string IE_vResult,
       String IE_User
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Inspection_Entry";
                com.Parameters.Add("@IE_iItem_ID", SqlDbType.VarChar).Value = IE_iItem_ID;
                com.Parameters.Add("@IE_vShift", SqlDbType.VarChar).Value = IE_vShift;
                com.Parameters.Add("@IE_vInspectionDate", SqlDbType.VarChar).Value = IE_vInspectionDate;
                com.Parameters.Add("@IE_InspectionID", SqlDbType.VarChar).Value = IE_InspectionID;
                com.Parameters.Add("@IE_vReference", SqlDbType.VarChar).Value = IE_vReference;
                com.Parameters.Add("@IE_vMachineID", SqlDbType.VarChar).Value = IE_vMachineID;
                com.Parameters.Add("@IE_iSNo", SqlDbType.VarChar).Value = IE_iSNo;
                com.Parameters.Add("@IE_vPoints", SqlDbType.VarChar).Value = IE_vPoints;
                com.Parameters.Add("@IE_vControl", SqlDbType.VarChar).Value = IE_vControl;
                com.Parameters.Add("@IE_vX1", SqlDbType.VarChar).Value = IE_vX1;
                com.Parameters.Add("@IE_vX2", SqlDbType.VarChar).Value = IE_vX2;
                com.Parameters.Add("@IE_vX3", SqlDbType.VarChar).Value = IE_vX3;

                com.Parameters.Add("@IE_vX11", SqlDbType.VarChar).Value = IE_vX11;
                com.Parameters.Add("@IE_vX12", SqlDbType.VarChar).Value = IE_vX12;
                com.Parameters.Add("@IE_vX13", SqlDbType.VarChar).Value = IE_vX13;


                com.Parameters.Add("@IE_vX4", SqlDbType.VarChar).Value = IE_vX4;
                com.Parameters.Add("@IE_vX5", SqlDbType.VarChar).Value = IE_vX5;
                com.Parameters.Add("@IE_vX6", SqlDbType.VarChar).Value = IE_vX6;
                com.Parameters.Add("@IE_vX7", SqlDbType.VarChar).Value = IE_vX7;
                com.Parameters.Add("@IE_vX8", SqlDbType.VarChar).Value = IE_vX8;
                com.Parameters.Add("@IE_vX9", SqlDbType.VarChar).Value = IE_vX9;
                com.Parameters.Add("@IE_vResult", SqlDbType.VarChar).Value = IE_vResult;
                com.Parameters.Add("@IE_User", SqlDbType.VarChar).Value = IE_User;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Inspection_Entry(
       string IE_Iid,
       string IE_iItem_ID,
       string IE_vShift,
       string IE_vInspectionDate,
       string IE_InspectionID,
       string IE_vReference,
       string IE_iSNo,
       string IE_vPoints,
       string IE_vControl,
       string IE_vX1,
       string IE_vX2,
       string IE_vX3,
       string IE_vX4,
       string IE_vX5,
       string IE_vX6,
       string IE_vX7,
       string IE_vX8,
       string IE_vX9,
       string IE_vResult,
       String IE_User
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Inspection_Entry";
                com.Parameters.Add("@IE_Iid", SqlDbType.VarChar).Value = IE_Iid;
                com.Parameters.Add("@IE_iItem_ID", SqlDbType.VarChar).Value = IE_iItem_ID;
                com.Parameters.Add("@IE_vShift", SqlDbType.VarChar).Value = IE_vShift;
                com.Parameters.Add("@IE_vInspectionDate", SqlDbType.VarChar).Value = System.DateTime.Now.ToString("dd-MMM-yyy");
                com.Parameters.Add("@IE_InspectionID", SqlDbType.VarChar).Value = IE_InspectionID;
                com.Parameters.Add("@IE_vReference", SqlDbType.VarChar).Value = IE_vReference;
                com.Parameters.Add("@IE_iSNo", SqlDbType.VarChar).Value = IE_iSNo;
                com.Parameters.Add("@IE_vPoints", SqlDbType.VarChar).Value = IE_vPoints;
                com.Parameters.Add("@IE_vControl", SqlDbType.VarChar).Value = IE_vControl;
                com.Parameters.Add("@IE_vX1", SqlDbType.VarChar).Value = IE_vX1;
                com.Parameters.Add("@IE_vX2", SqlDbType.VarChar).Value = IE_vX2;
                com.Parameters.Add("@IE_vX3", SqlDbType.VarChar).Value = IE_vX3;
                com.Parameters.Add("@IE_vX4", SqlDbType.VarChar).Value = IE_vX4;
                com.Parameters.Add("@IE_vX5", SqlDbType.VarChar).Value = IE_vX5;
                com.Parameters.Add("@IE_vX6", SqlDbType.VarChar).Value = IE_vX6;
                com.Parameters.Add("@IE_vX7", SqlDbType.VarChar).Value = IE_vX7;
                com.Parameters.Add("@IE_vX8", SqlDbType.VarChar).Value = IE_vX8;
                com.Parameters.Add("@IE_vX9", SqlDbType.VarChar).Value = IE_vX9;
                com.Parameters.Add("@IE_vResult", SqlDbType.VarChar).Value = IE_vResult;
                com.Parameters.Add("@IE_User", SqlDbType.VarChar).Value = IE_User;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        [WebMethod]
        public static string Pr_Delete_FeedBack(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_FeedBack '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_FeedBack(
       string FB_vTitle,
       string FB_vDescription,
       String FB_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_FeedBack";
                com.Parameters.Add("@FB_vTitle", SqlDbType.VarChar).Value = FB_vTitle;
                com.Parameters.Add("@FB_vDescription", SqlDbType.VarChar).Value = FB_vDescription;
                com.Parameters.Add("@FB_vCreatedBy", SqlDbType.VarChar).Value = FB_vCreatedBy;
                com.ExecuteNonQuery();
                dbFunctions.send_MailG("client.support@larch.in", "Feedback Raized  Title :" + FB_vTitle, FB_vDescription, "");

                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_FeedBack(
       string FB_iID,
       string FB_vTitle,
       string FB_vDescription,
       String FB_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_FeedBack";
                com.Parameters.Add("@FB_iID", SqlDbType.VarChar).Value = FB_iID;
                com.Parameters.Add("@FB_vTitle", SqlDbType.VarChar).Value = FB_vTitle;
                com.Parameters.Add("@FB_vDescription", SqlDbType.VarChar).Value = FB_vDescription;
                com.Parameters.Add("@FB_vCreatedBy", SqlDbType.VarChar).Value = FB_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();

                dbFunctions.send_MailG("client.support@larch.in", "Feedback Updated  Title :" + FB_vTitle, FB_vDescription, "");

                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        [WebMethod]
        public static string Pr_Delete_Support_Ticket(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Support_Ticket '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Support_Ticket(
       string ST_vPriority,
       string ST_vSubject,
       string ST_vDescripiton,
       string ST_vTicket_Status,
       String ST_vCreatedby
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Support_Ticket";
                com.Parameters.Add("@ST_vPriority", SqlDbType.VarChar).Value = ST_vPriority;
                com.Parameters.Add("@ST_vSubject", SqlDbType.VarChar).Value = ST_vSubject;
                com.Parameters.Add("@ST_vDescripiton", SqlDbType.VarChar).Value = ST_vDescripiton;
                com.Parameters.Add("@ST_vTicket_Status", SqlDbType.VarChar).Value = ST_vTicket_Status;
                com.Parameters.Add("@ST_vCreatedby", SqlDbType.VarChar).Value = ST_vCreatedby;
                com.ExecuteNonQuery();

                dbFunctions.send_MailG("client.support@larch.in", "Support Ticket Raized -" + ST_vSubject, ST_vDescripiton,"");
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Support_Ticket(
       string ST_iID,
       string ST_vPriority,
       string ST_vSubject,
       string ST_vDescripiton,
       string ST_vTicket_Status,
       String ST_vCreatedby
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Support_Ticket";
                com.Parameters.Add("@ST_iID", SqlDbType.VarChar).Value = ST_iID;
                com.Parameters.Add("@ST_vPriority", SqlDbType.VarChar).Value = ST_vPriority;
                com.Parameters.Add("@ST_vSubject", SqlDbType.VarChar).Value = ST_vSubject;
                com.Parameters.Add("@ST_vDescripiton", SqlDbType.VarChar).Value = ST_vDescripiton;
                com.Parameters.Add("@ST_vTicket_Status", SqlDbType.VarChar).Value = ST_vTicket_Status;
                com.Parameters.Add("@ST_vCreatedby", SqlDbType.VarChar).Value = ST_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();

                dbFunctions.send_MailG("client.support@larch.in", "Support Ticket Updated - Status : " + ST_vTicket_Status +" \n Subject :"+ ST_vSubject, ST_vDescripiton, "");
         
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Delete_Idle_Type_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Idle_Type_Master '" + ID + "'");
            return "True";
        }



        [WebMethod]
        public static string Pr_Delete_MenuMaster(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_MenuMaster '" + ID + "'");
            return "True";
        }



        [WebMethod]
        public static string Pr_Insert_MenuRightts(
       string MR_iUser_ID,
       string MR_iMenu_ID,
       string MR_iVisible_Status,
       String MR_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_MenuRightts";
                com.Parameters.Add("@MR_iUser_ID", SqlDbType.VarChar).Value = MR_iUser_ID;
                com.Parameters.Add("@MR_iMenu_ID", SqlDbType.VarChar).Value = MR_iMenu_ID;
                com.Parameters.Add("@MR_iVisible_Status", SqlDbType.VarChar).Value = MR_iVisible_Status;
                com.Parameters.Add("@MR_vCreatedBy", SqlDbType.VarChar).Value = MR_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Insert_MenuMaster(
       string MM_vMenu_Name,
       string MM_iMenu_Order,
       string MM_iMenuLevel,
       string MM_iParentID,
       string MM_vURL,
       string MM_vIcon,
       String MM_vCreatedby
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_MenuMaster";
                com.Parameters.Add("@MM_vMenu_Name", SqlDbType.VarChar).Value = MM_vMenu_Name;
                com.Parameters.Add("@MM_iMenu_Order", SqlDbType.VarChar).Value = MM_iMenu_Order;
                com.Parameters.Add("@MM_iMenuLevel", SqlDbType.VarChar).Value = MM_iMenuLevel;
                com.Parameters.Add("@MM_iParentID", SqlDbType.VarChar).Value = MM_iParentID;
                com.Parameters.Add("@MM_vURL", SqlDbType.VarChar).Value = MM_vURL;
                com.Parameters.Add("@MM_vIcon", SqlDbType.VarChar).Value = MM_vIcon;
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








        [WebMethod]
        public static string Pr_Insert_Inspection_Details(
       string IN_iItem_Id,
       string IN_vReference,
       string IN_iOrderNo,
       string IN_vPoints,
       string IN_vControls,
       string IN_Inpection_Type,
       string IN_vEqual_Value,
       string IN_iMin,
       string IN_iMax,
       String IN_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Inspection_Details";
                com.Parameters.Add("@IN_iItem_Id", SqlDbType.VarChar).Value = IN_iItem_Id;
                com.Parameters.Add("@IN_vReference", SqlDbType.VarChar).Value = IN_vReference;
                com.Parameters.Add("@IN_iOrderNo", SqlDbType.VarChar).Value = IN_iOrderNo;
                com.Parameters.Add("@IN_vPoints", SqlDbType.VarChar).Value = IN_vPoints;
                com.Parameters.Add("@IN_vControls", SqlDbType.VarChar).Value = IN_vControls;
                com.Parameters.Add("@IN_Inpection_Type", SqlDbType.VarChar).Value = IN_Inpection_Type;
                com.Parameters.Add("@IN_vEqual_Value", SqlDbType.VarChar).Value = IN_vEqual_Value;
                com.Parameters.Add("@IN_iMin", SqlDbType.VarChar).Value = IN_iMin;
                com.Parameters.Add("@IN_iMax", SqlDbType.VarChar).Value = IN_iMax;
                com.Parameters.Add("@IN_vUser", SqlDbType.VarChar).Value = IN_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Inspection_Details(
       string IN_iId,
       string IN_iItem_Id,
       string IN_vReference,
       string IN_iOrderNo,
       string IN_vPoints,
       string IN_vControls,
       string IN_Inpection_Type,
       string IN_vEqual_Value,
       string IN_iMin,
       string IN_iMax,
       String IN_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Inspection_Details";
                com.Parameters.Add("@IN_iId", SqlDbType.VarChar).Value = IN_iId;
                com.Parameters.Add("@IN_iItem_Id", SqlDbType.VarChar).Value = IN_iItem_Id;
                com.Parameters.Add("@IN_vReference", SqlDbType.VarChar).Value = IN_vReference;
                com.Parameters.Add("@IN_iOrderNo", SqlDbType.VarChar).Value = IN_iOrderNo;
                com.Parameters.Add("@IN_vPoints", SqlDbType.VarChar).Value = IN_vPoints;
                com.Parameters.Add("@IN_vControls", SqlDbType.VarChar).Value = IN_vControls;
                com.Parameters.Add("@IN_Inpection_Type", SqlDbType.VarChar).Value = IN_Inpection_Type;
                com.Parameters.Add("@IN_vEqual_Value", SqlDbType.VarChar).Value = IN_vEqual_Value;
                com.Parameters.Add("@IN_iMin", SqlDbType.VarChar).Value = IN_iMin;
                com.Parameters.Add("@IN_iMax", SqlDbType.VarChar).Value = IN_iMax;
                com.Parameters.Add("@IN_vUser", SqlDbType.VarChar).Value = IN_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Update_MenuMaster(
       string MM_iId,
       string MM_vMenu_Name,
       string MM_iMenu_Order,
       string MM_iMenuLevel,
       string MM_iParentID,
       string MM_vURL,
       string MM_vIcon,
       String MM_vCreatedby
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_MenuMaster";
                com.Parameters.Add("@MM_iId", SqlDbType.VarChar).Value = MM_iId;
                com.Parameters.Add("@MM_vMenu_Name", SqlDbType.VarChar).Value = MM_vMenu_Name;
                com.Parameters.Add("@MM_iMenu_Order", SqlDbType.VarChar).Value = MM_iMenu_Order;
                com.Parameters.Add("@MM_iMenuLevel", SqlDbType.VarChar).Value = MM_iMenuLevel;
                com.Parameters.Add("@MM_iParentID", SqlDbType.VarChar).Value = MM_iParentID;
                com.Parameters.Add("@MM_vURL", SqlDbType.VarChar).Value = MM_vURL;
                com.Parameters.Add("@MM_vIcon", SqlDbType.VarChar).Value = MM_vIcon;
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




        [WebMethod]
        public static string Pr_Delete_Mould_Usage(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Mould_Usage '" + ID + "'");
            return "True";
        }






        [WebMethod]
        public static string Pr_Delete_Rejection_Details(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Rejection_Details '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Rejection_Details(
       string RD_iPlan_No,
       string RD_iRejectedQty,
       string RD_iDefect_Type,
       string RD_vRemarks,
       String RD_vCreatedby
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Rejection_Details";
                com.Parameters.Add("@RD_iPlan_No", SqlDbType.VarChar).Value = RD_iPlan_No;
                com.Parameters.Add("@RD_iRejectedQty", SqlDbType.VarChar).Value = RD_iRejectedQty;
                com.Parameters.Add("@RD_iDefect_Type", SqlDbType.VarChar).Value = RD_iDefect_Type;
                com.Parameters.Add("@RD_vRemarks", SqlDbType.VarChar).Value = RD_vRemarks;
                com.Parameters.Add("@RD_vCreatedby", SqlDbType.VarChar).Value = RD_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Rejection_Details(
       string RD_iID,
       string RD_iPlan_No,
       string RD_iRejectedQty,
       string RD_iDefect_Type,
       string RD_vRemarks,
       String RD_vCreatedby
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Rejection_Details";
                com.Parameters.Add("@RD_iID", SqlDbType.VarChar).Value = RD_iID;
                com.Parameters.Add("@RD_iPlan_No", SqlDbType.VarChar).Value = RD_iPlan_No;
                com.Parameters.Add("@RD_iRejectedQty", SqlDbType.VarChar).Value = RD_iRejectedQty;
                com.Parameters.Add("@RD_iDefect_Type", SqlDbType.VarChar).Value = RD_iDefect_Type;
                com.Parameters.Add("@RD_vRemarks", SqlDbType.VarChar).Value = RD_vRemarks;
                com.Parameters.Add("@RD_vCreatedby", SqlDbType.VarChar).Value = RD_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        [WebMethod]
        public static string Pr_Insert_Mould_Usage(
       string MU_iPlan_ID,
       string MU_iMouldID,
       String MU_vCreatedby
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Mould_Usage";
                com.Parameters.Add("@MU_iPlan_ID", SqlDbType.VarChar).Value = MU_iPlan_ID;
                com.Parameters.Add("@MU_iMouldID", SqlDbType.VarChar).Value = MU_iMouldID;
                com.Parameters.Add("@MU_vCreatedby", SqlDbType.VarChar).Value = MU_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Mould_Usage(
       string MU_iID,
       string MU_iPlan_ID,
       string MU_iMouldID,
       String MU_vCreatedby
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Mould_Usage";
                com.Parameters.Add("@MU_iID", SqlDbType.VarChar).Value = MU_iID;
                com.Parameters.Add("@MU_iPlan_ID", SqlDbType.VarChar).Value = MU_iPlan_ID;
                com.Parameters.Add("@MU_iMouldID", SqlDbType.VarChar).Value = MU_iMouldID;
                com.Parameters.Add("@MU_vCreatedby", SqlDbType.VarChar).Value = MU_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Delete_Plan_Entry(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Plan_Entry '" + ID + "'");
            return "True";
        }




        [WebMethod]
        public static string Pr_Delete_ItemMaster(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_ItemMaster '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_ItemMaster(
       string IM_vERP_Code,
       string IM_vExternal_PartNo,
       string IM_vInternal_PartNo,
       string IM_vPart_Name,
       string IM_vModel,
       string IM_iCategory,
       string IM_vUOM_ID,
       string IM_vStockPerday,
       string IM_vBufferDay,
       string IM_vImage,
       String IM_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_ItemMaster";
                com.Parameters.Add("@IM_vERP_Code", SqlDbType.VarChar).Value = IM_vERP_Code;
                com.Parameters.Add("@IM_vExternal_PartNo", SqlDbType.VarChar).Value = IM_vExternal_PartNo;
                com.Parameters.Add("@IM_vInternal_PartNo", SqlDbType.VarChar).Value = IM_vInternal_PartNo;
                com.Parameters.Add("@IM_vPart_Name", SqlDbType.VarChar).Value = IM_vPart_Name;
                com.Parameters.Add("@IM_vModel", SqlDbType.VarChar).Value = IM_vModel;
                com.Parameters.Add("@IM_iCategory", SqlDbType.VarChar).Value = IM_iCategory;
                com.Parameters.Add("@IM_vUOM_ID", SqlDbType.VarChar).Value = IM_vUOM_ID;
                com.Parameters.Add("@IM_vStockPerday", SqlDbType.VarChar).Value = IM_vStockPerday;
                com.Parameters.Add("@IM_vBufferDay", SqlDbType.VarChar).Value = IM_vBufferDay;
                com.Parameters.Add("@IM_vImage", SqlDbType.VarChar).Value = IM_vImage;
                com.Parameters.Add("@IM_vUser", SqlDbType.VarChar).Value = IM_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        [WebMethod]
        public static string Pr_Delete_Contributor_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Contributor_Master '" + ID + "'");
            return "True";
        }



        [WebMethod]
        public static string Pr_Delete_Daily_Check_Sheet(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Daily_Check_Sheet '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Daily_Check_Sheet(
       string DS_iOrder,
       string DS_vCheck_Point,
       string DS_vDescription,
       String DS_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Daily_Check_Sheet";
                com.Parameters.Add("@DS_iOrder", SqlDbType.VarChar).Value = DS_iOrder;
                com.Parameters.Add("@DS_vCheck_Point", SqlDbType.VarChar).Value = DS_vCheck_Point;
                com.Parameters.Add("@DS_vDescription", SqlDbType.VarChar).Value = DS_vDescription;
                com.Parameters.Add("@DS_vUser", SqlDbType.VarChar).Value = DS_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Delete_Mould_Assignment(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Mould_Assignment '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Mould_Assignment(
       string MA_iMouldID,
       string MA_iPartNo,
       String MA_cUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Mould_Assignment";
                com.Parameters.Add("@MA_iMouldID", SqlDbType.VarChar).Value = MA_iMouldID;
                com.Parameters.Add("@MA_iPartNo", SqlDbType.VarChar).Value = MA_iPartNo;
                com.Parameters.Add("@MA_cUser", SqlDbType.VarChar).Value = MA_cUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Mould_Assignment(
       string MA_iid,
       string MA_iMouldID,
       string MA_iPartNo,
       String MA_cUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Mould_Assignment";
                com.Parameters.Add("@MA_iid", SqlDbType.VarChar).Value = MA_iid;
                com.Parameters.Add("@MA_iMouldID", SqlDbType.VarChar).Value = MA_iMouldID;
                com.Parameters.Add("@MA_iPartNo", SqlDbType.VarChar).Value = MA_iPartNo;
                com.Parameters.Add("@MA_cUser", SqlDbType.VarChar).Value = MA_cUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Update_Daily_Check_Sheet(
       string DS_iid,
       string DS_iOrder,
       string DS_vCheck_Point,
       string DS_vDescription,
       String DS_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Daily_Check_Sheet";
                com.Parameters.Add("@DS_iid", SqlDbType.VarChar).Value = DS_iid;
                com.Parameters.Add("@DS_iOrder", SqlDbType.VarChar).Value = DS_iOrder;
                com.Parameters.Add("@DS_vCheck_Point", SqlDbType.VarChar).Value = DS_vCheck_Point;
                com.Parameters.Add("@DS_vDescription", SqlDbType.VarChar).Value = DS_vDescription;
                com.Parameters.Add("@DS_vUser", SqlDbType.VarChar).Value = DS_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Insert_Contributor_Master(
       string CM_vERP_Code,
       string CM_vContributor_Code,
       string CM_vContributor_Name,
       string CM_vBilling_Name,
       string CM_vAddress,
       string CM_vBilling_Address,
       string CM_vGroup,
       string CM_vCity_Name,
       string CM_vState,
       string CM_vCountry,
       string CM_vPincode,
       string CM_vContactNo1,
       string CM_vContractNo2,
       string CM_vType,
       string CM_vEmail_ID,
       string CM_vWebsite,
       string CM_vPAN_No,
       string CM_vTIN_No,
       string CM_vCST_No,
       string CM_vECCNo,
       string CM_vCon_Type,
       String CM_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Contributor_Master";
                com.Parameters.Add("@CM_vERP_Code", SqlDbType.VarChar).Value = CM_vERP_Code;
                com.Parameters.Add("@CM_vContributor_Code", SqlDbType.VarChar).Value = CM_vContributor_Code;
                com.Parameters.Add("@CM_vContributor_Name", SqlDbType.VarChar).Value = CM_vContributor_Name;
                com.Parameters.Add("@CM_vBilling_Name", SqlDbType.VarChar).Value = CM_vBilling_Name;
                com.Parameters.Add("@CM_vAddress", SqlDbType.VarChar).Value = CM_vAddress;
                com.Parameters.Add("@CM_vBilling_Address", SqlDbType.VarChar).Value = CM_vBilling_Address;
                com.Parameters.Add("@CM_vGroup", SqlDbType.VarChar).Value = CM_vGroup;
                com.Parameters.Add("@CM_vCity_Name", SqlDbType.VarChar).Value = CM_vCity_Name;
                com.Parameters.Add("@CM_vState", SqlDbType.VarChar).Value = CM_vState;
                com.Parameters.Add("@CM_vCountry", SqlDbType.VarChar).Value = CM_vCountry;
                com.Parameters.Add("@CM_vPincode", SqlDbType.VarChar).Value = CM_vPincode;
                com.Parameters.Add("@CM_vContactNo1", SqlDbType.VarChar).Value = CM_vContactNo1;
                com.Parameters.Add("@CM_vContractNo2", SqlDbType.VarChar).Value = CM_vContractNo2;
                com.Parameters.Add("@CM_vType", SqlDbType.VarChar).Value = CM_vType;
                com.Parameters.Add("@CM_vEmail_ID", SqlDbType.VarChar).Value = CM_vEmail_ID;
                com.Parameters.Add("@CM_vWebsite", SqlDbType.VarChar).Value = CM_vWebsite;
                com.Parameters.Add("@CM_vPAN_No", SqlDbType.VarChar).Value = CM_vPAN_No;
                com.Parameters.Add("@CM_vTIN_No", SqlDbType.VarChar).Value = CM_vTIN_No;
                com.Parameters.Add("@CM_vCST_No", SqlDbType.VarChar).Value = CM_vCST_No;
                com.Parameters.Add("@CM_vECCNo", SqlDbType.VarChar).Value = CM_vECCNo;
                com.Parameters.Add("@CM_vCon_Type", SqlDbType.VarChar).Value = CM_vCon_Type;
                com.Parameters.Add("@CM_vUser", SqlDbType.VarChar).Value = CM_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Delete_Preventive_Check_Sheet(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Preventive_Check_Sheet '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Preventive_Check_Sheet(
       string PS_iOrder_No,
       string PS_vClass,
       string PS_vDescription,
       string PS_vStandard,
       string PS_vCheck_Method,
       string PS_vAction_if_Abnormal,
       string PS_vCorrective_Tool,
       String PS_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Preventive_Check_Sheet";
                com.Parameters.Add("@PS_iOrder_No", SqlDbType.VarChar).Value = PS_iOrder_No;
                com.Parameters.Add("@PS_vClass", SqlDbType.VarChar).Value = PS_vClass;
                com.Parameters.Add("@PS_vDescription", SqlDbType.VarChar).Value = PS_vDescription;
                com.Parameters.Add("@PS_vStandard", SqlDbType.VarChar).Value = PS_vStandard;
                com.Parameters.Add("@PS_vCheck_Method", SqlDbType.VarChar).Value = PS_vCheck_Method;
                com.Parameters.Add("@PS_vAction_if_Abnormal", SqlDbType.VarChar).Value = PS_vAction_if_Abnormal;
                com.Parameters.Add("@PS_vCorrective_Tool", SqlDbType.VarChar).Value = PS_vCorrective_Tool;
                com.Parameters.Add("@PS_vUser", SqlDbType.VarChar).Value = PS_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Preventive_Check_Sheet(
       string PS_iid,
       string PS_iOrder_No,
       string PS_vClass,
       string PS_vDescription,
       string PS_vStandard,
       string PS_vCheck_Method,
       string PS_vAction_if_Abnormal,
       string PS_vCorrective_Tool,
       String PS_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Preventive_Check_Sheet";
                com.Parameters.Add("@PS_iid", SqlDbType.VarChar).Value = PS_iid;
                com.Parameters.Add("@PS_iOrder_No", SqlDbType.VarChar).Value = PS_iOrder_No;
                com.Parameters.Add("@PS_vClass", SqlDbType.VarChar).Value = PS_vClass;
                com.Parameters.Add("@PS_vDescription", SqlDbType.VarChar).Value = PS_vDescription;
                com.Parameters.Add("@PS_vStandard", SqlDbType.VarChar).Value = PS_vStandard;
                com.Parameters.Add("@PS_vCheck_Method", SqlDbType.VarChar).Value = PS_vCheck_Method;
                com.Parameters.Add("@PS_vAction_if_Abnormal", SqlDbType.VarChar).Value = PS_vAction_if_Abnormal;
                com.Parameters.Add("@PS_vCorrective_Tool", SqlDbType.VarChar).Value = PS_vCorrective_Tool;
                com.Parameters.Add("@PS_vUser", SqlDbType.VarChar).Value = PS_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Contributor_Master(
       string CM_iid,
       string CM_vERP_Code,
       string CM_vContributor_Code,
       string CM_vContributor_Name,
       string CM_vBilling_Name,
       string CM_vAddress,
       string CM_vBilling_Address,
       string CM_vGroup,
       string CM_vCity_Name,
       string CM_vState,
       string CM_vCountry,
       string CM_vPincode,
       string CM_vContactNo1,
       string CM_vContractNo2,
       string CM_vType,
       string CM_vEmail_ID,
       string CM_vWebsite,
       string CM_vPAN_No,
       string CM_vTIN_No,
       string CM_vCST_No,
       string CM_vECCNo,
       string CM_vCon_Type,
       String CM_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Contributor_Master";
                com.Parameters.Add("@CM_iid", SqlDbType.VarChar).Value = CM_iid;
                com.Parameters.Add("@CM_vERP_Code", SqlDbType.VarChar).Value = CM_vERP_Code;
                com.Parameters.Add("@CM_vContributor_Code", SqlDbType.VarChar).Value = CM_vContributor_Code;
                com.Parameters.Add("@CM_vContributor_Name", SqlDbType.VarChar).Value = CM_vContributor_Name;
                com.Parameters.Add("@CM_vBilling_Name", SqlDbType.VarChar).Value = CM_vBilling_Name;
                com.Parameters.Add("@CM_vAddress", SqlDbType.VarChar).Value = CM_vAddress;
                com.Parameters.Add("@CM_vBilling_Address", SqlDbType.VarChar).Value = CM_vBilling_Address;
                com.Parameters.Add("@CM_vGroup", SqlDbType.VarChar).Value = CM_vGroup;
                com.Parameters.Add("@CM_vCity_Name", SqlDbType.VarChar).Value = CM_vCity_Name;
                com.Parameters.Add("@CM_vState", SqlDbType.VarChar).Value = CM_vState;
                com.Parameters.Add("@CM_vCountry", SqlDbType.VarChar).Value = CM_vCountry;
                com.Parameters.Add("@CM_vPincode", SqlDbType.VarChar).Value = CM_vPincode;
                com.Parameters.Add("@CM_vContactNo1", SqlDbType.VarChar).Value = CM_vContactNo1;
                com.Parameters.Add("@CM_vContractNo2", SqlDbType.VarChar).Value = CM_vContractNo2;
                com.Parameters.Add("@CM_vType", SqlDbType.VarChar).Value = CM_vType;
                com.Parameters.Add("@CM_vEmail_ID", SqlDbType.VarChar).Value = CM_vEmail_ID;
                com.Parameters.Add("@CM_vWebsite", SqlDbType.VarChar).Value = CM_vWebsite;
                com.Parameters.Add("@CM_vPAN_No", SqlDbType.VarChar).Value = CM_vPAN_No;
                com.Parameters.Add("@CM_vTIN_No", SqlDbType.VarChar).Value = CM_vTIN_No;
                com.Parameters.Add("@CM_vCST_No", SqlDbType.VarChar).Value = CM_vCST_No;
                com.Parameters.Add("@CM_vECCNo", SqlDbType.VarChar).Value = CM_vECCNo;
                com.Parameters.Add("@CM_vCon_Type", SqlDbType.VarChar).Value = CM_vCon_Type;
                com.Parameters.Add("@CM_vUser", SqlDbType.VarChar).Value = CM_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public static string Pr_Update_ItemMaster(
       string IM_iid,
       string IM_vERP_Code,
       string IM_vExternal_PartNo,
       string IM_vInternal_PartNo,
       string IM_vPart_Name,
       string IM_vModel,
       string IM_iCategory,
       string IM_vUOM_ID,
       string IM_vStockPerday,
       string IM_vBufferDay,
       string IM_vImage,
       string IM_vUser,
            string IM_vStatus
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_ItemMaster";
                com.Parameters.Add("@IM_iid", SqlDbType.VarChar).Value = IM_iid;
                com.Parameters.Add("@IM_vERP_Code", SqlDbType.VarChar).Value = IM_vERP_Code;
                com.Parameters.Add("@IM_vExternal_PartNo", SqlDbType.VarChar).Value = IM_vExternal_PartNo;
                com.Parameters.Add("@IM_vInternal_PartNo", SqlDbType.VarChar).Value = IM_vInternal_PartNo;
                com.Parameters.Add("@IM_vPart_Name", SqlDbType.VarChar).Value = IM_vPart_Name;
                com.Parameters.Add("@IM_vModel", SqlDbType.VarChar).Value = IM_vModel;
                com.Parameters.Add("@IM_iCategory", SqlDbType.VarChar).Value = IM_iCategory;
                com.Parameters.Add("@IM_vUOM_ID", SqlDbType.VarChar).Value = IM_vUOM_ID;
                com.Parameters.Add("@IM_vStockPerday", SqlDbType.VarChar).Value = IM_vStockPerday;
                com.Parameters.Add("@IM_vBufferDay", SqlDbType.VarChar).Value = IM_vBufferDay;
                com.Parameters.Add("@IM_vImage", SqlDbType.VarChar).Value = IM_vImage;
                com.Parameters.Add("@IM_vUser", SqlDbType.VarChar).Value = IM_vUser;
                com.Parameters.Add("@IM_vStatus", SqlDbType.VarChar).Value = (IM_vStatus=="Active")?'A':'D';
                
                com.ExecuteNonQuery();
                con.Close();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Insert_Plan_Entry(
       string PD_dDate,
       string PD_vShift,
       string PD_dFrom,
       string PD_dTo_Time,
       string PD_iMachine,
       string PD_iPart_No,
       string PD_vMould,
       string PD_iQuantity,
       string PD_vRemarks,
       String PD_vCreated_By
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Plan_Entry";
                com.Parameters.Add("@PD_dDate", SqlDbType.VarChar).Value = PD_dDate;
                com.Parameters.Add("@PD_vShift", SqlDbType.VarChar).Value = PD_vShift;
                com.Parameters.Add("@PD_dFrom", SqlDbType.VarChar).Value = PD_dFrom;
                com.Parameters.Add("@PD_dTo_Time", SqlDbType.VarChar).Value = PD_dTo_Time;
                com.Parameters.Add("@PD_iMachine", SqlDbType.VarChar).Value = PD_iMachine;
                com.Parameters.Add("@PD_iPart_No", SqlDbType.VarChar).Value = PD_iPart_No;
                com.Parameters.Add("@PD_vMould", SqlDbType.VarChar).Value = PD_vMould;
                com.Parameters.Add("@PD_iQuantity", SqlDbType.VarChar).Value = PD_iQuantity;
                com.Parameters.Add("@PD_vRemarks", SqlDbType.VarChar).Value = PD_vRemarks;
                com.Parameters.Add("@PD_vCreated_By", SqlDbType.VarChar).Value = PD_vCreated_By;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Delete_UOM_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_UOM_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_UOM_Master(
       string UM_vUOM_Name,
       String UM_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_UOM_Master";
                com.Parameters.Add("@UM_vUOM_Name", SqlDbType.VarChar).Value = UM_vUOM_Name;
                com.Parameters.Add("@UM_vUser", SqlDbType.VarChar).Value = UM_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        [WebMethod]
        public static string Pr_Delete_Country_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Country_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Country_Master(
       string CNT_vCountry,
       String CNT_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Country_Master";
                com.Parameters.Add("@CNT_vCountry", SqlDbType.VarChar).Value = CNT_vCountry;
                com.Parameters.Add("@CNT_vUser", SqlDbType.VarChar).Value = CNT_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Delete_Inspection_Details(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Inspection_Details '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Update_Country_Master(
       string CNT_iid,
       string CNT_vCountry,
       String CNT_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Country_Master";
                com.Parameters.Add("@CNT_iid", SqlDbType.VarChar).Value = CNT_iid;
                com.Parameters.Add("@CNT_vCountry", SqlDbType.VarChar).Value = CNT_vCountry;
                com.Parameters.Add("@CNT_vUser", SqlDbType.VarChar).Value = CNT_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_UOM_Master(
       string UM_iid,
       string UM_vUOM_Name,
       String UM_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_UOM_Master";
                com.Parameters.Add("@UM_iid", SqlDbType.VarChar).Value = UM_iid;
                com.Parameters.Add("@UM_vUOM_Name", SqlDbType.VarChar).Value = UM_vUOM_Name;
                com.Parameters.Add("@UM_vUser", SqlDbType.VarChar).Value = UM_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Plan_Entry(
       string PD_iId,
       string PD_dDate,
       string PD_vShift,
       string PD_dFrom,
       string PD_dTo_Time,
       string PD_iMachine,
       string PD_iPart_No,
       string PD_vMould,
       string PD_iQuantity,
       string PD_vRemarks,
       String PD_vCreated_By
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Plan_Entry";
                com.Parameters.Add("@PD_iId", SqlDbType.VarChar).Value = PD_iId;
                com.Parameters.Add("@PD_dDate", SqlDbType.VarChar).Value = PD_dDate;
                com.Parameters.Add("@PD_vShift", SqlDbType.VarChar).Value = PD_vShift;
                com.Parameters.Add("@PD_dFrom", SqlDbType.VarChar).Value = PD_dFrom;
                com.Parameters.Add("@PD_dTo_Time", SqlDbType.VarChar).Value = PD_dTo_Time;
                com.Parameters.Add("@PD_iMachine", SqlDbType.VarChar).Value = PD_iMachine;
                com.Parameters.Add("@PD_iPart_No", SqlDbType.VarChar).Value = PD_iPart_No;
                com.Parameters.Add("@PD_vMould", SqlDbType.VarChar).Value = PD_vMould;
                com.Parameters.Add("@PD_iQuantity", SqlDbType.VarChar).Value = PD_iQuantity;
                com.Parameters.Add("@PD_vRemarks", SqlDbType.VarChar).Value = PD_vRemarks;
                com.Parameters.Add("@PD_vCreated_By", SqlDbType.VarChar).Value = PD_vCreated_By;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Update_Plan_EntryAchived(
       string PD_iId,
       string PD_Achived
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Plan_EntryAchived";
                com.Parameters.Add("@PD_iId", SqlDbType.VarChar).Value = PD_iId;
                com.Parameters.Add("@PD_Achived", SqlDbType.VarChar).Value = PD_Achived;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }





        [WebMethod]
        public static string Pr_Delete_Defect_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Defect_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Defect_Master(
       string DF_vDefect_Type,
       string DF_vDescription,
       String DF_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Defect_Master";
                com.Parameters.Add("@DF_vDefect_Type", SqlDbType.VarChar).Value = DF_vDefect_Type;
                com.Parameters.Add("@DF_vDescription", SqlDbType.VarChar).Value = DF_vDescription;
                com.Parameters.Add("@DF_vCreatedBy", SqlDbType.VarChar).Value = DF_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "Defect type Saved Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Defect_Master(
       string DF_iID,
       string DF_vDefect_Type,
       string DF_vDescription,
       String DF_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Defect_Master";
                com.Parameters.Add("@DF_iID", SqlDbType.VarChar).Value = DF_iID;
                com.Parameters.Add("@DF_vDefect_Type", SqlDbType.VarChar).Value = DF_vDefect_Type;
                com.Parameters.Add("@DF_vDescription", SqlDbType.VarChar).Value = DF_vDescription;
                com.Parameters.Add("@DF_vCreatedBy", SqlDbType.VarChar).Value = DF_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        [WebMethod]
        public static string Pr_Delete_Shift_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Shift_Master '" + ID + "'");
            return "True";
        }



        [WebMethod]
        public static string Pr_Delete_Module_Assignment(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Module_Assignment '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Module_Assignment(
       string MA_vModuleName,
       string MA_iMenuID,
       String MA_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Module_Assignment";
                com.Parameters.Add("@MA_vModuleName", SqlDbType.VarChar).Value = MA_vModuleName;
                com.Parameters.Add("@MA_iMenuID", SqlDbType.VarChar).Value = MA_iMenuID;
                com.Parameters.Add("@MA_vUser", SqlDbType.VarChar).Value = MA_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Module_Assignment(
       string MA_iid,
       string MA_vModuleName,
       string MA_iMenuID,
       String MA_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Module_Assignment";
                com.Parameters.Add("@MA_iid", SqlDbType.VarChar).Value = MA_iid;
                com.Parameters.Add("@MA_vModuleName", SqlDbType.VarChar).Value = MA_vModuleName;
                com.Parameters.Add("@MA_iMenuID", SqlDbType.VarChar).Value = MA_iMenuID;
                com.Parameters.Add("@MA_vUser", SqlDbType.VarChar).Value = MA_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Insert_Shift_Master(
       string SF_vCode,
       string SF_vName,
       string SF_dFrom_Time,
       string SF_dTo_Time,
       string SF_dFirst_Off_End_Time,
       string SF_dMiddle_Off_End_Time,
       string SF_dLast_Off_End_time,
       String SF_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Shift_Master";
                com.Parameters.Add("@SF_vCode", SqlDbType.VarChar).Value = SF_vCode;
                com.Parameters.Add("@SF_vName", SqlDbType.VarChar).Value = SF_vName;
                com.Parameters.Add("@SF_dFrom_Time", SqlDbType.VarChar).Value = SF_dFrom_Time;
                com.Parameters.Add("@SF_dTo_Time", SqlDbType.VarChar).Value = SF_dTo_Time;
                com.Parameters.Add("@SF_dFirst_Off_End_Time", SqlDbType.VarChar).Value = SF_dFirst_Off_End_Time;
                com.Parameters.Add("@SF_dMiddle_Off_End_Time", SqlDbType.VarChar).Value = SF_dMiddle_Off_End_Time;
                com.Parameters.Add("@SF_dLast_Off_End_time", SqlDbType.VarChar).Value = SF_dLast_Off_End_time;
                com.Parameters.Add("@SF_vUser", SqlDbType.VarChar).Value = SF_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Shift_Master(
       string SF_iID,
       string SF_vCode,
       string SF_vName,
       string SF_dFrom_Time,
       string SF_dTo_Time,
       string SF_dFirst_Off_End_Time,
       string SF_dMiddle_Off_End_Time,
       string SF_dLast_Off_End_time,
       String SF_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Shift_Master";
                com.Parameters.Add("@SF_iID", SqlDbType.VarChar).Value = SF_iID;
                com.Parameters.Add("@SF_vCode", SqlDbType.VarChar).Value = SF_vCode;
                com.Parameters.Add("@SF_vName", SqlDbType.VarChar).Value = SF_vName;
                com.Parameters.Add("@SF_dFrom_Time", SqlDbType.VarChar).Value = SF_dFrom_Time;
                com.Parameters.Add("@SF_dTo_Time", SqlDbType.VarChar).Value = SF_dTo_Time;
                com.Parameters.Add("@SF_dFirst_Off_End_Time", SqlDbType.VarChar).Value = SF_dFirst_Off_End_Time;
                com.Parameters.Add("@SF_dMiddle_Off_End_Time", SqlDbType.VarChar).Value = SF_dMiddle_Off_End_Time;
                com.Parameters.Add("@SF_dLast_Off_End_time", SqlDbType.VarChar).Value = SF_dLast_Off_End_time;
                com.Parameters.Add("@SF_vUser", SqlDbType.VarChar).Value = SF_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Delete_CityMaster(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_CityMaster '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_CityMaster(
       string CT_vCity_Name,
       String CT_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_CityMaster";
                com.Parameters.Add("@CT_vCity_Name", SqlDbType.VarChar).Value = CT_vCity_Name;
                com.Parameters.Add("@CT_vUser", SqlDbType.VarChar).Value = CT_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_CityMaster(
       string CT_iid,
       string CT_vCity_Name,
       String CT_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_CityMaster";
                com.Parameters.Add("@CT_iid", SqlDbType.VarChar).Value = CT_iid;
                com.Parameters.Add("@CT_vCity_Name", SqlDbType.VarChar).Value = CT_vCity_Name;
                com.Parameters.Add("@CT_vUser", SqlDbType.VarChar).Value = CT_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Plan_EntryRejected(
       string PD_iId,
       string PD_Rejected,
            string PD_vRemarks
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Plan_EntryRejected";
                com.Parameters.Add("@PD_iId", SqlDbType.VarChar).Value = PD_iId;
                com.Parameters.Add("@PD_Rejected", SqlDbType.VarChar).Value = PD_Rejected;
                com.Parameters.Add("@PD_vRemarks", SqlDbType.VarChar).Value = PD_vRemarks;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Delete_Category_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Category_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Category_Master(
       string CA_vCategoryName,
       String CA_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Category_Master";
                com.Parameters.Add("@CA_vCategoryName", SqlDbType.VarChar).Value = CA_vCategoryName;
                com.Parameters.Add("@CA_vUser", SqlDbType.VarChar).Value = CA_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Category_Master(
       string CA_iid,
       string CA_vCategoryName,
       String CA_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Category_Master";
                com.Parameters.Add("@CA_iid", SqlDbType.VarChar).Value = CA_iid;
                com.Parameters.Add("@CA_vCategoryName", SqlDbType.VarChar).Value = CA_vCategoryName;
                com.Parameters.Add("@CA_vUser", SqlDbType.VarChar).Value = CA_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Delete_item_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_item_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string Pr_Insert_item_Master(
       string IM_vCode,
       string IM_vPartName,
       string IM_vModel,
       string IM_iImage1,
       string IM_iImage2,
       string IM_image3,
       string IM_iimage4,
       String IM_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_item_Master";
                com.Parameters.Add("@IM_vCode", SqlDbType.VarChar).Value = IM_vCode;
                com.Parameters.Add("@IM_vPartName", SqlDbType.VarChar).Value = IM_vPartName;
                com.Parameters.Add("@IM_vModel", SqlDbType.VarChar).Value = IM_vModel;
                com.Parameters.Add("@IM_iImage1", SqlDbType.VarChar).Value = IM_iImage1;
                com.Parameters.Add("@IM_iImage2", SqlDbType.VarChar).Value = IM_iImage2;
                com.Parameters.Add("@IM_image3", SqlDbType.VarChar).Value = IM_image3;
                com.Parameters.Add("@IM_iimage4", SqlDbType.VarChar).Value = IM_iimage4;
                com.Parameters.Add("@IM_vUser", SqlDbType.VarChar).Value = IM_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string Pr_Update_item_Master(
       string IM_iid,
       string IM_vCode,
       string IM_vPartName,
       string IM_vModel,
       string IM_iImage1,
       string IM_iImage2,
       string IM_image3,
       string IM_iimage4,
       String IM_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_item_Master";
                com.Parameters.Add("@IM_iid", SqlDbType.VarChar).Value = IM_iid;
                com.Parameters.Add("@IM_vCode", SqlDbType.VarChar).Value = IM_vCode;
                com.Parameters.Add("@IM_vPartName", SqlDbType.VarChar).Value = IM_vPartName;
                com.Parameters.Add("@IM_vModel", SqlDbType.VarChar).Value = IM_vModel;
                com.Parameters.Add("@IM_iImage1", SqlDbType.VarChar).Value = IM_iImage1;
                com.Parameters.Add("@IM_iImage2", SqlDbType.VarChar).Value = IM_iImage2;
                com.Parameters.Add("@IM_image3", SqlDbType.VarChar).Value = IM_image3;
                com.Parameters.Add("@IM_iimage4", SqlDbType.VarChar).Value = IM_iimage4;
                com.Parameters.Add("@IM_vUser", SqlDbType.VarChar).Value = IM_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Delete_Idle_Hours_Details(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Idle_Hours_Details '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Delete_Company_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Company_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Company_Master(
       string C_vName,
       string C_vAddress1,
       string C_vAddress2,
       string C_vAddress3,
       string C_Address4,
       string C_vPhone_No1,
       string C_vPhone_No2,
       string C_vMobile_No1,
       string C_vMobile_No2,
       string C_vTin_No,
       string C_vTax_No,
       string C_vCST_No,
       String C_VGST_No
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Company_Master";
                com.Parameters.Add("@C_vName", SqlDbType.VarChar).Value = C_vName;
                com.Parameters.Add("@C_vAddress1", SqlDbType.VarChar).Value = C_vAddress1;
                com.Parameters.Add("@C_vAddress2", SqlDbType.VarChar).Value = C_vAddress2;
                com.Parameters.Add("@C_vAddress3", SqlDbType.VarChar).Value = C_vAddress3;
                com.Parameters.Add("@C_Address4", SqlDbType.VarChar).Value = C_Address4;
                com.Parameters.Add("@C_vPhone_No1", SqlDbType.VarChar).Value = C_vPhone_No1;
                com.Parameters.Add("@C_vPhone_No2", SqlDbType.VarChar).Value = C_vPhone_No2;
                com.Parameters.Add("@C_vMobile_No1", SqlDbType.VarChar).Value = C_vMobile_No1;
                com.Parameters.Add("@C_vMobile_No2", SqlDbType.VarChar).Value = C_vMobile_No2;
                com.Parameters.Add("@C_vTin_No", SqlDbType.VarChar).Value = C_vTin_No;
                com.Parameters.Add("@C_vTax_No", SqlDbType.VarChar).Value = C_vTax_No;
                com.Parameters.Add("@C_vCST_No", SqlDbType.VarChar).Value = C_vCST_No;
                com.Parameters.Add("@C_VGST_No", SqlDbType.VarChar).Value = C_VGST_No;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Company_Master(
       string C_iId,
       string C_vName,
       string C_vAddress1,
       string C_vAddress2,
       string C_vAddress3,
       string C_Address4,
       string C_vPhone_No1,
       string C_vPhone_No2,
       string C_vMobile_No1,
       string C_vMobile_No2,
       string C_vTin_No,
       string C_vTax_No,
       string C_vCST_No,
       String C_VGST_No
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Company_Master";
                com.Parameters.Add("@C_iId", SqlDbType.VarChar).Value = C_iId;
                com.Parameters.Add("@C_vName", SqlDbType.VarChar).Value = C_vName;
                com.Parameters.Add("@C_vAddress1", SqlDbType.VarChar).Value = C_vAddress1;
                com.Parameters.Add("@C_vAddress2", SqlDbType.VarChar).Value = C_vAddress2;
                com.Parameters.Add("@C_vAddress3", SqlDbType.VarChar).Value = C_vAddress3;
                com.Parameters.Add("@C_Address4", SqlDbType.VarChar).Value = C_Address4;
                com.Parameters.Add("@C_vPhone_No1", SqlDbType.VarChar).Value = C_vPhone_No1;
                com.Parameters.Add("@C_vPhone_No2", SqlDbType.VarChar).Value = C_vPhone_No2;
                com.Parameters.Add("@C_vMobile_No1", SqlDbType.VarChar).Value = C_vMobile_No1;
                com.Parameters.Add("@C_vMobile_No2", SqlDbType.VarChar).Value = C_vMobile_No2;
                com.Parameters.Add("@C_vTin_No", SqlDbType.VarChar).Value = C_vTin_No;
                com.Parameters.Add("@C_vTax_No", SqlDbType.VarChar).Value = C_vTax_No;
                com.Parameters.Add("@C_vCST_No", SqlDbType.VarChar).Value = C_vCST_No;
                com.Parameters.Add("@C_VGST_No", SqlDbType.VarChar).Value = C_VGST_No;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Insert_Idle_Hours_Details(
       string IH_iMachine,
       string IH_dFrom_Time,
       string IH_dTo_Time,
       string IH_nBreak_Down_Time,
       string IH_iIdle_Type,
       string IH_vCounter_Measure,
       string IH_vCorrective_Action,
       string IH_vUpdated_By,
       String IH_vRemarks
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Idle_Hours_Details";
                com.Parameters.Add("@IH_iMachine", SqlDbType.VarChar).Value = IH_iMachine;
                com.Parameters.Add("@IH_dFrom_Time", SqlDbType.VarChar).Value = IH_dFrom_Time;
                com.Parameters.Add("@IH_dTo_Time", SqlDbType.VarChar).Value = IH_dTo_Time;
                com.Parameters.Add("@IH_nBreak_Down_Time", SqlDbType.VarChar).Value = IH_nBreak_Down_Time;
                com.Parameters.Add("@IH_iIdle_Type", SqlDbType.VarChar).Value = IH_iIdle_Type;
                com.Parameters.Add("@IH_vCounter_Measure", SqlDbType.VarChar).Value = IH_vCounter_Measure;
                com.Parameters.Add("@IH_vCorrective_Action", SqlDbType.VarChar).Value = IH_vCorrective_Action;
                com.Parameters.Add("@IH_vUpdated_By", SqlDbType.VarChar).Value = IH_vUpdated_By;
                com.Parameters.Add("@IH_vRemarks", SqlDbType.VarChar).Value = IH_vRemarks;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Idle_Hours_Details(
       string IH_iID,
       string IH_iMachine,
       string IH_dFrom_Time,
       string IH_dTo_Time,
       string IH_nBreak_Down_Time,
       string IH_iIdle_Type,
       string IH_vCounter_Measure,
       string IH_vCorrective_Action,
       string IH_vUpdated_By,
       String IH_vRemarks
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Idle_Hours_Details";
                com.Parameters.Add("@IH_iID", SqlDbType.VarChar).Value = IH_iID;
                com.Parameters.Add("@IH_iMachine", SqlDbType.VarChar).Value = IH_iMachine;
                com.Parameters.Add("@IH_dFrom_Time", SqlDbType.VarChar).Value = IH_dFrom_Time;
                com.Parameters.Add("@IH_dTo_Time", SqlDbType.VarChar).Value = IH_dTo_Time;
                com.Parameters.Add("@IH_nBreak_Down_Time", SqlDbType.VarChar).Value = IH_nBreak_Down_Time;
                com.Parameters.Add("@IH_iIdle_Type", SqlDbType.VarChar).Value = IH_iIdle_Type;
                com.Parameters.Add("@IH_vCounter_Measure", SqlDbType.VarChar).Value = IH_vCounter_Measure;
                com.Parameters.Add("@IH_vCorrective_Action", SqlDbType.VarChar).Value = IH_vCorrective_Action;
                com.Parameters.Add("@IH_vUpdated_By", SqlDbType.VarChar).Value = IH_vUpdated_By;
                com.Parameters.Add("@IH_vRemarks", SqlDbType.VarChar).Value = IH_vRemarks;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Delete_Mould_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Mould_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Mould_Master(
       string MM_vCode,
       string MM_vName,
       string MM_dLife_Hours,
       string MM_iLife_Shots,
       string MM_iLife_Days,
       string MM_dOpening_Hours,
       string MM_iOpening_Shots,
       string MM_iOpening_Days,
       string MM_vRunning_Cavity,
       string MM_nActual_Cavity,
       string MM_nCycle_Time,
       string MM_nMinAlert_Cycle_Time,
       string MM_nMaxAlert_Cycle_Time,
       string MM_nCleaning_Interval_Shot,
       string MM_nPreventive_Interval_Shot,
       String MM_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Mould_Master";
                com.Parameters.Add("@MM_vCode", SqlDbType.VarChar).Value = MM_vCode;
                com.Parameters.Add("@MM_vName", SqlDbType.VarChar).Value = MM_vName;
                com.Parameters.Add("@MM_dLife_Hours", SqlDbType.VarChar).Value = MM_dLife_Hours;
                com.Parameters.Add("@MM_iLife_Shots", SqlDbType.VarChar).Value = MM_iLife_Shots;
                com.Parameters.Add("@MM_iLife_Days", SqlDbType.VarChar).Value = MM_iLife_Days;
                com.Parameters.Add("@MM_dOpening_Hours", SqlDbType.VarChar).Value = MM_dOpening_Hours;
                com.Parameters.Add("@MM_iOpening_Shots", SqlDbType.VarChar).Value = MM_iOpening_Shots;
                com.Parameters.Add("@MM_iOpening_Days", SqlDbType.VarChar).Value = MM_iOpening_Days;
                com.Parameters.Add("@MM_vRunning_Cavity", SqlDbType.VarChar).Value = MM_vRunning_Cavity;
                com.Parameters.Add("@MM_nActual_Cavity", SqlDbType.VarChar).Value = MM_nActual_Cavity;
                com.Parameters.Add("@MM_nCycle_Time", SqlDbType.VarChar).Value = MM_nCycle_Time;
                com.Parameters.Add("@MM_nMinAlert_Cycle_Time", SqlDbType.VarChar).Value = MM_nMinAlert_Cycle_Time;
                com.Parameters.Add("@MM_nMaxAlert_Cycle_Time", SqlDbType.VarChar).Value = MM_nMaxAlert_Cycle_Time;
                com.Parameters.Add("@MM_nCleaning_Interval_Shot", SqlDbType.VarChar).Value = MM_nCleaning_Interval_Shot;
                com.Parameters.Add("@MM_nPreventive_Interval_Shot", SqlDbType.VarChar).Value = MM_nPreventive_Interval_Shot;
                com.Parameters.Add("@MM_vCreatedBy", SqlDbType.VarChar).Value = MM_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Mould_Master(
       string MM_iId,
       string MM_vCode,
       string MM_vName,
       string MM_dLife_Hours,
       string MM_iLife_Shots,
       string MM_iLife_Days,
       string MM_dOpening_Hours,
       string MM_iOpening_Shots,
       string MM_iOpening_Days,
       string MM_vRunning_Cavity,
       string MM_nActual_Cavity,
       string MM_nCycle_Time,
       string MM_nMinAlert_Cycle_Time,
       string MM_nMaxAlert_Cycle_Time,
       string MM_nCleaning_Interval_Shot,
       string MM_nPreventive_Interval_Shot,
       String MM_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Mould_Master";
                com.Parameters.Add("@MM_iId", SqlDbType.VarChar).Value = MM_iId;
                com.Parameters.Add("@MM_vCode", SqlDbType.VarChar).Value = MM_vCode;
                com.Parameters.Add("@MM_vName", SqlDbType.VarChar).Value = MM_vName;
                com.Parameters.Add("@MM_dLife_Hours", SqlDbType.VarChar).Value = MM_dLife_Hours;
                com.Parameters.Add("@MM_iLife_Shots", SqlDbType.VarChar).Value = MM_iLife_Shots;
                com.Parameters.Add("@MM_iLife_Days", SqlDbType.VarChar).Value = MM_iLife_Days;
                com.Parameters.Add("@MM_dOpening_Hours", SqlDbType.VarChar).Value = MM_dOpening_Hours;
                com.Parameters.Add("@MM_iOpening_Shots", SqlDbType.VarChar).Value = MM_iOpening_Shots;
                com.Parameters.Add("@MM_iOpening_Days", SqlDbType.VarChar).Value = MM_iOpening_Days;
                com.Parameters.Add("@MM_vRunning_Cavity", SqlDbType.VarChar).Value = MM_vRunning_Cavity;
                com.Parameters.Add("@MM_nActual_Cavity", SqlDbType.VarChar).Value = MM_nActual_Cavity;
                com.Parameters.Add("@MM_nCycle_Time", SqlDbType.VarChar).Value = MM_nCycle_Time;
                com.Parameters.Add("@MM_nMinAlert_Cycle_Time", SqlDbType.VarChar).Value = MM_nMinAlert_Cycle_Time;
                com.Parameters.Add("@MM_nMaxAlert_Cycle_Time", SqlDbType.VarChar).Value = MM_nMaxAlert_Cycle_Time;
                com.Parameters.Add("@MM_nCleaning_Interval_Shot", SqlDbType.VarChar).Value = MM_nCleaning_Interval_Shot;
                com.Parameters.Add("@MM_nPreventive_Interval_Shot", SqlDbType.VarChar).Value = MM_nPreventive_Interval_Shot;
                com.Parameters.Add("@MM_vCreatedBy", SqlDbType.VarChar).Value = MM_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        [WebMethod]
        public static string Pr_Delete_Modules(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Modules '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Modules(
       string MD_vModuleName,
       string MD_vModuleDescription,
       string MD_vIcon,
       string MD_vMenuID,
       String MD_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Modules";
                com.Parameters.Add("@MD_vModuleName", SqlDbType.VarChar).Value = MD_vModuleName;
                com.Parameters.Add("@MD_vModuleDescription", SqlDbType.VarChar).Value = MD_vModuleDescription;
                com.Parameters.Add("@MD_vIcon", SqlDbType.VarChar).Value = MD_vIcon;
                com.Parameters.Add("@MD_vMenuID", SqlDbType.VarChar).Value = MD_vMenuID;
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


        [WebMethod]
        public static string Pr_Update_Modules(
       string MD_iid,
       string MD_vModuleName,
       string MD_vModuleDescription,
       string MD_vIcon,
       string MD_vMenuID,
       String MD_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Modules";
                com.Parameters.Add("@MD_iid", SqlDbType.VarChar).Value = MD_iid;
                com.Parameters.Add("@MD_vModuleName", SqlDbType.VarChar).Value = MD_vModuleName;
                com.Parameters.Add("@MD_vModuleDescription", SqlDbType.VarChar).Value = MD_vModuleDescription;
                com.Parameters.Add("@MD_vIcon", SqlDbType.VarChar).Value = MD_vIcon;
                com.Parameters.Add("@MD_vMenuID", SqlDbType.VarChar).Value = MD_vMenuID;
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


        [WebMethod]
        public static string Pr_Insert_Idle_Type_Master(
       string ITM_vCode,
       string ITM_vDescription,
       String ITM_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Idle_Type_Master";
                com.Parameters.Add("@ITM_vCode", SqlDbType.VarChar).Value = ITM_vCode;
                com.Parameters.Add("@ITM_vDescription", SqlDbType.VarChar).Value = ITM_vDescription;
                com.Parameters.Add("@ITM_vCreatedBy", SqlDbType.VarChar).Value = ITM_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Delete_Group_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Group_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Group_Master(
       string GP_vGroupName,
       String GP_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Group_Master";
                com.Parameters.Add("@GP_vGroupName", SqlDbType.VarChar).Value = GP_vGroupName;
                com.Parameters.Add("@GP_vUser", SqlDbType.VarChar).Value = GP_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Group_Master(
       string GP_iid,
       string GP_vGroupName,
       String GP_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Group_Master";
                com.Parameters.Add("@GP_iid", SqlDbType.VarChar).Value = GP_iid;
                com.Parameters.Add("@GP_vGroupName", SqlDbType.VarChar).Value = GP_vGroupName;
                com.Parameters.Add("@GP_vUser", SqlDbType.VarChar).Value = GP_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Update_Idle_Type_Master(
       string ITM_iId,
       string ITM_vCode,
       string ITM_vDescription,
       String ITM_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Idle_Type_Master";
                com.Parameters.Add("@ITM_iId", SqlDbType.VarChar).Value = ITM_iId;
                com.Parameters.Add("@ITM_vCode", SqlDbType.VarChar).Value = ITM_vCode;
                com.Parameters.Add("@ITM_vDescription", SqlDbType.VarChar).Value = ITM_vDescription;
                com.Parameters.Add("@ITM_vCreatedBy", SqlDbType.VarChar).Value = ITM_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_Delete_Machine_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Machine_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Machine_Master(
       string MM_iType,
       string MM_vCode,
       string MM_vDescription,
       string MM_vTonnage,
       string MM_vCurrent_Position,
       String MM_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Machine_Master";
                com.Parameters.Add("@MM_iType", SqlDbType.VarChar).Value = MM_iType;
                com.Parameters.Add("@MM_vCode", SqlDbType.VarChar).Value = MM_vCode;
                com.Parameters.Add("@MM_vDescription", SqlDbType.VarChar).Value = MM_vDescription;
                com.Parameters.Add("@MM_vTonnage", SqlDbType.VarChar).Value = MM_vTonnage;
                com.Parameters.Add("@MM_vCurrent_Position", SqlDbType.VarChar).Value = MM_vCurrent_Position;
                com.Parameters.Add("@MM_vCreatedBy", SqlDbType.VarChar).Value = MM_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Machine_Master(
       string MM_iId,
       string MM_iType,
       string MM_vCode,
       string MM_vDescription,
       string MM_vTonnage,
       string MM_vCurrent_Position,
       String MM_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Machine_Master";
                com.Parameters.Add("@MM_iId", SqlDbType.VarChar).Value = MM_iId;
                com.Parameters.Add("@MM_iType", SqlDbType.VarChar).Value = MM_iType;
                com.Parameters.Add("@MM_vCode", SqlDbType.VarChar).Value = MM_vCode;
                com.Parameters.Add("@MM_vDescription", SqlDbType.VarChar).Value = MM_vDescription;
                com.Parameters.Add("@MM_vTonnage", SqlDbType.VarChar).Value = MM_vTonnage;
                com.Parameters.Add("@MM_vCurrent_Position", SqlDbType.VarChar).Value = MM_vCurrent_Position;
                com.Parameters.Add("@MM_vCreatedBy", SqlDbType.VarChar).Value = MM_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


       



       

        [WebMethod]
        public static string Pr_Delete_Machine_Type(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Machine_Type '" + ID + "'");
            return "True";
        }



        [WebMethod]
        public static string Pr_Delete_Role_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Role_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Role_Master(
       string RM_vCode,
       string RM_vDescription,
       String RM_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Role_Master";
                com.Parameters.Add("@RM_vCode", SqlDbType.VarChar).Value = RM_vCode;
                com.Parameters.Add("@RM_vDescription", SqlDbType.VarChar).Value = RM_vDescription;
                com.Parameters.Add("@RM_vCreatedBy", SqlDbType.VarChar).Value = RM_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        [WebMethod]
        public static string Pr_Delete_Help_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Help_Master '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string Pr_Insert_Help_Master(
       string HM_iMenuID,
       string HM_vTitle,
       string HM_VDescription,
       string HM_vNotes,
       String HM_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Help_Master";
                com.Parameters.Add("@HM_iMenuID", SqlDbType.VarChar).Value = HM_iMenuID;
                com.Parameters.Add("@HM_vTitle", SqlDbType.VarChar).Value = HM_vTitle;
                com.Parameters.Add("@HM_VDescription", SqlDbType.VarChar).Value = HM_VDescription;
                com.Parameters.Add("@HM_vNotes", SqlDbType.VarChar).Value = HM_vNotes;
                com.Parameters.Add("@HM_vUser", SqlDbType.VarChar).Value = HM_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Help_Master(
       string HM_iid,
       string HM_iMenuID,
       string HM_vTitle,
       string HM_VDescription,
       string HM_vNotes,
       String HM_vUser
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Help_Master";
                com.Parameters.Add("@HM_iid", SqlDbType.VarChar).Value = HM_iid;
                com.Parameters.Add("@HM_iMenuID", SqlDbType.VarChar).Value = HM_iMenuID;
                com.Parameters.Add("@HM_vTitle", SqlDbType.VarChar).Value = HM_vTitle;
                com.Parameters.Add("@HM_VDescription", SqlDbType.VarChar).Value = HM_VDescription;
                com.Parameters.Add("@HM_vNotes", SqlDbType.VarChar).Value = HM_vNotes;
                com.Parameters.Add("@HM_vUser", SqlDbType.VarChar).Value = HM_vUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Update_Role_Master(
       string RM_iId,
       string RM_vCode,
       string RM_vDescription,
       String RM_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Role_Master";
                com.Parameters.Add("@RM_iId", SqlDbType.VarChar).Value = RM_iId;
                com.Parameters.Add("@RM_vCode", SqlDbType.VarChar).Value = RM_vCode;
                com.Parameters.Add("@RM_vDescription", SqlDbType.VarChar).Value = RM_vDescription;
                com.Parameters.Add("@RM_vCreatedBy", SqlDbType.VarChar).Value = RM_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public static string Pr_Insert_Machine_Type(
       string MT_vCode,
       string MT_vTypeName,
       string MT_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Machine_Type";
                com.Parameters.Add("@MT_vCode", SqlDbType.VarChar).Value = MT_vCode;
                com.Parameters.Add("@MT_vTypeName", SqlDbType.VarChar).Value = MT_vTypeName;
                com.Parameters.Add("@MT_vCreatedBy", SqlDbType.VarChar).Value = MT_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public static string Pr_Update_Machine_Type(
       string MT_iID,
       string MT_vCode,
       string MT_vTypeName,
       string MT_vCreatedBy
       )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Machine_Type";
                com.Parameters.Add("@MT_iID", SqlDbType.VarChar).Value = MT_iID;
                com.Parameters.Add("@MT_vCode", SqlDbType.VarChar).Value = MT_vCode;
                com.Parameters.Add("@MT_vTypeName", SqlDbType.VarChar).Value = MT_vTypeName;
                com.Parameters.Add("@MT_vCreatedBy", SqlDbType.VarChar).Value = MT_vCreatedBy;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


      



        [WebMethod]
        public static string Pr_Insert_User_Master(
            string UM_vCode,
            string UM_vFirstName,
            string UM_vLastName,
            string UM_vUserID,
            string UM_vPassword,
            string UM_iRole,
            string UM_vCreatedby
            )
        {

            SqlConnection con = new SqlConnection(connectionstring);
            try
            {

                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_User_Master";

                com.Parameters.Add("@UM_vCode", SqlDbType.VarChar).Value = UM_vCode;
                com.Parameters.Add("@UM_vFirstName", SqlDbType.VarChar).Value = UM_vFirstName;
                com.Parameters.Add("@UM_vLastName", SqlDbType.VarChar).Value = UM_vLastName;
                com.Parameters.Add("@UM_vUserID", SqlDbType.VarChar).Value = UM_vUserID;
                com.Parameters.Add("@UM_vPassword ", SqlDbType.VarChar).Value = UM_vPassword;
                com.Parameters.Add("@UM_iRole", SqlDbType.VarChar).Value = UM_iRole;
                com.Parameters.Add("@UM_vCreatedby", SqlDbType.VarChar).Value = UM_vCreatedby;
               
                com.ExecuteNonQuery();
                con.Close();




                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        public static string Pr_update_User_Master(
            string UM_iID,
            string UM_vCode,
            string UM_vFirstName,
            string UM_vLastName,
            string UM_vUserID,
            string UM_vPassword,
            string UM_iRole,
            string UM_vCreatedby
            )
        {

            SqlConnection con = new SqlConnection(connectionstring);
            try
            {

                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_update_User_Master";
                com.Parameters.Add("@UM_iID", SqlDbType.VarChar).Value = UM_iID;
                com.Parameters.Add("@UM_vCode", SqlDbType.VarChar).Value = UM_vCode;
                com.Parameters.Add("@UM_vFirstName", SqlDbType.VarChar).Value = UM_vFirstName;
                com.Parameters.Add("@UM_vLastName", SqlDbType.VarChar).Value = UM_vLastName;
                com.Parameters.Add("@UM_vUserID", SqlDbType.VarChar).Value = UM_vUserID;
                com.Parameters.Add("@UM_vPassword ", SqlDbType.VarChar).Value = UM_vPassword;
                com.Parameters.Add("@UM_iRole", SqlDbType.VarChar).Value = UM_iRole;
                com.Parameters.Add("@UM_vCreatedby", SqlDbType.VarChar).Value = UM_vCreatedby;

                com.ExecuteNonQuery();
                con.Close();




                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        //[WebMethod]
        //public static string Pr_export(string ID, string from, string to)
        //{
        //    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
        //    // creating new WorkBook within Excel application
        //    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
        //    // creating new Excelsheet in workbook
        //    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
        //    // see the excel sheet behind the program
        //    //Funny
        //    app.Visible = true;
        //    // get the reference of first sheet. By default its name is Sheet1.
        //    // store its reference to worksheet
        //    try
        //    {
        //        //Fixed:(Microsoft.Office.Interop.Excel.Worksheet)
        //        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
        //        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
        //        // changing the name of active sheet
        //        worksheet.Name = "Export by Genuine IT";
        //        // storing header part in Excel
        //        DataTable dt = dbFunctions.getTable(ID+"'"+from+"','"+to+"'");
        //        for (int i = 1; i < dt.Columns.Count + 1; i++)
        //        {
        //            worksheet.Cells[1, i] = dt.Columns[i - 1].ColumnName.ToString();
                    
        //        }
        //        // storing Each row and column value to excel sheet
        //        for (int i = 0; i < dt.Rows.Count ; i++)
        //        {
        //            for (int j = 0; j < dt.Columns.Count ; j++)
        //            {
        //                worksheet.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();
        //            }
        //        }

       

        
        //            workbook.SaveAs(@"E:\\data.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

               

        //        // Exit from the application
        //        //app.Quit();
        //    }
        //    catch (System.Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        app.Quit();
        //        workbook = null;
        //        app = null;
        //    }


        //    return "True";
        //}


        [WebMethod]
        public static string pr_deleteUserMaster(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_deleteUserMaster '" + ID + "'");
            return "True";
        }


        [WebMethod]
        public static string DownloadFile(string Path)
        {
            string fileName = System.Uri.UnescapeDataString(Path);
            string Name = System.IO.Path.GetFileName(fileName);
           
            //HttpContext.Current.Response.ContentType = "application/octet-stream";
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Name);
            //HttpContext.Current.Response.TransmitFile(Server.MapPath("~/Files/" + fileName));
            //HttpContext.Current.Response.End();
            String result = "Result : " + DateTime.Now.ToString() + " - From Server";
            System.IO.FileInfo filePath = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/UploadFile/")+Name);

          //  string actualFilePath = System.IO.Path.Combine(filePath, Name);
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            string filename = Name;
            String Header = "Attachment; Filename=" + filename;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            HttpContext.Current.Response.WriteFile(fileName);
            HttpContext.Current.Response.End();

            return "true";
        }


        [WebMethod]
        public static string SaveFile(string file)
        {
            string msg = "";
            try
            {
              
                file = System.Uri.UnescapeDataString(file);
                string FileName = System.IO.Path.GetFileName(file);
                string Extension = System.IO.Path.GetExtension(file);

                string SaveLocation = HttpContext.Current.Server.MapPath("~/UploadFile/");
                System.IO.File.Copy(file, SaveLocation, true);
                if (System.IO.File.Exists(SaveLocation))
                {
                    System.IO.File.Delete(SaveLocation);
                }
                //fileupload.SaveAs(FilePath);

                System.IO.FileInfo fi = new System.IO.FileInfo(SaveLocation);
                if (!fi.Exists)
                {
                    msg = "File " + SaveLocation + " Does Not Exists";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return msg;
        }


        [WebMethod]
        public static string pr_insertItemMaster(string IM_ItemName, string IM_user)
        {

            SqlConnection con = new SqlConnection(connectionstring);
            try
            {

                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_insertItemMaster";


                com.Parameters.Add("@IM_ItemName", SqlDbType.VarChar).Value = IM_ItemName;
                com.Parameters.Add("@IM_user", SqlDbType.VarChar).Value = IM_user;


                com.ExecuteNonQuery();
                con.Close();




                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        public static string GetJSONString(DataTable Dt)
        {
            StringBuilder Sb = new StringBuilder();
            try
            {
                string[] StrDc = new string[Dt.Columns.Count];
                string HeadStr = string.Empty;

                for (int i = 0; i < Dt.Columns.Count; i++)
                {

                    StrDc[i] = Dt.Columns[i].Caption;

                    HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                }

                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

                
                Sb.Append("{\"record" + Dt.TableName + "\" : [");

                for (int i = 0; i < Dt.Rows.Count; i++)
                {

                    string TempStr = HeadStr;
                    Sb.Append("{");

                    for (int j = 0; j < Dt.Columns.Count; j++)
                    {

                        TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString());
                    }

                    Sb.Append(TempStr + "},");
                }

                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                Sb.Append("]}");
            }
            catch { }



            return Sb.Replace("\n", "\\n").Replace("\r", "\\r").ToString();
        }

        public static string EncryptThis(string ToEncrypt, string Keys)
        {
            byte[] IV = new byte[8] { 210, 1, 25, 101, 0, 76, 173, 59 };
            byte[] buffer = Encoding.ASCII.GetBytes(ToEncrypt);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Keys));
            des.IV = IV;
            return Convert.ToBase64String(
            des.CreateEncryptor().TransformFinalBlock(
            buffer,
            0,
            buffer.Length
            )
            );
        }


        public static string GetJSONString2(DataTable Dt)
        {

            string[] StrDc = new string[Dt.Columns.Count];
            string HeadStr = string.Empty;

            for (int i = 0; i < Dt.Columns.Count; i++)
            {

                StrDc[i] = Dt.Columns[i].Caption;

                HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

            StringBuilder Sb = new StringBuilder();
            Sb.Append(" [");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {

                string TempStr = HeadStr;
                Sb.Append("{");

                for (int j = 0; j < Dt.Columns.Count; j++)
                {

                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString());
                }

                Sb.Append(TempStr + "},");
            }

            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
            Sb.Append("]");

            return Sb.ToString();
        }
    }
}
