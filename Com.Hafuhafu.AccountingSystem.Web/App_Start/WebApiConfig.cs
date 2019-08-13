using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Com.Hafuhafu.AccountingSystem.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务


            //跨域请求
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //注册过滤器
            //config.Filters.Add(new GlobalExceptionHandleAttribute());
            config.Filters.Add(new GlobalActionAttribute());


            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //设置返回json
            if (Configuration.TEST)
            {
                config.Formatters.Clear();
                config.Formatters.Add(new JsonMediaTypeFormatter());
            }
            else
            {
                var jsonFormatter = new JsonMediaTypeFormatter();
                config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
            }

        }
    }
}
