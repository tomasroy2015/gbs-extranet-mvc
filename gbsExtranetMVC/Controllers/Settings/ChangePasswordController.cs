using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Web.Mvc;
using gbsExtranetMVC.Models.Enumerations;
using MessageColumnCaptions;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Business;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Globalization;

namespace gbsExtranetMVC.Controllers.Settings
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class ChangePasswordController : Controller
    {
        private string Smtp_Username = ConfigurationManager.AppSettings["SMTP_Server"].ToString();
        private string Smtp_Password = ConfigurationManager.AppSettings["SMTP_Password"].ToString();
        private string Smtp_Mail = ConfigurationManager.AppSettings["SMTP_Mail"].ToString();
        private int Smtp_PortNo = Convert.ToInt32(ConfigurationManager.AppSettings["SMTP_PortNo"].ToString());
        private string SMTP_TempEmailTo = ConfigurationManager.AppSettings["TempEmailTo"].ToString();
        private string SMTP_SupportEmailTo = ConfigurationManager.AppSettings["SupportEmailTo"].ToString();
        private string SMTP_SenderName = ConfigurationManager.AppSettings["SMTP_SenderName"].ToString();
        //
        // GET: /ChangePassword/
         //BizContext BizContext = new BizContext();
         //if (Session["GBAdminBizContext"] != null)
         //   {
         //       BizContext = (BizContext)Session["GBAdminBizContext"];
         //   }
         //   Session["GBAdminBizContext"] = BizContext;
            
         //   if(BizContext==null)
         //   {
         //       RedirectToAction("LogOn", "Invoice");
         //   }

        public const string ActiveMenu = "Setting";
        BizContext BizContext = new BizContext();
      
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
                
            }
            Session["GBAdminBizContext"] = BizContext;          
        }
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult ChangePassword()        
        {
            Session["PageName"] = "ChangePassword";
           // string Login = "Account/LogOn";

            if (Session["GBAdminBizContext"] == null)
            {
              //  Response.Redirect(Login + "?Page=" + Server.UrlEncode(BizUtil.GetPageName(Request.RawUrl, true)));
                return  RedirectToAction("LogOn", "Account");
            }
            //string user = Convert.ToString(Session["username"]);
            //string Pass = Convert.ToString(Session["password"]);

            //if (user == "" && Pass == "")
            //{
            //   return  RedirectToAction("LogOn", "Account");
            //}
            else
            {
                try 
                {
                    List<ChangePasswordExt> list = new List<ChangePasswordExt>();
                    ChangePasswordRepository obj = new ChangePasswordRepository();
                    list = obj.GetCode();
                    ViewBag.Valid = list;
                    //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
                    AssignBizContext();
                    SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
                }
                catch (Exception ex)
                {
                    string hostName1 = Dns.GetHostName();
                    string GetUserIPAddress = Dns.GetHostByName(hostName1).AddressList[0].ToString();
                    string PageName = Convert.ToString(Session["PageName"]);
                    //string GetUserIPAddress = GetUserIPAddress1();
                    using (BaseRepository baseRepo = new BaseRepository())
                    {
                        //BizContext BizContext1 = new BizContext();
                        BizApplication.AddError(baseRepo.BizDB, PageName, ex.Message, ex.StackTrace, DateTime.Now, GetUserIPAddress);
                    }
                    Session["PageName"] = "";
                    string error = ErrorHandling.HandleException(ex);
                    return this.Json(new DataSourceResult { Errors = error });
                }
              
                return View();
            }          
        }

        public JsonResult ChangePasswords(string CurrentPassword, string NewPassword, string Verification)
        {
            int i = 0;
            string Culturecode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            try
            {

                using (BaseRepository baseRepo = new BaseRepository())
                {
                    Business.BizTbl_User user = Business.BizUser.GetUser(baseRepo.BizDB, Convert.ToInt32(BizContext.UserContext.UserID));


                    if ((new BizCrypto.AES128()).Decrypt(user.Password) == CurrentPassword)
                    {
                        //Şifre güncellenir
                        BizUser.UpdateUserPassword(baseRepo.BizDB, BizContext.UserContext.UserID, (new BizCrypto.AES128()).Encrypt(CurrentPassword));

                        Mail(Culturecode, BizContext.UserContext.Email, BizContext.UserContext.FullName);
                        i = 1;

                    }
                    else
                    {
                        i = 0;
                    }

                }

                //ChangePasswordRepository obj = new ChangePasswordRepository();
                //string EncryptedCurrentPassword = (new BizCrypto.AES128()).Encrypt(CurrentPassword);

                //DataTable UserTable = obj.GetUser(this);
                //DataRow[] RowUserTable = UserTable.Select(string.Format("Password ='{0}'", EncryptedCurrentPassword));


                //if (RowUserTable.Length > 0)
                //{
                //    i = obj.Update(NewPassword, this);
                //    if (i == 1)
                //    {
                //        Mail(Culturecode, RowUserTable[0]["Email"].ToString(), RowUserTable[0]["UserName"].ToString());
                //    }
                //    else
                //    {
                //        i = 0;
                //    }
                //}

            }
            catch (Exception ex)
            {
                string hostName1 = Dns.GetHostName();
                string GetUserIPAddress = Dns.GetHostByName(hostName1).AddressList[0].ToString();
                string PageName = Convert.ToString(Session["PageName"]);
                //string GetUserIPAddress = GetUserIPAddress1();
                using (BaseRepository baseRepo = new BaseRepository())
                {
                    //BizContext BizContext1 = new BizContext();
                    BizApplication.AddError(baseRepo.BizDB, PageName, ex.Message, ex.StackTrace, DateTime.Now, GetUserIPAddress);
                }
                Session["PageName"] = "";
                string error = ErrorHandling.HandleException(ex);
                return this.Json(new DataSourceResult { Errors = error });
            }
           

            return Json(i, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Mail(string Culturecode, string Email, string Username)
        {
           
            //string Username = "";
            //string Email = "";
            string mailTemplateID = "";
            string mailFrom = "";
            string mailSubject = "";
            string mailBody = "";
            string status = "";

            List<ChangePasswordExt> objList = new List<ChangePasswordExt>();
            DataTable MailTemplate = new DataTable();
            DataTable dt = new DataTable();
            ChangePasswordRepository homemail = new ChangePasswordRepository();

            MailTemplate = homemail.GetMailTemplates("SendUserPasswordChangedEmail", Culturecode);

            // List<ChangePasswordExt> values = homemail.GetUser(this, Username, mailFrom, status);

            //foreach (var items in values)
            //{
            //    Username = items.Username;
            //    Email = items.EmailID;
            //}
            int MailCount = 1;
            if (MailTemplate != null)
            {
                if (MailTemplate.Rows.Count > 0)
                {
                    foreach (DataRow dr in MailTemplate.Rows)
                    {
                        try
                        {
                            mailTemplateID = dr["ID"].ToString();
                            mailFrom = dr["MailFrom"].ToString();
                            mailSubject = dr["MailSubject"].ToString();
                            mailBody = dr["MailBody"].ToString();
                            mailBody = mailBody.Replace("#UserFullName#", Username);
                            if (MailCount == 1)
                            {
                                string MailTemplateID1 = dr["ID"].ToString();
                                string MailFrom1 = dr["MailFrom"].ToString();
                                string MailTo1 = Email;
                                // string MailCC1="";
                                string Subject1 = dr["MailSubject"].ToString();                              
                                //string Body1 = dr["MailBody"].ToString();
                                DateTime SendingDateTime1 = DateTime.Now;
                                DateTime OpDateTime1 = DateTime.Now;
                                long OpUserID1 = 0;

                                using (BaseRepository baseRepo = new BaseRepository())
                                {
                                    string Status = Convert.ToString(BizMail.AddMailForSending(baseRepo.BizDB, MailTemplateID1, MailFrom1, MailTo1, string.Empty, Subject1,
    mailBody, DateTime.Now, OpUserID1));
                                }
                            }
                            MailCount = MailCount + 1;
                            System.Net.Mail.MailAddress from = new MailAddress(SMTP_TempEmailTo, SMTP_SenderName);
                            System.Net.Mail.MailAddress to = new MailAddress(Email);
                            System.Net.Mail.MailMessage m = new MailMessage(from, to);
                            m.IsBodyHtml = true;
                            m.Subject = mailSubject;
                            m.Priority = System.Net.Mail.MailPriority.High;
                            SmtpClient smtp = new SmtpClient(Smtp_Mail, Smtp_PortNo);
                            smtp.UseDefaultCredentials = false;
                           // smtp.EnableSsl = true;
                            // m.Body = sb.ToString();
                            m.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                            (System.Text.RegularExpressions.Regex.Replace(mailBody, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mailBody, null, "text/html");
                            m.AlternateViews.Add(plainView);
                            m.AlternateViews.Add(htmlView);
                            smtp.Credentials = new NetworkCredential(Smtp_Username, Smtp_Password);

                            try
                            {
                                smtp.Send(m);
                                status = "Success";
                            }
                            catch
                            {
                                status = "Failure";
                            }
                        }
                        catch
                        {

                        }
                        
                    }
                }

            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //  string CurrentCulture_TwoLetter = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (requestContext.HttpContext.Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)requestContext.HttpContext.Session["GBAdminBizContext"];
            }
            //string Nameax = ReturnSyatemCulture();
            string SelectedLanguage = "en-Gb";
            if (BizContext.SystemCultureCode != null)
            {
                SelectedLanguage = BizContext.SystemCultureCode;
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(SelectedLanguage);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(SelectedLanguage);

            base.Initialize(requestContext);

            if (SelectedLanguage != "en-Gb")
            {
                try
                {
                    CultureInfo TempCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                    TempCulture.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    TempCulture.NumberFormat.NumberDecimalSeparator = ".";
                    System.Threading.Thread.CurrentThread.CurrentCulture = TempCulture;
                    CultureInfo TempCulture1 = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentUICulture.Clone();
                    TempCulture1.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    TempCulture1.NumberFormat.NumberDecimalSeparator = ".";
                    System.Threading.Thread.CurrentThread.CurrentUICulture = TempCulture1;
                }
                catch
                {
                }

                //base.Initialize(requestContext);
            }
        }
    }
}
