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
        public static void WriteCache(string key, object value)
        {
            _cache.Insert(key, value);
        }

        public static void WriteCache(string key,object value,int seconds,bool isNosliding=false)
        {
            //Cache.NoSlidingExpiration的意思是如果不滑动就是Cache.NoSlidingExpiration则缓存到了某个时间，就绝对失效
            //否则如果是滑动，则只要在这段时间里有人访问了这个缓存。则这个缓存的过期时间又变回这个时间
            //eg：失效时间是一分钟后，如果在这一分钟内没有人访问它，则不管是不是滑动都失效；如果是滑动的
            if (isNosliding)
            {
                _cache.Insert(key, value, null, DateTime.UtcNow.AddSeconds(seconds), Cache.NoSlidingExpiration);
            }
        }
    }
}