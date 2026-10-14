using SriPravanTech;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBLT2.Controllers
{
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Summary_Report()
        {
            return View();
        }
        public ActionResult Filling_Report()
        {
            return View();
        }

        public ActionResult Transit_report()
        {
            return View();
        }

        public ActionResult Inventory_Report()
        {
            return View();
        }

        public ActionResult Material_Issue_Report()
        {
            return View();
        }

        public ActionResult Non_Moving_Report()
        {
            return View();
        }

        public ActionResult FIFO_Inventory_Report()
        {
            return View();
        }

        public ActionResult FIFO_Display()
        {
            return View();
        }

        public ActionResult Rework_Rejection()
        {
            return View();
        }

        public string pr_fetch_Rework_Rejection_Report(string Type = "All", string from = null, string to = null)
        {
            string cleanType = (Type ?? "All").Replace("'", "''");
            string cleanFrom = (from ?? "").Replace("'", "''");
            string cleanTo = (to ?? "").Replace("'", "''");
            string query = string.Format("pr_fetch_Rework_Rejection_Report '{0}', '{1}', '{2}'", cleanType, cleanFrom, cleanTo);
            DataTable dt = dbFunctions.getTable(query);
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchFIFODisplay()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchFIFODisplay");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_SummaryReport(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_SummaryReport '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_FillingReport(string from, string to, string shift)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_FillingReport '" + from + "','" + to + "','" + shift + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_FillingReport2(string from, string to, string tub, string bank)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_FillingReport2 '" + from + "','" + to + "','" + tub + "','" + bank + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_SummaryCHN(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_SummaryCHN '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_SummaryCHNINV(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_SummaryCHNINV '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_SummaryAPR(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_SummaryAPR '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_SummaryAPRINV(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_SummaryAPRINV '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_TransitReport(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_TransitReport '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchQIPendingReport(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchQIPendingReport '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchInventoryReport(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchInventoryReport '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchMaterialIssueReport(string from, string to, string Tag)
        {

            DataTable dt = dbFunctions.getTable("pr_FetchMaterialIssueReport '" + from + "','" + to + "','"+Tag+"'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_RSR_Report(string from, string to)
        {

            DataTable dt = dbFunctions.getTable("pr_Fetch_RSR_Report '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string pr_Fetch_Time_Summary(string from, string to, string model, string type, string plant)
        {

            DataTable dt = dbFunctions.getTable("pr_Fetch_Time_Summary '" + from + "','" + to + "','" + model + "','" + type + "','" + plant + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string pr_Fetch_AL_Report(string from, string to)
        {

            DataTable dt = dbFunctions.getTable("pr_Fetch_AL_Report '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Pending()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Pending ");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchMatCodeFIFO()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchMatCodeFIFO");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchMatCodeFIFOReport()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchMatCodeFIFOReport");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchFIFOInventoryReport(string from, string matCode)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchFIFOInventoryReport '" + from + "','" + matCode + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchNonMovingReport(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchNonMovingReport '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchIssueSummary_Roll_Web_Sum(string from, string to)
        {
            DataTable dt = dbFunctions.getTable("pr_FetchIssueSummary_Roll_Web_Sum '" + from + "','" + to + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_FetchFIFOReport()
        {
            DataTable dt = dbFunctions.getTable("pr_FetchFIFOReport");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Bobbin_Loading_Type()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Bobbin_Loading_Type ");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Bobbin_Loading_Plant()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Bobbin_Loading_Plant ");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Time_Summary_Today(string model, string type, string plant)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Time_Summary_Today'" + model + "','" + type + "','" + plant + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Time_Summary_Month(string model, string type, string plant)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Time_Summary_Month'" + model + "','" + type + "','" + plant+ "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Time_Summary_Yesterday(string model, string type, string plant)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Time_Summary_Yesterday'" + model + "','" + type + "','" + plant + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_GameName_ByMatch(string MG_iGame_ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_GameName_ByMatch '" + MG_iGame_ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_MatchDetails_ById(string MM_iID)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_MatchDetails_ById '" + MM_iID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Bobbin_Status(string ID)
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Bobbin_Status '" + ID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_UserPoints_ByGame(string GameId)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_UserPoints_ByGame '" + GameId + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_UserPoints_ByGame_ByArea(string GameId, string Area)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_UserPoints_ByGame_ByArea '" + GameId + "','" + Area + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string Pr_Fetch_PlayersList(string MM_iID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Fetch_PlayersList '" + MM_iID + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Stock_Available()
        {
            DataTable dt = dbFunctions.getTable("pr_Stock_Available");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_Plant()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_Plant");// '" + User + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_RSR_Report_Today()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_RSR_Report_Today");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string pr_Fetch_RSR_Report_Yesterday()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_RSR_Report_Yesterday");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }

        public string pr_Fetch_RSR_Report_Month()
        {
            DataTable dt = dbFunctions.getTable("pr_Fetch_RSR_Report_Month");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }


        public string pr_Stock_Available_Report(string User)
        {
            DataTable dt = dbFunctions.getTable("pr_Stock_Available_Report '"+ User + "'");
            string s = dbFunctions.GetJSONString(dt);
            return s;
        }
        public string Pr_Delete_Bobbin_Summary_Report(string ID)
        {
            DataTable dt = dbFunctions.getTable("Pr_Delete_Bobbin_Summary_Report '" + ID + "'");
            return "True";
        }

    }
}
