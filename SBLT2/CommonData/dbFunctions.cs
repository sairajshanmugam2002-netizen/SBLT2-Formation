using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;

namespace SriPravanTech
{
    class dbFunctions
    {
        public static string connectionstring = System.Configuration.ConfigurationSettings.AppSettings["ConStr"];
        public static string VisitorPass = System.Configuration.ConfigurationSettings.AppSettings["VisitorPass"];
        public static string Label = System.Configuration.ConfigurationSettings.AppSettings["BiasTube"];
        public static string PrinterName = System.Configuration.ConfigurationSettings.AppSettings["Printer"];
        public static string Payslip = System.Configuration.ConfigurationSettings.AppSettings["Payslip"];
        public static bool loadid = false;
        public static string username = "";
        public static string rights = "";
        public static string rightsName = ""; 

      
        public static DataTable getTable(string sqlQuery)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionstring);

            try
            {
                con.Open();
               
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Connection = con;
                cmd.CommandTimeout = 999999999;
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(dt);
                con.Close();
                
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                con.Close();
            }
            
            return dt;
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

                        string S= Dt.Rows[i][j].ToString();

                        if (S.Equals("Template"))
                        {
                            DataTable ddd = dbFunctions.getTable("pr_get_Row_Details '" + Dt.Rows[i][0].ToString() + "','" + Dt.Rows[i][1].ToString() + "'");
                            S = dbFunctions.GetJSONString1(ddd);

                            TempStr = TempStr.Replace("\"Temp10¾\"", S);

                        }
                        else
                        TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾",S);


                    }

                    Sb.Append(TempStr + "},");
                }

                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                Sb.Append("]}");
            }
            catch { }



            return Sb.Replace("\n", "\\n").Replace("\r", "\\r").ToString();
        }



        public static string GetJSONString1(DataTable Dt)
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


                Sb.Append("{\"Tempplates" + Dt.TableName + "\" :[ ");

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


        public static string sendsms(string PhoneNo, string msg)
        {
            //DataTable dt = dbFunctions.getTable("select * from Sms_Settings");

            try
            {
                if (PhoneNo != "")
                {
                    //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://sms.nettyfish.com/vendorsms/pushsms.aspx?apikey=3c13c47f-9e29-4365-8b87-bed9c94c58e2&clientId=" + PhoneNo + "&from=SVVBMS&udh=&text=" + "HI" + "&dlr-mask=19");
                    //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://sms.nettyfish.com/vendorsms/pushsms.aspx?apikey=3c13c47f-9e29-4365-8b87-bed9c94c58e2&clientId=f3eca487-5772-47d4-a072-cf2f5fe2cb4b&msisdn=" + PhoneNo + "&sid=GITSCD&msg=" + msg + "&fl=0&gwid=2");
                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://pay4sms.in/sendsms/?token=22f458b57c4062a0399d0019c6dd8bca&credit=2&sender=GITSCD&message=" + msg + "&GITSCD&number=" + PhoneNo);

                    //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(dt.Rows[0]["SM_ConStr1"].ToString() + PhoneNo + dt.Rows[0]["SM_ConStr2"].ToString() + msg + dt.Rows[0]["SM_ConStr3"].ToString());

                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    // MessageBox.Show("hai");
                    myResp.Close();

                    return "true";
                }
                else
                {
                    return "false";
                }

            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }


        public static bool send_MailG(string Tomail, string Subject, string Body, string Attachment)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("mail.scheduler@larch.in");
                mail.To.Add(Tomail);
                mail.Subject = Subject;
                mail.Body = Body; mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("mail.scheduler@larch.in", "larchtech");
                SmtpServer.EnableSsl = true;
                //SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Send(mail);
                
            }
            catch (Exception ex)
            {
              
            }
            return true;
        }



        public static bool sendsms(string PhoneNo)
        {
            //DataTable dt = dbFunctions.getTable("select * from Sms_Settings");

            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://203.212.70.200/smpp/sendsms?username=srivijaya&password=srivijaya123&to=" + PhoneNo + "&from=SVVBMS&udh=&text=" + "HI" + "&dlr-mask=19");
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(dt.Rows[0]["SM_ConStr1"].ToString() + PhoneNo + dt.Rows[0]["SM_ConStr2"].ToString() + msg + dt.Rows[0]["SM_ConStr3"].ToString());

                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                // MessageBox.Show("hai");
                myResp.Close();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }




        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
               // MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

       
    }
}
