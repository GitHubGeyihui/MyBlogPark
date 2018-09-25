using AutoMapper;
using MyBlogPark.Areas.Admin.ViewModels;
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
            return View();
        }
       // [HttpPost]
        //public ActionResult Add(Add info)
        //{
        //    if (ModelState.IsValid)
        //    {
              

        //    }
        //    return View(info);
        //}
    }
} 