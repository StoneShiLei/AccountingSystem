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
    /// <summary>
    /// 明细条目Detail 操作接口
    /// </summary>
    public class DetailController : ApiController
    {
        public DetailService DetailService { get; }
        public TagService TagService { get; }
        public AccountService AccountService { get; set; }

        public DetailController()
        {
            DetailService = new DetailService();
            TagService = new TagService();
            AccountService = new AccountService();
        }

        /// <summary>
        /// 查询所有明细
        /// </summary>
        /// <returns></returns>
        public IList<Detail> Get()
        {
            return DetailService.GetAll();
        }

        /// <summary>
        /// 根据id查询明细
        /// </summary>
        /// <param name="id">账户id</param>
        /// <returns></returns>
        public Detail Get(string id)
        {
            return DetailService.Get(a => a.ID == new Guid(id));
        }

        /// <summary>
        /// 创建一个明细
        /// </summary>
        /// <param name="detail">（ignore：ID,AddDateTime,TagInfo,AccountName）</param>
        /// <returns></returns>
        public Detail Post(Detail detail)
        {
            var tag = TagService.Get(d => d.ID == detail.TagID);
            if (tag == null) throw new Exception("TagID 不存在");

            var account = AccountService.Get(a => a.ID == detail.AccountID);
            if (account == null) throw new Exception("AccountID 不存在");

            var newDetail = new Detail()
            {
                AccountID = account.ID,
                AccountName = account.AccountName,
                TagID = tag.ID,
                TagInfo = tag.TagInfo,
                Type = detail.Type,
                Date = detail.Date.Date,
                Amount = detail.Amount,
                Note = detail.Note
            };

            bool result = DetailService.Add(newDetail);
            if (!result)
            {
                throw new Exception("数据创建失败");
            }
            else
            {
                return newDetail;
            }
        }

        /// <summary>
        /// 修改一个明细
        /// </summary>
        /// <param name="detail">（ignore：ID,AddDateTime,TagInfo,AccountName）</param>
        /// <returns></returns>
        public bool Put(Detail detail)
        {
            var tag = TagService.Get(d => d.ID == detail.TagID);
            if (tag == null) throw new Exception("TagID 不存在");

            var account = AccountService.Get(a => a.ID == detail.AccountID);
            if (account == null) throw new Exception("AccountID 不存在");

            bool result = DetailService.Update(a => new Detail()
            {
                AccountID = account.ID,
                AccountName = account.AccountName,
                Amount = detail.Amount,
                Date = detail.Date.Date,
                Note = detail.Note,
                TagID = tag.ID,
                TagInfo = tag.TagInfo,
                Type = detail.Type
            }, 
            a => a.ID == detail.ID);


            if (!result)
            {
                throw new Exception("修改明细失败");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 删除一个明细
        /// </summary>
        /// <param name="id">明细id</param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            bool result = DetailService.Remove(id);
            if (!result)
            {
                throw new Exception("删除明细失败");
            }
            else
            {
                return true;
            }
        }
    }
}