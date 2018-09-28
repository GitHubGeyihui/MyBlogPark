using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogPark.Areas.Admin.ViewModels
{
    public class UserInfo
    {

        [Required]
        [StringLength(30)]
        [Display(Name = "邮箱")]
        public string Email { set; get; }

    }
}