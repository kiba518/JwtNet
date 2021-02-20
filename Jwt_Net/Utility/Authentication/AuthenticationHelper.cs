using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;

namespace Utility
{
    //Microsoft.Owin.Security.Jwt
    public class AuthenticationHelper
    { 
        /// <summary>
        /// 默认30分钟
        /// </summary>
        /// <param name="username"></param>
        public static void AddUserAuth(string username)
        {
            var token = JwtHelper.SetJwtEncode(username, 30);
            CacheHelper.SetCache(username, token, new TimeSpan(TimeSpan.TicksPerHour / 2));

        }

        public static void AddUserAuth(string username, TimeSpan ts)
        { 
            var token = JwtHelper.SetJwtEncode(username, ts.Minutes);
            CacheHelper.SetCache(username, token, ts);

        }
        public static string GetToken(string username)
        {
            var cachetoken = CacheHelper.GetCache(username);
            return cachetoken.ParseToString();  
        }

        public static bool CheckAuth(string token)
        {
            var dicInfo = JwtHelper.GetJwtDecode(token);
            var username = dicInfo["username"];

            var cachetoken = CacheHelper.GetCache(username.ToString());
            if (!cachetoken.IsNullOrEmpty() && cachetoken.ToString() == token)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

    }

}
