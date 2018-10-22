using MyBlogPark.Core;
using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace MyBlogPark.Controllers
{
    public class APiController : BaseController
    {
        /// <summary>
        /// 当记录点赞数失效的时候 执行这个函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="reason"></param>
        /// <param name="expensiveObject"></param>
        /// <param name="dependency"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        private void MyCacheItemUpdateCallback(string key, CacheItemUpdateReason reason, out object expensiveObject, out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration)
        {
            try
            {
                //根据key 去取得id 然后读取缓存值 缓存值就是点赞数 然后把它记录到数据库中。
                string idstr = key.Substring(key.LastIndexOf("_") + 1, key.Length - key.LastIndexOf("_") - 1);
                int id = Convert.ToInt32(idstr);

                //得到缓存
                var value = HttpRuntime.Cache.Get(key);

                var article = dbContext.Article.Where(m => m.ID == id).First();
                article.UP = Convert.ToInt32(value);
                dbContext.SaveChanges();

                //虽然准备失效了 但是我们在这个回调函数里面还是可以读取到缓存的

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

        // GET: Api
        public ActionResult Up(int id)
        {
            try
            {
                var key = "article_up_" + id;
                var obj = CacheHelper.ReadCache(key);
                int up = 0;
                if (obj == null)
                {
                    var article = dbContext.Article.Where(m => m.ID == id).First();
                    up = article.UP;
                    //这里应该读取数据库 我们这里因为没有 所有就先写为1 文章打开的时候 直接读文章表 文本表也是这个点赞数
                }
                else
                {
                    up = Convert.ToInt32(obj);
                }
                up = up + 1;
                CacheHelper.WriteCache("sdfdsfdsfds", "ddd", 6, false);
                HttpRuntime.Cache.Insert(key, up, null, DateTime.UtcNow.AddSeconds(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, MyCacheItemRemovedCallback);
            }
            catch (Exception ex)
            {
                return Json(new { status = false });
            }
            return Json(new { status = true });
        }

        private void MyCacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            if (reason == CacheItemRemovedReason.Expired)
            {
                //根据key 去取得id 然后读取缓存值 缓存值就是点赞数 然后把它记录到数据库中。
                string idstr = key.Substring(key.LastIndexOf("_") + 1, key.Length - key.LastIndexOf("_") - 1);
                int id = Convert.ToInt32(idstr);

                var article = dbContext.Article.Where(m => m.ID == id).First();
                article.UP = Convert.ToInt32(value);
                dbContext.SaveChanges();
            }
        }
    }

}