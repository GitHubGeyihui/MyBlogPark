using MyBGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MyBGO.Framework.MyModels;

namespace MyBGO.Controllers
{
    public class SettingController : BaseController
    {
        public  ActionResult Index()
        {
            return View();
        }
            // GET: Setting

            public ActionResult Catalog()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem() { Text = "无", Value = "0" }
            };
            var catalogList = dbContext.WebCatalog.Where(m => m.PID == 0).ToList();
            foreach (var item in catalogList)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
            }
            ViewBag.ParentList = list;

            ViewBag.DataList = dbContext.WebCatalog.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult AddCatalog(CatalogAdd info)
        {
            if (ModelState.IsValid)
            {
                var model = Mapper.Map<WebCatalog>(info);
                model.AddTime = DateTime.Now;
                model.EditTime = DateTime.Now;
                model.Status = true;
                model.Total = 0;
                model.Refleshs = 0;
                dbContext.WebCatalog.Add(model);
                if (dbContext.SaveChanges() > 0)
                {
                    return Redirect("/setting/catalog");
                }
                ModelState.AddModelError("Name", "保存失败");
            }
            return View(info);
        }

    }
}