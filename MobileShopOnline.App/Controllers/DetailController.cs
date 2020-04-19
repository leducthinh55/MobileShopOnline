using MobileShopOnline.Models.Dao;
using MobileShopOnline.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShopOnline.App.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        public ActionResult Index(int id)
        {
            MobileDAO MobileDAO = new MobileDAO();
            MobileViewModel Mobile = MobileDAO.GetMobileViewModelById(id);
            return View(Mobile);
        }
    }
}