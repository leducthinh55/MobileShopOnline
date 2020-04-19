using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobileShopOnline.App.Models
{
    public class NewAccount
    {
        [Required(ErrorMessage ="Please input Email.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please input Password.")]
        [MinLength(6,ErrorMessage ="Password length more than 5")]
        [MaxLength(20, ErrorMessage = "Password length less than 21")]
        public string Password { get; set; }

        [NotMapped] // does not effect with database
        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        public string Confirm { get; set; }

        [MinLength(4, ErrorMessage = "Name length more than 3")]
        [MaxLength(50, ErrorMessage = "Password length less than 51")]
        [Required(ErrorMessage = "Please input Name.")]
        public string Name { get; set; }

        [Phone]
        [Required(ErrorMessage = "Please input Phone.")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Please input Date of Birth.")]
        [Display(Name = "Date of birth:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DoB{ get; set; }
    }
}