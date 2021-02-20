using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utility;

namespace Jwt_Net.Controllers
{
    public class LoginController : ApiController
    { 
        /// <summary>
        /// GET api/values http://localhost:50525/api/Login/?username=k&pwd=123
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string Get(string username,string pwd)
        {
            AuthenticationHelper.AddUserAuth(username, new TimeSpan(TimeSpan.TicksPerMinute * 5));//5分钟
            //AuthenticationHelper.AddUserAuth(username, new TimeSpan(TimeSpan.TicksPerMinute / 60));//1分钟
            //AuthenticationHelper.AddUserAuth(username, new TimeSpan(TimeSpan.TicksPerMinute / 2));//30分钟
            //AuthenticationHelper.AddUserAuth(username);

            string token = AuthenticationHelper.GetToken(username);
            return token;
        } 
      
    }
}
