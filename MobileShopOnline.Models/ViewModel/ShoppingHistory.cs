using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopOnline.Models.ViewModel
{
    public class ShoppingHistory
    {
        public int OrderId { get; set; }
        public int MobileId { get; set; }
        public string MobileName { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? TotalMoney { get; set; }
        public DateTime? TimeOrder { get; set; }
    }
}
