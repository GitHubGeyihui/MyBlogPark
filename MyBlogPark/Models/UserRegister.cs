using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogPark.Models
{
    public class UserRegister
    {
        [Required]
        [StringLength(20)]
        [Display(Name="用户名")]
        public string Name { set; get; }
        [Required]
        [StringLength(30)]
        [Display(Name = "邮箱")]
        public string Email { set; get; }
        [Required]
        [StringLength(20)]
        [Display(Name = "密码")]
        public string Pwd { set; get; }
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Compare("Pwd", ErrorMessage = "两次密码必须一样")]
        public string PasswordConfirm { get; set; }


    }
}