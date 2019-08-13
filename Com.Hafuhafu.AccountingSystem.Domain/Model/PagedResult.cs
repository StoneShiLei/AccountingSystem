using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Hafuhafu.AccountingSystem.Domain.Model
{
    public class PagedResult<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页尺寸
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总计
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 分页集
        /// </summary>
        public List<T> PagedList { get; set; }
    }
}
