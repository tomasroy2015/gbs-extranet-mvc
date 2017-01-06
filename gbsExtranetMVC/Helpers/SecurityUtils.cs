using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Mail;
using gbsExtranetMVC.Models.Repositories;

using System.Reflection;
using System.Diagnostics;
using System.Data;
using gbsExtranetMVC.Models.Enumerations;
using gbsExtranetMVC.Models;
using System.Web.UI;
using Business;



namespace gbsExtranetMVC.Helpers
{
    public static class SecurityUtils
    {
        #region Statis Data Members


        public static Boolean IsSendEmail = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmail"]);
        public static string TempEmailTo = ConfigurationManager.AppSettings["TempEmailTo"].ToString();
        public static Boolean SendTempEmail = Convert.ToBoolean(ConfigurationManager.AppSettings["SendTempEmail"]);
        public static string AdminMAILING_EMAIL_ADDRESS = ConfigurationManager.AppSettings["AdminMailingAddress"].ToString().Trim();
        public static string SupportEmailTo = ConfigurationManager.AppSettings["SupportEmailTo"].ToString();
        public static LoginTypes DefaultAuthenticationMethod = (LoginTypes)Convert.ToInt32(ConfigurationManager.AppSettings["DefaultAuthenticationMethod"]);
        public static string SQLConnectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
        public static string BusinessConnectionString = ConfigurationManager.AppSettings["BusinessConnectionString"].ToString();
        public static Boolean UseHTTPS = Convert.ToBoolean(ConfigurationManager.AppSettings["UseHTTPS"]);

        /// <summary>
        /// Culture Two Letters e.g. en if current Culture is en-GB
        /// </summary>
        public static string Culture_TwoLetterISOLanguageName = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;




        //public static string ApplicationType = ConfigurationManager.AppSettings["ApplicationType"].ToString();
        //public static string ApplicationHeader = GetApplicationHeader();

        public static int Thumb_Height = 150;
        public static int Thumb_Width = 150;

        #endregion


        #region Detect Mobile Device

        public static bool IsMobileDevice(Controller ctrl)
        {
            string strUserAgent = ctrl.Request.UserAgent.ToString().ToLower();
            if (strUserAgent != null)
            {
                if (ctrl.Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                    strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                    strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                    strUserAgent.Contains("palm"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Encryption / Decryption Functions

      
        public static string EncryptText(string strText)
        {
            if (strText != null)
            {
                return Encrypt(strText, "&#?:*%@,");
            }
            else
            {
                return null;
            }
        }

        public static string DecryptCypher(string strText)
        {
            if (strText != null)
            {
                return Decrypt(strText, "&#?:*%@,");
            }
            else
            {
                return null;
            }
        }

        public static string Encrypt(string strText, string strEncrKey)
        {
            //------------------------------------------------------------------------
            //Encryption algorithm code
            //------------------------------------------------------------------------
            byte[] byKey = {
		
	};
            byte[] IV = {
		0x12,
		0x34,
		0x56,
		0x78,
		0x90,
		0xab,
		0xcd,
		0xef
	};

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(strEncrKey, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string Decrypt(string strText, string sDecrKey)
        {
            //------------------------------------------------------------------------
            //Decryption algorithm code
            //------------------------------------------------------------------------
            byte[] byKey = {
		
	};
            byte[] IV = {
		0x12,
		0x34,
		0x56,
		0x78,
		0x90,
		0xab,
		0xcd,
		0xef
	};
            byte[] inputByteArray = new byte[strText.Length + 1];

            strText = Microsoft.VisualBasic.Strings.Replace(strText, " ", "+");

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(sDecrKey, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;

                return encoding.GetString(ms.ToArray());

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        #endregion

        #region Get User

        //Uncomment Following Code to
        /// <summary>
        /// Get Current Logged in User Details
        /// </summary>
        /// <param name="ctlr"></param>
        /// <returns>User Details from Users Table</returns>

        //public static Users GetUserDetails(Controller ctlr)
        //{
        //    using (DBEntities db = new DBEntities())
        //    {
        //        if ((LoginTypes)ctlr.Session["LoginType"] == LoginTypes.WindowsLogin)
        //        {
        //            string Username = ctlr.User.Identity.Name.ToLower();
        //            Users user = user = db.Users.FirstOrDefault(u => u.WindowsUsername.ToLower().Equals(Username) && u.Deleted != true);
        //            return user;

        //        }
        //        else
        //        {
        //            string Username = ctlr.User.Identity.Name.ToLower();
        //            Users user = db.Users.FirstOrDefault(u => u.Username.ToLower().Equals(Username) && u.Deleted != true);

        //            return user;
        //        }


        //    }
        //}

        #endregion

        #region File name Validation Functions

        public static List<char> GetInvalidFileNameCharacters()
        {
            List<char> invalidPathChars = Path.GetInvalidFileNameChars().ToList();
            invalidPathChars.Add('&');
            invalidPathChars.Add('?');
            invalidPathChars.Add('{');
            invalidPathChars.Add('}');
            invalidPathChars.Add('\'');

            return invalidPathChars;
        }

        public static void CheckforInvalidFileNameChar(ref string filename)
        {
            List<char> invalidFilenameChar = SecurityUtils.GetInvalidFileNameCharacters();

            foreach (char c in invalidFilenameChar)
            {
                filename = filename.Replace(c.ToString(), "");
            }
        }

        #endregion

        #region Validation Functions for Login Process


        public static Boolean ValidateAdmin(Controller ctlr)
        {
            Boolean validate = false;
            if (!ctlr.User.Identity.IsAuthenticated)
            {
                if (((ctlr.Session["UserRoleID"] != null)))
                {
                    if (ctlr.Session["UserRoleID"].ToString().Equals("1"))
                    {
                        validate = true;

                    }
                    else
                    {
                        validate = false;
                    }
                }
            }
            else if (ctlr.User.Identity.IsAuthenticated)
            {
                if (((ctlr.Session["UserRoleID"] != null)))
                {
                    if (!(ctlr.Session["UserRoleID"].ToString().Equals("1")))
                    {
                        validate = false;
                    }
                    else
                    {
                        validate = true;
                    }

                }
                else
                { validate = false; }
            }

            return validate;
        }

        #endregion

        #region Conversion Helping Functions

        public static DateTime? ConvertToDateTimeOrNull(string val)
        {
            if (val != "" && val != null)
                return Convert.ToDateTime(val);
            else
                return null;
        }

        public static int? ConvertToIntOrNull(string val)
        {
            if (val != "" && val != null)
                return Convert.ToInt32(val);
            else
                return null;
        }

        public static long? ConvertToLongOrNull(string val)
        {
            if (val != "" && val != null)
                return Convert.ToInt64(val);
            else
                return null;
        }

        public static decimal ConvertToDecimalOrZero(object val)
        {
            if (val != null)
                return Convert.ToDecimal(val);
            else
                return 0.00M;
        }

        public static bool? ConvertToBooleanOrNull(string val)
        {
            if (val != "" && val != null)
                return Convert.ToBoolean(val);
            else
                return null;
        }

        public static decimal? ConvertToDecimalOrNull(string val)
        {
            if (val != "" && val != null)
                return Convert.ToDecimal(val);
            else
                return null;
        }

        public static decimal? ConvertToDecimalOrZero(string val)
        {
            if (val != "" && val != null)
                return Convert.ToDecimal(val);
            else
                return 0;
        }

        public static void ConvertToLong(string val, ref long result)
        {
            if (val != "")
                result = Convert.ToInt64(val);
        }

        public static void ConvertToDecimal(string val, ref Decimal result)
        {
            if (val != "")
                result = Convert.ToDecimal(val);
        }

        public static void ConvertToDateTime(string val, ref DateTime result)
        {
            if (val != "")
                result = Convert.ToDateTime(val);
        }

        public static void ConvertToBoolean(string val, ref Boolean result)
        {
            if (val != "")
                result = Convert.ToBoolean(val);
        }




        public static string TrimText(string str, int n)
        {
            if (str.Length > n) return str.Substring(0, n - 3) + "...";
            return str;
        }

        public static string GetRowStampString(byte[] RowVersion)
        {

            var enumuration = RowVersion.GetEnumerator();
            string _RowVersion = "";
            while (enumuration.MoveNext())
            {
                _RowVersion += enumuration.Current.ToString();
            }


            return _RowVersion;
        }

        public static string GetRowVersionByte(byte[] RowVersion)
        {

            var enumuration = RowVersion.GetEnumerator();
            string _RowVersion = "";
            while (enumuration.MoveNext())
            {
                _RowVersion += enumuration.Current.ToString();
            }


            return _RowVersion;
        }
        #endregion

        #region Email Functions

        public static string EmailErrorMsg = "";
        public static Boolean SendEmail(string From, string To, string Subject, string Body)
        {
            string[] EmailAddresses = To.Replace(" ", string.Empty).Split(';');
            bool status = true;

            //(3) Create the SmtpClient object
            SmtpClient smtp = new SmtpClient();
            // smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            //(4) Send the MailMessage (will use the Web.config settings)
            try
            {
                if (IsSendEmail)
                    foreach (var item in EmailAddresses)
                    {
                        if (item != "")
                        {
                            MailMessage mm;
                            MailAddress from = new MailAddress(From);
                            //(1) Create the MailMessage instance
                            if (SendTempEmail)
                                mm = new MailMessage(From, TempEmailTo, Subject, Body);
                            else
                            {
                                mm = new MailMessage();

                                mm.IsBodyHtml = true;
                                mm.From = from;
                                mm.Subject = Subject;
                                mm.Body = Body;
                            }


                            mm.To.Add(item);
                            smtp.Send(mm);
                        }
                    }
            }
            catch (Exception ex)
            {
                status = false;
                EmailErrorMsg = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message : "";

            }

            return status;
        }

        #endregion

        #region Audit Log
        /// <summary>
        /// Add Audit Log Entries
        /// </summary>
        /// <param name="_Action"> Action happened </param>
        /// <param name="ctlr">Object of Current Controller, just use "this" keyword</param>
        public static void AddAuditLog(string _Action, Controller ctlr)
        {

            int? userID = null;

            if (ctlr.Session["UserID"] != null)
                userID = Convert.ToInt32(ctlr.Session["UserID"].ToString());

            var auditLogRepository = new AuditLogRepository();
            auditLogRepository.AddAuditLog(_Action, userID);

        }

        /// <summary>
        /// Audit logging for transactions, where we pass in the data context and save the changes from outside
        /// </summary>
        public static void AddAuditLog(string _Action, int? userID, DBEntities db, Controller ctlr)
        {
            //Add to Log           

            var auditLogRepository = new AuditLogRepository(db);
            auditLogRepository.AddAuditLog(_Action, userID);

        }


        #endregion

        #region Convert List to DataSet

        public static DataSet ConvertListToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }

        #endregion

        public static string RenderPartialToString(Controller controller, string partialViewName, object model, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialViewName);

            if (result.View != null)
            {
                controller.ViewData.Model = model;
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (HtmlTextWriter output = new HtmlTextWriter(sw))
                    {
                        ViewContext viewContext = new ViewContext(controller.ControllerContext, result.View, viewData, tempData, output);
                        result.View.Render(viewContext, output);
                    }
                }

                return sb.ToString();
            }

            return String.Empty;
        }

        
        public static void SetGlobalViewbags(Controller ctrl, string ActiveMenu = "Home", bool IsAdmin=false,bool IsHotelAdmin=false,int HotelID=0)
        {
           
            ctrl.ViewBag.ActiveMenu = ActiveMenu;
            ctrl.ViewBag.IsAdmin = IsAdmin;
            ctrl.ViewBag.IsHotelAdmin = IsHotelAdmin;
            ctrl.ViewBag.HotelID = HotelID;
        }

       




    }



    public class CalculateTotalFunds
    {
        public Int32 RegionID { get; set; }
        public long? ClientID { get; set; }
        public decimal? Value { get; set; }
    }

    public class PartialView
    {
        public string ViewString { get; set; }

        public PartialView()
        {
            ViewString = "";
        }
    }



    public enum SQLErrorEnums
    {
        //Users
        Username,


        //Clients
        ClientName_AlreadyExists

    }

    public static class Helper
    {
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }


  
    
}

namespace System.Web.Mvc
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using gbsExtranetMVC.Helpers;

    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
        Justification = "Unsealed because type contains virtual extensibility points.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RequireHttpsProdAttribute : FilterAttribute, IAuthorizationFilter
    {

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            string absURL = filterContext.HttpContext.Request.Url.AbsoluteUri;

          
                //modified to check for local requests since cant use ssl for dev server.
                if (!filterContext.HttpContext.Request.IsSecureConnection && !filterContext.HttpContext.Request.IsLocal)
                {
                    if (SecurityUtils.UseHTTPS)
                        HandleNonHttpsRequest(filterContext);
                }
            
        }

        protected virtual void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            // only redirect for GET requests, otherwise the browser might not propagate the verb and request
            // body correctly.

            string absURL = filterContext.HttpContext.Request.Url.AbsoluteUri;

                if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Requests to the given url must use SSL");
                }

                // redirect to HTTPS version of page
                string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult(url);
            
        }

    }
}