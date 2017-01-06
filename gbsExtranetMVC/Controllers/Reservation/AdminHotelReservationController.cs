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
using System.Net;
namespace gbsExtranetMVC.Controllers.Reservation
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class AdminHotelReservationController : Controller
    {
        //
        // GET: /AdminHotelReservation/
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
        public JsonResult GetReservation(int ReservationID)
        {
            string Msg = "";
            string request = "false";
            AssignBizContext();
          
            try
            {
                AdminHotelReservationRepository modelRepo = new AdminHotelReservationRepository();
              // AdminHotelReservationRepository.AdminHotelReservationExt model = new AdminHotelReservationRepository.AdminHotelReservationExt();
                ViewBag.adminCreditCard = BizContext.UserContext.IsSystemAdmin();
                if (modelRepo.GetReservations(ReservationID, this, BizContext.UserContext.IsSystemAdmin(),BizContext.UserContext.OriginalUserID) == null)
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

       
    }
}
