using MobileShopOnline.App.Common;
using MobileShopOnline.Models.Dao;
using MobileShopOnline.Models.EF;
using MobileShopOnline.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShopOnline.App.Controllers
{
    public class CartController : AuthorizedController
    {
        // GET: Cart
        public ActionResult Index()
        {
            Cart cart = (Cart) Session[Constants.CART];
            if (cart == null)
            {
                cart = new Cart();
            }
            ViewBag.NotAvailable = TempData["NotAvailable"];
            ViewBag.OutOfStock = TempData["OutOfStock"];
            return View();
        }
        public ActionResult AddToCart(int id, int page)
        {
            Cart cart = (Cart) Session[Constants.CART];
            if (cart == null)
            {
                cart = new Cart();
            }
            cart.AddToCart(id);
            Session[Constants.CART] = cart;
            return RedirectToAction("Index", "Home",new { page});
        }

        public ActionResult RemoveItem(int id)
        {
            Cart cart = (Cart)Session[Constants.CART];
            if (cart == null)
            {
                return RedirectToAction("Index");
            }
            cart.RemoveFromCart(id);
            if(cart.items.Count() == 0)
            {
                Session.Remove(Constants.CART);
            }
            return RedirectToAction("Index");
        }
        public ActionResult UpdateCart(FormCollection form)
        {

            Cart cart = (Cart)Session[Constants.CART];
            if (cart == null)
            {
                return RedirectToAction("Index");
            }
            Dictionary<MobileItem, int> items = cart.items;
            string[] quantities = form.GetValues("quantityOfItem");
            MobileItem[] keys = items.Keys.ToArray<MobileItem>();
            for(int i = 0; i< keys.Length; i++)
            {
                items[keys[i]] = int.Parse(quantities[i]);
            }
            return RedirectToAction("Index");
        }

        public ActionResult CheckOut()
        {
            Cart cart = (Cart)Session[Constants.CART];
            if (cart == null)
            {
                return RedirectToAction("Index");
            }
            Dictionary<MobileItem, int> items = cart.items;
            MobileItem[] keys = items.Keys.ToArray<MobileItem>();
            MobileDAO MobileDao = new MobileDAO();
            bool check = true;
            for (int i = 0; i < keys.Length; i++)
            {
                int quantity = MobileDao.GetQuantity(keys[i].MobileId);
                if (quantity == -1)
                {
                    TempData["NotAvailable"] += string.Format("{0} is not available,  \n", keys[i].Name);
                    check = false;
                }
                else if (quantity < items[keys[i]])
                {
                    TempData["OutOfStock"] += string.Format("{0} is out of the stock,  \n", keys[i].Name);
                    check = false;
                }
            }
            if (!check)
            {
                return RedirectToAction("Index");
            } 
            else
            {
                double? amountOfMoney = 0;
                for (int i = 0; i < keys.Length; i++)
                {
                    amountOfMoney += keys[i].Price * items[keys[i]];
                }
                string email = ((AccountLogin)Session[Constants.USER_SESSION]).Email;
                string payment = "cash";
                Transaction transaction = new Transaction();
                transaction.Email = email;
                transaction.AmountOfMoney = amountOfMoney;
                transaction.Payment = payment;
                transaction.TransactionTime = DateTime.Now;
                TransactionDAO transactionDAO = new TransactionDAO();
                int transactionId = transactionDAO.AddTransaction(transaction);

                MobileDAO MobileDAO = new MobileDAO();
                for (int i = 0; i< keys.Length;i++)
                {
                    int MobileId = keys[i].MobileId;
                    int quantityInDB = MobileDao.GetQuantity(MobileId);
                    int quantityInCart = items[keys[i]];
                    int remain = quantityInDB - quantityInCart;
                    MobileDAO.SetQuantity(MobileId, remain);
                }
                OrderDAO orderDAO = new OrderDAO();
                for (int i = 0; i < keys.Length; i++)
                {
                    Order order = new Order();
                    order.Quantity = items[keys[i]];
                    order.MobileId = keys[i].MobileId;
                    order.MobileName = keys[i].Name;
                    order.OrderTime = DateTime.Now;
                    order.TransactionId = transactionId;
                    order.Email = email;
                    orderDAO.SaveOrder(order);
                }
                Session.Remove(Constants.CART);
            }
            return RedirectToAction("Index");
        }
    }
}