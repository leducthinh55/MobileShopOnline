using MobileShopOnline.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MobileShopOnline.App.Common;

namespace MobileShopOnline.App.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string searchName, string searchCategory ,int page=1, int pageSize=9)
        {
            var dao = new MobileDAO();
            var model = dao.getListMobileViewModelPaing(searchName, searchCategory, page, pageSize);
            var categoryDao = new CategoryDAO();
            if (HttpContext.Cache[Constants.LIST_CATEGORY] == null)
            {
                HttpContext.Cache[Constants.LIST_CATEGORY] = categoryDao.GetListCategoryName();
            }
            ViewBag.ListCategory = HttpContext.Cache[Constants.LIST_CATEGORY];
            ViewBag.searchName = searchName;
            ViewBag.searchCategory = searchCategory;
            return View(model);
        }
    }
}