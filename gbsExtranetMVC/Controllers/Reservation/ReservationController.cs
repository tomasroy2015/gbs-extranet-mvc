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
using System.Net;
using System.Net.Mail;
using Business;
using System.Globalization;

namespace gbsExtranetMVC.Controllers.Reservation
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class ReservationController : Controller
    {

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
        //public const string ActiveMenu = "Reservations";
        public ActionResult Reservations()
        {
         
            Session["PageName"] = "Reservations";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            return View();
        }

        #region Read
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            ReservationRepository modelRepo = new ReservationRepository();
            DataSourceResult result = modelRepo.GetReservationOperations().ToDataSourceResult(request);
            return Json(result);
        }
        #endregion

        public ActionResult AdminHotelReservationOperations(string ReservationID, string Operation)
        {
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
           
            AdminHotelReservationRepository.Encryption64 ob = new AdminHotelReservationRepository.Encryption64();
            string Decryptedoperations = ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(Operation)), "58421043");
            long ReservationID1 = Convert.ToInt64(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(ReservationID)), "58421043"));
            //using (DBEntities db = new DBEntities())
            //{
           // long DecryptedReservationID = ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(ReservationID)), "58421043");
            AdminHotelReservationRepository modelRepo = new AdminHotelReservationRepository();

            using (BaseRepository Repo = new BaseRepository())
            {
                DataTable ReservationHistory = new DataTable();
                ReservationHistory = BizApplication.GetUserOperations(Repo.BizDB, "Date DESC, ID DESC", BizContext.CultureCode, null, Convert.ToString(ReservationID1));

                List<ReservationExt> ListOfModel = new List<ReservationExt>();

                if (ReservationHistory.Rows.Count > 0)
                {
                    foreach (DataRow dr in ReservationHistory.Rows)
                    {
                        ReservationExt Obj = new ReservationExt();

                        // Obj.ReservationID = Convert.ToInt32(dr["RecordID"]);
                        Obj.ReservationOwner = dr["UserFullName"].ToString();

                        DateTime dt1 = Convert.ToDateTime(dr["Date"]);

                        Obj.ReservationDate = (dt1.ToString("d"));
                        Obj.ReservationOperation = dr["OperationTypeName"].ToString();

                        ListOfModel.Add(Obj);

                    }
                }
                ViewBag.HotelHistory = ListOfModel;

            }

            var Hotel = modelRepo.GetReservations(ReservationID1, this, BizContext.UserContext.IsSystemAdmin(), BizContext.UserContext.OriginalUserID).FirstOrDefault(f => f.ReservationID == ReservationID1);
            //BindViewBags(Hotel);
            ViewBag.NameontheCreditCard = Hotel.NameontheCreditCard;
            ViewBag.Operations = Decryptedoperations;
            //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            return View(Hotel);
            //}
        }
       
        public const string ActiveMenu = "Reservations";

        public JsonResult SaveDate(string ID, string CheckInDate, string CheckOutDate)
        {
            string Msg = "";
            string request = "false";
            if (Session["GBAdminBizContext"]!=null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
           
            long UserSessionID = Convert.ToInt64(BizContext.UserSessionID);
            Session["GBAdminBizContext"] = BizContext;

            try
            {
                AdminHotelReservationRepository.Encryption64 ob = new AdminHotelReservationRepository.Encryption64();
                PropertyReservationsRepository modelRepo = new PropertyReservationsRepository();
                long ReservationID = Convert.ToInt64(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(ID)), "58421043"));
                var Reservation = modelRepo.GetReservations().FirstOrDefault(f => f.ReservationID == ReservationID);


                if (modelRepo.ChangeResDate(Reservation, CheckInDate, CheckOutDate, UserSessionID,this) == false)
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

        public ActionResult ReportAsInvalidCC(long ReservationID)
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }

            long UserSessionID = Convert.ToInt64(BizContext.UserSessionID);
            Session["GBAdminBizContext"] = BizContext;
            PropertyReservationsRepository modelRepo = new PropertyReservationsRepository();
            var Reservation = modelRepo.GetReservations().FirstOrDefault(f => f.ReservationID == ReservationID);
            var Result = modelRepo.ReportAsInvalidCC(Reservation, UserSessionID,this);            
            return RedirectToAction("PropertyReservations");
        }
        public ActionResult CancelReservation(long ReservationID)
        {
            PropertyReservationsRepository modelRepo = new PropertyReservationsRepository();
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }

            long UserSessionID = Convert.ToInt64(BizContext.UserSessionID);
            Session["GBAdminBizContext"] = BizContext;

            var Reservation = modelRepo.GetReservations().FirstOrDefault(f => f.ReservationID == ReservationID);
            var Result = modelRepo.CancelReservation(Reservation, this);
            var RoomAvailablity = modelRepo.SaveHotelRoomAvailability(Reservation,this);
            var ReservationStatusHistory = modelRepo.AddReservationStatusHistory(Reservation,this);
            var UserOperation = modelRepo.AddUserOperation(Reservation,UserSessionID, this);
            DataTable MailTemplate = new DataTable();
            MailTemplate = modelRepo.GetMailTemplates("SendHotelReservationInvalidCreditCardCancelEmail", Reservation.ReservationCultureCode);
            string mailTemplateID = "";
            string mailFrom = "";
            string mailSubject = "";
            string mailBody = "";
            //string RoomInfo = "";
            int MailCount = 1;
            if (MailTemplate != null)
            {
                if (MailTemplate.Rows.Count > 0)
                {
                    foreach (DataRow dr in MailTemplate.Rows)
                    {
                        mailTemplateID = dr["ID"].ToString();
                        mailFrom = dr["MailFrom"].ToString();
                        mailSubject = dr["MailSubject"].ToString();
                        mailBody = dr["MailBody"].ToString();
                        //mailSubject = mailSubject.Replace("#HotelName#", HotelName);
                        mailBody = mailBody.Replace("#ReservationID#", Convert.ToString(Reservation.ReservationID));
                        mailBody = mailBody.Replace("#ReservationOwnerFullName#", Reservation.OwnerFullName);
                        mailBody = mailBody.Replace("#HotelName#", Reservation.HotelName);
                        mailBody = mailBody.Replace("#CheckInDate#", Convert.ToString(Reservation.CheckInDate));
                        mailBody = mailBody.Replace("#CheckOutDate#", Convert.ToString(Reservation.CheckOutDate));
                        mailBody = mailBody.Replace("#RoomInfo#", Reservation.Rooms + " " + Reservation.RoomCount + " " + Reservation.PayableAmount);
                        mailBody = mailBody.Replace("#Amount#", Reservation.PayableAmount);
                        if (MailCount == 1)
                        {
                            string MailTemplateID1 = dr["ID"].ToString();
                            string MailFrom1 = dr["MailFrom"].ToString();
                            string MailTo1 = Reservation.Email;
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
                        System.Net.Mail.MailAddress from = new MailAddress(mailFrom, "GBS Hotels");
                        //System.Net.Mail.MailAddress to = new MailAddress("ramkumar@balstechnology.com");
                        System.Net.Mail.MailAddress to = new MailAddress(Reservation.Email);
                        System.Net.Mail.MailMessage m = new MailMessage(from, to);                        

                        m.IsBodyHtml = true;                      
                        m.Subject = mailSubject;
                        m.Priority = System.Net.Mail.MailPriority.High;
                        SmtpClient smtp = new SmtpClient("mail.gbshotels.com", 25);
                        smtp.UseDefaultCredentials = false;
                        // smtp.EnableSsl = true;
                        // m.Body = sb.ToString();
                        m.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                        System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                        (System.Text.RegularExpressions.Regex.Replace(mailBody, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                        System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mailBody, null, "text/html");
                        m.AlternateViews.Add(plainView);
                        m.AlternateViews.Add(htmlView);
                        smtp.Credentials = new NetworkCredential("info@gbshotels.com", "7Ghg3DIJSGBS");
                        try
                        {
                            smtp.Send(m);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            return RedirectToAction("PropertyReservations");
        }   

        public ActionResult MarkAsNoUse(long ReservationID)
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }

            long UserSessionID = Convert.ToInt64(BizContext.UserSessionID);
            Session["GBAdminBizContext"] = BizContext;
            PropertyReservationsRepository modelRepo = new PropertyReservationsRepository();
            var Reservation = modelRepo.GetReservations().FirstOrDefault(f => f.ReservationID == ReservationID);
            var Result = modelRepo.MarkAsNoUse(Reservation, UserSessionID, this);            
            return RedirectToAction("PropertyReservations");
        }   
        
       
        public ActionResult AdminHotelReservation()
        {
            ViewBag.PageTitle = "AdminHotelReservation";
            AdminHotelReservationRepository.Encryption64 ob = new AdminHotelReservationRepository.Encryption64();
            if (Request.QueryString["ReservationID"] != null)
            {
                string checkReservationID = Request.QueryString["ReservationID"].ToString();

                if (checkReservationID != "")
                {

                    string DecryptedReservationID = ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(checkReservationID)), "58421043");
                    ViewBag.ReservationID = DecryptedReservationID;

                }
            }
            return View();
        }

        public string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            try
            {
                foreach (char c in asciiString)
                {
                    int tmp = c;
                    hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
                }
            }
            catch(Exception ex)
            {
                string error = ErrorHandling.HandleException(ex);
                return error;
            }
            
            return hex;
        }

        public string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            try
            {
                while (HexValue.Length > 0)
                {
                    StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                    HexValue = HexValue.Substring(2, HexValue.Length - 2);
                }
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
                return error;
            }
            return StrValue;
        }

      public ActionResult PropertyReservations()
       {
           AssignBizContext();

           if (Request.QueryString["mtid"] != null)
           {
               string mtid = Request.QueryString["mtid"].ToString();
               if (mtid == "3")
               {
                   Session["MenuTabid"] = mtid;
                   SecurityUtils.SetGlobalViewbags(this, "Reservations_P", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
               }
           }
           else
           {
               Session["MenuTabid"] = null;
               SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
           }
           
           //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

           ViewBag.CheckinStart = DateTime.Now.Date.AddDays(-10).ToString("MM/dd/yyyy");
           ViewBag.CheckinEnd = DateTime.Now.Date.AddDays(20).ToString("MM/dd/yyyy");
           return View();
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
