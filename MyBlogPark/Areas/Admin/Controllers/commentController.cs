using MyBGO.App_Start;
using MyBGO.Framework.MyModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyBlogPark.Areas.Admin.Controllers
{
    public class CommentController : AdminBaseController
    {
        // GET: Admin/comment
        public ActionResult Index(int page = 1)
        {
            if (page <= 0) page = 1;
            int pSize = 5;
            int sk = Convert.ToInt32((page - 1) * pSize);
            var u = Session["loginUser"] as User;
            var a = Session["LoginArticle"] as Article;
            var nList = dbContext.Comment.Where(o => o.Form_UserID == u.ID).OrderByDescending(n => n.ID).Skip(sk).Take(pSize).ToList();
            //ViewBag.ArticleList = dbContext.Comment.Where(m => m.ArticlesID == a.ID).ToList();
            MyPager p = new MyPager(dbContext.Comment.Count(), page, pSize, 5);
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
            var nList = dbContext.Comment.OrderByDescending(o => o.ID).Skip(sk).ToList();
            //生成分页操作
            MyPager p = new MyPager(sk, page, pageSize, 5);
            ViewBag.pager = p.ToHTML();
            return View(nList);
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