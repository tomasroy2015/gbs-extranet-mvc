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


namespace gbsExtranetMVC.Controllers.Promotions
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class PromotionsController : Controller
    {
        //
        // GET: /Promotion/
        BizContext BizContext = new BizContext();
        public const string ActiveMenu = "Promotions";
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        public ActionResult Promotions()
        {
            Session["PageName"] = "Promotions";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
            return View();
        }
        public ActionResult NewPromotion()
        {
            Session["PageName"] = "NewPromotion";
            PromotionsRepository modelRepo = new PromotionsRepository();
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            int id = BizContext.HotelID;
            LoadViewBagPromotion(id);
            //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
            return View();
        }
      public void LoadViewBagPromotion( int Hotelid)
      {
          DropDownListsRepository DrpRep = new DropDownListsRepository();
          NewPromotionRepository NewProm = new NewPromotionRepository();
          ViewBag.PricePolicies = DrpRep.GetPricePlicy();
          ViewBag.HotelRooms = NewProm.GetHotelRooms(Hotelid);
          ViewBag.DaysDetails = NewProm.GetDay();
          int MinimumPromotionDiscountPercentage = NewProm.GetParameterValue("MinimumPromotionDiscountPercentage");
          int MaximumPromotionDiscountPercentage = NewProm.GetParameterValue("MaximumPromotionDiscountPercentage");
          ViewBag.MinimumPromotionDiscountPercentage = MinimumPromotionDiscountPercentage;
          ViewBag.MaximumPromotionDiscountPercentage = MaximumPromotionDiscountPercentage;
          ViewBag.DefaultPromotionDiscountPercentage = NewProm.GetParameterValue("DefaultPromotionDiscountPercentage");
          ViewBag.MaximumDayCountForMinimumStayPromotion = NewProm.GetParameterValue("MaximumDayCountForMinimumStayPromotion");
          ViewBag.MaximumHourCountForMinimumStayPromotion = NewProm.GetParameterValue("MaximumHourCountForMinimumStayPromotion");
          ViewBag.DiscountPercentValidation = Resources.Resources.RangeWarning + " " + Resources.Resources.MinimumValue + ": " + MinimumPromotionDiscountPercentage + " ," + Resources.Resources.MaximumValue + ": " + MaximumPromotionDiscountPercentage;
      }

        #region Grid Read, Delete Functions
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            PromotionsRepository modelRepo = new PromotionsRepository();
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int id = BizContext.HotelID;
            Session["GBAdminBizContext"] = BizContext;
            DataSourceResult result = modelRepo.ReadAll(id).ToDataSourceResult(request);
            return Json(result);
        }
        public ActionResult _Destroy([DataSourceRequest]DataSourceRequest request, TB_HotelPromotionExt model)
        {
            string Msg = "";
            try
            {
                TB_HotelPromotionRepository modelRepo = new TB_HotelPromotionRepository();
                if (modelRepo.Delete(model, ref Msg, this) == false)
                {
                    return this.Json(new DataSourceResult { Errors = Msg });
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
                return this.Json(new DataSourceResult { Errors = error });
            }


            return Json(request);
        }
        //public ActionResult _Update([DataSourceRequest]DataSourceRequest request, PromotionExt model)
        //{

        //    string Msg = "";
        //    try
        //    {
        //        PromotionsRepository modelRepo = new PromotionsRepository();
        //        if (modelRepo.Update(model, ref Msg, this) == false)
        //        {
        //            return this.Json(new DataSourceResult { Errors = Msg });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ErrorHandling.HandleException(ex);
        //        return this.Json(new DataSourceResult { Errors = error });
        //    }

        //    return Json(request);
        //}
        #endregion

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
