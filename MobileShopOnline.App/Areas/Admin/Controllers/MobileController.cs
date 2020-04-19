using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using MobileShopOnline.Models.EF;
using MobileShopOnline.Models.Dao;
using MobileShopOnline.App.Common;
using MobileShopOnline.App.Areas.Admin.Models;
using MobileShopOnline.Models.ViewModel;

namespace MobileShopOnline.App.Areas.Admin.Controllers
{
    public class MobileController : BaseController
    {
        // GET: Admin/Mobile
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var dao = new MobileDAO();
            var model = dao.getListMobileViewModelPagingForAdmin(null, null,page, pageSize);
            return View(model);
        }

        public ActionResult Create()
        {
            
            if (HttpContext.Cache[Constants.LIST_CATEGORY] == null)
            {
                var categoryDao = new CategoryDAO();
                HttpContext.Cache[Constants.LIST_CATEGORY] = categoryDao.GetListCategoryName();
            }
            ViewBag.ListCategory = HttpContext.Cache[Constants.LIST_CATEGORY];
            return View();
        }
        [HttpPost]
        public ActionResult Create(MobileViewModel MobileCreate)
        {
            ViewBag.ListCategory = HttpContext.Cache[Constants.LIST_CATEGORY];//After Post, Model losses the initialized values, and it will have only values which are getting posted along with the Form i.e. input or hidden fields. Make sure you reinitialize the model before passing it to view.
            if (ModelState.IsValid)
            {
                var CategoryDao = new CategoryDAO();
                int CategoryId = CategoryDao.GetCategoryId(MobileCreate.CategoryName);
                var Mobile = new Mobile
                {
                    Name = MobileCreate.Name,
                    Image = MobileCreate.Image,
                    Price = MobileCreate.Price,
                    Quantity = MobileCreate.Quantity,
                    CategoryId = CategoryId,
                    Status = MobileCreate.Status,
                    CreateTime = DateTime.Now,
                };
                var dao = new MobileDAO();
                dao.InsertMobile(Mobile);
                TempData["ChangeStatus"] = "Create successful !";
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (HttpContext.Cache[Constants.LIST_CATEGORY] == null)
            {
                var categoryDao = new CategoryDAO();
                HttpContext.Cache[Constants.LIST_CATEGORY] = categoryDao.GetListCategoryName();
            }
            ViewBag.ListCategory = HttpContext.Cache[Constants.LIST_CATEGORY];

            var dao = new MobileDAO();
            var MobileViewModel = dao.GetMobileViewModelById(id);
            return View(MobileViewModel) ;
        }

        [HttpPost]
        public ActionResult Edit(MobileViewModel Mobile, double OldPrice, int OldQuantity,string OldImage)
        {
            ViewBag.ListCategory = HttpContext.Cache[Constants.LIST_CATEGORY];
            var CategoryDao = new CategoryDAO();
            int CategoryId = CategoryDao.GetCategoryId(Mobile.CategoryName);
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Mobile.Image))
                {
                    Mobile.Image = OldImage;
                }
                var dao = new MobileDAO();
                dao.EditMobile(new Mobile { 
                    MobileId = Mobile.MobileId,
                    Name = Mobile.Name,
                    Image = Mobile.Image,
                    Price = Mobile.Price,
                    Quantity = Mobile.Quantity,
                    Status = Mobile.Status,
                    CategoryId = CategoryId
                });
                var UpdateDao = new UpdateTableDAO();
                bool check = UpdateDao.InsertUpdateTable(new UpdateTable
                {
                    MobileId = Mobile.MobileId,
                    OldPrice = OldPrice,
                    OldQuantity = OldQuantity,
                    NewPrice = Mobile.Price,
                    NewQuantity = Mobile.Quantity,
                    StatusUpdate = "Edit",
                    Email = ((AccountLogin)Session[Constants.USER_SESSION]).Email,
                    TimeUpdate = DateTime.Now,
                }) ;
                TempData["ChangeStatus"] = "Update Successful !";
            } else
            {
                ModelState.AddModelError("", "Update is not Successful !");
                return View("Edit");
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var dao = new MobileDAO();
            bool check = dao.RemoveMobile(id);
            var UpdateDao = new UpdateTableDAO();
            UpdateDao.InsertUpdateTable(new UpdateTable
            {
                MobileId = id,
                StatusUpdate = "Delete",
                Email = ((AccountLogin)Session[Constants.USER_SESSION]).Email,
                TimeUpdate = DateTime.Now,
            });
            if (check)
            {
                TempData["ChangeStatus"] = "Delete Successful !";
            }
            else
            {
                ModelState.AddModelError("", "Delete Fail !");
            }
            return RedirectToAction("Index");
        }
    }
}