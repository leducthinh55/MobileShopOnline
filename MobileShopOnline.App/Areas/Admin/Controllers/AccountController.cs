using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileShopOnline.Models.EF;
using MobileShopOnline.Models.Dao;
using PagedList;

namespace MobileShopOnline.App.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Admin/Account
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var dao = new AccountDAO();
            var model = dao.listAllPaging(page, pageSize);
            return View(model);
        }

        public ActionResult Restore(string email)
        {
            var dao = new AccountDAO();
            bool result = dao.RestoreAccount(email);
            if (result)
            {
                TempData["ChangeStatus"] = "Restore user successful !";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string email)
        {
            var dao = new AccountDAO();
            bool result = dao.DeleteAccount(email);
            if(result)
            {
                TempData["ChangeStatus"] = "Delete user successful !";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Account newAccount)
        {
            if (ModelState.IsValid)
            {
                var AccountDao = new AccountDAO();
                bool check = AccountDao.AddUser(newAccount);
                if (check)
                {
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Add user not success !");
                }
            }
            return View("Index");
        }
    }
}