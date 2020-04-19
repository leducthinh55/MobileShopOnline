using MobileShopOnline.Models.EF;
using MobileShopOnline.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopOnline.Models.Dao
{
    public class OrderDAO
    {
        MobileShopOnlineDbContext db;
        public OrderDAO()
        {
            db = new MobileShopOnlineDbContext();
        }
        public bool SaveOrder(Order order)
        {
            db.Orders.Add(order);
            int row = db.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<ShoppingHistory> GetShoppingHistories(string email)
        {
            var list =  from o in db.Orders
                        join l in db.Mobiles on o.MobileId equals l.MobileId 
                        select new ShoppingHistory
                        {
                            OrderId = o.OrderId,
                            MobileId = l.MobileId,
                            MobileName = o.MobileName,
                            Quantity = o.Quantity,
                            Price = l.Price,
                            TotalMoney = o.Quantity*l.Price,
                            TimeOrder = o.OrderTime 
                        };
            return list.OrderByDescending(x => x.TimeOrder);
        }
    }
}
