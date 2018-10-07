using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogPark.Areas.Admin.ViewModels
{
    public class UserPassword
    {
        [StringLength(50)]
        [Required]
        [Display(Name = "原密码")]
        public string Pwd { set; get; }

        [StringLength(50)]
        [Required]
        [Display(Name = "新密码")]
        public string NewPwd { set; get; }

        [StringLength(50)]
        [Required]
        [Display(Name = "确认密码")]
        [Compare("NewPwd", ErrorMessage = "{0}和新密码不一致")]
        public string ComfrimPwd { set; get; }



    }
}