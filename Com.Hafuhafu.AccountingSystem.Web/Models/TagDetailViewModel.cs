using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Hafuhafu.AccountingSystem.Web.Models
{
    /// <summary>
    /// 标签明细视图模型
    /// </summary>
    public class TagDetailViewModel
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        public string TagID { get; set; }
        
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagInfo { get; set; }

        /// <summary>
        /// 涉及金额 单位（分）
        /// </summary>
        public int Amount { get; set; }
    }
}