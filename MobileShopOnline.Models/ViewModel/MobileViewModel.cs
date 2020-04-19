using MobileShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopOnline.Models.ViewModel
{
    public class MobileViewModel
    {
        public int MobileId { get; set; }

        [Required(ErrorMessage = "Plese input username")]
        public string Name { get; set; }

        public string Image { get; set; }


        [Required(ErrorMessage = "Plese input Price")]
        [Range(1, 99999.99, ErrorMessage ="Price must be more than 0 and less than 100000")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Plese input Quantity")]
        [Range(1, 9999,ErrorMessage ="Quantity must be more than 1 and less than 10000")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Plese choose Category")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Plese choose Status")]
        public string Status { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
