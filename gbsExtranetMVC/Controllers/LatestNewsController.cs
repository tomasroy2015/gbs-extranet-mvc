using Business;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models;
using gbsExtranetMVC.Models.Repositories;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Controllers
{
    [RequiresSSL]
    public class LatestNewsController : Controller
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
        public const string ActiveMenu = "Maintanance";
        //
        // GET: /LatestNews/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            //EmailExt model = new EmailExt();
           // ViewBag.EmailTemplates = DropDownLists.GetListEmailTemplates(model.MailTemplateID);
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            return View();
        }
        public ActionResult Edit(int id)
        {
            Session["PostImage"] = "";
            //EmailExt model = new EmailExt();
            TB_LatestNewsRepository modelRepo = new TB_LatestNewsRepository();
            TB_LatestNewsExt model = modelRepo.GetLatestNewsByID(id);
            Session["PostImage"] = model.PostImage;
            // ViewBag.EmailTemplates = DropDownLists.GetListEmailTemplates(model.MailTemplateID);
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            return View(model);
        }




        public ActionResult InsertLatestNews(HttpPostedFileBase txtPostImage, string UserID, string Title_tr, string Title_en, string Title_de, string Title_fr, string Title_ru,
            string Title_ar, string Description_tr, string Description_en, string Description_de, string Description_fr, string Description_ru, string Description_ar, string Active)
        {
                //Session["RegisteredStatus"] = "";
                //Session["VisitorCount"] = "";
                string ContentfileName = "";
                string filenames = "";
                TB_LatestNewsRepository inserthome = new TB_LatestNewsRepository();
                string path = "";
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            int UserId=0;
                try
                {
                    if (txtPostImage != null)
                    {
                        var UploadPath = "~/Upload/LatestNews/";
                        UploadPath = Server.MapPath(UploadPath);
                        CreateDirectory(UploadPath);
                        ContentfileName = Path.GetFileName(UserID + date + txtPostImage.FileName);
                        path = Path.Combine(UploadPath,ContentfileName);
                        try
                        {
                            txtPostImage.SaveAs(path);
                        }
                        catch
                        {

                        }
                        filenames = Convert.ToString(ContentfileName);

                    }

                    UserId = inserthome.InsertLatestNews(UserID, Title_tr, Title_en, Title_de, Title_fr, Title_ru, Title_ar, Description_tr, Description_en, Description_de,
                        Description_fr, Description_ru, Description_ar, Active, ContentfileName);

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
            //Session["VisitorCount"] = "1";
             return Json(UserId, JsonRequestBehavior.AllowGet);
        }

        public string CreateDirectory(string UploadPath)
        {
            string status = "Success";


            var fileInfo = new System.IO.FileInfo(UploadPath);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            return status;
        }

        public ActionResult UpdateLatestNews(HttpPostedFileBase txtPostImage,string ID, string UserID, string Title_tr, string Title_en, string Title_de, string Title_fr, string Title_ru,
           string Title_ar, string Description_tr, string Description_en, string Description_de, string Description_fr, string Description_ru, string Description_ar, string Active)
        {
            //Session["RegisteredStatus"] = "";
            //Session["VisitorCount"] = "";
            string ContentfileName = "";
            string filenames = "";
            TB_LatestNewsRepository inserthome = new TB_LatestNewsRepository();
            string path = "";
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            int UserId = 0;
            try
            {
                if (txtPostImage != null)
                {
                    var UploadPath = "~/Upload/LatestNews/";
                    UploadPath = Server.MapPath(UploadPath);
                    CreateDirectory(UploadPath);
                    ContentfileName = Path.GetFileName(UserID + date + txtPostImage.FileName);
                    path = Path.Combine(UploadPath, ContentfileName);
                    try
                    {
                        txtPostImage.SaveAs(path);
                    }
                    catch
                    {

                    }https://lh3.googleusercontent.com/-JjVs3xz_n7E/AAAAAAAAAAI/AAAAAAAAAAA/Zp4pdAdqtw8/s46-c-k-no/photo.jpg
                   

                    //objAdmin.MimeType = Request.Files["FileUpload1"].ContentType;
                    //objAdmin.FileName = Path.GetFileName(Request.Files["FileUpload1"].FileName);
                    //ContentfileName = Path.GetFileName(Request.Files["FileUpload1"].FileName);
                    filenames = Convert.ToString(ContentfileName);

                }
                else
                {
                    ContentfileName = Convert.ToString(Session["PostImage"]);
                }
                UserId = inserthome.UpdateLatestNews(ID, UserID, Title_tr, Title_en, Title_de, Title_fr, Title_ru, Title_ar, Description_tr, Description_en, Description_de,
                    Description_fr, Description_ru, Description_ar, Active, ContentfileName);

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
            //Session["VisitorCount"] = "1";
            return Json(UserId, JsonRequestBehavior.AllowGet);
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

            if (SelectedLanguage == "ar-SA")
            {
                try
                {
                    CultureInfo TempCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                    TempCulture.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    System.Threading.Thread.CurrentThread.CurrentCulture = TempCulture;
                    CultureInfo TempCulture1 = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentUICulture.Clone();
                    TempCulture1.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = TempCulture1;
                }
                catch
                {
                }
            }
        }
        
    }
}
