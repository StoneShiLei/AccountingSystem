using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Web.Http;
using System.Security.Principal;
using System.Threading;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using Com.Hafuhafu.AccountingSystem.Web.Models;

namespace Com.Hafuhafu.AccountingSystem.Web
{
    public class ApiAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)
            {
                base.OnAuthorization(actionContext);
                return;
            }

            if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                base.OnAuthorization(actionContext);
                return;
            }

            string authParameter = null;

            var authValue = actionContext.Request.Headers.Authorization;
            if (authValue != null && authValue.Scheme == "Basic")
            {
                authParameter = authValue.Parameter;  //authparameter:获取请求中经过Base64编码的（用户：密码）
            }

            if (string.IsNullOrEmpty(authParameter))
            {
                Challenge(actionContext);
                return;
            }

            authParameter = Encoding.Default.GetString(Convert.FromBase64String(authParameter));

            var authToken = authParameter.Split(':');
            if (authToken.Length < 2)
            {
                Challenge(actionContext);
                return;
            }

            if (!ValidateUser(authToken[0], authToken[1]))
            {
                Challenge(actionContext);
                return;
            }

            var principal = new GenericPrincipal(new GenericIdentity(authToken[0]), null);
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            base.OnAuthorization(actionContext);
        }

        /// <summary>
        /// 返回质询
        /// </summary>
        /// <param name="actionContext"></param>
        private void Challenge(HttpActionContext actionContext)
        {
            ApiResultModel result = new ApiResultModel()
            {
                Code = "System_Unauthorized",
                ErrorMessage = "请求未授权，拒绝访问。",
                Success = false,
                Data = null
            };
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = ToJson(result);
            actionContext.Response.StatusCode = HttpStatusCode.Unauthorized;
            actionContext.Response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", string.Format("realm=\"{0}\"", host)));
        }

        /// <summary>
        /// 验证账号密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        protected virtual bool ValidateUser(string userName, string password)
        {
            return (userName.Equals("a", StringComparison.OrdinalIgnoreCase) && password.Equals("a")); //判断用户名及密码，实际可从数据库查询验证,可重写
        }

        protected static HttpResponseMessage ToJson(Object obj)
        {
            string str;
            if (obj is string || obj is char)//如果是字符串或字符直接返回
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