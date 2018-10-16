using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBGO.Framework.Models 
{
    public class User
    {
        [Key]
        public int ID { set; get; }
        [Required]
        [StringLength(20)] 
        public string Name { set; get; }
        [StringLength(30)]
        public string Email { set; get; }
        [StringLength(50)]
        public string Pwd { set; get; }
        [StringLength(20)]
        public string IP { set; get; }
        public DateTime AddTime { set; get; }
        public DateTime LastLoginTime { set; get; }
        public int LoginTimes { set; get; } 
        public int BlogID { set; get; }
        public DateTime EditTime { set; get; }
        public bool Status { set; get; }

    }
}