using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileShopOnline.App.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Plese input username")]
        public string Email {set;get;}

        [Required(ErrorMessage = "Plese input password")]
        public string Password { set; get; }
    }
}