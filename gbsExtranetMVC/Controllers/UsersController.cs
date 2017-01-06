using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models;
using gbsExtranetMVC.Models.Repositories;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using gbsExtranetMVC.Models.Enumerations;

namespace gbsExtranetMVC.Controllers
{
    [Authorization(Permissions.AllUsers)]
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View();
        }

        #region Create New User

        [HttpGet]
        public ActionResult Create()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult Create(UsersExt model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsersRepository uRepo = new UsersRepository();
                    if (uRepo.Create(model, ModelState, this))
                    {
                        return RedirectToAction("Index", "Users");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error: Please Correct/Enter All the Information below to Save this Record, All Fields are Mandatory");
                    ErrorHandling.HandleModelStateException(ex, ModelState);
                }
            }
        
            return View(model);
        }

        #endregion Create New User

        #region Edit User Details

        [HttpGet]
        public ActionResult Edit(long id)
        {
            using (DBEntities db = new DBEntities())
            {
                UsersRepository uRepo = new UsersRepository();
                var user = uRepo.ReadAll().FirstOrDefault(u => u.UserID == id);

                user.Password = SecurityUtils.DecryptCypher(user.Password);

               

                return View(user);
            }
        }

        [HttpPost]
        public ActionResult Edit(UsersExt model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsersRepository uRepo = new UsersRepository();
                    if (uRepo.Update(model, ModelState, this))
                    {
                        return RedirectToAction("Index", "Users");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error: Please Correct/Enter All the Information below to Save this Record, All Fields are Mandatory");
                    ErrorHandling.HandleModelStateException(ex, ModelState);
                }
            }
          
           
            return View(model);
        }

        #endregion Edit User Details

        #region Bind Form Data

        

        #endregion Bind Form Data

        #region Grid Read & Delete Functions

        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            UsersRepository uRepo = new UsersRepository();
            DataSourceResult result = uRepo.ReadAll().ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult _Destroy([DataSourceRequest]DataSourceRequest request, UsersExt userExt)
        {
            using (DBEntities db = new DBEntities())
            {
                try
                {
                    UsersRepository uRepo = new UsersRepository();
                    uRepo.Delete(userExt, this);
                }
                catch (Exception ex)
                {
                    ErrorHandling.HandleModelStateException(ex, ModelState);
                    ErrorHandling.SetErrorCode(ex);
                    throw new Exception("");
                }
            }

            return Json(request);
        }

        #endregion Grid Read & Delete Functions

    }
}
