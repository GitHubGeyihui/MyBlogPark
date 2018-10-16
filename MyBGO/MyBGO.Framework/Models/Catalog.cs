using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBGO.Framework.Models
{
    public class Catalog
    {
        [Key]
        public int ID { set; get; }
        [Required]
        [StringLength(20)]
        public string Name { set; get; } 
        public int BlogID { set; get; }
        public DateTime AddTime { set; get; }
        public DateTime EditTime { set; get; }
        public bool Status { set; get; }
        public int ArticleID { set; get; }

    }
}