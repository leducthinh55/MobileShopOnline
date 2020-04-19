using MobileShopOnline.Models.Dao;
using MobileShopOnline.Models.EF;
using MobileShopOnline.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.App.Common
{
    public class Cart
    {
        public string Email { get; set; }
        public Dictionary<MobileItem, int> items { get; set; }
        public object MobileDao { get; private set; }

        public void AddToCart(int MobileId)
        {
            var dao = new MobileDAO();
            MobileItem Mobile = dao.GetMobileItem(MobileId);
            if (items == null)
            {
                items = new Dictionary<MobileItem, int>();
            }
            if(items.ContainsKey(Mobile))
            {
                int quantity = items[Mobile];
                items[Mobile] = ++quantity;
            } 
            else
            {
                items[Mobile] = 1;
            }
        }

        public void RemoveFromCart(int MobileId)
        {
            if (items == null)
            {
                return;
            }
            var dao = new MobileDAO();
            MobileItem Mobile = dao.GetMobileItem(MobileId);
            if (items.ContainsKey(Mobile))
            {
                items.Remove(Mobile);
            }
        }
    }
}