
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Utility;
namespace Jwt_Net
{
    /// <summary>
    /// 截获 apiControler的异常
    /// </summary>
    public class HttpExceptionFilter : System.Web.Http.Filters.IExceptionFilter
    {
        public bool AllowMultiple => true; 
        public async Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            await Task.Run(()=>
            {

                if (actionExecutedContext == null)
                {

                    return;
                }
                if (actionExecutedContext.Exception == null)
                {
                    return;
                }

                //记录actionExecutedContext.Exception
                string controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
                string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                string method = actionExecutedContext.Request.Method.ToString(); 
                string Location= $@" controllerName：{controllerName}/{actionName} Method：{method}";
                string msg = actionExecutedContext.Exception.Message; 
            }); 
        } 
    }
}