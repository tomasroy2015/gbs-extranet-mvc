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
using System.IO;
using System.Transactions;
using System.Drawing.Imaging;
using System.Globalization;
using System.Net;

namespace gbsExtranetMVC.Controllers.Property
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class PropertyPhotosController : Controller
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        // GET: /PropertyPhotos/
        BizContext BizContext = new BizContext();
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        public const string ActiveMenu = "Property";

        
        public ActionResult PLUpload()             
        {

            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            return View();
        }
        [HttpGet]
        public ActionResult PropertyPhotos()             
        {
            Session["PageName"] = "PropertyPhotos";
            AssignBizContext();
            int id = BizContext.HotelID;           
            PropertyPhotosRepository modelRepo = new PropertyPhotosRepository();
             NewPromotionRepository NewP = new NewPromotionRepository();

             var HotelRooms = modelRepo.GetHotelRooms(id);
             List<PropertyPhotosExt> ListOfModel = new List<PropertyPhotosExt>();
             ListOfModel = (List<PropertyPhotosExt>)HotelRooms;
             ViewBag.HotelRooms = ListOfModel;
            ViewBag.Path = modelRepo.GetParameterValue("HotelPhotoPath");
            ViewBag.MaxPhotoSize = modelRepo.GetParameterValue("MaxPhotoSize");
            ViewBag.AllowedPhotoFileExtensions = modelRepo.GetParameterValue("AllowedPhotoFileExtensions");
            ViewBag.HotelID = id;
            ViewBag.CultureValue = CultureValue;
            ViewBag.MaxHotelPhotoCount = modelRepo.GetParameterValue("MaxHotelPhotoCount");
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            return View(ListOfModel);
            //return View();
        }

        public JsonResult getListPhotos(string PropertyID, int PartID)
        {
            PropertyPhotosRepository modelRepo = new PropertyPhotosRepository();
            try { 
            if (PropertyID == "")
            {
                AssignBizContext();
                PropertyID = Convert.ToString(BizContext.HotelID);
                PartID=1;
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
            return Json(modelRepo.LoadPhoto(PartID,Convert.ToInt32(PropertyID)), JsonRequestBehavior.AllowGet);
        }

        public int DeletePhotos(string Photo)
        {
            PropertyPhotosRepository obj = new PropertyPhotosRepository();
            string[] PhotosID = Photo.Split(',');
            var values = "";
            foreach(string PhotoID in PhotosID)
            {
                if(PhotoID !="")
                { 
                values = obj.DeletePhotos(PhotoID,this);
                }
            }
            var val= Convert.ToInt32(values);
            return val;
        }
        public int SetMainPhoto(string ID, string RecordID, string PartID)
        {
            PropertyPhotosRepository obj = new PropertyPhotosRepository();
            var values = obj.MainPhoto(ID, RecordID, PartID, this);
            return values;
        }

        public int SavePhotos(string Photo, string PhValues)
        {
            string PhID = "";
            string PhValue = "";
            PropertyPhotosRepository obj = new PropertyPhotosRepository();
            string[] PhotosID = Photo.Split(',');
            var values = "";
            string[] Text = PhValues.Split(',');
            foreach (string textvalue in Text)
            {
                string[] Textval = textvalue.Split(' ');
                foreach (string val in Textval)
                {
                    if(val !="" && Convert.ToInt32(val)>100)
                    {
                        PhID = val;
                    }
                    else 
                    {
                        PhValue = val;
                    }
                    if (PhID != "" && PhValue!="")
                    { 
                    values = obj.UpdateSort(PhID, PhValue,this);
                    }
                }
                

            }
            foreach (string PhotoID in PhotosID)
            {
                if (PhotoID != "")
                {
                    values = obj.DeletePhotos(PhotoID, this);
                }
            }
            if(values=="")
            {
                values = "1";
            }
            var vall = Convert.ToInt32(values);
            return vall;
        }

        [HttpPost]
        public ActionResult Upload(int? chunk, string name)
        {
            AssignBizContext();
            var fileUpload = Request.Files[0];
            string subFolderParentName = "Hotel";
            string subFolderName = Convert.ToString(BizContext.HotelID);
            //var uploadPath = Server.MapPath("~/Upload/");
            chunk = chunk ?? 0;
            var UploadPath = "~/Upload/";
            if(subFolderParentName !="")
            {
                UploadPath=UploadPath+"/" +subFolderParentName+"/";
            }
            if(subFolderName !="")
            {
                UploadPath=UploadPath +subFolderName+"/";
            }
            UploadPath = Server.MapPath(UploadPath);
            CreateDirectory(UploadPath);
            using (var fs = new FileStream(Path.Combine(UploadPath, name), chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }
            return Content("chunk uploaded", "text/plain");
        }

        //public int UploadImage(string HotelID)
        //{

        //    BizContext = (BizContext)Session["GBAdminBizContext"];
        //    string Hotelname = Convert.ToString(BizContext.Hotels);
        //    Session["GBAdminBizContext"] = BizContext;
        //    PropertyPhotosRepository obj = new PropertyPhotosRepository();
        //    var values = obj.UploadImage(HotelID, Hotelname, this);          
        //    var val = Convert.ToInt32(values);
        //    return val;
        //} 

        public int UploadOperation(string Operation, string FileName, string PartID, int RecordIdHdn)
        {
            AssignBizContext();
            int id = BizContext.HotelID;  
            PropertyPhotosRepository modelRepo = new PropertyPhotosRepository();
            HotelPhotoPath = modelRepo.GetParameterValue("HotelPhotoPath");
            UploadPath = modelRepo.GetParameterValue("HotelPhotoPath");
            PhotoResizeWidt = modelRepo.GetParameterValue("PhotoResizeWidth");
            PhotoResizeHeig = modelRepo.GetParameterValue("PhotoResizeHeight");
            if(Operation == "Upload")
            {
                string NewFileName="";
                string[] FileNames = FileName.Split(';');
                int fileCount = Convert.ToInt16(FileNames.Length) - 1;
                int part = Convert.ToInt32(PartID);
                if (part == 1)
                {
                   MaxPhotoCount = modelRepo.GetParameterValue("MaxHotelPhotoCount"); 
                }
                else
                {
                    MaxPhotoCount = modelRepo.GetParameterValue("MaxRoomPhotoCount");
                }

                if(fileCount <=Convert.ToInt32(MaxPhotoCount))
                {
                    try
                    {
                       // TransactionScope ts = new TransactionScope();
                        foreach(string Filename in FileNames)
                        {
                            if(Filename.Trim() != "" && Filename !="undefined")
                            {
                                string[] FileNam = Filename.Split('.');
                                
                                foreach (string Name in FileNam)
                                {
                                    if (Name != "jpg" && Name != "jpeg" && Name != "png")
                                    { 
                                    NewFileName = Name + ".JPG";
                                    }
                                }
                                string Value = modelRepo.AddImage(part, NewFileName, RecordIdHdn, this);

                                
                                string imagePath = Server.MapPath("~/" + HotelPhotoPath + id + "/" + NewFileName);
                                string adminImagePath = Server.MapPath(HotelPhotoPath + id + "/" + NewFileName);
                               
                              //  UploadPath = UploadPath  + id + "/";
                               
                                int PhotoResizeWidth = Convert.ToInt32(PhotoResizeWidt);
                             
                                int PhotoResizeHeight = Convert.ToInt32(PhotoResizeHeig);
                             //   GetImageFile(Server.MapPath(UploadPath + Filename));
                                string subFolderParentName = "Hotel";
                                string subFolderName = Convert.ToString(BizContext.HotelID);
                                UploadPath = "/Upload/";
                                if (subFolderParentName != "")
                                {
                                    UploadPath = UploadPath + "/" + subFolderParentName + "/";
                                }
                                if (subFolderName != "")
                                {
                                    UploadPath = UploadPath + subFolderName + "/";
                                }
                                string Filepaths = Server.MapPath(UploadPath + Filename);

                                System.Drawing.Image Resize = BizUtil.GetImageFile(Server.MapPath(UploadPath + Filename));
                              System.Drawing.Size Size = new System.Drawing.Size(PhotoResizeWidth, PhotoResizeHeight);
                              System.Drawing.Image resizedImage = BizUtil.ResizeImage(BizUtil.GetImageFile(Server.MapPath(UploadPath + Filename)), Size);
                                CreateDirectory(imagePath);
                                CreateDirectory(adminImagePath);
                                System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
                                System.Drawing.Imaging.EncoderParameters encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                                System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, 90L);
                                encoderParameters.Param[0] = myEncoderParameter;
                                //System.Drawing.Imaging.ImageCodecInfo ImageCodecInfo = new System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders(1);                       
                                System.Drawing.Imaging.ImageCodecInfo ImageCodecInfo;
                                ImageCodecInfo = GetEncoderInfo("image/jpeg");
                                resizedImage.Save(imagePath, ImageCodecInfo, encoderParameters);                             
                                resizedImage.Save(adminImagePath, ImageCodecInfo, encoderParameters);
                            }

                                
                        }
                       // ts.Complete();
                    }
                    catch
                    {

                    }
                }

            }
            return id;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            string Encode;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    //  return encoders[j];
                    Encode = Convert.ToString(encoders[j]);
             
                return encoders[j];
            }
            return null;
        }
        public string CreateDirectory(string UploadPath)
        {
            string status = "Success";

           
                var fileInfo = new System.IO.FileInfo(UploadPath);
                if(!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
                return status;                
            }
        public string GetImageFile(string Path)
        {

          var val = System.Drawing.Image.FromFile(Path);

          return Convert.ToString(val);
        }
        
        
        
        
public  string MaxPhotoCount { get; set; }




public string HotelPhotoPath { get; set; }

public string UploadPath { get; set; }

public string PhotoResizeWidt { get; set; }

public string PhotoResizeHeig { get; set; }


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
