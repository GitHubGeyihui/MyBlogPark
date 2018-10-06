using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogPark.Areas.Admin.ViewModels
{
    public class ArticleUpdate
    {
        public int ID { set; get; }
        [DisplayName("栏目")]
        public int CatalogID { set; get; }

        [StringLength(100)]
        [DisplayName("博客标题")]
        public string Title { set; get; }

        [DisplayName("内容")]

        public string Content { set; get; }//博客描述

    }
}