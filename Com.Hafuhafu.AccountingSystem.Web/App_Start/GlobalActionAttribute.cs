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
using System.Web.Script.Serialization;

namespace Com.Hafuhafu.AccountingSystem.Web
{
    /// <summary>
    /// api全局返回
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class GlobalActionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 请求成功之后
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            // 不包裹返回值
            var noPackage = actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<NoPackageResult>();
            if (!noPackage.Any())
            {
                //初始化返回结果
                ApiResultModel result = new ApiResultModel();
                if (actionExecutedContext.Exception != null)
                {
                    result.Code = "System_Error";
                    result.Success = false;
                    result.ErrorMessage = actionExecutedContext.Exception.Message;
                }
                else
                {
                    // 取得由 API 返回的状态代码
                    result.Code = actionExecutedContext.ActionContext.Response.StatusCode.ToString();

                    var a = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>();
                    if (!a.IsFaulted)
                    {
                        // 取得由 API 返回的资料
                        result.Data = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;
                    }

                    //请求是否成功
                    result.Success = actionExecutedContext.ActionContext.Response.IsSuccessStatusCode;
                }


                //结果转为自定义消息格式
                HttpResponseMessage httpResponseMessage = ToJson(result);

                // 重新封装回传格式
                actionExecutedContext.Response = httpResponseMessage;
            }

        }

        private static HttpResponseMessage ToJson(Object obj)
        {
            String str;
            if (obj is String || obj is Char)//如果是字符串或字符直接返回
            {
                str = obj.ToString();
            }
            else//否则序列为json字串
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                str = serializer.Serialize(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
    }
}