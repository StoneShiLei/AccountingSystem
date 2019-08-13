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
    /// Tag标签 操作接口
    /// </summary>
    public class TagController : ApiController
    {
        public TagService TagService { get; }
        public TagController()
        {
            TagService = new TagService();
        }
        
        /// <summary>
        /// 获取所有标签
        /// </summary>
        /// <returns></returns>
        public IList<Tag> Get()
        {
            return TagService.GetAll();
        }

        /// <summary>
        /// 根据id获取标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns></returns>
        public Tag Get(string id)
        {
            return TagService.Get(a => a.ID == new Guid(id));
        }

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="tag">（ignore：ID,AddDateTime）</param>
        public Tag Post(Tag tag)
        {
            var newTag = new Tag()
            {
                TagInfo = tag.TagInfo
            };

            bool result = TagService.Add(newTag);

            if (!result)
            {
                throw new Exception("数据创建失败");
            }
            else
            {
                return newTag;
            }
        }

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="tag">（ignore：ID,AddDateTime）</param>
        public bool Put(Tag tag)
        {
            bool result = TagService.Update(a => new Tag() { TagInfo = tag.TagInfo }, a => a.ID == tag.ID);
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
        /// 根据id删除标签
        /// </summary>
        /// <param name="id">标签id</param>
        public bool Delete(string id)
        {
            bool result = TagService.Remove(id);
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