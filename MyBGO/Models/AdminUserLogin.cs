using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBGO.Models
{
    public class AdminUserLogin
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "用户名")]
        public string Name { set; get; }

        [Required]
        [StringLength(20)]
        [Display(Name = "密码")]
        public string Pwd { set; get; }
    }
}