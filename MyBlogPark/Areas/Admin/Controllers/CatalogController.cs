using AutoMapper;
using MyBGO.Framework.Models;
using MyBlogPark.Areas.Admin.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyBlogPark.Areas.Admin.Controllers
{
    public class CatalogController : AdminBaseController
    {
        // GET: Admin/Catalog
        public ActionResult Index()
        {
            var b = Session["LoginBlog"] as Blog;       
            var list = dbContext.Catalog.Where(o => o.BlogID == b.ID).ToList();
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
                dbContext.Catalog.Add(model);
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
            var model = dbContext.Catalog.FirstOrDefault(m => m.ID == id);       
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Catalog info)
        {
            if (ModelState.IsValid)
            {
               
                var model = dbContext.Catalog.FirstOrDefault(m => m.ID == info.ID);
                model.Name = info.Name;
                dbContext.Catalog.Add(model);          
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

            var model = dbContext.Catalog.FirstOrDefault(m => m.ID == id);

            dbContext.Catalog.Attach(model);
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