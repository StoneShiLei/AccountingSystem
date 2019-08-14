using Com.Hafuhafu.AccountingSystem.Application;
using Com.Hafuhafu.AccountingSystem.Domain.Model;
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
    public class NoteDetailController : ApiController
    {
        public DetailService DetailService { get; }
        public NoteDetailController()
        {
            DetailService = new DetailService();
        }

        /// <summary>
        /// 获取备注信息支出收入明细
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public NoteDetailViewModel GetNoteDetails(DateTime start, DateTime end)
        {
            var result = new NoteDetailViewModel();
            var details =  DetailService.GetAll(d => d.Date >= start.Date && d.Date <= end.Date, d => d.Date,SqlSugar.OrderByType.Desc);

            //按照支出收入进行分类且将支出类型明细的涉及金额变为负数
            result.Expend = details.Where(d => d.Type == Type.支出).Select(d =>
            {
                var model = new NoteDetailItem()
                {
                    Amount = d.Amount > 0 ? -d.Amount : d.Amount,
                    Note = d.Note,
                    TagID = d.TagID.ToString(),
                    TagInfo = d.TagInfo,
                    Type = d.Type
                };
                return model;
            }).ToList();
            result.Income = details.Where(d => d.Type == Type.收入).Select(d => 
            {
                var model = new NoteDetailItem()
                {
                    Amount = d.Amount,
                    Note = d.Note,
                    TagID = d.TagID.ToString(),
                    TagInfo = d.TagInfo,
                    Type = d.Type
                };
                return model;
            }).ToList();

            return result;
        }
    }
}