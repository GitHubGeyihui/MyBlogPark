using BotDetect.Web.Mvc;
using MyBGO.Framework.MyModels;
using MyBlogPark.Core;
using MyBlogPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace MyBlogPark.Controllers
{
    public class HomeController : BaseController
    {
        //DB_MyBlogEntities2 dbContext = new DB_MyBlogEntities2();
        public ActionResult Index()
        {
          
            //读取全站的博客分类 
            var key = "index_catalog";
            //读首页的缓存
            var list = CacheHelper.RedCache<List<Catalog>>(key);
            if (list == null)
            {
                list = dbContext.Catalog.ToList();
                CacheHelper.WriteCache(key,list,10,false);//十秒绝对失效
            }
            ViewBag.catalogs = list;
            ViewBag.blog = dbContext.Blog;
            ViewBag.ListArticle = dbContext.Article.OrderBy(m => m.ID).Take(10).ToList();
            ViewBag.ListUser = dbContext.User.OrderBy(m => m.ID).Take(8).ToList();
            List<ArticleDetail> c = dbContext.Database.SqlQuery<ArticleDetail>(@"SELECT a.*,b.[Identity] as BlogIdentity,u.Name as UserName
             FROM [dbo].[Article] as a
             left join Blog as b
             on a.BlogID = b.ID
             left join [User] as u
             on a.UserID = u.ID").ToList();
            ViewBag.HotArticleList = c;
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult Register(UserRegister info)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = info.Name,
                    Pwd = info.Pwd,
                    Email = info.Email,
                    IP = "127.0.0.1",
                    AddTime = DateTime.Now,
                    EditTime = DateTime.Now,          
                    BlogID = 0,
                    Status = true
                };
                dbContext.User.Add(user);
                var res = dbContext.SaveChanges();
                if (res > 0)
                {
                    //浏览器-（cookie）-》服务器（去查找cookie对应的session）-cookie对应的session可能有多个，所以通过key来取得
                    Session["loginUser"] = user;
                    return Redirect("/");
                }
                else
                {
                    return Content("注册失败");
                }         
            }
            return View(info);
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin info)
        {
            MvcCaptcha mvcCaptcha = new MvcCaptcha("ExampleCaptcha");
            string userInput = HttpContext.Request.Form["ValidateCode"];
            string validatingInstanceId = HttpContext.Request.Form[mvcCaptcha.ValidatingInstanceKey];
            if (mvcCaptcha.Validate(userInput, validatingInstanceId))
            {
                if (ModelState.IsValid)
                {
                    var dbUser = dbContext.User.Where(m => m.Name.ToLower() == info.Name.ToLower()).FirstOrDefault();
                    if (dbUser != null && dbUser.Pwd == info.Pwd)
                    {
                        Session["loginUser"] = dbUser;
                        //把当前用户的博客记录起来
                        var blog = dbContext.Blog.Where(m => m.UserID == dbUser.ID && m.Status).FirstOrDefault();
                        if (blog != null)
                        {
                            Session["loginBlog"] = blog;
                            return Redirect("/"+blog.Identity);
                        }
                        else
                        {
                            return Redirect("/");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "账号或密码不正确！");
                }
            }
            else
            {
                ModelState.AddModelError("ValidateCode", "验证码不正确");
            }
            return View(info);

        }
        public ActionResult LogOff()
        {
            Session["loginUser"] = null;
            Session["loginBlog"] = null;
            Session.Abandon();//取消当前会话
            Session.RemoveAll();
            Session.Clear();
            return Redirect("/");
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
        [HttpPost]
        public ActionResult Index(String ArticleGenre, String searchString)
        {
            /*获取Movie表中的’博文类型’数据，并将其封装在ViewBag中，给视图中的下拉列表使用*/
            var genreList = new List<String>();
            var genreQry = from d in dbContext.Article orderby d.Catalog.Blog.User.Name select d.Catalog.Blog.User.Name;
             genreList.AddRange(genreQry.Distinct());
            ViewBag.movieGenre = new SelectList(genreList);

            //获取Movie表中全部数据
            var articles = from a in dbContext.Article
                         select a;

            //判断参数是否有值，第一次请求参数是空的，所以就显示全部数据；当执行搜索后，表单会把两个参数传过来，就可以对数据过滤了；
            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.Content.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(ArticleGenre))
            {
                articles = articles.Where(x => x.Title == ArticleGenre);
            }

            //将封装好的数据传给视图
            return View(articles);
        }
    }
}