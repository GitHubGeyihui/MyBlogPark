using System;
using System.Linq;
using System.Web.Mvc;
using MyBGO.Framework.MyModels;

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
            ViewBag.ArticleList = dbContext.Article.Where(m => m.BlogID == model.ID).OrderByDescending(m => m.ID).Skip((p - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalCount = dbContext.Article.Where(m => m.BlogID == model.ID).Count();
            ViewBag.PageSize = pageSize;

            ViewBag.CatalogList = dbContext.Catalog.Where(m => m.BlogID == model.ID).OrderByDescending(m => m.ID).ToList();

            GetViewBag(model);
            return View(model);
        }
        public ActionResult Article(string blog, int id)
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

            ViewBag.CatalogList = dbContext.Catalog.Where(m => m.BlogID == model.BlogID).OrderByDescending(m => m.ID).ToList();
            ViewBag.CommentList = dbContext.Comment.Where(m => m.ArticleID == id).ToList();
            return View(blogModel);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Article(string content, int id, int to_UserID)
        {
            var model = dbContext.Article.Find(id);
            if (model == null)
            {
                return Content("博文不存在");
            }
            if (to_UserID == 0)
            {
                to_UserID = model.UserID;
            }
            if (Session["loginUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
         
            var comment = new Comment
                {
                    AddTime = DateTime.Now,
                    ArticleID = id,
                    Contents = content,
                    To_UserID = to_UserID,
                    Form_UserID = LoginUser.ID,
                };
                ViewBag.commentList = comment;
                dbContext.Comment.Add(comment);
                dbContext.SaveChanges();
                Blog b = dbContext.Blog.First(o => o.UserID == LoginUser.ID);
                return Redirect("/" + b.Identity + "/p/" + id + ".html");
            

        }
        public ActionResult Catalog(string blog, int id, int p = 1)
        {

            if (dbContext.Blog.Count(m => m.Identity.ToLower() == blog.ToLower()) == 0)
            {
                return Content("博客不存在");
            }


            //在判断分类存不存在
            if (dbContext.Catalog.Count(m => m.ID == id) == 0)
            {
                return Content("分类不存在");
            }
            //在判断文章存不存在
            var model = dbContext.Article.Where(m => m.CatalogID == id).ToList();
            if (model == null)
            {
                return Content("博文不存在");
            }

            int pageSize = 6;

            ViewBag.TotalCount = dbContext.Article.Count(m => m.CatalogID == id);

            ViewBag.PageSize = pageSize;

            ViewBag.blog = blog;
            ViewBag.ArticleList = model;

            ViewBag.CatalogList = dbContext.Catalog.Where(m => m.Blog.Identity == blog).OrderByDescending(m => m.ID).ToList();
            ViewBag.aa = dbContext.Catalog.Find(id);
            ViewBag.HotArticleList = dbContext.Article.Where(m => m.Catalog.Blog.Identity == blog).OrderByDescending(m => m.Views).Take(3).ToList();
            return View(dbContext.Blog.Where(o => o.Identity == blog).FirstOrDefault());

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