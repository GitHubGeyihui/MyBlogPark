 using AutoMapper;
using MyBGO.Framework.Core;
using MyBGO.Framework.Models;
using MyBlogPark.Areas.Admin.ViewModels;
using MyBlogPark.Core;
using MyBlogPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogPark.Areas.Admin.Controllers
{
    public class ArticleController : AdminBaseController
    {
        // GET: Admin/Article
        //分页
        public ActionResult Index(int p = 1)
        {
            int pageSize = 6;
            ViewBag.CatalogList = dbContext.Catalog.ToList();
            var b = Session["LoginBlog"] as Blog;
            var listArticle = dbContext.Article.Where(o=>o.UserID==b.UserID).OrderBy(m => m.ID).Skip((p - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.TotalCount = dbContext.Article.Count();
            ViewBag.PageSize = pageSize;
            return View(listArticle);
        }
        public ActionResult Add()
        {
            ViewBag.CatalogList = dbContext.Catalog.ToList();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ArticleAdd info)
        {
            if (ModelState.IsValid)
            {
                var model = Mapper.Map<Article>(info);
                model.EditTime = DateTime.Now;
                model.AddTime = DateTime.Now;
                model.BlogID = LoginBlog.ID;
                model.IsShowHome = true;
                model.Status = true;
                model.UserID = LoginUser.ID;
                model.UP = 0;
                model.Views = 0;
                model.Discription = StringHelper.ReplaceHtmlTag(model.Content, 150);
                dbContext.Article.Add(model);
                int res = dbContext.SaveChanges();
                if (res > 0)
                {
                    return Redirect("/admin/article/Index");

                }
                else
                {
                    return Content("保存失败");
                }
            }
            return View(info);
        }
        public ActionResult Update(int id)
        {
            ViewBag.CatalogList = dbContext.Catalog.ToList();
            var model = dbContext.Article.FirstOrDefault(m => m.ID == id);
            var info = new ArticleUpdate
            {
                Content = model.Content,
                CatalogID = model.CatalogID,
                Title = model.Title,
                ID = model.ID
            };
            return View(info);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ArticleUpdate info)
        {
            if (ModelState.IsValid)
            {
                var model = dbContext.Article.FirstOrDefault(m => m.ID == info.ID);
                model.Content = info.Content;
                model.Title = info.Title;
                model.CatalogID = info.CatalogID;
                model.Discription = StringHelper.ReplaceHtmlTag(model.Content, 150);
                dbContext.Article.Add(model);
                dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                int res = dbContext.SaveChanges();
                if (res > 0)
                {
                    return Redirect("/admin/article/Index");

                }
                else
                {
                    return Content("保存失败");
                }
            }
            return View(info);
        }
        [HttpPost]
        public int Delete(int id)
        {
           
                var model = dbContext.Article.FirstOrDefault(m => m.ID == id);
               
                dbContext.Article.Attach(model);
                dbContext.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                int res = dbContext.SaveChanges();
                if (res > 0)
                {
                    return 1;

                }
                else
                {
                    return 2;
                }
            
       
        }
    }
}