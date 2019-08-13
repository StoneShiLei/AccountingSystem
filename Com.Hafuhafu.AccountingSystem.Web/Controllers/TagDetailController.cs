using Com.Hafuhafu.AccountingSystem.Application;
using Com.Hafuhafu.AccountingSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Type = Com.Hafuhafu.AccountingSystem.Domain.Model.Type;


namespace Com.Hafuhafu.AccountingSystem.Web.Controllers
{
    public class TagDetailController : ApiController
    {
        public DetailService DetailService { get; }
        public TagDetailController()
        {
            DetailService = new DetailService();
        }

        /// <summary>
        /// 获取标签分类明细
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet]
        public List<TagDetailViewModel> GetTagDetail(DateTime start, DateTime end)
        {
            var details = DetailService.GetAll(d => d.Date >= start.Date && d.Date <= end.Date).ToList();

            //将支出类型的明细涉及金额变为负数
            details = details.Select(d =>
            {
                d.Amount = d.Type == Type.支出 ? Math.Abs(d.Amount) : d.Amount;
                return d;
            }).ToList();

            //按标签分组
            var groups = from d in details
                         group d by d.TagID into g
                         select new TagDetailViewModel()
                         {
                             Amount = g.Sum(d => d.Amount),
                             TagID = g.Select(d => d.TagID).Distinct().FirstOrDefault().ToString(),
                             TagInfo = g.Select(d => d.TagInfo).Distinct().FirstOrDefault().ToString()
                         };
            List<TagDetailViewModel> results = groups.OrderBy(d => d.Amount).ToList();

            return results;
        }
    }
}