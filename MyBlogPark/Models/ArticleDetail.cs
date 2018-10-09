using MyBGO.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogPark.Models
{
    public class ArticleDetail : Article
    {
        public string BlogIdentity { set; get; }
        public string UserName { set; get; }
    }
}