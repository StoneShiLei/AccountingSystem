using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Hafuhafu.AccountingSystem.Domain.Model
{
    [SugarTable("T_Detail")]
    public class Detail:BaseModel
    {
        /// <summary>
        /// 明细日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 收支类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 标签ID
        /// </summary>
        public Guid TagID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagInfo { get; set; }
        /// <summary>
        /// 明细内容
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 账户ID
        /// </summary>
        public Guid AccountID { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 涉及金额 单位：分
        /// </summary>
        public int Amount { get; set; }

        public Detail() : base() { }
    }
}
