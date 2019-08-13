using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Hafuhafu.AccountingSystem.Domain.Model
{
    [SugarTable("T_Tag")]
    public class Tag:BaseModel
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagInfo { get; set; }

        public Tag() : base() { }
    }
}
