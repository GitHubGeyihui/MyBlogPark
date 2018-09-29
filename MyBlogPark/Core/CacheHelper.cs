using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace MyBlogPark.Core
{
    public class CacheHelper
    {

        private static Cache _cache = HttpRuntime.Cache;
        //写入缓存(内嵌出这个缓存)

        /// <summary>
        /// 缓存永久不过期 除非网站挂掉 or 服务器重启
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        //public static void WriteCache(string key, object value)
        //{
        //    _cache.Insert(key, value);
        //}


        /// <summary>
        /// 如果second是0 ，则永久不会过期 否则会过期 ；什么时候过期 看是否滑动
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds">过期的时间（多少秒后过期）</param>
        /// <param name="isNosliding">是否滑动</param>
        public static void WriteCache(string key, object value, int seconds = 0, bool isNosliding = false)
        {
            //Cache.NoSlidingExpiration的意思是如果不滑动就是Cache.NoSlidingExpiration则缓存到了某个时间，就绝对失效
            //否则如果是滑动，则只要在这段时间里有人访问了这个缓存。则这个缓存的过期时间又变回这个时间
            //eg：失效时间是一分钟后，如果在这一分钟内没有人访问它，则不管是不是滑动都失效；如果是滑动的，那么就是在这一分钟内有人访问这个缓存，有人访问，那么缓存过期时间又变回一分钟后了。
            //58秒就有两秒过期但有人访问了就变回一分钟 
            if (seconds == 0)
            {
                _cache.Insert(key, value);
            }
            else
            {
                if (!isNosliding)
                {
                    _cache.Insert(key, value, null, DateTime.UtcNow.AddSeconds(seconds), Cache.NoSlidingExpiration);
                }
                else
                {
                    _cache.Insert(key, value, null, Cache.NoAbsoluteExpiration,new TimeSpan(0,0,seconds));
                }
            }
        }
        public static bool IsExist(string key)
        {
            return _cache.Get(key) != null;

        }
        public static T RedCache<T>(string key) where T:class
        {
            return _cache.Get(key) as T;

        }
        public static object RedCache(string key) 
        {
            return _cache.Get(key);

        }
    }
}