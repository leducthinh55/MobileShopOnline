using MobileShopOnline.App.Common;
using MobileShopOnline.App.Models;
using MobileShopOnline.Models.Dao;
using MobileShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShopOnline.App.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                AccountDAO accountDAO = new AccountDAO();
                bool check = accountDAO.Login(loginModel.Email, loginModel.Password);
                if (check)
                {
                    Account user = accountDAO.GetAccount(loginModel.Email);
                    AccountLogin accountLogin = new AccountLogin();
                    accountLogin.Email = user.Email;
                    accountLogin.Name = user.Name;
                    accountLogin.Role = user.Role;
                    Session[Constants.USER_SESSION] = accountLogin;
                    if (accountLogin.Role.Equals("admin"))
                    {
                        return RedirectToAction("Index", "Mobile", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("","Username or password is not correct!");
            }
            return View("Login");
        }
    }
}