using Com.Hafuhafu.AccountingSystem.Application;
using Com.Hafuhafu.AccountingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Hafuhafu.AccountingSystem.Web.Controllers
{
    /// <summary>
    /// 账户Account 操作接口
    /// </summary>
    public class AccountController : ApiController
    {
        public AccountService AccountService { get; } 
        public AccountController()
        {
            AccountService = new AccountService();
        }


        /// <summary>
        /// 查询所有账户
        /// </summary>
        /// <returns></returns>
        public IList<Account> Get()
        {
            return AccountService.GetAll();
        }

       /// <summary>
       /// 根据id查询账户
       /// </summary>
       /// <param name="id">账户id</param>
       /// <returns></returns>
        public Account Get(string id)
        {
            return AccountService.Get(a => a.ID == new Guid(id));
        }

        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="account">（ignore：ID,AddDateTime）</param>
        public Account Post(Account account)
        {
            var newAccount = new Account()
            {
                AccountName = account.AccountName,
                Balance = account.Balance
            };
            bool result = AccountService.Add(newAccount);

            if (!result)
            {
                throw new Exception("数据创建失败");
            }
            else
            {
                return newAccount;
            }
        }

        /// <summary>
        /// 修改账户
        /// </summary>
        /// <param name="account">（ignore：ID,AddDateTime）</param>
        public bool Put(Account account)
        {
            bool result = AccountService.Update(a => new Account() { Balance = account.Balance, AccountName = account.AccountName }, a => a.ID == account.ID);
            if (!result)
            {
                throw new Exception("修改账户失败");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 根据id删除账户
        /// </summary>
        /// <param name="id">账户id</param>
        public bool Delete(string id)
        {
            bool result = AccountService.Remove(id);
            if (!result)
            {
                throw new Exception("删除账户失败");
            }
            else
            {
                return true;
            }
        }
    }
}