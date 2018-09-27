using AutoMapper;
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
        public ActionResult Index()
        {
            ViewBag.CatalogList = dbContext.catalog.ToList();
            var listArticle = dbContext.article.ToList();
            return View(listArticle);
        }
        public ActionResult Add()
        {
            ViewBag.CatalogList = dbContext.catalog.ToList();
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
                model.Discription = StringHelper.ReplaceHtmlTag(model.Content,150);
                dbContext.article.Add(model);
                int res = dbContext.SaveChanges();
                if (res > 0)
                {
                    return Redirect(string.Format("/{0}/p/{1}.html", LoginBlog.Identity, model.ID));

                }
            }
            return View(info);
        }
    }
}