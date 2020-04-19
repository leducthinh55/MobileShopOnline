using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShopOnline.App.Areas.Admin.Controllers
{
    public class LogOutController : Controller
    {
        // GET: Admin/LogOut
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home", new { area =""});
        }
    }
}