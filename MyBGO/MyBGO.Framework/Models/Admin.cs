using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBGO.Framework.Models
{
    public class Admin
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Pwd { set; get; }
       public DateTime AddTime { set; get; }

        public DateTime EditTime { set; get; }

        public bool Status{ set; get; }
    }
}