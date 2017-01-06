using Business;
using gbsExtranetMVC.Models.Enumerations;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Helpers
{
    public class Authorization : AuthorizeAttribute
    {
        private string[] _permissions { get; set; }

        public Authorization(params object[] permission)
        {
            this._permissions = permission.Select(p => p.ToString()).ToArray();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            BaseRepository baseRepo = new BaseRepository();

            Boolean validate = false;
            Boolean locked = false;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                BizTbl_User user = null;
                if (httpContext.Session["UserID"] != null)
                {
                    //TODO: Uncomment the Following Line and remove ->  = null;
                    user = BizUser.GetUser(baseRepo.BizDB, Convert.ToInt32(httpContext.Session["UserID"]));
                }

                if (user != null)
                {
                    if (user.Locked != true)
                    {
                        foreach (var item in this._permissions)
                        {
                            var _enum = Enum.Parse(typeof(Permissions), item);

                            if (httpContext.Session["UserID"] != null &&
                             Convert.ToInt32(Permissions.AllUsers) == Convert.ToInt32(_enum))
                                validate = true;

                            if (httpContext.Session["UserID"] != null &&
                               Convert.ToInt32(httpContext.Session["RoleID"]) == Convert.ToInt32(_enum))
                                validate = true;
                        }
                    }
                    else
                    {
                        locked = true;
                    }
                }
            }
            else
            {
                validate = false;
            }

            if (validate)
                return true;
            else
            {
                if (httpContext.Request.Url.Segments.Count() <= 1 || httpContext.Request.Url.PathAndQuery.Contains("Home"))
                {
                    httpContext.Response.StatusCode = 200;
                    httpContext.Response.Redirect("/Account/LogOn");
                    ErrorHandling.SetErrorCode("UnauthorizedAccess");
                }
                else
                {
                    httpContext.Response.StatusCode = 200;
                    if (locked)
                        httpContext.Response.Redirect("/Error/UnAuthorizedAccess?Locked=True");
                    else
                        httpContext.Response.Redirect("/Error/UnAuthorizedAccess");

                    ErrorHandling.SetErrorCode("UnauthorizedAccess");
                }

                return false;
            }
        }
    }
}