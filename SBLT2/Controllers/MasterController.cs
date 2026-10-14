using CrystalDecisions.CrystalReports.Engine;
using SriPravanTech;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBLT2.Controllers
{
    public class MasterController : Controller 
    {
        //
        // GET: /Master/

        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult Employee_Master()
        {
            return View();
        }

     
        public ActionResult User_Master()
        {
            return View();
        }

        public ActionResult Reference_Master()
        {
            return View();
        }

        public ActionResult Material_Master()
        {
            return View();
        }

        public ActionResult Assembly_Product_Master()
        {
            return View();
        }

        public ActionResult Type_Master()
        {
            return View();  
        }   

        public ActionResult Supplier_Master()
        {
            return View();
        }

        public string Pr_Fetch_Expense_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Expense_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Product_MasterAssy()
        {
            DataTable dt = dbFunctions.getTable("select   PM_iID as iID,  PM_Item_Code as Part_No,   PM_Description as Description,  PM_Skid_Qty as Skid_Qty, chargeing_Dub_Hrs as [Charge Hrs],PM_Created_by as  Created_by  from  Product_Master_Assembly  where  PM_Status='A'  ");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Product_MasterAssy(string ID)
        {
            DataTable dt = dbFunctions.getTable("select * from  Product_Master_Assembly  where  PM_iID= " + ID);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchMaterialCode()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchMaterialCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Company_Master_Name()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Company_Master_Name");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Region_Master_Name(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Region_Master_Name '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_fetch_referencecode(string RGV_iRG_ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_fetch_referencecode '" + RGV_iRG_ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Delete_Reference_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Reference_Master '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Delete_Expense_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Expense_Master '" + ID + "'");
            return "True";
        }
        public string Pr_Delete_Team_Payers(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Team_Payers '" + ID + "'");
            return "True";
        }

        public string PR_INSERT_TYPE_MASTER(string tm_TypeName, string tm_Type, string tm_User)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "PR_INSERT_TYPE_MASTER";
                com.Parameters.Add("@tm_TypeName", SqlDbType.VarChar).Value = tm_TypeName;
                com.Parameters.Add("@tm_Type", SqlDbType.VarChar).Value = tm_Type;
                com.Parameters.Add("@tm_User", SqlDbType.VarChar).Value = tm_User;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string PR_UPDATE_TYPE_MASTER(string tm_Id, string tm_TypeName, string tm_Type)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "PR_UPDATE_TYPE_MASTER";
                com.Parameters.Add("@tm_Id", SqlDbType.VarChar).Value = tm_Id;
                com.Parameters.Add("@tm_TypeName", SqlDbType.VarChar).Value = tm_TypeName;
                com.Parameters.Add("@tm_Type", SqlDbType.VarChar).Value = tm_Type;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string pr_Insert_Bobbin_Master(string bm_BobbinName, string bm_Model, string bm_User)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Bobbin_Master";
                com.Parameters.Add("@bm_BobbinName", SqlDbType.VarChar).Value = bm_BobbinName;
                com.Parameters.Add("@bm_Model", SqlDbType.VarChar).Value = bm_Model;
                com.Parameters.Add("@bm_User", SqlDbType.VarChar).Value = bm_User;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_Update_Bobbin_Master(string bm_Id, string bm_BobbinName, string bm_Model)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Update_Bobbin_Master";
                com.Parameters.Add("@bm_Id", SqlDbType.VarChar).Value = bm_Id;
                com.Parameters.Add("@bm_BobbinName", SqlDbType.VarChar).Value = bm_BobbinName;
                com.Parameters.Add("@bm_Model", SqlDbType.VarChar).Value = bm_Model;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Pr_Insert_Team_Payers(
        string TP_Team_ID,
        string TP_Player_Code,
        string TP_Player_ID,
        string TP_Created_by
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Team_Payers";
                com.Parameters.Add("@TP_Team_ID", SqlDbType.VarChar).Value = TP_Team_ID;
                com.Parameters.Add("@TP_Player_Code", SqlDbType.VarChar).Value = TP_Player_Code;
                com.Parameters.Add("@TP_Player_ID", SqlDbType.VarChar).Value = TP_Player_ID;
                com.Parameters.Add("@TP_Created_by", SqlDbType.VarChar).Value = TP_Created_by;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Update_Team_Payers(
        string TP_iid,
        string TP_Team_ID,
        string TP_Player_Code,
        string TP_Player_ID,
        string TP_Created_by
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Team_Payers";
                com.Parameters.Add("@TP_iid", SqlDbType.VarChar).Value = TP_iid;
                com.Parameters.Add("@TP_Team_ID", SqlDbType.VarChar).Value = TP_Team_ID;
                com.Parameters.Add("@TP_Player_Code", SqlDbType.VarChar).Value = TP_Player_Code;
                com.Parameters.Add("@TP_Player_ID", SqlDbType.VarChar).Value = TP_Player_ID;
                com.Parameters.Add("@TP_Created_by", SqlDbType.VarChar).Value = TP_Created_by;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public string Pr_Fetch_User_Master_Details(string Area)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_User_Master_Details '" + Area + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string pr_get_Player()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Player");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string pr_get_Team()
        {
            DataTable dt = dbFunctions.getTable("pr_get_Team");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        
        

        public string Pr_Fetch_User_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_User_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_User_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_User_Master '" + ID + "'");
            return "True";
        }


        public string Pr_Insert_User_Master(
        string UM_vUser_Name,
        string UM_vUser_ID,
        string UM_vPassword,
        string UM_vC_Password,
        string UM_vPhone_Number,
        string UM_vEmail_ID,
        string UM_iArea,
        string UM_iUser_Type,
        string UM_iCoordinator,
        string UM_dApproved_Date,
        string UM_iApproved_By,
        string UM_vCreated_by
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_User_Master";
                com.Parameters.Add("@UM_vUser_Name", SqlDbType.VarChar).Value = UM_vUser_Name;
                com.Parameters.Add("@UM_vUser_ID", SqlDbType.VarChar).Value = UM_vUser_ID;
                com.Parameters.Add("@UM_vPassword", SqlDbType.VarChar).Value = UM_vPassword;
                com.Parameters.Add("@UM_vC_Password", SqlDbType.VarChar).Value = UM_vC_Password;
                com.Parameters.Add("@UM_vPhone_Number", SqlDbType.VarChar).Value = UM_vPhone_Number;
                com.Parameters.Add("@UM_vEmail_ID", SqlDbType.VarChar).Value = UM_vEmail_ID;
                com.Parameters.Add("@UM_iArea", SqlDbType.VarChar).Value = UM_iArea;
                com.Parameters.Add("@UM_iUser_Type", SqlDbType.VarChar).Value = UM_iUser_Type;
                com.Parameters.Add("@UM_iCoordinator", SqlDbType.VarChar).Value = UM_iCoordinator;
                com.Parameters.Add("@UM_dApproved_Date", SqlDbType.VarChar).Value = UM_dApproved_Date;
                com.Parameters.Add("@UM_iApproved_By", SqlDbType.VarChar).Value = UM_iApproved_By;
                com.Parameters.Add("@UM_vCreated_by", SqlDbType.VarChar).Value = UM_vCreated_by;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Update_User_Master(
        string UM_iID,
        string UM_vUser_Name,
        string UM_vUser_ID,
        string UM_vPassword,
        string UM_vC_Password,
        string UM_vPhone_Number,
        string UM_vEmail_ID,
        string UM_iArea,
        string UM_iUser_Type,
        string UM_iCoordinator,
        string UM_dApproved_Date,
        string UM_iApproved_By,
        string UM_vCreated_by
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_User_Master";
                com.Parameters.Add("@UM_iID", SqlDbType.VarChar).Value = UM_iID;
                com.Parameters.Add("@UM_vUser_Name", SqlDbType.VarChar).Value = UM_vUser_Name;
                com.Parameters.Add("@UM_vUser_ID", SqlDbType.VarChar).Value = UM_vUser_ID;
                com.Parameters.Add("@UM_vPassword", SqlDbType.VarChar).Value = UM_vPassword;
                com.Parameters.Add("@UM_vC_Password", SqlDbType.VarChar).Value = UM_vC_Password;
                com.Parameters.Add("@UM_vPhone_Number", SqlDbType.VarChar).Value = UM_vPhone_Number;
                com.Parameters.Add("@UM_vEmail_ID", SqlDbType.VarChar).Value = UM_vEmail_ID;
                com.Parameters.Add("@UM_iArea", SqlDbType.VarChar).Value = UM_iArea;
                com.Parameters.Add("@UM_iUser_Type", SqlDbType.VarChar).Value = UM_iUser_Type;
                com.Parameters.Add("@UM_iCoordinator", SqlDbType.VarChar).Value = UM_iCoordinator;
                com.Parameters.Add("@UM_dApproved_Date", SqlDbType.VarChar).Value = UM_dApproved_Date;
                com.Parameters.Add("@UM_iApproved_By", SqlDbType.VarChar).Value = UM_iApproved_By;
                com.Parameters.Add("@UM_vCreated_by", SqlDbType.VarChar).Value = UM_vCreated_by;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [HttpPost]
        public ActionResult Pr_Insert_Driver_Document_Upload()
        {

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string fname = "";
                    string file_Path = "";

                    HttpPostedFileBase file = files[0];

                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    DataTable dt = dbFunctions.getTable("select isnull(max(DD_iiD),0)+1 from Driver_Document");
                    string ID = dt.Rows[0][0].ToString();

                    file_Path = Request.QueryString["Code"].ToString() + "_" + Request.QueryString["DD_Document_Name"].ToString() + "_" + ID + "_" + fname;

                    fname = Path.Combine(Server.MapPath("~/FileUpload/Document/"), file_Path);
                    file.SaveAs(fname);


                    string DD_Driver_ID = Request.QueryString["DD_Driver_ID"].ToString();
                    string DD_Document_Name = Request.QueryString["DD_Document_Name"].ToString();
                    string DU_vDocument_Location = "FileUpload/Document/" + file_Path;
                    string DD_Created_by = Request.QueryString["DU_vCreatedby"].ToString();

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Insert_Driver_Document";
                        com.Parameters.Add("@DD_Driver_ID", SqlDbType.VarChar).Value = DD_Driver_ID;
                        com.Parameters.Add("@DD_Document_Name", SqlDbType.VarChar).Value = DD_Document_Name;
                        com.Parameters.Add("@DD_Document_Location", SqlDbType.VarChar).Value = DU_vDocument_Location;
                        com.Parameters.Add("@DD_Created_by", SqlDbType.VarChar).Value = DD_Created_by;
                        com.ExecuteNonQuery();
                        con.Close();
                        return Json("True");


                    }
                    catch (Exception ex)
                    {
                        return Json("" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }



        public string Pr_Fetch_Employee_Master_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Employee_Master_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_GetPlayerCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_GetPlayerCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Player_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Player_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_Player_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Player_Master '" + ID + "'");
            return "True";
        }



        public string Pr_Fetch_Team_Master_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Team_Master_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Vendor_Master_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vendor_Master_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Branch_Master()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Branch_Master");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Product_Master()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Product_Master");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Location_Master_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Location_Master_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_Team_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Team_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Vendor_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vendor_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Branch_Master_by_ID(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Branch_Master_by_ID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Product_Master_by_ID(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Product_Master_by_ID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchLocation()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchLocation");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchCustomer()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchCustomer");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_RSR_BIN_Master_Details_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_RSR_BIN_Master_Details_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_ReferenceGroup_Value_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_ReferenceGroup_Value_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Material_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Material_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_Material_Master_Details_Web()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Material_Master_Details_Web");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Supplier_Master_Details_Web()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Supplier_Master_Details_Web");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Supplier_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Supplier_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string Pr_Fetch_StorageLocation_Master_Details_Web()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_StorageLocation_Master_Details_Web");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_StorageLocation_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_StorageLocation_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Employee_Master_Details_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Employee_Master_Details_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Location_Master_Details_ById(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Location_Master_Details_ById '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Delete_Team_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Team_Master '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Material_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Material_Master '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Supplier_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Supplier_Master '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Vendor_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Vendor_Master '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_StorageLocation_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_StorageLocation_Master '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_RSR_BIN_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_RSR_BIN_Master '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Branch_Master_by_ID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Branch_Master_by_ID '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Employee_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Employee_Master '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Product_Master_by_ID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Product_Master_by_ID '" + ID + "'");
            return "True";
        }

        //public string Pr_Insert_Team_Master(
        //string TM_vTeam_Code,
        //string TM_vTeam_Name,
        //string TM_vShort_Name,
        //string TM_iTotal_Players,
        //string TM_iPlaying_Player,
        //string TM_Created_by
        //)
        //{
        //    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
        //    try
        //    {
        //        con.Open();
        //        SqlCommand com = new SqlCommand();
        //        com.Connection = con;
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.CommandText = "Pr_Insert_Team_Master";
        //        com.Parameters.Add("@TM_vTeam_Code", SqlDbType.VarChar).Value = TM_vTeam_Code;
        //        com.Parameters.Add("@TM_vTeam_Name", SqlDbType.VarChar).Value = TM_vTeam_Name;
        //        com.Parameters.Add("@TM_vShort_Name", SqlDbType.VarChar).Value = TM_vShort_Name;
        //        com.Parameters.Add("@TM_iTotal_Players", SqlDbType.VarChar).Value = TM_iTotal_Players;
        //        com.Parameters.Add("@TM_iPlaying_Player", SqlDbType.VarChar).Value = TM_iPlaying_Player;
        //        com.Parameters.Add("@TM_Created_by", SqlDbType.VarChar).Value = TM_Created_by;
        //        com.ExecuteNonQuery();
        //        con.Close();
        //        return "True";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        public ActionResult Pr_Insert_Team_Master()
        {

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string fname = "";
                    string file_Path = "";

                    HttpPostedFileBase file = files[0];

                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    DataTable dt = dbFunctions.getTable("select isnull(max(TM_iid),0)+1 from Team_Master");
                    string ID = dt.Rows[0][0].ToString();

                    string TM_vTeam_Code = Request.QueryString["TM_vTeam_Code"].ToString();
                    file_Path = TM_vTeam_Code + ".png";

                    fname = Path.Combine(Server.MapPath("~/FileUpload/Document/"), file_Path);
                    file.SaveAs(fname);



                    string TM_vTeam_Name = Request.QueryString["TM_vTeam_Name"].ToString();
                    string TM_vShort_Name = Request.QueryString["TM_vShort_Name"].ToString();
                    string TM_iTotal_Players = Request.QueryString["TM_iTotal_Players"].ToString();
                    string TM_iPlaying_Player = Request.QueryString["TM_iPlaying_Player"].ToString();
                    string TM_Created_by = Request.QueryString["TM_Created_by"].ToString();
                    string TM_vDocumentLocation = "FileUpload/Document/" + file_Path;

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Insert_Team_Master";
                        com.Parameters.Add("@TM_vTeam_Code", SqlDbType.VarChar).Value = TM_vTeam_Code;
                        com.Parameters.Add("@TM_vTeam_Name", SqlDbType.VarChar).Value = TM_vTeam_Name;
                        com.Parameters.Add("@TM_vShort_Name", SqlDbType.VarChar).Value = TM_vShort_Name;
                        com.Parameters.Add("@TM_iTotal_Players", SqlDbType.VarChar).Value = TM_iTotal_Players;
                        com.Parameters.Add("@TM_iPlaying_Player", SqlDbType.VarChar).Value = TM_iPlaying_Player;
                        com.Parameters.Add("@TM_Created_by", SqlDbType.VarChar).Value = TM_Created_by;
                        com.Parameters.Add("@TM_vDocumentLocation", SqlDbType.VarChar).Value = TM_vDocumentLocation;
                        com.ExecuteNonQuery();
                        con.Close();
                        return Json("True");


                    }
                    catch (Exception ex)
                    {
                        return Json("" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }


        [HttpPost]
        public ActionResult Pr_Insert_Vendor_Master()
        {

                try
                {

                    string vm_VendorName = Request.QueryString["vm_VendorName"].ToString();
                  
                    string vm_VendorAddress = Request.QueryString["vm_VendorAddress"].ToString();
                    string vm_Location = Request.QueryString["vm_Location"].ToString();
                    string vm_ContactPersonName = Request.QueryString["vm_ContactPersonName"].ToString();
                    string vm_ContactNumber = Request.QueryString["vm_ContactNumber"].ToString();
                    string vm_Email = Request.QueryString["vm_Email"].ToString();
                string vm_GSTIN_Number = Request.QueryString["vm_GSTIN_Number"].ToString();
                string vm_User = Request.QueryString["vm_User"].ToString();

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Insert_Vendor_Master";
                        com.Parameters.Add("@vm_VendorName", SqlDbType.VarChar).Value = vm_VendorName;
                        com.Parameters.Add("@vm_VendorAddress", SqlDbType.VarChar).Value = vm_VendorAddress;
                        com.Parameters.Add("@vm_Location", SqlDbType.VarChar).Value = vm_Location;
                        com.Parameters.Add("@vm_ContactPersonName", SqlDbType.VarChar).Value = vm_ContactPersonName;
                        com.Parameters.Add("@vm_ContactNumber", SqlDbType.VarChar).Value = vm_ContactNumber;
                        com.Parameters.Add("@vm_Email", SqlDbType.VarChar).Value = vm_Email;
                    com.Parameters.Add("@vm_GSTIN_Number", SqlDbType.VarChar).Value = vm_GSTIN_Number;
                    com.Parameters.Add("@vm_User", SqlDbType.VarChar).Value = vm_User;
                    // com.Parameters.Add("@TM_vDocumentLocation", SqlDbType.VarChar).Value = TM_vDocumentLocation;
                    com.ExecuteNonQuery();
                        con.Close();
                        return Json("True");


                    }
                    catch (Exception ex)
                    {
                        return Json("" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
           

        }

        [HttpPost]
        public ActionResult Pr_Insert_RSR_BIN_Master()
        {
            try
            {
              
                DataTable dt = dbFunctions.getTable("select isnull(max(rbm_Id),0)+1 from RSR_BIN_Master");
                string ID = dt.Rows[0][0].ToString();

                string rbm_Bin_Type = Request.QueryString["rbm_Bin_Type"].ToString();
                string rbm_Bin_Code = Request.QueryString["rbm_Bin_Code"].ToString();
                string rbm_Bin_Name = Request.QueryString["rbm_Bin_Name"].ToString();
                string rbm_Quantity = Request.QueryString["rbm_Quantity"].ToString();
                string rbm_UOM = Request.QueryString["rbm_UOM"].ToString();
                string rbm_User = Request.QueryString["rbm_User"].ToString();
                // string TM_vDocumentLocation = "FileUpload/Document/" + file_Path;

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_RSR_BIN_Master";
                    com.Parameters.Add("@rbm_Bin_Type", SqlDbType.VarChar).Value = rbm_Bin_Type;
                    com.Parameters.Add("@rbm_Bin_Code", SqlDbType.VarChar).Value = rbm_Bin_Code;
                    com.Parameters.Add("@rbm_Bin_Name", SqlDbType.VarChar).Value = rbm_Bin_Name;
                    com.Parameters.Add("@rbm_Quantity", SqlDbType.VarChar).Value = rbm_Quantity;
                    com.Parameters.Add("@rbm_UOM", SqlDbType.VarChar).Value = rbm_UOM;
                    com.Parameters.Add("@rbm_User", SqlDbType.VarChar).Value = rbm_User;
                    // com.Parameters.Add("@TM_vDocumentLocation", SqlDbType.VarChar).Value = TM_vDocumentLocation;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");


                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        public ActionResult Pr_Insert_Region_Master()
        {
            try
            {

                string rm_Company_Name = Request.QueryString["rm_Company_Name"].ToString();
                string rm_Region_Name = Request.QueryString["rm_Region_Name"].ToString();
              
                string rm_User = Request.QueryString["rm_User"].ToString();
                // string TM_vDocumentLocation = "FileUpload/Document/" + file_Path;

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Region_Master";
                    com.Parameters.Add("@rm_Company_Name", SqlDbType.VarChar).Value = rm_Company_Name;
                    com.Parameters.Add("@rm_Region_Name", SqlDbType.VarChar).Value = rm_Region_Name;
                   
                    com.Parameters.Add("@rm_User", SqlDbType.VarChar).Value = rm_User;
                    // com.Parameters.Add("@TM_vDocumentLocation", SqlDbType.VarChar).Value = TM_vDocumentLocation;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");


                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }

        public ActionResult Pr_Insert_Material_Master()
        {
            try
            {           
                string mm_MaterialCode = Request.QueryString["mm_MaterialCode"].ToString();
                string mm_MaterialDesc = Request.QueryString["mm_MaterialDesc"].ToString();
                string mm_Model = Request.QueryString["mm_Model"].ToString();
                string mm_CustomerName = Request.QueryString["mm_CustomerName"].ToString();
                string mm_Location = Request.QueryString["mm_Location"].ToString();
                string mm_MaxStock = Request.QueryString["mm_MaxStock"].ToString();
                string mm_MinStock = Request.QueryString["mm_MinStock"].ToString();
                string mm_User = Request.QueryString["mm_User"].ToString();
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Material_Master";
                    com.Parameters.Add("@mm_MaterialCode", SqlDbType.VarChar).Value = mm_MaterialCode;
                    com.Parameters.Add("@mm_MaterialDesc", SqlDbType.VarChar).Value = mm_MaterialDesc;
                    com.Parameters.Add("@mm_Model", SqlDbType.VarChar).Value = mm_Model;
                    com.Parameters.Add("@mm_CustomerName", SqlDbType.VarChar).Value = mm_CustomerName;
                    com.Parameters.Add("@mm_Location", SqlDbType.VarChar).Value = mm_Location;
                    com.Parameters.Add("@mm_MaxStock", SqlDbType.VarChar).Value = mm_MaxStock;
                    com.Parameters.Add("@mm_MinStock", SqlDbType.VarChar).Value = mm_MinStock;
                    com.Parameters.Add("@mm_User", SqlDbType.VarChar).Value = mm_User;                   
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }

        public ActionResult Pr_Update_Material_Master()
        {
            try
            {
                string mm_ID = Request.QueryString["mm_ID"].ToString();
                string mm_MaterialCode = Request.QueryString["mm_MaterialCode"].ToString();
                string mm_MaterialDesc = Request.QueryString["mm_MaterialDesc"].ToString();
                string mm_Model = Request.QueryString["mm_Model"].ToString();
                string mm_CustomerName = Request.QueryString["mm_CustomerName"].ToString();
                string mm_Location = Request.QueryString["mm_Location"].ToString();
                string mm_MaxStock = Request.QueryString["mm_MaxStock"].ToString();
                string mm_MinStock = Request.QueryString["mm_MinStock"].ToString();
                //string mm_User = Request.QueryString["mm_User"].ToString();
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Update_Material_Master";
                    com.Parameters.Add("@mm_ID", SqlDbType.VarChar).Value = mm_ID;
                    com.Parameters.Add("@mm_MaterialCode", SqlDbType.VarChar).Value = mm_MaterialCode;
                    com.Parameters.Add("@mm_MaterialDesc", SqlDbType.VarChar).Value = mm_MaterialDesc;
                    com.Parameters.Add("@mm_Model", SqlDbType.VarChar).Value = mm_Model;
                    com.Parameters.Add("@mm_CustomerName", SqlDbType.VarChar).Value = mm_CustomerName;
                    com.Parameters.Add("@mm_Location", SqlDbType.VarChar).Value = mm_Location;
                    com.Parameters.Add("@mm_MaxStock", SqlDbType.VarChar).Value = mm_MaxStock;
                    com.Parameters.Add("@mm_MinStock", SqlDbType.VarChar).Value = mm_MinStock;
                    //com.Parameters.Add("@mm_User", SqlDbType.VarChar).Value = mm_User;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }

        public ActionResult Pr_Insert_Customer_Master()
        {
            try
            {
                string cm_vCustomerID = Request.QueryString["cm_vCustomerID"].ToString();
                string cm_vCustomerName = Request.QueryString["cm_vCustomerName"].ToString();
                string cm_vAddress = Request.QueryString["cm_vAddress"].ToString();
                string cm_vMobileNo1 = Request.QueryString["cm_vMobileNo1"].ToString();
                string cm_EMail = Request.QueryString["cm_EMail"].ToString();
                string cm_GSTNo = Request.QueryString["cm_GSTNo"].ToString();
                string cm_vUser = Request.QueryString["cm_vUser"].ToString();
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Customer_Master";
                    com.Parameters.Add("@cm_vCustomerID", SqlDbType.VarChar).Value = cm_vCustomerID;
                    com.Parameters.Add("@cm_vCustomerName", SqlDbType.VarChar).Value = cm_vCustomerName;
                    com.Parameters.Add("@cm_vAddress", SqlDbType.VarChar).Value = cm_vAddress;
                    com.Parameters.Add("@cm_vMobileNo1", SqlDbType.VarChar).Value = cm_vMobileNo1;
                    com.Parameters.Add("@cm_EMail", SqlDbType.VarChar).Value = cm_EMail;
                    com.Parameters.Add("@cm_GSTNo", SqlDbType.VarChar).Value = cm_GSTNo;
                    com.Parameters.Add("@cm_vUser", SqlDbType.VarChar).Value = cm_vUser;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        public ActionResult Pr_Update_Customer_Master()
        {
            try
            {
                string cm_iTransId = Request.QueryString["cm_iTransId"].ToString();
                string cm_vCustomerID = Request.QueryString["cm_vCustomerID"].ToString();
                string cm_vCustomerName = Request.QueryString["cm_vCustomerName"].ToString();
                string cm_vAddress = Request.QueryString["cm_vAddress"].ToString();
                string cm_vMobileNo1 = Request.QueryString["cm_vMobileNo1"].ToString();
                string cm_EMail = Request.QueryString["cm_EMail"].ToString();
                string cm_GSTNo = Request.QueryString["cm_GSTNo"].ToString();
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Update_Customer_Master";
                    com.Parameters.Add("@cm_iTransId", SqlDbType.VarChar).Value = cm_iTransId;
                    com.Parameters.Add("@cm_vCustomerID", SqlDbType.VarChar).Value = cm_vCustomerID;
                    com.Parameters.Add("@cm_vCustomerName", SqlDbType.VarChar).Value = cm_vCustomerName;
                    com.Parameters.Add("@cm_vAddress", SqlDbType.VarChar).Value = cm_vAddress;
                    com.Parameters.Add("@cm_vMobileNo1", SqlDbType.VarChar).Value = cm_vMobileNo1;
                    com.Parameters.Add("@cm_EMail", SqlDbType.VarChar).Value = cm_EMail;
                    com.Parameters.Add("@cm_GSTNo", SqlDbType.VarChar).Value = cm_GSTNo;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }

        public ActionResult Pr_Update_Supplier_Master()
        {
            try
            {
                string sm_ID = Request.QueryString["sm_ID"].ToString();
                string sm_MaterialCode = Request.QueryString["sm_MaterialCode"].ToString();
                string sm_Place = Request.QueryString["sm_Place"].ToString();
                string sm_Supplier = Request.QueryString["sm_Supplier"].ToString();
                string sm_Source = Request.QueryString["sm_Source"].ToString();
                string sm_ApprovedSupplier = Request.QueryString["sm_ApprovedSupplier"].ToString();
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Update_Supplier_Master";
                    com.Parameters.Add("@sm_ID", SqlDbType.VarChar).Value = sm_ID;
                    com.Parameters.Add("@sm_MaterialCode", SqlDbType.VarChar).Value = sm_MaterialCode;
                    com.Parameters.Add("@sm_Place", SqlDbType.VarChar).Value = sm_Place;
                    com.Parameters.Add("@sm_Supplier", SqlDbType.VarChar).Value = sm_Supplier;
                    com.Parameters.Add("@sm_Source", SqlDbType.VarChar).Value = sm_Source;
                    com.Parameters.Add("@sm_ApprovedSupplier", SqlDbType.VarChar).Value = sm_ApprovedSupplier;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        public ActionResult Pr_Insert_StorageLocation_Master()
        {
            try
            {
                string sl_Group = Request.QueryString["sl_Group"].ToString();
                string sl_BinNo = Request.QueryString["sl_BinNo"].ToString();
                string sl_StorageArea = Request.QueryString["sl_StorageArea"].ToString();
                string sl_User = Request.QueryString["sl_User"].ToString();
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_StorageLocation_Master";
                    com.Parameters.Add("@sl_Group", SqlDbType.VarChar).Value = sl_Group;
                    com.Parameters.Add("@sl_BinNo", SqlDbType.VarChar).Value = sl_BinNo;
                    com.Parameters.Add("@sl_StorageArea", SqlDbType.VarChar).Value = sl_StorageArea;
                    com.Parameters.Add("@sl_User", SqlDbType.VarChar).Value = sl_User;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }

        public ActionResult Pr_Update_StorageLocation_Master()
        {
            try
            {
                string sl_ID = Request.QueryString["sl_ID"].ToString();
                string sl_Group = Request.QueryString["sl_Group"].ToString();
                string sl_BinNo = Request.QueryString["sl_BinNo"].ToString();
                string sl_StorageArea = Request.QueryString["sl_StorageArea"].ToString();
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Update_StorageLocation_Master";
                    com.Parameters.Add("@sl_ID", SqlDbType.VarChar).Value = sl_ID;
                    com.Parameters.Add("@sl_Group", SqlDbType.VarChar).Value = sl_Group;
                    com.Parameters.Add("@sl_BinNo", SqlDbType.VarChar).Value = sl_BinNo;
                    com.Parameters.Add("@sl_StorageArea", SqlDbType.VarChar).Value = sl_StorageArea;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        public ActionResult Pr_Update_Region_Master()
        {
            try
            {

                string rm_Id = Request.QueryString["rm_Id"].ToString();
                string rm_Company_Name = Request.QueryString["rm_Company_Name"].ToString();
                string rm_Region_Name = Request.QueryString["rm_Region_Name"].ToString();
              
                string rm_User = Request.QueryString["rm_User"].ToString();
                // string TM_vDocumentLocation = "FileUpload/Document/" + file_Path;

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Update_Region_Master";
                    ////com.Parameters.Add("@em_id", SqlDbType.VarChar).Value = em_id;
                    com.Parameters.Add("@rm_Company_Name", SqlDbType.VarChar).Value = rm_Company_Name;
                    com.Parameters.Add("@rm_Region_Name", SqlDbType.VarChar).Value = rm_Region_Name;
                   
                    com.Parameters.Add("@rm_User", SqlDbType.VarChar).Value = rm_User;
                    // com.Parameters.Add("@TM_vDocumentLocation", SqlDbType.VarChar).Value = TM_vDocumentLocation;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");


                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        public ActionResult Pr_Insert_Branch_Master()
        {
            try
            {

                string bm_Company_Name = Request.QueryString["bm_Company_Name"].ToString();       

                string bm_Region_Name = Request.QueryString["bm_Region_Name"].ToString();
                string bm_Branch_Name = Request.QueryString["bm_Branch_Name"].ToString();
                string bm_User = Request.QueryString["bm_User"].ToString();
                // string TM_vDocumentLocation = "FileUpload/Document/" + file_Path;

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Branch_Master";
                    com.Parameters.Add("@bm_Company_Name", SqlDbType.VarChar).Value = bm_Company_Name;
                    com.Parameters.Add("@bm_Region_Name", SqlDbType.VarChar).Value = bm_Region_Name;
                    com.Parameters.Add("@bm_Branch_Name", SqlDbType.VarChar).Value = bm_Branch_Name;
                    com.Parameters.Add("@bm_User", SqlDbType.VarChar).Value = bm_User;
                    // com.Parameters.Add("@TM_vDocumentLocation", SqlDbType.VarChar).Value = TM_vDocumentLocation;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");


                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }

        public ActionResult Pr_Insert_Product_Master()
        {
            try
            {

                string pm_Product = Request.QueryString["pm_Product"].ToString();
                string pm_Make = Request.QueryString["pm_Make"].ToString();
                string pm_Model = Request.QueryString["pm_Model"].ToString();
                string pm_Qty = Request.QueryString["pm_Qty"].ToString();
                string pm_User = Request.QueryString["pm_User"].ToString();
                // string TM_vDocumentLocation = "FileUpload/Document/" + file_Path;

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Insert_Product_Master";
                    com.Parameters.Add("@pm_Product", SqlDbType.VarChar).Value = pm_Product;
                    com.Parameters.Add("@pm_Make", SqlDbType.VarChar).Value = pm_Make;
                    com.Parameters.Add("@pm_Model", SqlDbType.VarChar).Value = pm_Model;
                    com.Parameters.Add("@pm_Qty", SqlDbType.VarChar).Value = pm_Qty;
                    com.Parameters.Add("@pm_User", SqlDbType.VarChar).Value = pm_User;
                    // com.Parameters.Add("@TM_vDocumentLocation", SqlDbType.VarChar).Value = TM_vDocumentLocation;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        public ActionResult Pr_Insert_Location_Master()
            {
                try
                {
                    string lm_LocationCode = Request.QueryString["lm_LocationCode"].ToString();
                    string lm_LocationName = Request.QueryString["lm_LocationName"].ToString();
                    string lm_User = Request.QueryString["lm_User"].ToString();
                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Insert_Location_Master";
                        com.Parameters.Add("@lm_LocationCode", SqlDbType.VarChar).Value = lm_LocationCode;
                        com.Parameters.Add("@lm_LocationName", SqlDbType.VarChar).Value = lm_LocationName;
                        com.Parameters.Add("@lm_User", SqlDbType.VarChar).Value = lm_User;
                        com.ExecuteNonQuery();
                        con.Close();
                        return Json("True");
                    }
                    catch (Exception ex)
                    {
                        return Json("" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }


                [HttpPost]
        public ActionResult Pr_Update_Team_Master()
        {

            //Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string fname = "";
                    string file_Path = "";

                    HttpPostedFileBase file = files[0];

                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    DataTable dt = dbFunctions.getTable("select isnull(max(TM_iid),0)+1 from Team_Master");
                    string ID = dt.Rows[0][0].ToString();
                    string TM_iid = Request.QueryString["TM_iid"].ToString();

                    string TM_vTeam_Code = Request.QueryString["TM_vTeam_Code"].ToString();
                    file_Path = TM_vTeam_Code + ".png";

                    fname = Path.Combine(Server.MapPath("~/FileUpload/Document/"), file_Path);
                    file.SaveAs(fname);

                    string TM_vTeam_Name = Request.QueryString["TM_vTeam_Name"].ToString();
                    string TM_vShort_Name = Request.QueryString["TM_vShort_Name"].ToString();
                    string TM_iTotal_Players = Request.QueryString["TM_iTotal_Players"].ToString();
                    string TM_iPlaying_Player = Request.QueryString["TM_iPlaying_Player"].ToString();
                    string TM_Created_by = Request.QueryString["TM_Created_by"].ToString();
                    string TM_vDocumentLocation = "FileUpload/Document/" + file_Path;

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Update_Team_Master";
                        com.Parameters.Add("@TM_iid", SqlDbType.VarChar).Value = TM_iid;
                        com.Parameters.Add("@TM_vTeam_Code", SqlDbType.VarChar).Value = TM_vTeam_Code;
                        com.Parameters.Add("@TM_vTeam_Name", SqlDbType.VarChar).Value = TM_vTeam_Name;
                        com.Parameters.Add("@TM_vShort_Name", SqlDbType.VarChar).Value = TM_vShort_Name;
                        com.Parameters.Add("@TM_iTotal_Players", SqlDbType.VarChar).Value = TM_iTotal_Players;
                        com.Parameters.Add("@TM_iPlaying_Player", SqlDbType.VarChar).Value = TM_iPlaying_Player;
                        com.Parameters.Add("@TM_Created_by", SqlDbType.VarChar).Value = TM_Created_by;
                        com.Parameters.Add("@TM_vDocumentLocation", SqlDbType.VarChar).Value = TM_vDocumentLocation;
                        com.ExecuteNonQuery();
                        con.Close();
                        return Json("True");


                    }
                    catch (Exception ex)
                    {
                        return Json("" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }



        [HttpPost]
        public ActionResult Pr_Update_Vendor_Master()
        {
          
                try
                {
                string vm_Id = Request.QueryString["vm_VendorNvm_Idame"].ToString();
                string vm_VendorName = Request.QueryString["vm_VendorName"].ToString();

                string vm_VendorAddress = Request.QueryString["vm_VendorAddress"].ToString();
                string vm_Location = Request.QueryString["vm_Location"].ToString();
                string vm_ContactPersonName = Request.QueryString["vm_ContactPersonName"].ToString();
                string vm_ContactNumber = Request.QueryString["vm_ContactNumber"].ToString();
                string vm_Email = Request.QueryString["vm_Email"].ToString();
                string vm_GSTIN_Number = Request.QueryString["vm_GSTIN_Number"].ToString();

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Update_Vendor_Master";
                        com.Parameters.Add("@vm_Id", SqlDbType.VarChar).Value = vm_Id;
                        com.Parameters.Add("@vm_VendorName", SqlDbType.VarChar).Value = vm_VendorName;
                        com.Parameters.Add("@vm_VendorAddress", SqlDbType.VarChar).Value = vm_VendorAddress;
                        com.Parameters.Add("@vm_Location", SqlDbType.VarChar).Value = vm_Location;
                        com.Parameters.Add("@vm_ContactPersonName", SqlDbType.VarChar).Value = vm_ContactPersonName;
                        com.Parameters.Add("@vm_ContactNumber", SqlDbType.VarChar).Value = vm_ContactNumber;
                        com.Parameters.Add("@vm_Email", SqlDbType.VarChar).Value = vm_Email;
                    com.Parameters.Add("@vm_GSTIN_Number", SqlDbType.VarChar).Value = vm_GSTIN_Number;
                    com.ExecuteNonQuery();
                        con.Close();
                        return Json("True");
                        

                    }
                    catch (Exception ex)
                    {
                        return Json("" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            //}
            //else
            //{
            //    return Json("No files selected.");
            //}

        }

        [HttpPost]
        public ActionResult Pr_Update_RSR_BIN_Master()
        {

          
            try
            {


                DataTable dt = dbFunctions.getTable("select isnull(max(rbm_Id),0)+1 from RSR_BIN_Master");
                string ID = dt.Rows[0][0].ToString();
                string rbm_Id = Request.QueryString["rbm_Id"].ToString();

                string rbm_Bin_Type = Request.QueryString["rbm_Bin_Type"].ToString();
                string rbm_Bin_Code = Request.QueryString["rbm_Bin_Code"].ToString();
                string rbm_Bin_Name = Request.QueryString["rbm_Bin_Name"].ToString();
                string rbm_Quantity = Request.QueryString["rbm_Quantity"].ToString();
                string rbm_UOM = Request.QueryString["rbm_UOM"].ToString();
                string rbm_User = Request.QueryString["rbm_User"].ToString();
                // string TM_vDocumentLocation = "FileUpload/Document/" + file_Path;

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Update_RSR_BIN_Master";
                    com.Parameters.Add("@rbm_Id", SqlDbType.VarChar).Value = rbm_Id;
                    com.Parameters.Add("@rbm_Bin_Type", SqlDbType.VarChar).Value = rbm_Bin_Type;
                    com.Parameters.Add("@rbm_Bin_Code", SqlDbType.VarChar).Value = rbm_Bin_Code;
                    com.Parameters.Add("@rbm_Bin_Name", SqlDbType.VarChar).Value = rbm_Bin_Name;
                    com.Parameters.Add("@rbm_Quantity", SqlDbType.VarChar).Value = rbm_Quantity;
                    com.Parameters.Add("@rbm_UOM", SqlDbType.VarChar).Value = rbm_UOM;
                    com.Parameters.Add("@rbm_User", SqlDbType.VarChar).Value = rbm_User;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
 
        }


        [HttpPost]
        public ActionResult Pr_Update_Branch_Master()
        {
            try
            {
                string bm_Id = Request.QueryString["bm_Id"].ToString();

                string bm_Company_Name = Request.QueryString["bm_Company_Name"].ToString();
                string bm_Region_Name = Request.QueryString["bm_Region_Name"].ToString();
                string bm_Branch_Name = Request.QueryString["bm_Branch_Name"].ToString();
                string bm_User = Request.QueryString["bm_User"].ToString();

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Update_Branch_Master";
                    com.Parameters.Add("@bm_Id", SqlDbType.VarChar).Value = bm_Id;
                    com.Parameters.Add("@bm_Company_Name", SqlDbType.VarChar).Value = bm_Company_Name;
                    com.Parameters.Add("@bm_Region_Name", SqlDbType.VarChar).Value = bm_Region_Name;
                    com.Parameters.Add("@bm_Branch_Name", SqlDbType.VarChar).Value = bm_Branch_Name;
                    com.Parameters.Add("@bm_User", SqlDbType.VarChar).Value = bm_User;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");


                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        [HttpPost]
        public ActionResult Pr_Update_Product_Master()
        {
            try
            {
                string pm_Id = Request.QueryString["pm_Id"].ToString();
                string pm_Product = Request.QueryString["pm_Product"].ToString();
                string pm_Make = Request.QueryString["pm_Make"].ToString();
                string pm_Model = Request.QueryString["pm_Model"].ToString();
                string pm_Qty = Request.QueryString["pm_Qty"].ToString();
                //string pm_User = Request.QueryString["pm_User"].ToString();

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Update_Product_Master";
                    com.Parameters.Add("@pm_Id", SqlDbType.VarChar).Value = pm_Id;
                    com.Parameters.Add("@pm_Product", SqlDbType.VarChar).Value = pm_Product;
                    com.Parameters.Add("@pm_Make", SqlDbType.VarChar).Value = pm_Make;
                    com.Parameters.Add("@pm_Model", SqlDbType.VarChar).Value = pm_Model;
                    com.Parameters.Add("@pm_Qty", SqlDbType.VarChar).Value = pm_Qty;
                    //com.Parameters.Add("@pm_User", SqlDbType.VarChar).Value = pm_User;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        [HttpPost]
        public ActionResult Pr_Update_Location_Master()
        {
            try
            {
                string lm_LocationCode = Request.QueryString["lm_LocationCode"].ToString();
                string lm_LocationName = Request.QueryString["lm_LocationName"].ToString();

                SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "Pr_Update_Location_Master";
                    com.Parameters.Add("@lm_LocationCode", SqlDbType.VarChar).Value = lm_LocationCode;
                    com.Parameters.Add("@lm_LocationName", SqlDbType.VarChar).Value = lm_LocationName;
                    com.ExecuteNonQuery();
                    con.Close();
                    return Json("True");
                }
                catch (Exception ex)
                {
                    return Json("" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        //public string Pr_Update_Team_Master(
        //string TM_iid,
        //string TM_vTeam_Code,
        //string TM_vTeam_Name,
        //string TM_vShort_Name,
        //string TM_iTotal_Players,
        //string TM_iPlaying_Player,
        //string TM_Created_by
        //)
        //{
        //    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
        //    try
        //    {
        //        con.Open();
        //        SqlCommand com = new SqlCommand();
        //        com.Connection = con;
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.CommandText = "Pr_Update_Team_Master";
        //        com.Parameters.Add("@TM_iid", SqlDbType.VarChar).Value = TM_iid;
        //        com.Parameters.Add("@TM_vTeam_Code", SqlDbType.VarChar).Value = TM_vTeam_Code;
        //        com.Parameters.Add("@TM_vTeam_Name", SqlDbType.VarChar).Value = TM_vTeam_Name;
        //        com.Parameters.Add("@TM_vShort_Name", SqlDbType.VarChar).Value = TM_vShort_Name;
        //        com.Parameters.Add("@TM_iTotal_Players", SqlDbType.VarChar).Value = TM_iTotal_Players;
        //        com.Parameters.Add("@TM_iPlaying_Player", SqlDbType.VarChar).Value = TM_iPlaying_Player;
        //        com.Parameters.Add("@TM_Created_by", SqlDbType.VarChar).Value = TM_Created_by;
        //        com.ExecuteNonQuery();
        //        con.Close();
        //        return "True";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        [HttpPost]
        public ActionResult Pr_Insert_Player_Master()
        {

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string fname = "";
                    string file_Path = "";

                    HttpPostedFileBase file = files[0];

                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    DataTable dt = dbFunctions.getTable("select isnull(max(PM_iID),0)+1 from Player_Master");
                    string ID = dt.Rows[0][0].ToString();

                    string PM_vCode = Request.QueryString["PM_vCode"].ToString();

                    file_Path = PM_vCode + ".png";

                    fname = Path.Combine(Server.MapPath("~/FileUpload/Document/"), file_Path);
                    file.SaveAs(fname);


                    
                    string PM_vPlayer_Name = Request.QueryString["PM_vPlayer_Name"].ToString();
                    string PM_vShort_Name = Request.QueryString["PM_vShort_Name"].ToString();
                    string PM_County = Request.QueryString["PM_County"].ToString();
                    string PM_vPlay_Type = Request.QueryString["PM_vPlay_Type"].ToString();
                    string PM_iCredits = Request.QueryString["PM_iCredits"].ToString();
                    string PM_iPoints = Request.QueryString["PM_iPoints"].ToString();
                    string PM_vDocumentLocation = "FileUpload/Document/" + file_Path;
                    string PM_Created_by = Request.QueryString["PM_Created_by"].ToString();

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Insert_Player_Master";
                        com.Parameters.Add("@PM_vCode", SqlDbType.VarChar).Value = PM_vCode;
                        com.Parameters.Add("@PM_vPlayer_Name", SqlDbType.VarChar).Value = PM_vPlayer_Name;
                        com.Parameters.Add("@PM_vShort_Name", SqlDbType.VarChar).Value = PM_vShort_Name;
                        com.Parameters.Add("@PM_County", SqlDbType.VarChar).Value = PM_County;
                        com.Parameters.Add("@PM_vPlay_Type", SqlDbType.VarChar).Value = PM_vPlay_Type;
                        com.Parameters.Add("@PM_iCredits", SqlDbType.VarChar).Value = PM_iCredits;
                        com.Parameters.Add("@PM_iPoints", SqlDbType.VarChar).Value = PM_iPoints;
                        com.Parameters.Add("@PM_Created_by", SqlDbType.VarChar).Value = PM_Created_by;
                        com.Parameters.Add("@PM_vDocumentLocation", SqlDbType.VarChar).Value = PM_vDocumentLocation;
                        com.ExecuteNonQuery();
                        con.Close();
                        return Json("True");


                    }
                    catch (Exception ex)
                    {
                        return Json("" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }


        [HttpPost]
        public ActionResult Pr_update_Player_Master()
        {

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string fname = "";
                    string file_Path = "";

                    HttpPostedFileBase file = files[0];

                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    DataTable dt = dbFunctions.getTable("select isnull(max(PM_iID),0)+1 from Player_Master");
                    string ID = dt.Rows[0][0].ToString();
                    string PM_iID = Request.QueryString["PM_iID"].ToString();

                    string PM_vCode = Request.QueryString["PM_vCode"].ToString();
                    file_Path = PM_vCode + ".png";

                    fname = Path.Combine(Server.MapPath("~/FileUpload/Document/"), file_Path);
                    file.SaveAs(fname);

                                        
                    string PM_vPlayer_Name = Request.QueryString["PM_vPlayer_Name"].ToString();
                    string PM_vShort_Name = Request.QueryString["PM_vShort_Name"].ToString();
                    string PM_County = Request.QueryString["PM_County"].ToString();
                    string PM_vPlay_Type = Request.QueryString["PM_vPlay_Type"].ToString();
                    string PM_iCredits = Request.QueryString["PM_iCredits"].ToString();
                    string PM_iPoints = Request.QueryString["PM_iPoints"].ToString();
                    string PM_vDocumentLocation = "FileUpload/Document/" + file_Path;
                    string PM_Created_by = Request.QueryString["PM_Created_by"].ToString();

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_update_Player_Master";
                        com.Parameters.Add("@PM_iID", SqlDbType.VarChar).Value = PM_iID;
                        com.Parameters.Add("@PM_vCode", SqlDbType.VarChar).Value = PM_vCode;
                        com.Parameters.Add("@PM_vPlayer_Name", SqlDbType.VarChar).Value = PM_vPlayer_Name;
                        com.Parameters.Add("@PM_vShort_Name", SqlDbType.VarChar).Value = PM_vShort_Name;
                        com.Parameters.Add("@PM_County", SqlDbType.VarChar).Value = PM_County;
                        com.Parameters.Add("@PM_vPlay_Type", SqlDbType.VarChar).Value = PM_vPlay_Type;
                        com.Parameters.Add("@PM_iCredits", SqlDbType.VarChar).Value = PM_iCredits;
                        com.Parameters.Add("@PM_iPoints", SqlDbType.VarChar).Value = PM_iPoints;
                        com.Parameters.Add("@PM_Created_by", SqlDbType.VarChar).Value = PM_Created_by;
                        com.Parameters.Add("@PM_vDocumentLocation", SqlDbType.VarChar).Value = PM_vDocumentLocation;
                        com.ExecuteNonQuery();
                        con.Close();
                        return Json("True");


                    }
                    catch (Exception ex)
                    {
                        return Json("" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }


        //public string Pr_Insert_Player_Master(
        //string PM_vCode,
        //string PM_vPlayer_Name,
        //string PM_vShort_Name,
        //string PM_County,
        //string PM_vPlay_Type,
        //string PM_iCredits,
        //string PM_iPoints,
        //string PM_Created_by
        //)
        //{
        //    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
        //    try
        //    {
        //        con.Open();
        //        SqlCommand com = new SqlCommand();
        //        com.Connection = con;
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.CommandText = "Pr_Insert_Player_Master";
        //        com.Parameters.Add("@PM_vCode", SqlDbType.VarChar).Value = PM_vCode;
        //        com.Parameters.Add("@PM_vPlayer_Name", SqlDbType.VarChar).Value = PM_vPlayer_Name;
        //        com.Parameters.Add("@PM_vShort_Name", SqlDbType.VarChar).Value = PM_vShort_Name;
        //        com.Parameters.Add("@PM_County", SqlDbType.VarChar).Value = PM_County;
        //        com.Parameters.Add("@PM_vPlay_Type", SqlDbType.VarChar).Value = PM_vPlay_Type;
        //        com.Parameters.Add("@PM_iCredits", SqlDbType.VarChar).Value = PM_iCredits;
        //        com.Parameters.Add("@PM_iPoints", SqlDbType.VarChar).Value = PM_iPoints;
        //        com.Parameters.Add("@PM_Created_by", SqlDbType.VarChar).Value = PM_Created_by;
        //        com.ExecuteNonQuery();
        //        con.Close();
        //        return "True";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}


        public string Pr_Update_Player_Master(
        string PM_iID,
        string PM_vCode,
        string PM_vPlayer_Name,
        string PM_vShort_Name,
        string PM_County,
        string PM_vPlay_Type,
        string PM_iCredits,
        string PM_iPoints,
        string PM_Created_by
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Player_Master";
                com.Parameters.Add("@PM_iID", SqlDbType.VarChar).Value = PM_iID;
                com.Parameters.Add("@PM_vCode", SqlDbType.VarChar).Value = PM_vCode;
                com.Parameters.Add("@PM_vPlayer_Name", SqlDbType.VarChar).Value = PM_vPlayer_Name;
                com.Parameters.Add("@PM_vShort_Name", SqlDbType.VarChar).Value = PM_vShort_Name;
                com.Parameters.Add("@PM_County", SqlDbType.VarChar).Value = PM_County;
                com.Parameters.Add("@PM_vPlay_Type", SqlDbType.VarChar).Value = PM_vPlay_Type;
                com.Parameters.Add("@PM_iCredits", SqlDbType.VarChar).Value = PM_iCredits;
                com.Parameters.Add("@PM_iPoints", SqlDbType.VarChar).Value = PM_iPoints;
                com.Parameters.Add("@PM_Created_by", SqlDbType.VarChar).Value = PM_Created_by;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [HttpPost]
        public ActionResult Pr_Insert_Vehicle_Document_Upload()
        {

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string fname = "";
                    string file_Path = "";

                    HttpPostedFileBase file = files[0];

                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    DataTable dt = dbFunctions.getTable("select isnull(max(VD_iiD),0)+1 from Vehicle_Document");
                    string ID = dt.Rows[0][0].ToString();

                    file_Path = Request.QueryString["Code"].ToString() + "_" + Request.QueryString["VD_Document_Name"].ToString() + "_" + ID + "_" + fname;

                    fname = Path.Combine(Server.MapPath("~/FileUpload/Document/"), file_Path);
                    file.SaveAs(fname);


                    string DD_Driver_ID = Request.QueryString["VD_Vehicle_ID"].ToString();
                    string DD_Document_Name = Request.QueryString["VD_Document_Name"].ToString();
                    string DU_vDocument_Location = "FileUpload/Document/" + file_Path;
                    string DD_Created_by = Request.QueryString["VD_vCreatedby"].ToString();

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Insert_Vehicle_Document";
                        com.Parameters.Add("@VD_Vehicle_ID", SqlDbType.VarChar).Value = DD_Driver_ID;
                        com.Parameters.Add("@VD_Document_Name", SqlDbType.VarChar).Value = DD_Document_Name;
                        com.Parameters.Add("@VD_Document_Location", SqlDbType.VarChar).Value = DU_vDocument_Location;
                        com.Parameters.Add("@VD_Created_by", SqlDbType.VarChar).Value = DD_Created_by;
                        com.ExecuteNonQuery();
                        con.Close();
                        return Json("True");


                    }
                    catch (Exception ex)
                    {
                        return Json("" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }




        public string Pr_Fetch_Vehicle_Company_Master_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Company_Master_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_Driver_Document_Details(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Driver_Document_Details "+ID);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_Vehicle_Document_Details(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Document_Details " + ID);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_Vehicle_Document(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Vehicle_Document '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Location_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Location_Master '" + ID + "'");
            return "True";
        }


        public string Pr_Delete_Driver_Document(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Driver_Document '" + ID + "'");
            return "True";
        }



        public string Pr_Fetch_Vehicle_Company_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Company_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_BM_vCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_BM_vCode ");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Load_CM_vCompanyName1()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_CM_vCompanyName1 ");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Load_CM_vCompanyName_All()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_CM_vCompanyName_All ");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        
        public string Pr_Fetch_DM_vCode( string Category)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_DM_vCode '" + Category+"'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        
        
        public string pr_get_Reference_Values(string Type)
        {
            DataTable dt = dbFunctions.getTable("pr_get_Reference_Values  '" + Type + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }



        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string fname = "";
                    string imagename = "";
                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    imagename = "Org" + Request.QueryString["ORG_iID"].ToString() + fname;
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/FileUpload/"), "" + Request.QueryString["ORG_iID"].ToString()+".jpg");
                    file.SaveAs(fname);
                }
                catch (Exception ex)
                {
                    return Json("No files selected.");
                }
            }
            else
            {
                return Json("No files selected.");
            }
            return Json("No files selected.");
        } 


        
        public string Pr_Delete_Vehicle_Company_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Vehicle_Company_Master '" + ID + "'");
            return "True";
        }


        public string Pr_Insert_Vehicle_Company_Master(
        string VC_iVehicle_No,
        string VC_iCompay_Name,
        string VC_vCreatedby
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Vehicle_Company_Master";
                com.Parameters.Add("@VC_iVehicle_No", SqlDbType.VarChar).Value = VC_iVehicle_No;
                com.Parameters.Add("@VC_iCompay_Name", SqlDbType.VarChar).Value = VC_iCompay_Name;
                com.Parameters.Add("@VC_vCreatedby", SqlDbType.VarChar).Value = VC_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string pr_Insert_Product_MasterAssy(string PM_Item_Code,string PM_Mold,string PM_Sector,string PM_Description, string PM_Variant_Code,
            string PM_Skid_Qty,string PM_MOQ_Qty, string SerialStart, string SerialEnd, string VStart, string VEnd, string compare_code, string chargeing_Dub_Hrs,
            string PM_Created_by
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "declare @PM_iID  int select  @PM_iID = isnull(max(PM_iID), 0) + 1 from Product_Master_Assembly insert into Product_Master_Assembly    (PM_iID, PM_Item_Code, PM_Mold, PM_Sector, PM_Description, PM_Variant_Code, PM_Skid_Qty, PM_MOQ_Qty, PM_Created_by, PM_Created_Date, PM_Status, SerialStart, SerialEnd, VStart, VEnd, compare_code, chargeing_Dub_Hrs) Values(@PM_iID, @PM_Item_Code, @PM_Mold, @PM_Sector, @PM_Description, @PM_Variant_Code, @PM_Skid_Qty, @PM_MOQ_Qty, @PM_Created_by, getdate(),    'A',  @SerialStart,  @SerialEnd,  @VStart,  @VEnd   ,  @compare_code ,@chargeing_Dub_Hrs )    ";
                com.Parameters.Add("@PM_Item_Code", SqlDbType.VarChar).Value = PM_Item_Code;
                com.Parameters.Add("@PM_Mold", SqlDbType.VarChar).Value = PM_Mold;
                com.Parameters.Add("@PM_Sector", SqlDbType.VarChar).Value = PM_Sector;
                com.Parameters.Add("@PM_Description", SqlDbType.VarChar).Value = PM_Description;
                com.Parameters.Add("@PM_Variant_Code", SqlDbType.VarChar).Value = PM_Variant_Code;
                com.Parameters.Add("@PM_Skid_Qty", SqlDbType.VarChar).Value = PM_Skid_Qty;
                com.Parameters.Add("@PM_MOQ_Qty", SqlDbType.VarChar).Value = PM_MOQ_Qty;
                com.Parameters.Add("@PM_Created_by", SqlDbType.VarChar).Value = PM_Created_by;
                com.Parameters.Add("@SerialStart", SqlDbType.VarChar).Value = SerialStart;
                com.Parameters.Add("@SerialEnd", SqlDbType.VarChar).Value = SerialEnd;
                com.Parameters.Add("@VStart", SqlDbType.VarChar).Value = VStart;
                com.Parameters.Add("@VEnd", SqlDbType.VarChar).Value = VEnd;
                com.Parameters.Add("@compare_code", SqlDbType.VarChar).Value = compare_code;
                com.Parameters.Add("@chargeing_Dub_Hrs", SqlDbType.VarChar).Value = chargeing_Dub_Hrs;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_Update_Product_MasterAssy(string PM_Item_Code, string PM_Mold, string PM_Sector, string PM_Description, string PM_Variant_Code,
            string PM_Skid_Qty, string PM_MOQ_Qty, string SerialStart, string SerialEnd, string VStart, string VEnd, string compare_code, string chargeing_Dub_Hrs,
            string PM_iID
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "update Product_Master_Assembly Set PM_Item_Code=@PM_Item_Code, PM_Mold=@PM_Mold, PM_Sector=@PM_Sector, PM_Description=@PM_Description,    PM_Variant_Code=@PM_Variant_Code, PM_Skid_Qty=@PM_Skid_Qty, PM_MOQ_Qty=@PM_MOQ_Qty, SerialStart=@SerialStart,  SerialEnd=@SerialEnd,  VStart=@VStart ,  VEnd=@VEnd ,  compare_code=@compare_code, chargeing_Dub_Hrs = @chargeing_Dub_Hrs  where  PM_iID=@PM_iID   ";
                com.Parameters.Add("@PM_iID", SqlDbType.VarChar).Value = PM_iID;
                com.Parameters.Add("@PM_Item_Code", SqlDbType.VarChar).Value = PM_Item_Code;
                com.Parameters.Add("@PM_Mold", SqlDbType.VarChar).Value = PM_Mold;
                com.Parameters.Add("@PM_Sector", SqlDbType.VarChar).Value = PM_Sector;
                com.Parameters.Add("@PM_Description", SqlDbType.VarChar).Value = PM_Description;
                com.Parameters.Add("@PM_Variant_Code", SqlDbType.VarChar).Value = PM_Variant_Code;
                com.Parameters.Add("@PM_Skid_Qty", SqlDbType.VarChar).Value = PM_Skid_Qty;
                com.Parameters.Add("@PM_MOQ_Qty", SqlDbType.VarChar).Value = PM_MOQ_Qty;
                com.Parameters.Add("@SerialStart", SqlDbType.VarChar).Value = SerialStart;
                com.Parameters.Add("@SerialEnd", SqlDbType.VarChar).Value = SerialEnd;
                com.Parameters.Add("@VStart", SqlDbType.VarChar).Value = VStart;
                com.Parameters.Add("@VEnd", SqlDbType.VarChar).Value = VEnd;
                com.Parameters.Add("@compare_code", SqlDbType.VarChar).Value = compare_code;
                com.Parameters.Add("@chargeing_Dub_Hrs", SqlDbType.VarChar).Value = chargeing_Dub_Hrs;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Load_VM_vVehicle_No()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_VM_vVehicle_No");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string pr_get_Vehicle_Zonal()
        {

            DataTable dt = dbFunctions.getTable("pr_get_Vehicle_Zonal");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Load_VM_vVehicle_No1()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_VM_vVehicle_No1");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }



        public string Pr_Load_CM_vCompanyName()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_CM_vCompanyName");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_Vehicle_Driver_Master_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Driver_Master_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Fetch_Vehicle_Driver_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Driver_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string Pr_Delete_Vehicle_Driver_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Vehicle_Driver_Master '" + ID + "'");
            return "True";
        }


        public string pr_get_Driver_Group(string Group)
        {
            DataTable dt = dbFunctions.getTable("pr_get_Driver_Group  '" + Group + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string pr_get_Driver_Group1(string Group)
        {
            DataTable dt = dbFunctions.getTable("pr_get_Driver_Group1  '" + Group + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }       
      
        public string Pr_get_Group()
        {
            DataTable dt = dbFunctions.getTable("Pr_get_Group");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_get_Group1()
        {
            DataTable dt = dbFunctions.getTable("Pr_get_Group1");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
       
        public string Pr_Insert_Vehicle_Driver_Master(
        string VD_iVehicle_No,
        string VD_iDriver_Name,
        string VD_vCreatedby
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Vehicle_Driver_Master";
                com.Parameters.Add("@VD_iVehicle_No", SqlDbType.VarChar).Value = VD_iVehicle_No;
                com.Parameters.Add("@VD_iDriver_Name", SqlDbType.VarChar).Value = VD_iDriver_Name;
                com.Parameters.Add("@VD_vCreatedby", SqlDbType.VarChar).Value = VD_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Pr_Update_Vehicle_Driver_Master(
        string VD_iid,
        string VD_iVehicle_No,
        string VD_iDriver_Name,
        string VD_vCreatedby
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Vehicle_Driver_Master";
                com.Parameters.Add("@VD_iid", SqlDbType.VarChar).Value = VD_iid;
                com.Parameters.Add("@VD_iVehicle_No", SqlDbType.VarChar).Value = VD_iVehicle_No;
                com.Parameters.Add("@VD_iDriver_Name", SqlDbType.VarChar).Value = VD_iDriver_Name;
                com.Parameters.Add("@VD_vCreatedby", SqlDbType.VarChar).Value = VD_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Pr_Load_RM_vPlace()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_RM_vPlace");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        
         public string Pr_Load_RM_vPlace1(string id)
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_RM_vPlace1 "+id);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        

        public string Pr_Load_DM_vName()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_DM_vName");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Load_DM_vName1()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_DM_vName1");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
     
        public string Pr_Load_RG_vDescription()
        {
            DataTable dt = dbFunctions.getTable("Pr_Load_RG_vDescription");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_ReferenceGroup_Value_Details(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_ReferenceGroup_Value_Details'" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Vehicle_Master_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Master_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string Pr_Fetch_Vehicle_Master_Details1(string Status)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Master_Details1 '" + Status+"'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Vehicle_Master_Details_A()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Master_Details_A");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Vehicle_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_get_Route_Code(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_get_Route_Code '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }       

        public string Pr_Delete_Vehicle_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Vehicle_Master '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Vehicle_MasterA(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Vehicle_MasterA '" + ID + "'");
            return "True";
        }


        public string Pr_Insert_ReferenceGroup_Value(string RGV_iRG_ID, string RGV_vCode, string RGV_vDesciption )
        {
            
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_ReferenceGroup_Value";
                com.Parameters.Add("@RGV_iRG_ID", SqlDbType.VarChar).Value = RGV_iRG_ID;
                com.Parameters.Add("@RGV_vCode", SqlDbType.VarChar).Value = RGV_vCode;
                com.Parameters.Add("@RGV_vDesciption", SqlDbType.VarChar).Value = RGV_vDesciption;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Pr_Update_ReferenceGroup_Value(string RGV_iID, string RGV_vCode, string RGV_vDesciption)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_ReferenceGroup_Value";
                com.Parameters.Add("@RGV_iID", SqlDbType.VarChar).Value = RGV_iID;
                //com.Parameters.Add("@RGV_iRG_ID", SqlDbType.VarChar).Value = RGV_iRG_ID;
                com.Parameters.Add("@RGV_vCode", SqlDbType.VarChar).Value = RGV_vCode;
                com.Parameters.Add("@RGV_vDesciption", SqlDbType.VarChar).Value = RGV_vDesciption;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

      
        public string Pr_Fetch_Bunk_Master_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Bunk_Master_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Bunk_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Bunk_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Delete_Bunk_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Bunk_Master '" + ID + "'");
            return "True";
        }


        public string Pr_Fetch_Refernce_Group_Value_Details(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Refernce_Group_Value_Details "+ID);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Refernce_Group_Value_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Refernce_Group_Value_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Delete_Refernce_Group_Value(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Refernce_Group_Value '" + ID + "'");
            return "True";
        }

        public string Pr_Insert_Refernce_Group_Value(
        string RGV_iRG_ID,
        string RGV_vCode,
        string RGV_vDesciption,
        string RGV_vCreatedby
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Refernce_Group_Value";
                com.Parameters.Add("@RGV_iRG_ID", SqlDbType.VarChar).Value = RGV_iRG_ID;
                com.Parameters.Add("@RGV_vCode", SqlDbType.VarChar).Value = RGV_vCode;
                com.Parameters.Add("@RGV_vDesciption", SqlDbType.VarChar).Value = RGV_vDesciption;
                com.Parameters.Add("@RGV_vCreatedby", SqlDbType.VarChar).Value = RGV_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Pr_Update_Refernce_Group_Value(
        string RGV_iID,
        string RGV_iRG_ID,
        string RGV_vCode,
        string RGV_vDesciption,
        string RGV_vCreatedby
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Refernce_Group_Value";
                com.Parameters.Add("@RGV_iID", SqlDbType.VarChar).Value = RGV_iID;
                com.Parameters.Add("@RGV_iRG_ID", SqlDbType.VarChar).Value = RGV_iRG_ID;
                com.Parameters.Add("@RGV_vCode", SqlDbType.VarChar).Value = RGV_vCode;
                com.Parameters.Add("@RGV_vDesciption", SqlDbType.VarChar).Value = RGV_vDesciption;
                com.Parameters.Add("@RGV_vCreatedby", SqlDbType.VarChar).Value = RGV_vCreatedby;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Pr_Fetch_Driver_Master_Details(string Type,string Category)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Driver_Master_Details  '"+Type+"','"+Category+"'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Driver_Master_Salay_Details(string Type, string Category)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Driver_Master_Salay_Details  '" + Type + "','" + Category + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Driver_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Driver_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Delete_Driver_Master(string ID,string Reason)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Driver_Master '" + ID + "','"+Reason+"'");
            return "True";
        }

        public string Pr_Delete_Driver_Master_Salary(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Driver_Master_Salary '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Driver_Master1(string ID, string Reason,string Date,string Type)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Driver_Master1 '" + ID + "','"+Reason+"','"+Date+"','"+Type+"'");
            return "True";
        }
                
      
        public string Pr_Fetch_Route_Master_Details(string Status)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Route_Master_Details '"+Status+"'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string Pr_Fetch_RM_iid()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_RM_iid");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string Pr_Fetch_CM_vCompanyCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_CM_vCompanyCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Route_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Route_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Vehicle_Category_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Category_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Vehicle_Category_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Vehicle_Category_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Delete_Vehicle_Category(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Vehicle_Category '" + ID + "'");
            return "True";
        }

        public string Pr_Insert_Vehicle_Category(
        string Vehicle_No,
        string Created_by
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Vehicle_Category";
                com.Parameters.Add("@Vehicle_No", SqlDbType.VarChar).Value = Vehicle_No;
                com.Parameters.Add("@Created_by", SqlDbType.VarChar).Value = Created_by;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Pr_Update_Vehicle_Category(
        string ID,
        string Vehicle_No,
        string Created_by
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Vehicle_Category";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@Vehicle_No", SqlDbType.VarChar).Value = Vehicle_No;
                com.Parameters.Add("@Created_by", SqlDbType.VarChar).Value = Created_by;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Pr_Delete_Route_Master(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Route_Master '" + ID + "'");
            return "True";
        }

        public string Pr_GetTeamCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_GetTeamCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_GetVendorCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_GetVendorCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_GetRSRBINCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_GetRSRBINCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_GetREmployeeCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_GetREmployeeCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_GetCustomerCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_GetCustomerCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_GetBranchCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_GetBranchCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_GetLocationCode()
        {
            DataTable dt = dbFunctions.getTable("Pr_GetLocationCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Company_Master_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Company_Master_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public ActionResult Export_To_Excel(string FileName, string Heading, string Data)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            Response.Write("<font style='font-size:10.0pt; font-family:Verdana;'>");
            Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:12.0pt; font-family:Times New Roman; background:white;'><caption><font  style='font-size:14.0pt;' >" + Heading + "</font></caption> <TR>");
            //am getting my grid's column headers
            DataTable dt = dbFunctions.getTable(Data);
            int columnscount = dt.Columns.Count;
            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                Response.Write("<Td>");
                //Get column headers and make it as bold in excel columns
                Response.Write("<B>");
                Response.Write(dt.Columns[j].ColumnName.ToString());
                Response.Write("</B>");
                Response.Write("</Td>");
            }
            Response.Write("</TR>");
            for (int j = 0; j < dt.Rows.Count; j++)
            {//write in new row
                Response.Write("<TR style='font-size:11.0pt; font-family:Times New Roman; background:white;'>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write("<Td>");
                    Response.Write(dt.Rows[j][i].ToString());
                    Response.Write("</Td>");
                }
                Response.Write("</TR>");
            }
            Response.Write("</Table>");
            Response.Write("</font>");
            Response.Flush();
            Response.End();
            return View("Index");
        }
    }
}
