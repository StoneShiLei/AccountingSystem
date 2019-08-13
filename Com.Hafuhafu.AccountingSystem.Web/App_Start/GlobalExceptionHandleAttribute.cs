using Com.Hafuhafu.AccountingSystem.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace Com.Hafuhafu.AccountingSystem.Web
{
    /// <summary>
    /// api全局异常处理
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class GlobalExceptionHandleAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            var apiResult = new ApiResultModel()
            {
                Status = ResultStatus.异常,
                Message = actionExecutedContext.Exception.Message,
                Body = null
            };

            string result = JsonConvert.SerializeObject(apiResult);

            actionExecutedContext.Response.Content = new StringContent(result, Encoding.UTF8);

            base.OnException(actionExecutedContext);
        }
    }
}