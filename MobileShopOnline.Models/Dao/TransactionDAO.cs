using MobileShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopOnline.Models.Dao
{
    public class TransactionDAO
    {
        MobileShopOnlineDbContext db;
        public TransactionDAO()
        {
            db = new MobileShopOnlineDbContext();
        }
        public int AddTransaction(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            db.SaveChanges();
            return db.Transactions.OrderByDescending(x => x.TransactionId).FirstOrDefault().TransactionId;
        }
    }
}
