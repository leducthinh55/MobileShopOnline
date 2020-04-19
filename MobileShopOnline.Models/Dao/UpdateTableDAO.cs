using MobileShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopOnline.Models.Dao
{
    public class UpdateTableDAO
    {
        public readonly MobileShopOnlineDbContext db;

        public UpdateTableDAO()
        {
            db = new MobileShopOnlineDbContext();
        }

        public bool InsertUpdateTable(UpdateTable updateTable)
        {
            db.UpdateTables.Add(updateTable);
            db.SaveChanges();
            return true;
        }
    }
}
