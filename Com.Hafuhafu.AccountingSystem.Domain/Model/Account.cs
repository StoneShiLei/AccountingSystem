using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Com.Hafuhafu.AccountingSystem.Domain.Model
{
    [SugarTable("T_Account")]
    public class Account : BaseModel
    {
        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public int Balance { get; set; }

        public Account() : base() { }
    }
}
