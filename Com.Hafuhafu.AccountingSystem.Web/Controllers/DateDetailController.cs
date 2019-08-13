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
    public class DateDetailController : ApiController
    {
        public DetailService DetailService { get; }
        public DateDetailController()
        {
            DetailService = new DetailService();
        }

        /// <summary>
        /// 获取日期明细
        /// </summary>
        /// <param name="start">起始日期（含）e.g 2019-08-10</param>
        /// <param name="end">结束日期（含）e.g 2019-08-13</param>
        /// <returns></returns>
        [HttpGet]
        public DateDetailViewModel Getaaa(DateTime start, DateTime end)
        {
            var result = new DateDetailViewModel();
            var details = DetailService.GetAll(d => d.Date >= start.Date && d.Date <= end.Date, d => d.Date,
                SqlSugar.OrderByType.Desc);

            result.Expend = details.Where(d => d.Type == Type.支出).OrderByDescending(d => d.Date).ToList();
            result.Income = details.Where(d => d.Type == Type.收入).OrderByDescending(d => d.Date).ToList();

            return result;
        }
    }
}