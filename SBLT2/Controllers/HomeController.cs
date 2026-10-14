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
    public class HomeController : Controller
    {

        //GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return RedirectToAction("Dashboard", "Home");
        }


        public ActionResult Login()
        {
            return View();
        }




        public ActionResult Driver_Status()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public string pr_fetch_Today_Rework_Rejection_Counts(string Type = null)
        {
            string query = string.IsNullOrEmpty(Type)
                ? "pr_fetch_Today_Rework_Rejection_Counts"
                : "pr_fetch_Today_Rework_Rejection_Counts '" + Type.Replace("'", "''") + "'";
            DataTable dt = dbFunctions.getTable(query);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_Today_Rework_Count()
        {
            return pr_fetch_Today_Rework_Rejection_Counts("Rework");
        }

        public string pr_fetch_Today_Rejection_Count()
        {
            return pr_fetch_Today_Rework_Rejection_Counts("Rejection");
        }


        public ActionResult LogoPage()
        {
            return View();
        }


        public string pr_fetch_failedqty_chart()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_failedqty_chart");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_passedqty_chart()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_passedqty_chart");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_Today_Rework_Chart()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_Today_Rework_Chart");
            if (dt.Rows.Count == 0)
            {
                return "{\"record\":[]}";
            }
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_Today_Rejection_Chart()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_Today_Rejection_Chart");
            if (dt.Rows.Count == 0)
            {
                return "{\"record\":[]}";
            }
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string pr_getMenu_listNew1(string Role, string module)
        {
            DataTable dt = dbFunctions.getTable("pr_getMenu_listNew1 " + Role + "," + module);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_getMenu_listbyParentnew1(string Role, string module, string ParentID)
        {
            DataTable dt = dbFunctions.getTable("pr_getMenu_listbyParentnew1 " + Role + "," + module + "," + ParentID);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_getMenu_listbyParentnew(string Role, string module, string ParentID)
        {
            DataTable dt = dbFunctions.getTable("pr_getMenu_listbyParentnew " + Role + "," + module + "," + ParentID);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string pr_Points(string user)
        {
            DataTable dt = dbFunctions.getTable("pr_Points '" + user + "'");
            string s = dbFunctions.GetJSONString(dt);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            return "0";
        }

        public string pr_getlogin1(string user, string pwd)
        {
            string s = "false";
            //string query = "select UM_User_Name as UM_vUser_Name,'0' as UM_iUser_Type,'0' as UM_iArea, UM_ID as UM_vUser_ID ,UM_ID as UM_iId   *,dbo.fget_reference_value(UM_Rights),0 as Points  from User_Master where  UM_User_Name= '" + user + "' and UM_Password='" + pwd + "' and UM_Status='a'  COLLATE  SQL_Latin1_General_CP1_CS_AS  ";
            DataTable dt = dbFunctions.getTable("pr_getlogin1 '" + user + "','" + pwd + "'");
            if (dt.Rows.Count > 0)
            {
                s = dt.Rows[0]["UM_vUser_Name"].ToString() + "~" + dt.Rows[0]["UM_iUser_Type"].ToString() + "~" + dt.Rows[0]["UM_iArea"].ToString() + "~" + dt.Rows[0]["UM_vUser_ID"].ToString() + "~" + dt.Rows[0]["UM_iId"].ToString();
            }
            else
            {
                s = "false";
            }
            return s;
        }





        public string pr_get_Disticwise_Count(string Category)
        {
            DataTable dt = dbFunctions.getTable("pr_get_Disticwise_Count '" + Category + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string pr_get_Birthdat_List(string Category, string Date)
        {
            DataTable dt = dbFunctions.getTable("pr_get_Birthdat_List '" + Category + "','" + Date + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_LogoPage_ById(string Id)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_LogoPage_ById '" + Id + "' ");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_get_Gradewise_Driver(string Category)
        {
            DataTable dt = dbFunctions.getTable("pr_get_Gradewise_Driver '" + Category + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_delete_LogoPage(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_delete_LogoPage '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        [HttpPost]
        public ActionResult Pr_Insert_Logo()
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


                    string User = Request.QueryString["User"].ToString();

                    file_Path = User + ".png";

                    fname = Path.Combine(Server.MapPath("~/FileUpload/"), file_Path);
                    file.SaveAs(fname);




                    string LP_image = "FileUpload/" + file_Path;

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

            return Json("Success");
        }


        [HttpPost]
        public ActionResult Pr_Insert_LogoPage()
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

                    DataTable dt = dbFunctions.getTable("select isnull(max(LP_id),0)+1 from LogoPage");
                    string ID = dt.Rows[0][0].ToString();

                    file_Path = ID + ".jpg";

                    fname = Path.Combine(Server.MapPath("~/FileUpload_Logo/"), file_Path);
                    file.SaveAs(fname);


                    string LP_CreatedBy = Request.QueryString["LP_CreatedBy"].ToString();
                    string LP_Heading1 = Request.QueryString["LP_Heading1"].ToString();
                    string LP_Heading2 = Request.QueryString["LP_Heading2"].ToString();

                    string LP_image = "FileUpload_Logo/" + file_Path;

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Insert_LogoPage";
                        com.Parameters.Add("@LP_Heading1", SqlDbType.VarChar).Value = LP_Heading1;
                        com.Parameters.Add("@LP_Heading2", SqlDbType.VarChar).Value = LP_Heading2;
                        com.Parameters.Add("@LP_image", SqlDbType.VarChar).Value = LP_image;
                        com.Parameters.Add("@LP_CreatedBy", SqlDbType.VarChar).Value = LP_CreatedBy;

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
        public ActionResult Pr_Update_LogoPage()
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


                    string LP_CreatedBy = Request.QueryString["LP_CreatedBy"].ToString();
                    string LP_Heading1 = Request.QueryString["LP_Heading1"].ToString();
                    string LP_Heading2 = Request.QueryString["LP_Heading2"].ToString();
                    string LP_id = Request.QueryString["LP_id"].ToString();

                    file_Path = LP_id + ".jpg";

                    fname = Path.Combine(Server.MapPath("~/FileUpload_Logo/"), file_Path);
                    file.SaveAs(fname);





                    string LP_image = "FileUpload_Logo/" + file_Path;

                    SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                    try
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "Pr_Update_LogoPage";
                        com.Parameters.Add("@LP_Heading1", SqlDbType.VarChar).Value = LP_Heading1;
                        com.Parameters.Add("@LP_Heading2", SqlDbType.VarChar).Value = LP_Heading2;

                        com.Parameters.Add("@LP_id", SqlDbType.VarChar).Value = LP_id;
                        com.Parameters.Add("@LP_image", SqlDbType.VarChar).Value = LP_image;
                        com.Parameters.Add("@LP_CreatedBy", SqlDbType.VarChar).Value = LP_CreatedBy;

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

    }
}
