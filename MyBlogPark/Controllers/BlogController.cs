using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBGO.Framework.Models;

namespace MyBlogPark.Controllers
{
    public class BlogController : BaseController
    {
        // GET: Blog    
        public ActionResult Index(string blog)
        {
            // ViewBag.BlogName = blog;//传递数据的动态类型
            //在博客前台首页这里读取博客的实体，根据博客的标识符在前台呈现
            var model = dbContext.Blog.Where(m => m.Identity.ToLower() == blog.ToLower()).FirstOrDefault();
            if (model == null)
            {
                return Content("博客不存在");
            }
            ViewBag.AritcleList = dbContext.Article.Where(m => m.BlogID == model.ID).OrderByDescending(m => m.ID).ToList();

            ViewBag.CatalogList = dbContext.Catalog.Where(m => m.BlogID == model.ID).OrderByDescending(m => m.ID).ToList();
            return View(model);
        }
        public ActionResult Article(string blog,int id)
        {
        // 先判断用户存不存在
            var blogModel = dbContext.Blog.Where(m => m.Identity.ToLower() == blog.ToLower()).FirstOrDefault();
            if (blogModel == null)
            {
                return Content("博文不存在"); 
            }
            
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
    } 
}