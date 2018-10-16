using System;
using System.Linq;
using System.Web.Mvc;
using MyBGO.Framework.Models;

namespace MyBlogPark.Controllers
{
    public class BlogController : BaseController
    {   
        // GET: Blog    
        public ActionResult Index(string blog, int p = 1)
        {
            // ViewBag.BlogName = blog;//传递数据的动态类型
            //在博客前台首页这里读取博客的实体，根据博客的标识符在前台呈现
            var model = dbContext.Blog.Where(m => m.Identity.ToLower() == blog.ToLower()).FirstOrDefault();
            if (model == null)
            {
                return Content("博客不存在");
            }
            int pageSize = 6;
            ViewBag.ArticleList = dbContext.Article.Where(m => m.BlogID == model.ID).OrderByDescending(m => m.ID).Skip(( p - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalCount = dbContext.Article.Where(m => m.BlogID == model.ID).Count();
            ViewBag.PageSize = pageSize;

            ViewBag.CatalogList = dbContext.Catalog.Where(m => m.BlogID == model.ID).OrderByDescending(m => m.ID).ToList();
            GetViewBag(model);
            return View(model);
        }
       

        public ActionResult Article(string blog,int id)
        {
            // 先判断用户存不存在
            var blogModel = dbContext.Blog.Where(m => m.Identity.ToLower() == blog.ToLower()).FirstOrDefault();
            if (blogModel == null)
            {
                return Content("博客不存在");
            }
            ViewBag.blog = blogModel;
            GetViewBag(blogModel);
            //在判断文章存不存在
            var model = dbContext.Article.Where(m => m.ID == id).FirstOrDefault();
            if (model == null)
            {
                return Content("博文不存在");
            }
            ViewBag.Article = model;

            ViewBag.CatalogList = dbContext.Catalog.Where(m => m.BlogID == model.ID).OrderByDescending(m => m.ID).ToList();

            return View(blogModel);
          
        }
        //右侧栏热门博文
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {           
             base.OnActionExecuting(filterContext);
        }
        private void GetViewBag(Blog blog)
        {
            ViewBag.HotArticleList = dbContext.Article.Where(m => m.BlogID == blog.ID).OrderByDescending(m => m.Views).Take(3).ToList();
        }
       
    }
}