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
    public class AccountController : Controller
    {
        // GET: CreateAccount
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NewAccount account)
        {
            if (ModelState.IsValid)
            {
                AccountDAO accountDAO = new AccountDAO();
                bool check = accountDAO.AddUser(new Account
                {
                    Email = account.Email,
                    Password = account.Password,
                    Name = account.Name,
                    Phone = account.Phone,
                    DoB = account.DoB,
                    Status = "active",
                    Role = "client"
                });
                if (check)
                {
                    return RedirectToAction("Login", "Login");
                } else
                {
                    ModelState.AddModelError("", "Email is existed !");
                    return View("Create");
                }
            } 
            return View("Create");
        }

        public ActionResult Edit()
        {
            string Email = ((AccountLogin)Session[Constants.USER_SESSION]).Email;
            AccountDAO accountDAO = new AccountDAO();
            var account = accountDAO.GetAccount(Email);
            var editAccount = new NewAccount
            {
                Email = account.Email,
                Name = account.Name,
                Password = account.Password,
                Confirm = account.Password,
                DoB = account.DoB.Value,
                Phone = account.Phone,
            };
            return View(editAccount);
        }
        [HttpPost]
        public ActionResult Edit(NewAccount account)
        {
            if (ModelState.IsValid)
            {
                AccountDAO accountDAO = new AccountDAO();
                var AccountModel = accountDAO.GetAccount(account.Email);
                AccountModel.Name = account.Name;
                AccountModel.Password = account.Password;
                AccountModel.Phone = account.Phone;
                bool result = accountDAO.UpdateAccount(AccountModel);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Can not update!");
                    return View("Edit");
                }
            }
            return View("Edit");
        }
    }
}