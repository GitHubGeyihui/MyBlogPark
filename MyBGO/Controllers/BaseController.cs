using MyBGO.Framework.MyModels;
using System.Web.Mvc;

namespace MyBGO.Controllers
{

    public class BaseController : Controller
    {
        public DB_MyBlogEntities3 dbContext = new DB_MyBlogEntities3();
        protected Admin AdminLoginUser
        {
            get
            {
                return Session["AdminLoginUser"] as Admin;
            }
        }
        protected Article AdminLoginArticle
        {
            get
            {
                return Session["AdminLoginArticle"] as Article;
            }
        }
        
    }
}
