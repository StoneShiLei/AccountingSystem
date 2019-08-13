using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Com.Hafuhafu.AccountingSystem.Web.Models
{
    /// <summary>
    /// 响应实体
    /// </summary>
    public class ApiResultModel
    {
        /// <summary>
        /// 状态代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
    }
}