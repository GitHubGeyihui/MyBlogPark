using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBGO.Models
{
    public class CatalogAdd
    {
        [Display(Name = "所属分类")]
        public int PID { set; get; }
        [Required]
        [StringLength(20)]
        [Display(Name = "名称")]
        public string Name { set; get; }
    }
}