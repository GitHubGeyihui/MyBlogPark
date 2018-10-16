using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBGO.Framework.Models
{
    public class Article
    {
        [Key]
        public int ID { set; get; }
        public int CatalogID { set; get; }
        [StringLength(100)]
        public string Title { set; get; } 
        [StringLength(250)]
        public string Discription { set; get; }//博客描述
        public string Content { set; get; }//内容
        public int UP { set; get; }//点赞
        public int UserID { set; get; }
        public int BlogID  { set; get; }
        [StringLength(20)]
        public string Views { set; get; }//文章点击数
        public DateTime AddTime { set; get; }
        public DateTime EditTime { set; get; }
        public bool Status { set; get; }
        public bool IsShowHome { set; get; }//是否显示在首页
        public int LeveMax  { set; get; }//评论最大楼层ID 
    }
} 