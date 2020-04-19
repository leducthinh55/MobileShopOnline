using MobileShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
namespace MobileShopOnline.Models.Dao
{
    public class AccountDAO
    {
        MobileShopOnlineDbContext db;
        public AccountDAO()
        {
            db = new MobileShopOnlineDbContext();
        }
        public bool AddUser(Account entity) 
        {
            try
            {
                db.Accounts.Add(entity);
                int row = db.SaveChanges();
                if (row > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("duplicate"))
                {
                    return false;
                }
            }
            return false;         
        } 
        public bool Login(string email, string password)
        {
            var count =  db.Accounts.Count(x => x.Email == email && x.Password == password && x.Status.Equals("active"));
            if (count > 0)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
        public IEnumerable<Account> listAllPaging(int page, int pageSize)
        {
            return db.Accounts.Where(x => !x.Role.Equals("admin")).OrderBy(x => x.Email).ToPagedList(page, pageSize);
        }
        public Account GetAccount(string email)
        {
            return db.Accounts.FirstOrDefault(x => x.Email == email);
        }

        public bool UpdateAccount(Account entity)
        {
            var account = db.Accounts.FirstOrDefault(x => x.Email == entity.Email);
            account.Phone = entity.Phone;
            account.Name = entity.Name;
            account.Password = entity.Password;
            db.SaveChanges();
            return true;
        }

        public bool DeleteAccount(string email)
        {
            var account = db.Accounts.FirstOrDefault(x => x.Email.Equals(email));
            account.Status = "inactive";
            int row = db.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            return false;
        }

        public bool RestoreAccount(string email)
        {
            var account = db.Accounts.FirstOrDefault(x => x.Email.Equals(email));
            account.Status = "active";
            int row = db.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            return false;
        }

    }
}
