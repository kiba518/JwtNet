using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utility;

namespace Jwt_Net.Controllers
{
    public class RequestController : ApiController
    {
        // GET api/values
        public string Get()
        {
            return "请求成功";
        } 
      
    }
}
