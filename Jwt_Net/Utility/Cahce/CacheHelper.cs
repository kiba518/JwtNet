using System;
using System.Web;
using System.Web.Caching;

namespace Utility
{
    /// <summary>
    /// 系统缓存帮助类
    /// </summary>
    public class CacheHelper
    {
        public static object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public static T GetCache<T>(string key) where T : class
        {
            return (T)HttpRuntime.Cache[key];
        }

        public static bool ContainsKey(string key)
        {
            return GetCache(key) != null;
        }

        public static void RemoveCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public static void SetKeyExpire(string key, TimeSpan expire)
        {
            object value = GetCache(key);
            SetCache(key, value, expire);
        }

        public static void SetCache(string key, object value)
        {
            _SetCache(key, value, null, null);
        }

        public static void SetCache(string key, object value, TimeSpan timeout)
        {
            _SetCache(key, value, timeout, ExpireType.Absolute);
        }

        public static void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            _SetCache(key, value, timeout, expireType);
        }

        private static void _SetCache(string key, object value, TimeSpan? timeout, ExpireType? expireType)
        {
            if (timeout == null)
                HttpRuntime.Cache[key] = value;
            else
            {
                if (expireType == ExpireType.Absolute)
                {
                    DateTime endTime = DateTime.Now.AddTicks(timeout.Value.Ticks);
                    HttpRuntime.Cache.Insert(key, value, null, endTime, Cache.NoSlidingExpiration);
                }
                else
                {
                    HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, timeout.Value);
                }
            }
        }
    }
    /// <summary>
    /// 过期类型
    /// </summary>
    public enum ExpireType
    {
        /// <summary>
        /// 绝对过期
        /// 注：即自创建一段时间后就过期
        /// </summary>
        Absolute,

        /// <summary>
        /// 相对过期
        /// 注：即该键未被访问后一段时间后过期，若此键一直被访问则过期时间自动延长
        /// </summary>
        Relative,
    }
}