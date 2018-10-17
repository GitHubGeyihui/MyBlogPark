using AutoMapper;
using MyBGO.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogPark.Areas.Admin.Controllers
{
    public class CommentController : AdminBaseController
    {
        // GET: Admin/comment
        public ActionResult Index()
        {
           
                return View();
        }
        [HttpPost]
        public int Delete(int id)
        {

            var model = dbContext.Comment.FirstOrDefault(m => m.ID == id);

            dbContext.Comment.Attach(model);
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