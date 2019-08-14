using Com.Hafuhafu.AccountingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Type = Com.Hafuhafu.AccountingSystem.Domain.Model.Type;


namespace Com.Hafuhafu.AccountingSystem.Web.Models
{
    /// <summary>
    /// 备注明细视图模型
    /// </summary>
    public class NoteDetailViewModel
    {
        /// <summary>
        /// 支出类别明细
        /// </summary>
        public List<NoteDetailItem> Expend { get; set; }

        /// <summary>
        /// 收入类别明细
        /// </summary>
        public List<NoteDetailItem> Income { get; set; }
    }

    public class NoteDetailItem
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
        /// 明细类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 涉及金额
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 详情备注
        /// </summary>
        public string Note { get; set; }
    }
}