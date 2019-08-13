using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Com.Hafuhafu.AccountingSystem.Domain.Model
{
    public abstract class BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDateTime { get; set; }

        public BaseModel()
        {
            ID = Guid.NewGuid();
            AddDateTime = DateTime.Now;
        }
    }
}
