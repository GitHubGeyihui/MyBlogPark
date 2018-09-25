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
    public class CatalogController : AdminBaseController
    {
        // GET: Admin/Catalog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(CatalogAdd info)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CatalogAdd, Catalog>()
                );
                var model = Mapper.Map<Catalog>(info);
                model.AddTime = DateTime.Now;
                model.EditTime = DateTime.Now;
                model.BlogID = LoginBlog.ID;
                model.Status = true;
                dbContext.catalog.Add(model);
                int res = dbContext.SaveChanges();
                if (res>0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(info);
        }
    }
} 