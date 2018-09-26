using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogPark.Models
{
    public class Article
    {
        public int ID { set; get; }
        public int CatalogID { set; get; }
        [StringLength(100)]
        public string Title { set; get; } 
        [StringLength(250)]
        public string Discription { set; get; }//博客描述
        public string Content { set; get; }//内容
        public int UserID { set; get; }
        public int BlogID { set; get; }
        public DateTime AddTime { set; get; }
        public DateTime EditTime { set; get; }
        public bool Status { set; get; }
        public bool IsShowHome { set; get; }//是否显示在首页

    }
} 