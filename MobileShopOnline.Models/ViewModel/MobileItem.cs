using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopOnline.Models.ViewModel
{
    public class MobileItem : IEquatable<MobileItem>
    {
        public int MobileId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public override bool Equals(object o)
        {
            return this.Equals(o as MobileItem);
        }

        public bool Equals(MobileItem other)
        {
            return other!= null && this.MobileId == other.MobileId;
        }

        public override int GetHashCode()
        {
            return MobileId;
        }
    }
}
