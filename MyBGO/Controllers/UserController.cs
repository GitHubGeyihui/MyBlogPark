using MyBGO.App_Start;
using MyBlogPark.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MyBGO.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index(int page = 1)
        {
            if (page <= 0) page = 1;
            int pSize = 5;
            int sk = Convert.ToInt32((page - 1) * pSize);

            var nList = dbContext.User.OrderByDescending(n => n.ID)
                .Skip(sk).Take(pSize).ToList();

            MyPager p = new MyPager(dbContext.User.Count(), page, pSize, 5);
            ViewBag.Pager = p.ToHTML();

            List<MyBGOArticleDetail> c = dbContext.Database.SqlQuery<MyBGOArticleDetail>(@"SELECT a.*,b.[Identity] as BlogIdentity,u.Name as UserName
             FROM [Article] as a
             left join Blog as b
             on a.BlogID = b.ID
             left join [User] as u
             on a.UserID = u.ID").ToList();
            ViewBag.HotArticleList = c;
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
            var nList = dbContext.User.OrderByDescending(o => o.ID).Skip(sk).ToList();
            //生成分页操作
            MyPager p = new MyPager(sk, page, pageSize, 5);
            ViewBag.pager = p.ToHTML();
            return View(nList);
        }
        [HttpPost]
        public int Delete(int id)
        {

            var model = dbContext.User.FirstOrDefault(m => m.ID == id);

            dbContext.User.Attach(model);
            dbContext.Entry(model).State = EntityState.Deleted;
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