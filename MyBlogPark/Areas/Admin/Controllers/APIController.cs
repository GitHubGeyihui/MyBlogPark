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

        // CacheItemUpdateCallback 这个回调函数 其实像一个时钟的效果 ，他每次过期的时候又会把缓存存储回去然后等下一次失效的时候又会触发一次。循环，类似一个定时器的效果，相当于定时更新数据库
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
                string idster = key.Substring(key.LastIndexOf("_") + 1, key.Length - key.LastIndexOf("_") - 1);
                int id = Convert.ToInt32(idster);
                //得到缓存
                var value = HttpRuntime.Cache.Get(key);
                var article = dbContext.article.Where(m => m.ID == id).First();
                article.UP = Convert.ToInt32(value);
                dbContext.SaveChanges();
                //准备失效，但回调函数里还是可以读取到缓存的
                expensiveObject = value;
                HttpRuntime.Cache.Remove(key);
            }
            catch (Exception)
            {
                expensiveObject = DateTime.Now.ToString();
            }
            dependency = null;
            absoluteExpiration = DateTime.UtcNow.AddSeconds(-1);
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
                    //文章打开的时候直接读文章表文章表也是点在数
                }
                else
                {
                    up = Convert.ToInt32(obj);
                }
                up = up + 1;
                //CacheHelper.WriteCache("hello", "eqe", 6, false);
                HttpRuntime.Cache.Insert(key, up, null, DateTime.UtcNow.AddSeconds(10), Cache.NoSlidingExpiration, CacheItemPriority.Normal, MyCacheItemPriority);
            }
            catch (Exception)
            {
                return Json(new { status = false });
            }
            return Json(new { status = true });
        }

        private void MyCacheItemPriority(string key, object value, CacheItemRemovedReason reason)
        {
            if (reason == CacheItemRemovedReason.Expired)
            {
                //根据key去取得ID然后读取缓存值 缓存值=点赞数 然后把它记录到数据库中。
                string idster = key.Substring(key.LastIndexOf("_") + 1, key.Length - key.LastIndexOf("_") - 1);
                int id = Convert.ToInt32(idster);
                var article = dbContext.article.Where(m => m.ID == id).First();
                article.UP = Convert.ToInt32(value);
                dbContext.SaveChanges();
            }

             
        }


    }
}