using Com.Hafuhafu.AccountingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Hafuhafu.AccountingSystem.Web.Models
{
    /// <summary>
    /// 日期明细视图模型
    /// </summary>
    public class DateDetailViewModel
    {
        /// <summary>
        /// 支出类别明细
        /// </summary>
        public List<Detail> Expend { get; set; }

        /// <summary>
        /// 收入类别明细
        /// </summary>
        public List<Detail> Income { get; set; }
    }
}