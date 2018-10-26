using MyBGO.Framework.MyModels;

namespace MyBlogPark.Models
{
    public class ArticleDetail : Article
    {
        public string BlogIdentity { set; get; }
        public string UserName { set; get; }
    }
}