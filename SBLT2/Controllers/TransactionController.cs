using SriPravanTech;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using SBLT2.PayslipCrystal;
using System.Globalization;
using System.Drawing;
using QRCoder;
using GenCode128;

namespace SBLT2.Controllers
{
    public class TransactionController : Controller
    {
        //
        // GET: /Transaction/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Label_Print()
        {
            return View();
        }

        public ActionResult MVRLA()
        {
            return View();
        }

        public ActionResult APR_Outward()
        {
            return View();
        }

        public ActionResult CHE_Outward()
        {
            return View();
        }

        public ActionResult APR_Inward()
        {
            return View();
        }

        public ActionResult Rework_Rejection()
        {
            return View();
        }

        public string pr_fetch_Rework_Rejection_Types()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_Rework_Rejection_Types");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_Active_Employees()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_Active_Employees");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_Rework_Rejection_Reasons(string Type_ID = null, string ID = null)
        {
            string selectedType = !string.IsNullOrEmpty(Type_ID) ? Type_ID : ID;
            if (string.IsNullOrEmpty(selectedType))
            {
                return "[]";
            }
            DataTable dt = dbFunctions.getTable("pr_fetch_Rework_Rejection_Reasons '" + selectedType.Replace("'", "''") + "'");
            if (dt.Rows.Count == 0)
            {
                return "[]";
            }
            string s = dbFunctions.GetJSONString(dt);
            return s;
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

        public string pr_insert_Formation_Rework_Rejection(
            string FRR_Type_ID,
            string FRR_Type_Name,
            string FRR_Reason,
            string FRR_Employee_ID,
            string FRR_Employee_Name,
            string FRR_Created_By,
            string FRR_QRCode = "",
            string FRR_BatteryType = "",
            string FRR_CKTNo = ""
        )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_insert_Formation_Rework_Rejection";
                com.Parameters.Add("@FRR_Formation_ID", SqlDbType.Int).Value = 68;
                com.Parameters.Add("@FRR_Type_ID", SqlDbType.Int).Value = Convert.ToInt32(FRR_Type_ID);
                com.Parameters.Add("@FRR_Type_Name", SqlDbType.VarChar, 50).Value = FRR_Type_Name ?? "";
                com.Parameters.Add("@FRR_Reason", SqlDbType.VarChar, 500).Value = FRR_Reason ?? "";
                com.Parameters.Add("@FRR_Employee_ID", SqlDbType.Int).Value = Convert.ToInt32(FRR_Employee_ID);
                com.Parameters.Add("@FRR_Employee_Name", SqlDbType.VarChar, 100).Value = FRR_Employee_Name ?? "";
                com.Parameters.Add("@FRR_Created_By", SqlDbType.VarChar, 50).Value = FRR_Created_By ?? "System";
                com.Parameters.Add("@FRR_QRCode", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_QRCode) ? (object)DBNull.Value : FRR_QRCode;
                com.Parameters.Add("@FRR_BatteryType", SqlDbType.VarChar, 100).Value = string.IsNullOrEmpty(FRR_BatteryType) ? (object)DBNull.Value : FRR_BatteryType;
                com.Parameters.Add("@FRR_CKTNo", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(FRR_CKTNo) ? (object)DBNull.Value : FRR_CKTNo;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_fetch_Formation_Rework_Rejection_List()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_Formation_Rework_Rejection_List");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_MaterialReceipt_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_MaterialReceipt_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchCustomerDetails(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchCustomerDetails '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchMaterialDescription(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchMaterialDescription '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchProductName()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchProductName");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_BankNoCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_BankNoCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_BatteryTypeCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_BatteryTypeCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_ReadingOperatorCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_ReadingOperatorCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_OperatorCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_OperatorCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_LineCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_LineCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_ShiftEngineerCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_ShiftEngineerCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_InspectorCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_InspectorCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_SupervisorCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_SupervisorCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchQRCode()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchQRCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_MVRLA()
        {
            // Pr_Fetch_MVRLA only returns today's rows; the MVRLA grid shows every stored record, newest first
            DataTable dt = dbFunctions.getTable("select * from MVRLA where mv_Status='A' order by mv_ID desc");

            // GetJSONString does a string Replace per column per row (~9s for 15k rows), so serialize directly.
            // Same shape and string values as GetJSONString: {"record":[{"col":"value",...}]}
            var rows = new List<Dictionary<string, string>>(dt.Rows.Count);
            foreach (DataRow row in dt.Rows)
            {
                var item = new Dictionary<string, string>(dt.Columns.Count);
                foreach (DataColumn col in dt.Columns)
                {
                    item[col.ColumnName] = row[col].ToString();
                }
                rows.Add(item);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { record = rows });
        }

        public string pr_FetchProductDetails(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchProductDetails'" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_TubCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_TubCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_CKTCombos()
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_CKTCombos");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_fetch_CHEInwardDetails(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_fetch_CHEInwardDetails'" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchSource()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchSource");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Send_SMS_Web(string PhoneNo, string msg)
        {
            string dt = dbFunctions.sendsms(PhoneNo, msg);
            //string s = dbFunctions.GetJSONString(dt);
            //return dt;
            if(dt == "true")
            {
                DataTable dt1 = dbFunctions.getTable("declare @ID varchar(50) set @ID=(select top 1 [fi_vRollNo] from [Fund_Issue] where [fi_cStatus]='A' order by  [fi_dCreatedDate] desc) update [Fund_Issue] set SMS_Status='Send' where [fi_cStatus]='A' and [fi_vRollNo]=@ID");
            }
            //else
            //{
            //    return "false";
            //}
            return dt;
        }

        public string Pr_Send_SMS_Web_Col(string PhoneNo, string msg)
        {
            string dt = dbFunctions.sendsms(PhoneNo, msg);
            //string s = dbFunctions.GetJSONString(dt);
            //return dt;
            if (dt == "true")
            {
                DataTable dt1 = dbFunctions.getTable("declare @ID varchar(50) set @ID=(select top 1 [fc_vRollNo] from [dbo].[Fund_Collection] where [fc_cStatus]='A' order by  [fc_dCreatedDt] desc) update [Fund_Collection] set SMS_Status='Send' where [fc_cStatus]='A' and [fc_vRollNo]=@ID");
            }
            //else
            //{
            //    return "false";
            //}
            return dt;
        }

        public string Pr_Fetch_Expense_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Expense_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Fund_Collection_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Fund_Collection_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Expense_Details_ByID(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Expense_Details_ByID '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchCustomerRolls(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchCustomerRolls '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchRollNo()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchRollNo");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchSupplierName(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchSupplierName'" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_LabelPrint_Available(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_LabelPrint_Available'" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchMaterialCode()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchMaterialCode");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchCustomerDropDown_Web()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchCustomerDropDown_Web");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Inventory()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Inventory");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_CHNInward()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_CHNInward");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_CHNOutward()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_CHNOutward");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_APRInward()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_APRInward");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_APROutward()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_APROutward");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Delete_Material_Receipt(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Material_Receipt '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_MVRLA(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_MVRLA '" + ID + "'");
            return "True";
        }

        public string Pr_Delete_Expense_Details(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Expense_Details '" + ID + "'");
            return "True";
        }

        public string pr_Fetch_Product()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Product");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchQualityInspection()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchQualityInspection");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchLocationUpdate(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchLocationUpdate '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_purchase_Order()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_purchase_Order");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_Payment_Details()
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_Payment_Details");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public void pr_FetchPDF_NewXx_Web(string FileName, string Heading, string tp_QuoteNo)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".pdf");
            //Response.ContentType = "application/pdf";
            //Response.Charset = "utf-8";
            ////Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //Response.Write("<font style='font-size:10.0pt; font-family:Verdana;'>");
            //Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            //Response.Write("<Table border='1' bgColor='#ffffff' " +
            //"borderColor='#000000' cellSpacing='0' cellPadding='0' " +
            //"style='font-size:12.0pt; font-family:Times New Roman; background:white;'>  <caption><font  style='font-size:14.0pt;' >" + Heading + "</font></caption> <TR>");
            DataTable dtData = new DataTable();
            DataTable ReportData = new DataTable();
            ReportDocument oRpt = new ReportDocument();                                 
            {
                ReportData = dbFunctions.getTable("pr_FetchPDF_NewXx_Web '" + tp_QuoteNo + "'");
            }
            if (dbFunctions.Payslip == "1")
            {
                oRpt = new PayslipALL();
            }
            else
            {
                oRpt = new PayslipALL();
            }
            try
            {
                //Response.Output.Write(oRpt.ToString());
                //Response.Flush();
                //Response.End();
                //return View("Index");
                //string folderName = @"E:\";
                //string pathString = System.IO.Path.Combine(folderName, "Payslip");
                //if (!System.IO.Directory.Exists(pathString))
                //{
                //    System.IO.Directory.CreateDirectory(pathString);
                //}
                //else
                //{
                ExportOptions exportOpts = new ExportOptions();
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                oRpt.Database.Tables[0].SetDataSource(ReportData);
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.ContentType = "Application/pdf";
                System.Web.HttpContext.Current.Response.AddHeader("Content_DisPosition", "inline;filename=Report.pdf");
                System.Web.HttpContext.Current.Response.BinaryWrite(ExportReportToStream(oRpt, ExportFormatType.PortableDocFormat));
                oRpt.Close();
                oRpt.Dispose();
                GC.Collect();
                System.Web.HttpContext.Current.Response.End();
                //ExportOptions CrExportOptions = default(ExportOptions);
                //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                //String path_name = "~/PDF/";
                //var pdfPath = System.IO.Path.Combine(Server.MapPath(path_name));
                //var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);
                //var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);
                //System.IO.File.WriteAllBytes(System.IO.Path.Combine(pdfPath), pdfContents);
                //WebRequest request = WebRequest.Create(Server.MapPath("~/PDF/" + pdfContents));
                //request.Method = WebRequestMethods.Ftp.UploadFile;
                //CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("~/PDF/") + from + ".pdf";
                ////CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine("C:/Users/user/Downloads", " - " + from + ".pdf");  //~/PDF/
                ////CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine("~/PDF/");
                //CrExportOptions = oRpt.ExportOptions;
                //var _with1 = CrExportOptions;
                //_with1.ExportDestinationType = ExportDestinationType.DiskFile;
                //_with1.ExportFormatType = ExportFormatType.PortableDocFormat;
                //_with1.DestinationOptions = CrDiskFileDestinationOptions;
                //_with1.FormatOptions = CrFormatTypeOptions;
                //oRpt.Export();
                //DialogResult result = MessageBox.Show("Payslip Created and Saved, If You Want Print Now Press YES", "Jamaai", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //if (result == DialogResult.Yes)
                //{
                // System.Diagnostics.Process.Start(CrDiskFileDestinationOptions.DiskFileName);
                //    string path = CrDiskFileDestinationOptions.DiskFileName;
                //    WebClient client = new WebClient();
                //    Byte[] buffer = client.DownloadData(path);
                //    if (buffer != null)
                //    {
                //        Response.ContentType = "application/pdf";
                //        Response.AddHeader("content-length", buffer.Length.ToString());
                //        Response.BinaryWrite(buffer);
                //    }
                //}
                //lblWait.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // Cursor.Current = Cursors.Default;
        }

        public static byte[] ExportReportToStream(ReportDocument crReportDoc, ExportFormatType expType)
        {
            Stream st;
            st = crReportDoc.ExportToStream(expType);
            byte[] arr = new byte[st.Length];
            st.Read(arr, 0, (int)st.Length);
            return arr;
        }

        [HttpPost]
        public ActionResult PreviewUpload()
        {
            if (Request.Files.Count > 0)
            {
                foreach (string files in Request.Files)
                {
                    var _file = Request.Files[files];
                    FileInfo Fi = new FileInfo(Path.GetFileName(_file.FileName));
                    string fileExtention = Fi.Extension;
                    if (_file != null)
                    {
                        if (fileExtention == ".pdf")
                        {
                            string fileName = Path.GetFileName(_file.FileName);
                            if (_file.ContentLength <= 120000000)
                            {
                                _file.SaveAs(Server.MapPath("~/PreviewPDF/" + fileName));
                            }
                            string path = "/PreviewPDF/" + Path.GetFileName(_file.FileName);
                            ViewData["error"] = path;
                            return Json(new
                            {
                                Success = path
                            });
                        }
                        else
                        {
                            return Json(new
                            {
                                fileError = "Only Support PDF"
                            });
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            error = "Please Select the file"
                        });
                    }
                }
            }
            return Json(new
            {
                error = "Please Select the file"
            });
        }

        public void Pr_Fetch_MVRLA_PDF(string FileName, string Heading, string mv_QRCode, string from, string to, string tub)
        {           
            DataTable dtData = new DataTable();
            DataTable ReportData = new DataTable();
            ReportDocument oRpt = new ReportDocument();
            {
                ReportData = dbFunctions.getTable("Pr_Fetch_MVRLA_PDF '" + mv_QRCode + "','" + from + "','" + to + "','" + tub + "'");
            }
            //if (dbFunctions.VisitorPass == "1")
            //{
                    oRpt = new MVRLAGroupingNew();
            //}
            try
            {
                ExportOptions exportOpts = new ExportOptions();
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                oRpt.Database.Tables[0].SetDataSource(ReportData);
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.ContentType = "Application/pdf";
                System.Web.HttpContext.Current.Response.AddHeader("Content_DisPosition", "inline;filename=Report.pdf");
                System.Web.HttpContext.Current.Response.BinaryWrite(ExportReportToStream(oRpt, ExportFormatType.PortableDocFormat));
                oRpt.Close();
                oRpt.Dispose();
                GC.Collect();
                System.Web.HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string pr_checkQRCode(string ID)
        {
            string s = "false";
            DataTable dt = dbFunctions.getTable("pr_checkQRCode '" + ID + "'");
            if (dt.Rows.Count > 0)
            {
                s = dt.Rows[0]["mv_QRCode"].ToString();
            }
            else
            {
                s = "false";
            }
            return s;
        }

        public JsonResult pr_fetch_QRCode_Details(string ID)
        {
            ID = (ID ?? "").Trim();
            if (string.IsNullOrEmpty(ID))
            {
                return Json(new { success = false, message = "Please provide a serial number." }, JsonRequestBehavior.AllowGet);
            }
            DataTable dt = dbFunctions.getTable("pr_checkQRCode '" + ID.Replace("'", "''") + "'");
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[dt.Rows.Count - 1];
                string bType = dt.Columns.Contains("mv_BatteryType") ? row["mv_BatteryType"].ToString() : "";
                string ckt = dt.Columns.Contains("mv_CKTNo") ? row["mv_CKTNo"].ToString() : "";
                string qr = dt.Columns.Contains("mv_QRCode") ? row["mv_QRCode"].ToString() : ID;
                return Json(new
                {
                    success = true,
                    qrCode = qr,
                    batteryType = bType,
                    cktNo = ckt
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Serial / QR Code '" + ID + "' not found in formation records." }, JsonRequestBehavior.AllowGet);
            }
        }

        public string pr_Fetch_QRCode_Available(string ID)
        {
            if (ID.Length != 10)
            {

            }
            string s = "false";
            DataTable dt = dbFunctions.getTable("pr_Fetch_QRCode_Available '" + ID + "'");
            if (dt.Rows.Count > 0)
            {
                s = dt.Rows[0]["mv_QRCode"].ToString();
            }
            else
            {
                s = "false";
            }
            return s;
        }

        public string pr_Fetch_CHNOutward_Available(string ID)
        {
            string s = "false";
            DataTable dt = dbFunctions.getTable("pr_Fetch_CHNOutward_Available '" + ID + "'");
            if (dt.Rows.Count > 0)
            {
                s = dt.Rows[0]["inv_QRCode"].ToString();
            }
            else
            {
                s = "false";
            }
            return s;
        }

        public string pr_Fetch_APRInward_Available(string ID)
        {
            string s = "false";
            DataTable dt = dbFunctions.getTable("pr_Fetch_APRInward_Available '" + ID + "'");
            if (dt.Rows.Count > 0)
            {
                s = dt.Rows[0]["inv_QRCode"].ToString();
            }
            else
            {
                s = "false";
            }
            return s;
        }

        public string Pr_Update_LocationUpdate(string ID, string Bin)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_LocationUpdate";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@Bin", SqlDbType.VarChar).Value = Bin;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Pr_Update_QualityInspection(string mr_ID, string mr_DOE)
        {
            var date = mr_DOE.Substring(0, 2);
            var month = mr_DOE.Substring(3, 2);
            var year = mr_DOE.Substring(6, 4);
            var DOE = year + '-' + month + '-' + date;

            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_QualityInspection";
                com.Parameters.Add("@mr_ID", SqlDbType.VarChar).Value = mr_ID;
                com.Parameters.Add("@mr_DOE", SqlDbType.VarChar).Value = DOE;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Update_MaterialIssueQty(string mr_ID, string mr_IssuedQty)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_MaterialIssueQty";
                com.Parameters.Add("@mr_ID", SqlDbType.VarChar).Value = mr_ID;
                com.Parameters.Add("@mr_IssuedQty", SqlDbType.VarChar).Value = mr_IssuedQty;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_update_CHEInward(string inv_QRCode, string inv_User)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_update_CHEInward";
                com.Parameters.Add("@inv_QRCode", SqlDbType.VarChar).Value = inv_QRCode;
                com.Parameters.Add("@inv_User", SqlDbType.VarChar).Value = inv_User;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_update_CHEOutward(string inv_QRCode, string inv_User, string inv_CHEOutwardInvoiceNo, string inv_CHEOutwardVehicleNo)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_update_CHEOutward";
                com.Parameters.Add("@inv_QRCode", SqlDbType.VarChar).Value = inv_QRCode;
                com.Parameters.Add("@inv_User", SqlDbType.VarChar).Value = inv_User;
                com.Parameters.Add("@inv_CHEOutwardInvoiceNo", SqlDbType.VarChar).Value = inv_CHEOutwardInvoiceNo;
                com.Parameters.Add("@inv_CHEOutwardVehicleNo", SqlDbType.VarChar).Value = inv_CHEOutwardVehicleNo;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_update_APRInward(string inv_QRCode, string inv_User, string inv_Location)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_update_APRInward";
                com.Parameters.Add("@inv_QRCode", SqlDbType.VarChar).Value = inv_QRCode;
                com.Parameters.Add("@inv_User", SqlDbType.VarChar).Value = inv_User;
                com.Parameters.Add("@inv_Location", SqlDbType.VarChar).Value = inv_Location;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_update_APROutward(string inv_QRCode, string inv_User)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_update_APROutward";
                com.Parameters.Add("@inv_QRCode", SqlDbType.VarChar).Value = inv_QRCode;
                com.Parameters.Add("@inv_User", SqlDbType.VarChar).Value = inv_User;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_insert_LabelPrint(int inv_ProdName, string inv_ProdCode, string inv_Model, string inv_Customer, string inv_Location,
            string inv_Qty, string inv_LabelCount, string inv_QRCode, string inv_User, string inv_OracleStage, string inv_RouteCard)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_insert_LabelPrint";
                com.Parameters.Add("@inv_ProdName", SqlDbType.VarChar).Value = inv_ProdName;
                com.Parameters.Add("@inv_ProdCode", SqlDbType.VarChar).Value = inv_ProdCode;
                com.Parameters.Add("@inv_Model", SqlDbType.VarChar).Value = inv_Model;
                com.Parameters.Add("@inv_Customer", SqlDbType.VarChar).Value = inv_Customer;
                com.Parameters.Add("@inv_Location", SqlDbType.VarChar).Value = inv_Location;
                com.Parameters.Add("@inv_Qty", SqlDbType.VarChar).Value = inv_Qty;
                com.Parameters.Add("@inv_LabelCount", SqlDbType.VarChar).Value = inv_LabelCount;
                com.Parameters.Add("@inv_QRCode", SqlDbType.VarChar).Value = inv_QRCode;
                com.Parameters.Add("@inv_User", SqlDbType.VarChar).Value = inv_User;
                com.Parameters.Add("@inv_OracleStage", SqlDbType.VarChar).Value = inv_OracleStage;
                com.Parameters.Add("@inv_RouteCard", SqlDbType.VarChar).Value = inv_RouteCard;
                com.ExecuteNonQuery();
                con.Close();

                for (int i=0;i< inv_LabelCount.Length;i++) {
                    string text = System.IO.File.ReadAllText(@"" + dbFunctions.Label);
                    string BARCODE = text.Replace("@BARCODE@", inv_ProdCode);
                    string ID = BARCODE.Replace("@RackNo@", inv_Model);
                    string Name = ID.Replace("@GRNDate@", inv_Customer);
                    string CName = Name.Replace("@BoxId@", inv_Location);
                    string Purpose = CName.Replace("@BoxNo@", inv_QRCode);
                    string Meet = Purpose.Replace("@InvoiceQty@", inv_OracleStage);
                    string COUNT = Meet.Replace("@InvoiceNo@", inv_RouteCard);
                    RawPrinterHelper.SendStringToPrinter(dbFunctions.PrinterName, COUNT);
                }
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string pr_insert_MVRLA(string mv_BatteryType, string mv_FillingLine, string mv_BankNo, string mv_Date, string mv_NoOfCells, 
            string mv_FillingStartTime, string mv_FillingEndTime, string mv_CDPassedQty, string mv_CDFailQty, string mv_AcidSPG,
            string mv_AcidVolume, string mv_TubNo, string mv_CKTNo, string mv_CellShortQty, string mv_Temperature, string mv_PRDSupervisior,
            string mv_FillingOperator, string mv_QAInspector, string mv_ShiftEngineer, string mv_QRCode, string mv_User, string mv_Shift,
            string mv_ReadingOperator)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_insert_MVRLA";
                com.Parameters.Add("@mv_BatteryType", SqlDbType.VarChar).Value = mv_BatteryType;
                com.Parameters.Add("@mv_FillingLine", SqlDbType.VarChar).Value = mv_FillingLine;
                com.Parameters.Add("@mv_BankNo", SqlDbType.VarChar).Value = mv_BankNo;
                com.Parameters.Add("@mv_Date", SqlDbType.VarChar).Value = mv_Date;
                com.Parameters.Add("@mv_NoOfCells", SqlDbType.VarChar).Value = mv_NoOfCells;
                com.Parameters.Add("@mv_FillingStartTime", SqlDbType.VarChar).Value = mv_FillingStartTime;
                com.Parameters.Add("@mv_FillingEndTime", SqlDbType.VarChar).Value = mv_FillingEndTime;
                com.Parameters.Add("@mv_CDPassedQty", SqlDbType.VarChar).Value = mv_CDPassedQty;
                com.Parameters.Add("@mv_CDFailQty", SqlDbType.VarChar).Value = mv_CDFailQty;
                com.Parameters.Add("@mv_AcidSPG", SqlDbType.VarChar).Value = mv_AcidSPG;
                com.Parameters.Add("@mv_AcidVolume", SqlDbType.VarChar).Value = mv_AcidVolume;
                com.Parameters.Add("@mv_TubNo", SqlDbType.VarChar).Value = mv_TubNo;
                com.Parameters.Add("@mv_CKTNo", SqlDbType.VarChar).Value = mv_CKTNo;
                com.Parameters.Add("@mv_CellShortQty", SqlDbType.VarChar).Value = mv_CellShortQty;
                com.Parameters.Add("@mv_Temperature", SqlDbType.VarChar).Value = mv_Temperature;
                com.Parameters.Add("@mv_PRDSupervisior", SqlDbType.VarChar).Value = mv_PRDSupervisior;
                com.Parameters.Add("@mv_FillingOperator", SqlDbType.VarChar).Value = mv_FillingOperator;
                com.Parameters.Add("@mv_QAInspector", SqlDbType.VarChar).Value = mv_QAInspector;
                com.Parameters.Add("@mv_ShiftEngineer", SqlDbType.VarChar).Value = mv_ShiftEngineer;
                com.Parameters.Add("@mv_QRCode", SqlDbType.VarChar).Value = mv_QRCode;
                com.Parameters.Add("@mv_User", SqlDbType.VarChar).Value = mv_User;
                com.Parameters.Add("@mv_Shift", SqlDbType.VarChar).Value = mv_Shift;
                com.Parameters.Add("@mv_ReadingOperator", SqlDbType.VarChar).Value = mv_ReadingOperator;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Pr_Insert_Expense_Details(string em_Expense_Date, string em_Expense_Type, string em_Expense_Name, string em_Amount,
            string em_PayMode,string em_User //string tp_ApproveUser
            )
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Expense_Details";
                com.Parameters.Add("@em_Expense_Date", SqlDbType.VarChar).Value = em_Expense_Date;
                com.Parameters.Add("@em_Expense_Type", SqlDbType.VarChar).Value = em_Expense_Type;
                com.Parameters.Add("@em_Expense_Name", SqlDbType.VarChar).Value = em_Expense_Name;
                com.Parameters.Add("@em_Amount", SqlDbType.VarChar).Value = em_Amount;
                com.Parameters.Add("@em_PayMode", SqlDbType.VarChar).Value = em_PayMode;
                com.Parameters.Add("@em_User", SqlDbType.VarChar).Value = em_User;
                //com.Parameters.Add("@tp_ApproveUser", SqlDbType.VarChar).Value = tp_ApproveUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
     
        public string Pr_Update_Expense_Details(string em_Id, string em_Expense_Date, string em_Expense_Type, string em_Expense_Name,
 string em_Amount, string em_PayMode, string em_User)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Expense_Details";
                com.Parameters.Add("@em_Id", SqlDbType.VarChar).Value = em_Id;
                com.Parameters.Add("@em_Expense_Date", SqlDbType.VarChar).Value = em_Expense_Date;
                com.Parameters.Add("@em_Expense_Type", SqlDbType.VarChar).Value = em_Expense_Type;
                com.Parameters.Add("@em_Expense_Name", SqlDbType.VarChar).Value = em_Expense_Name;
                com.Parameters.Add("@em_Amount", SqlDbType.VarChar).Value = em_Amount;
                com.Parameters.Add("@em_PayMode", SqlDbType.VarChar).Value = em_PayMode;
                com.Parameters.Add("@em_User", SqlDbType.VarChar).Value = em_User;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
      

        public string Pr_Insert_Payment_Details(string tp_PONo, string tp_Accounts, string tp_PaymentValue, string tp_PaymentMode,
   string tp_PaymentDate, string tp_ReferenceNo, string tp_PaymentAttachment, string tp_PaymentUser)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Insert_Payment_Details";
                com.Parameters.Add("@tp_PONo", SqlDbType.VarChar).Value = tp_PONo;
                com.Parameters.Add("@tp_Accounts", SqlDbType.VarChar).Value = tp_Accounts;
                com.Parameters.Add("@tp_PaymentValue", SqlDbType.VarChar).Value = tp_PaymentValue;
                com.Parameters.Add("@tp_PaymentMode", SqlDbType.VarChar).Value = tp_PaymentMode;
                com.Parameters.Add("@tp_PaymentDate", SqlDbType.VarChar).Value = tp_PaymentDate;
                com.Parameters.Add("@tp_ReferenceNo", SqlDbType.VarChar).Value = tp_ReferenceNo;
                com.Parameters.Add("@tp_PaymentAttachment", SqlDbType.VarChar).Value = tp_PaymentAttachment;
                com.Parameters.Add("@tp_PaymentUser", SqlDbType.VarChar).Value = tp_PaymentUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_Insert_Delivery(string tp_PONo, string tp_InvoiceNO, string tp_InvoiceDate, string tp_InvoiceNumber,
  string tp_InvoiceAttachment, string tp_InvoiceUser)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_Insert_Delivery";
                com.Parameters.Add("@tp_PONo", SqlDbType.VarChar).Value = tp_PONo;
                com.Parameters.Add("@tp_InvoiceNO", SqlDbType.VarChar).Value = tp_InvoiceNO;
                com.Parameters.Add("@tp_InvoiceDate", SqlDbType.VarChar).Value = tp_InvoiceDate;
                com.Parameters.Add("@tp_InvoiceNumber", SqlDbType.VarChar).Value = tp_InvoiceNumber;
                com.Parameters.Add("@tp_InvoiceAttachment", SqlDbType.VarChar).Value = tp_InvoiceAttachment;
                com.Parameters.Add("@tp_Inv oiceUser", SqlDbType.VarChar).Value = tp_InvoiceUser;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

      
        public string Pr_Update_Game_Master(string MG_iID, string MG_iGame_ID, string MG_iNo_of_Player, string MG_iGame_Type,
      string MG_iPoints, string MG_Created_by)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Pr_Update_Game_Master";
                com.Parameters.Add("@MG_iID", SqlDbType.VarChar).Value = MG_iID;
                com.Parameters.Add("@MG_iGame_ID", SqlDbType.VarChar).Value = MG_iGame_ID;
                com.Parameters.Add("@MG_iNo_of_Player", SqlDbType.VarChar).Value = MG_iNo_of_Player;
                com.Parameters.Add("@MG_iGame_Type", SqlDbType.VarChar).Value = MG_iGame_Type;
                com.Parameters.Add("@MG_iPoints", SqlDbType.VarChar).Value = MG_iPoints;
                com.Parameters.Add("@MG_Created_by", SqlDbType.VarChar).Value = MG_Created_by;
                com.ExecuteNonQuery();
                con.Close();
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string pr_insert_MaterialReceipt(string mr_MRC_No, string mr_InvoiceNo, string mr_DOR, string mr_MaterialCode,
        string mr_MaterialDescription, string mr_Weight, string mr_NoOfBages, string mr_BondNo, string mr_BE_No, string mr_Place,
        string mr_AvgWeight, string mr_Lot_No, string mr_Source, string mr_SupplierName, string mr_DOM, string mr_ApprovedSource,
        string mr_CreadtedBy, string mr_Remarks, string mr_User)
        {
            try
            {
                Image BarCodeImage = default(Image);
                BarCodeImage = Code128Rendering.MakeBarcodeImage(mr_MRC_No, Convert.ToInt16(2), true);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(mr_MRC_No, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                imgBarCode.Height = 50;
                imgBarCode.Width = 50;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        var image = Convert.ToBase64String(byteImage);

                        SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "pr_insert_MaterialReceipt";
                        com.Parameters.Add("@mr_MRC_No", SqlDbType.VarChar).Value = mr_MRC_No;
                        com.Parameters.Add("@mr_InvoiceNo", SqlDbType.VarChar).Value = mr_InvoiceNo;
                        com.Parameters.Add("@mr_DOR", SqlDbType.VarChar).Value = mr_DOR;
                        com.Parameters.Add("@mr_MaterialCode", SqlDbType.VarChar).Value = mr_MaterialCode;
                        com.Parameters.Add("@mr_MaterialDescription", SqlDbType.VarChar).Value = mr_MaterialDescription;
                        com.Parameters.Add("@mr_Weight", SqlDbType.VarChar).Value = mr_Weight;
                        com.Parameters.Add("@mr_NoOfBages", SqlDbType.VarChar).Value = mr_NoOfBages;
                        com.Parameters.Add("@mr_BondNo", SqlDbType.VarChar).Value = mr_BondNo;
                        com.Parameters.Add("@mr_BE_No", SqlDbType.VarChar).Value = mr_BE_No;
                        com.Parameters.Add("@mr_Place", SqlDbType.VarChar).Value = mr_Place;
                        com.Parameters.Add("@mr_AvgWeight", SqlDbType.VarChar).Value = mr_AvgWeight;
                        com.Parameters.Add("@mr_Lot_No", SqlDbType.VarChar).Value = mr_Lot_No;
                        com.Parameters.Add("@mr_Source", SqlDbType.VarChar).Value = mr_Source;
                        com.Parameters.Add("@mr_SupplierName", SqlDbType.VarChar).Value = mr_SupplierName;
                        com.Parameters.Add("@mr_DOM", SqlDbType.VarChar).Value = mr_DOM;
                        com.Parameters.Add("@mr_ApprovedSource", SqlDbType.VarChar).Value = mr_ApprovedSource;
                        com.Parameters.Add("@mr_CreadtedBy", SqlDbType.VarChar).Value = mr_CreadtedBy;
                        com.Parameters.Add("@mr_Remarks", SqlDbType.VarChar).Value = mr_Remarks;
                        com.Parameters.Add("@mr_User", SqlDbType.VarChar).Value = mr_User;

                        byte[] imageBytes1 = null;
                        byte[] imagedataBarcode = null;
                        try
                        {
                            MemoryStream msb = new MemoryStream();
                            BarCodeImage.Save(msb, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imagedataBarcode = msb.GetBuffer();

                            string fileName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
                            imageBytes1 = Convert.FromBase64String(image.Split(',')[0]);
                            string filePath = System.IO.Path.Combine(string.Format("E:/Rajesh/Captures/{0}.jpg", fileName));
                            System.IO.File.WriteAllBytes(filePath, imageBytes1);
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                            //imagedata = imageData_Enter;
                        }
                        com.Parameters.Add("@mr_Barcode", SqlDbType.Image).Value = (object)imageBytes1;
                        com.Parameters.Add("@mr_1DBarcode", SqlDbType.Image).Value = (object)imagedataBarcode;
                        com.ExecuteNonQuery();
                        con.Close();
                        return "True";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string pr_update_MaterialReceipt(string mr_ID, string mr_MRC_No, string mr_InvoiceNo, string mr_DOR, string mr_MaterialCode,
     string mr_MaterialDescription, string mr_Weight, string mr_NoOfBages, string mr_BondNo, string mr_BE_No, string mr_Place,
     string mr_AvgWeight, string mr_Lot_No, string mr_Source, string mr_SupplierName, string mr_DOM, string mr_ApprovedSource,
     string mr_CreadtedBy, string mr_Remarks)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "pr_update_MaterialReceipt";
                com.Parameters.Add("@mr_ID", SqlDbType.VarChar).Value = mr_ID;
                com.Parameters.Add("@mr_MRC_No", SqlDbType.VarChar).Value = mr_MRC_No;
                com.Parameters.Add("@mr_InvoiceNo", SqlDbType.VarChar).Value = mr_InvoiceNo;
                com.Parameters.Add("@mr_DOR", SqlDbType.VarChar).Value = mr_DOR;
                com.Parameters.Add("@mr_MaterialCode", SqlDbType.VarChar).Value = mr_MaterialCode;
                com.Parameters.Add("@mr_MaterialDescription", SqlDbType.VarChar).Value = mr_MaterialDescription;
                com.Parameters.Add("@mr_Weight", SqlDbType.VarChar).Value = mr_Weight;
                com.Parameters.Add("@mr_NoOfBages", SqlDbType.VarChar).Value = mr_NoOfBages;
                com.Parameters.Add("@mr_BondNo", SqlDbType.VarChar).Value = mr_BondNo;
                com.Parameters.Add("@mr_BE_No", SqlDbType.VarChar).Value = mr_BE_No;
                com.Parameters.Add("@mr_Place", SqlDbType.VarChar).Value = mr_Place;
                com.Parameters.Add("@mr_AvgWeight", SqlDbType.VarChar).Value = mr_AvgWeight;
                com.Parameters.Add("@mr_Lot_No", SqlDbType.VarChar).Value = mr_Lot_No;
                com.Parameters.Add("@mr_Source", SqlDbType.VarChar).Value = mr_Source;
                com.Parameters.Add("@mr_SupplierName", SqlDbType.VarChar).Value = mr_SupplierName;
                com.Parameters.Add("@mr_DOM", SqlDbType.VarChar).Value = mr_DOM;
                com.Parameters.Add("@mr_ApprovedSource", SqlDbType.VarChar).Value = mr_ApprovedSource;
                com.Parameters.Add("@mr_CreadtedBy", SqlDbType.VarChar).Value = mr_CreadtedBy;
                com.Parameters.Add("@mr_Remarks", SqlDbType.VarChar).Value = mr_Remarks;
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
