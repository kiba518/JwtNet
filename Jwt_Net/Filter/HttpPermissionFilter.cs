
using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Utility;
namespace Jwt_Net
{
    /// <summary>
    /// 截获 apiControler的action
    /// </summary>
    public class HttpPermissionFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string url ="请求Url" + actionContext.Request.RequestUri.ToString();
            #region 日志记录输入参数
            try
            {
                if (actionContext.Request.Method == HttpMethod.Get)
                {
                    string getStr = "";
                    var arList = actionContext.ActionArguments;
                    foreach (var ar in arList)
                    {
                        getStr += ar.ToString();
                    }
                    Logger.Info("Get请求参数" + getStr);
                }
                if (actionContext.Request.Method == HttpMethod.Post)
                {
                    var arList = actionContext.ActionArguments;
                    var postStr = JsonConvert.SerializeObject(arList);
                    Logger.Info("Post请求参数" + postStr);
                }
            }
            catch
            {
            }
            #endregion

            var action = actionContext.ActionDescriptor.ActionName.ToLower();
            var controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName.ToLower();
            if (controller != "login" && controller != "loginout")
            { 
                //客户端段token获取
                var token = actionContext.Request.Headers.Authorization != null ? actionContext.Request.Headers.Authorization.ToString() : "";
                //服务端获取token 与客户端token进行比较
                if (!token.IsNullOrEmpty() && AuthenticationHelper.CheckAuth(token))
                {
                    //认证通过，可进行日志等处理 
                }
                else
                {

                    throw new Exception("Token无效");
                }

            }
        }
        
        public override async void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        { 
            var oc = actionExecutedContext.Response.Content as System.Net.Http.ObjectContent;
            var json = oc.Value.ToJson();
            Logger.Info($"{actionExecutedContext.Request.Method.Method}请求返回值{json}");
            base.OnActionExecuted(actionExecutedContext);
        }

        public static void SetHttpRequestCacheControl(HttpActionContext context)
        {
            int second = 365 * 24 * 60 * 60;
            context.Response.Headers.Add("Cache-Control", new[] { "public,max-age=" + second });
            context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123
        }
        public static void SetHttpResponseAuthorization(HttpActionContext context)
        { 
            context.Response.Headers.Add("Authorization", new[] { "Authorization" }); 
        }
        //actionExecutedContext.Response.Content.Headers
        //ContentType = "application/json";
        //actionExecutedContext.Response.StatusCode;
    }
}