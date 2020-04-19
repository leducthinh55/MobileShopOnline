using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileShopOnline.App.Areas.Admin.Models
{
    public class MobileModel
    {
        [Key]
        public int MobileId { get; set; }

        [Required(ErrorMessage = "Please input Name.")]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Please input Price.")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Please input Quantity.")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Please input Category.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please input Status.")]
        public string Status { get; set; }

    }
}