using MyBlogPark.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace MyBlogPark.Areas.Admin.Controllers
{
    public class APIController : AdminBaseController
    {
        /// <summary>
        /// 当记录点赞数失效的时候，记录这个函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="reason"></param>
        /// <param name="expensiveObject"></param>
        /// <param name="dependency"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        public void MyCacheItemUpdateCallback(
            string key,
            CacheItemUpdateReason reason,
            out object expensiveObject,
            out CacheDependency dependency,
            out DateTime absoluteExpiration,
            out TimeSpan slidingExpiration)
        {
            try
            { 
                //根据key去取得ID然后读取缓存值 缓存值=点赞数 然后把它记录到数据库中。
                string idster = key.Substring(key.LastIndexOf("_")+1,key.Length - key.LastIndexOf("_") - 1);
                int id = Convert.ToInt32(idster);
                 //得到缓存
                var value = HttpRuntime.Cache.Get(key);
                var article = dbContext.article.Where(m => m.ID == id).First();
                article.UP = Convert.ToInt32(value);
                dbContext.SaveChanges();
               
                //准备失效，但回调函数里还是可以读取到缓存的
                expensiveObject = DateTime.Now.ToString();
            }
            catch (Exception)
            {
                expensiveObject = DateTime.Now.ToString();
            }
            dependency = null;
            absoluteExpiration = DateTime.UtcNow.AddSeconds(5);
            slidingExpiration = Cache.NoSlidingExpiration;
        }
        // GET: Admin/API
        public ActionResult Up(int id)
        {
            try
            {
                var key = "arcticle_up_" + id;
                var obj = CacheHelper.RedCache(key);
                int up = 0;
                if (obj == null)
                {
                    var article = dbContext.article.Where(m => m.ID == id).First();
                    up = article.UP;
                    //现在点赞表没有映射到数据库,所以就先设为1 。文章打开的时候直接读文章表文章表也是点在数
                }
                HttpRuntime.Cache.Insert(key, ++up, null, DateTime.UtcNow.AddSeconds(20), Cache.NoSlidingExpiration, MyCacheItemUpdateCallback);
            }
            catch (Exception )
            {

                return Json(new { status = false });
            }
            return Json(new { status = true });
        }
    }
}