using System;
using System.ComponentModel.DataAnnotations;

namespace MyBGO.Framework.Models
{
    public class Admin
    {
        [Key]
        public int ID { set; get; }
        public string Name { set; get; }
        public string Pwd { set; get; }
       public DateTime AddTime { set; get; }

        public DateTime EditTime { set; get; }

        public bool Status{ set; get; }

        public int UserID { set; get; }
        public virtual User User { set; get; }
    }
}