
using System.Net.Http;
using System.Runtime.Remoting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Utility;
namespace Jwt_Net
{
    /// <summary>
    /// 截获 MVC的Controler的异常
    /// </summary>
    public class MvcExceptionFilter : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            HttpResponseBase resp = filterContext.RequestContext.HttpContext.Response; 
            //指定要跳转的路径页（必须全部在Shared下）
            ViewResult vr = null;

            if (filterContext.Exception is ServerException)
            {
                resp.StatusCode = 500;
                vr = new ViewResult { ViewName = "500Error" };
                filterContext.Result = vr;
                //filterContext.HttpContext.Response.WriteFile("~/HttpError/500.html");
            }
            else
            {
                resp.StatusCode = 404;
                vr = new ViewResult { ViewName = "404Error" };
                filterContext.Result = vr;
                //filterContext.HttpContext.Response.WriteFile("~/HttpError/404.html");
            }
            //设置异常已经处理,否则会被其他异常过滤器覆盖
            filterContext.ExceptionHandled = true;
            //在派生类中重写时，获取或设置一个值，该值指定是否禁用IIS自定义错误。
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}