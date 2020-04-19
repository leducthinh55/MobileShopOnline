using MobileShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList.Mvc;
using PagedList;
using MobileShopOnline.Models.ViewModel;

namespace MobileShopOnline.Models.Dao
{
    public class MobileDAO
    {
        private readonly MobileShopOnlineDbContext db;
        public MobileDAO()
        {
            db = new MobileShopOnlineDbContext();
        }
        public bool InsertMobile(Mobile Mobile)
        {
            db.Mobiles.Add(Mobile);
            int row = db.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            return false;
        }

        public bool RemoveMobile(int id)
        {
            Mobile Mobile = db.Mobiles.FirstOrDefault(x => x.MobileId == id);
            Mobile.Status = "inactive";
            int row = db.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            return false;
        }
        public IPagedList<MobileViewModel> getListMobileViewModelPaing(string searchName, string searchCategory ,int page, int pageSize)
        {
            var query = from l in db.Mobiles
                       join
                       c in db.Categories
                       on l.CategoryId equals c.CategoryId
                       select new MobileViewModel
                       {
                           MobileId = l.MobileId,
                           Name = l.Name,
                           Image = l.Image,
                           Price = l.Price,
                           Quantity = l.Quantity,
                           CategoryName = c.CategoryName,
                           Status = l.Status,
                           CreateTime = l.CreateTime,
                       };
            var list = query;
            list = list.Where(x => x.Status.Equals("active"));
            if (!String.IsNullOrEmpty(searchName))
            {
                list = list.Where(x => x.Name.Contains(searchName));
            }

            if (!String.IsNullOrEmpty(searchCategory))
            {
                list = list.Where(x => x.CategoryName.Equals(searchCategory));
            }
            return list.OrderBy(x => x.CreateTime).ToPagedList(page, pageSize);
        }

        public IPagedList<MobileViewModel> getListMobileViewModelPagingForAdmin(string searchName, string searchCategory, int page, int pageSize)
        {
            var query = from l in db.Mobiles
                        join
                        c in db.Categories
                        on l.CategoryId equals c.CategoryId
                        select new MobileViewModel
                        {
                            MobileId = l.MobileId,
                            Name = l.Name,
                            Image = l.Image,
                            Price = l.Price,
                            Quantity = l.Quantity,
                            CategoryName = c.CategoryName,
                            Status = l.Status,
                            CreateTime = l.CreateTime,
                        };
            var list = query;
            if (!String.IsNullOrEmpty(searchName))
            {
                list = list.Where(x => x.Name.Contains(searchName));
            }

            if (!String.IsNullOrEmpty(searchCategory))
            {
                list = list.Where(x => x.CategoryName.Equals(searchCategory));
            }
            return list.OrderBy(x => x.CreateTime).ToPagedList(page, pageSize);
        }

        public Mobile GetMobile(int MobileId)
        {
            return db.Mobiles.FirstOrDefault(x => x.MobileId == MobileId);
        }

        public MobileViewModel GetMobileViewModelById(int MobileId)
        {
            var query = from l in db.Mobiles
                        join
                        c in db.Categories
                        on l.CategoryId equals c.CategoryId
                        select new MobileViewModel
                        {
                            MobileId = l.MobileId,
                            Name = l.Name,
                            Image = l.Image,
                            Price = l.Price,
                            Quantity = l.Quantity,
                            CategoryName = c.CategoryName,
                            Status = l.Status,
                            CreateTime = l.CreateTime,
                        };
            MobileViewModel Mobile = query.Where(x => x.MobileId == MobileId).FirstOrDefault();
            return Mobile;
        }

        public int GetQuantity(int MobileId)
        {
            var Mobile =  db.Mobiles.FirstOrDefault(x => x.MobileId == MobileId && x.Status.Equals("active"));
            if (Mobile != null)
            {
                return Mobile.Quantity.GetValueOrDefault(-1);
            }
            return -1;
        }

        public MobileItem GetMobileItem(int MobileId)
        {
            var Mobile =  db.Mobiles.FirstOrDefault(x => x.MobileId == MobileId);
            return new MobileItem
            {
                MobileId = Mobile.MobileId,
                Name = Mobile.Name,
                Image = Mobile.Image,
                Price = Mobile.Price,
                Quantity = Mobile.Quantity
            };
        }
        public bool EditMobile(Mobile Mobile)
        {
            var MobileUpdate = db.Mobiles.FirstOrDefault(x => x.MobileId == Mobile.MobileId);
            MobileUpdate.Image = Mobile.Image;
            MobileUpdate.Name = Mobile.Name;
            MobileUpdate.Price = Mobile.Price;
            MobileUpdate.Quantity = Mobile.Quantity;
            MobileUpdate.Status = Mobile.Status;
            MobileUpdate.CategoryId = Mobile.CategoryId;
            int row = db.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            return false;
        }

        public bool SetQuantity(int MobileId, int quantity)
        {
            var MobileUpdate = db.Mobiles.FirstOrDefault(x => x.MobileId == MobileId);
            MobileUpdate.Quantity = quantity;
            int row = db.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            return false;
        }
    }
}
