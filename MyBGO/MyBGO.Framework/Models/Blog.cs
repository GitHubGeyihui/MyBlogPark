using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBGO.Framework.Models
{
    public class Blog
    {
        [Key]
        public int ID { set; get; }

        [Required]
        [StringLength(20)]
        public string NiName { set; get; }
        [Required]
        [StringLength(20)]
        public string Identity { set; get; }//博客标识符 
        [StringLength(100)]
        public string Title { set; get; }
        [StringLength(100)]
        public string Signal { set; get; }//签名
        [StringLength(250)]
        public string Discription { set; get; }//博客描述
        public int UserID { set; get; }
        public string LOGO { set; get; }
        public int ThemeID { set; get; }//皮肤，zhutiID
        public DateTime AddTime { set; get; }
        public DateTime EditTime { set; get; }
        public bool Status { set; get; }
        public virtual User User { set; get; }

    }
}