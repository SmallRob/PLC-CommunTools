using System;
using System.Runtime.Caching;

namespace ZCS_Common
{
    /// <summary>
    /// 缓存类
    /// </summary>
    public class DataCache
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            ObjectCache objCache = MemoryCache.Default;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            RemoveCache(CacheKey);

            ObjectCache objCache = MemoryCache.Default;
            object obj = objCache.Get(CacheKey);

            if (obj == null && objObject != null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now.AddMinutes(120);//取得或设定值，这个值会指定是否应该在指定期间过后清除
                objCache.Set(CacheKey, objObject, policy);
            }
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTimeOffset absoluteExpiration)
        {
            ObjectCache objCache = MemoryCache.Default;
            objCache.Set(CacheKey, objObject, absoluteExpiration);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="CacheKey">缓存key</param>
        public static void RemoveCache(string CacheKey)
        {
            ObjectCache objCache = MemoryCache.Default;
            objCache.Remove(CacheKey);
        }

        /// <summary>
        /// 删除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            MemoryCache.Default.Trim(100);
        }
    }
}
