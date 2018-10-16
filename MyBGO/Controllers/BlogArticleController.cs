﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBGO.Controllers
{
    public class BlogArticleController : Controller
    {
        // GET: BlogArticle
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult Load(int sEcho, int? iDisplayStart, int? iDisplayLength, string KeyWords)
        {
            var list = new object[] {
                new { name = @User.Identity.Name, position = "广元", salary = 123, state_date = "asd@@fa", office = true, extn = "123" },
               
  
            };
            if (iDisplayStart == null)
            {
                list = list.Take(4).ToList().ToArray();
            }
            //Json返回的格式 new { draw=2,recordsTotal=24,recordsFiltered=24, data =list }
            //draw:表示请求次数，每次返回去不能是一样的否则数据不会刷新
            //recordsTotal:总记录数
            //recordsFiltered：过滤后的总记录数
            //data:具体的数据对象数组
            return Json(new { draw = sEcho, recordsTotal = 24, recordsFiltered = 24, data = list }, JsonRequestBehavior.AllowGet);
        }
        private long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
    }
}