using gbsExtranetMVC.Helpers;
using Resources.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace gbsExtranetMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            if (SecurityUtils.UseHTTPS)
            {
                GlobalFilters.Filters.Add(new RequireHttpsProdAttribute());
            }

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            WebApiConfig.Register(GlobalConfiguration.Configuration);

           
            ///<summary>Only Uncomment the following lines when you need to Update the Resource File, it required ReCompilation of the Project///</summary>
            // string GlobalisationDirectory = Path.Combine(Server.MapPath("/Globalization"), "Resources.cs");
            //var builder = new Resources.Utility.ResourceBuilder();
            //string filePath = builder.Create(new DbResourceProvider(""),
            //    summaryCulture: "en-GB", filePath: GlobalisationDirectory);
        }
        
    }
}