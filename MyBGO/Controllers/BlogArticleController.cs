using MyBGO.App_Start;
using MyBGO.Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBGO.Controllers
{
    public class BlogArticleController : BaseController
    {

        // GET: BlogArticle
        public ActionResult Index(int page = 1)
        {
            if (page <= 0) page = 1;
            int pSize = 5;
            int sk = Convert.ToInt32((page - 1) * pSize);

            var nList = dbContext.Article.OrderByDescending(n => n.ID)
                .Skip(sk).Take(pSize).ToList();

            MyPager p = new MyPager(dbContext.Article.Count(), page, pSize, 5);
            ViewBag.Pager = p.ToHTML();

            return View(nList);
        }
        public ActionResult StuList(int page = 1)
        {
            if (page < 1)
            {
                page = 1;

            }
            int pageSize = 10;
            int sk = (page - 1) * pageSize;
            var nList = dbContext.Article.OrderByDescending(o => o.ID).Skip(sk).ToList();
            //生成分页操作
            MyPager p = new MyPager(sk, page, pageSize, 5);
            ViewBag.pager = p.ToHTML();
            return View(nList);
        }

    }
}