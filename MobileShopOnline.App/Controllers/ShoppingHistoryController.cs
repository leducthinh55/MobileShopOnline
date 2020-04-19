using MobileShopOnline.App.Common;
using MobileShopOnline.Models.Dao;
using MobileShopOnline.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShopOnline.App.Controllers
{
    public class ShoppingHistoryController : Controller
    {
        // GET: ShoppingHistory
        public ActionResult Index()
        {
            string email = ((AccountLogin)Session[Constants.USER_SESSION]).Email;
            OrderDAO orderDAO = new OrderDAO();
            IEnumerable<ShoppingHistory> list = orderDAO.GetShoppingHistories(email);
            return View(list);
        }
    }
}