using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MyBlogPark.Areas.Admin.ViewModels
{
    public class CatalogAdd
    {
        [Display(Name = "所属分类")]
        public string PID { set; get; }
        [Required]
        [StringLength(20)] 
        [Display(Name="名称")]
        public string Name { set; get; }


    }
}