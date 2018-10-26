using MyBGO.Framework.MyModels;
using System.Web.Mvc;

namespace MyBlogPark.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
          protected User LoginUser
          {
            get
            {
                return Session["loginUser"] as User;
            }
          }

        protected Blog LoginBlog
        {
            get
            {
                return Session["loginBlog"] as Blog;
            }
        }
        protected Catalog LoginCatalog
        {
            get
            {
                return Session["loginCatalog"] as Catalog;
            }
        }
        protected Article LoginArticle
        {
            get
            {
                return Session["LoginArticle"] as Article;
            }
        }
        
      public  DB_MyBlogEntities3 dbContext = new DB_MyBlogEntities3();

    }
}