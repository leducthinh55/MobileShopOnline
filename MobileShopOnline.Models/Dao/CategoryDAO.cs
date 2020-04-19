using MobileShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopOnline.Models.Dao
{
    public class CategoryDAO
    {
        MobileShopOnlineDbContext db;
        public CategoryDAO()
        {
            db = new MobileShopOnlineDbContext();
        }
        public List<string> GetListCategoryName()
        {
            return db.Categories.Select(x => x.CategoryName).ToList();
        }
        public int GetCategoryId(string Category)
        {
            return db.Categories.FirstOrDefault(x => x.CategoryName.Equals(Category)).CategoryId;
        }
    }
}
