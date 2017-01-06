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
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace gbsExtranetMVC.Controllers.Management
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]

    [RequiresSSL]
    public class FirmRequestsController : Controller
    {

        private string Smtp_Username = ConfigurationManager.AppSettings["SMTP_Server"].ToString();
        private string Smtp_Password = ConfigurationManager.AppSettings["SMTP_Password"].ToString();
        private string Smtp_Mail = ConfigurationManager.AppSettings["SMTP_Mail"].ToString();
        private int Smtp_PortNo = Convert.ToInt32(ConfigurationManager.AppSettings["SMTP_PortNo"].ToString());
        private string SMTP_TempEmailTo = ConfigurationManager.AppSettings["TempEmailTo"].ToString();
        private string SMTP_SupportEmailTo = ConfigurationManager.AppSettings["SupportEmailTo"].ToString();
        private string SMTP_SenderName = ConfigurationManager.AppSettings["SMTP_SenderName"].ToString();

        BizContext BizContext = new BizContext();

        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        public ActionResult FirmRequests()
        {
            return View();
        } 
        //public const string ActiveMenu = "Communications";

        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            FirmRequestsRepository modelRepo = new FirmRequestsRepository();
            DataSourceResult result = modelRepo.ReadAll(this).ToDataSourceResult(request);
            return Json(result);

        }

        public string Culturecode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        //public JsonResult UpdateStatus(int ID, int RequestTypeID, int ReservationID, string ChechInDate, string ChechOutDate)
        public JsonResult UpdateStatus(int ID, int RequestTypeID, int ReservationID)
        {

            AssignBizContext();
            int HotelID = BizContext.HotelID;
            string UserSessionID = BizContext.UserSessionID;
            string UserID = Convert.ToString(Session["UserID"]);
            string Msg = "";
            string request = "false";
            try
            {
                FirmRequestsRepository modelRepo = new FirmRequestsRepository();
                BaseRepository baseRepo = new BaseRepository();
                FirmRequestExt model = new FirmRequestExt();

                if (modelRepo.UpdateFirmRequestStatus(ID,this))
                {
                    BizUser.AddUserOperation(baseRepo.BizDB, UserID, DateTime.Now.ToString(), BizCommon.Operation.FirmRequestApproved, "", "", GetUserIPAddress(), UserSessionID);
                }
                if (RequestTypeID == 2)
                {
                    modelRepo.SaveReservationStatus(ReservationID, this);
                    modelRepo.AddReservationStatusHistory(ReservationID, this);
                    BizUser.AddUserOperation(baseRepo.BizDB, UserID, DateTime.Now.ToString(), BizCommon.Operation.ReservationStatusUpdated, "", "", GetUserIPAddress(), UserSessionID);
                }
                else if (RequestTypeID == 1)
                {
                    string FirmreqID = Convert.ToString(ID);
                    string RequesttyID = Convert.ToString(RequestTypeID);
                    string ReservationIDs = Convert.ToString(ReservationID);                    
                    System.Linq.IQueryable<Business.TB_FirmRequest> firmRequest = BizFirm.GetFirmRequests(baseRepo.BizDB, FirmreqID, "", RequesttyID, ReservationIDs, "", null);
                    DateTime? ChechInDa = firmRequest.SingleOrDefault().CheckInDate;
                    DateTime? ChechOutDa = firmRequest.SingleOrDefault().CheckOutDate;
                    modelRepo.SaveHotelReservationDates(ReservationID, ChechInDa,ChechOutDa, this);
                    BizUser.AddUserOperation(baseRepo.BizDB, UserID, DateTime.Now.ToString(), BizCommon.Operation.HotelReservationDatesUpdated, "", "", GetUserIPAddress(), UserSessionID);
                }
                else if (RequestTypeID == 3)
                {
                    string ChargedAmount ="";
                    string ChargedAmountCurrencyID ="";
                    string ChargeDate ="";                
                    int Reservation = Convert.ToInt32(ReservationID);
                    string FirmreqID = Convert.ToString(ID);
                    string RequesttyID = Convert.ToString(RequestTypeID);
                    string ReservationIDs = Convert.ToString(ReservationID);
                    int ReservationOperationID = 4;
                    string RedirectPage = "";
                    string Email ="";
                    modelRepo.SaveReservationOperation(Reservation, ChargedAmount,ReservationOperationID, ChargedAmountCurrencyID, ChargeDate, this);
                    BizUser.AddUserOperation(baseRepo.BizDB, UserID, DateTime.Now.ToString(), BizCommon.Operation.NewCCAsked, "", "", GetUserIPAddress(), UserSessionID);
                    System.Linq.IQueryable<Business.TB_FirmRequest> firmRequest = BizFirm.GetFirmRequests(baseRepo.BizDB, FirmreqID, "", RequesttyID, ReservationIDs, "", null);
                    string EmailID = Convert.ToString(firmRequest.SingleOrDefault().TB_Reservation.Email);
                    if (firmRequest.SingleOrDefault().TB_Reservation.PartID == 1)
                    {
                        RedirectPage = "Home/HotelReservations";
                    }
                    else
                    {
                        RedirectPage = "Home/Reservations";
                    }

                    Mail(Culturecode, ID, RequestTypeID, ReservationID, EmailID, RedirectPage);
                }
                request = "true";
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
          
            return Json(request);
        }

        public JsonResult Mail(string Culturecode,int ID,int RequestTypeID,int ReservationID,string EmailID,string RedirectPage)
        {

            //string Username = "";
            //string Email = "";
            string mailTemplateID = "";
            string mailFrom = "";
            string mailSubject = "";
            string mailBody = "";
            string status = "";
            string FirmreqID = Convert.ToString(ID);
            string RequesttyID = Convert.ToString(RequestTypeID);
            string ReservationIDs = Convert.ToString(ReservationID);
            
           // string statusid = "";
          //  string FirmID = "";
            List <FirmRequestExt> objList = new List<FirmRequestExt>();
            DataTable MailTemplate = new DataTable();
            DataTable Template = new DataTable();
            DataTable dt = new DataTable();
            DataTable MailTemplated = new DataTable();
            FirmRequestsRepository homemail = new FirmRequestsRepository();

            MailTemplate = homemail.GetMailTemplates("SendReservationInvalidCreditCardEmail", Culturecode);

            // List<ChangePasswordExt> values = homemail.GetUser(this, Username, mailFrom, status);

            //foreach (var items in values)
            //{
            //    Username = items.Username;
            //    Email = items.EmailID;
            //}

            BaseRepository baseRepos = new BaseRepository();
         
            System.Linq.IQueryable<Business.TB_FirmRequest> firmRequest = BizFirm.GetFirmRequests(baseRepos.BizDB, FirmreqID, "", RequesttyID, ReservationIDs, "", null);

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
                            mailBody = mailBody.Replace("#ReservationID#", ReservationIDs);
                            mailBody = mailBody.Replace("#PinCode#", firmRequest.SingleOrDefault().TB_Reservation.PinCode);
                            mailBody = mailBody.Replace("#ReservationOwnerFullName#", firmRequest.SingleOrDefault().TB_Reservation.Name + " " + firmRequest.SingleOrDefault().TB_Reservation.Surname);
                            mailBody = mailBody.Replace("#ReservationDate#", firmRequest.SingleOrDefault().TB_Reservation.ReservationDate.ToString());
                            mailBody = mailBody.Replace("#EncReservationID#", firmRequest.SingleOrDefault().TB_Reservation.EncReservationID);
                            mailBody = mailBody.Replace("#EncPinCode#", firmRequest.SingleOrDefault().TB_Reservation.EncPinCode);
                            mailBody = mailBody.Replace("#CultureID#", firmRequest.SingleOrDefault().TB_Reservation.CultureID.ToString());
                            mailBody = mailBody.Replace("#RedirectPage#", RedirectPage);
                           
                            //mailBody = mailBody.Replace("#ReservationDate#", firmRequest.TB_Reservation.ReservationDate.ToString(BizCommon.DateTimeDisplayFormat, Culturecode));
               
                            if (MailCount == 1)
                            {
                                string MailTemplateID1 = dr["ID"].ToString();
                                string MailFrom1 = dr["MailFrom"].ToString();
                                string MailTo1 = EmailID;
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
                            System.Net.Mail.MailAddress to = new MailAddress(EmailID);
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


        public JsonResult RejiectStatus(int ID)
        {
            
            AssignBizContext();
            int HotelID = BizContext.HotelID;
            string UserSessionID = BizContext.UserSessionID;
            string UserID = Convert.ToString(Session["UserID"]);
            string Msg = "";
            string request = "false";
            try
            {
                BaseRepository baseRepo = new BaseRepository();
                FirmRequestsRepository modelRepo = new FirmRequestsRepository();
                FirmRequestExt model = new FirmRequestExt();

                if (modelRepo.RejiectStatus(ID, this) == false)
                {
                    BizUser.AddUserOperation(baseRepo.BizDB, UserID, DateTime.Now.ToString(), BizCommon.Operation.FirmRequestRejected, "", "", GetUserIPAddress(), UserSessionID);
                    return this.Json(new DataSourceResult { Errors = Msg });
                }
                request = "true";
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

            return Json(request);
        }

        protected string GetUserIPAddress()
        {
            string VisitorsIPAddr = string.Empty;
            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }
        public JsonResult deletefirmrequest(int ID)
        {
            string Msg = "";
            string request = "false";
            try
            {
                FirmRequestsRepository modelRepo = new FirmRequestsRepository();
                FirmRequestExt model = new FirmRequestExt();

                if (modelRepo.deletefirmrequest(ID, this) == false)
                {
                    return this.Json(new DataSourceResult { Errors = Msg });
                }
                request = "true";
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

            return Json(request);
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
