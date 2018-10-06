using AutoMapper;
using MyBGO.Framework.Core;
using MyBGO.Framework.Models;
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
            var list = dbContext.catalog.ToList();
            return View(list);
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
        public ActionResult Update(int id)
        {
            var model = dbContext.catalog.FirstOrDefault(m => m.ID == id);       
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Catalog info)
        {
            if (ModelState.IsValid)
            {
                var model = dbContext.catalog.FirstOrDefault(m => m.ID == info.ID);
                model.Name = info.Name;
                dbContext.catalog.Add(model);          
                dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                int res = dbContext.SaveChanges();
                if (res > 0)
                {
                    return Redirect("/admin/catalog/Index");

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

            var model = dbContext.catalog.FirstOrDefault(m => m.ID == id);

            dbContext.catalog.Attach(model);
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