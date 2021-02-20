
using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Utility;
namespace Jwt_Net
{
    /// <summary>
    ///  截获 MVC的Controler的action
    /// </summary>
    public class MvcPermissionFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {

           
        }
    }
}