using Com.Hafuhafu.AccountingSystem.Domain.Dao;
using Com.Hafuhafu.AccountingSystem.Domain.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Hafuhafu.AccountingSystem.Application
{
    public abstract class BaseService<T> where T:BaseModel,new()
    {
        public DbContext DbContext { get; }

        public BaseService()
        {
            this.DbContext = new DbContext();
        }

        #region 增

        /// <summary>
        /// 根据实体插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(T model)
        {
            return DbContext.Db.Insertable(model).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 根据实体 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool Add(List<T> models)
        {
            return DbContext.Db.Insertable(models).ExecuteCommand() > 0;
        }

        #endregion

        #region 删

        /// <summary>
        /// 根据主键批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Remove(dynamic[] ids)
        {
            return DbContext.Db.Deleteable<T>(ids).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(dynamic id)
        {
            return DbContext.Db.Deleteable<T>(id).ExecuteCommand() > 0;
        }

        #endregion

        #region 改

        /// <summary>
        /// 根据表达式更新
        /// </summary>
        /// <param name="updateCol">需要更新的列和数据</param>
        /// <param name="whereCol">where条件</param>
        /// <returns></returns>
        public bool Update(Expression<Func<T,T>> updateCol,Expression<Func<T,bool>> whereCol)
        {
            return DbContext.Db.Updateable<T>().SetColumns(updateCol).Where(whereCol).ExecuteCommand() > 0;
        }

        #endregion

        #region 查

        /// <summary>
        /// 查询一组数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="type">排序类型</param>
        /// <returns></returns>
        public IList<T> GetAll(Expression<Func<T, bool>> @where = null, Expression<Func<T, dynamic>> orderBy = null,OrderByType type = OrderByType.Desc)
        {
            return DbContext.Db.Queryable<T>().WhereIF(where != null, where).OrderByIF(orderBy != null,orderBy,type).ToList();
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> @where)
        {
            return DbContext.Db.Queryable<T>().Where(where).First();
        }

        #endregion

        #region 分页

        /// <summary>
        /// 分页查询一组数据
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="type">排序类型</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public PagedResult<T> Paging(int pageIndex, int pageSize, Expression<Func<T, dynamic>> orderBy, OrderByType type = OrderByType.Desc, Expression<Func<T, bool>> @where = null)
        {
            int totalCount = 0;
            var result = new PagedResult<T>();
            result.PagedList = DbContext.Db.Queryable<T>().OrderBy(orderBy, type).WhereIF(where != null, where).ToPageList(pageIndex, pageSize, ref totalCount);
            result.TotalCount = totalCount;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;

            return result;
        }


        #endregion
    }
}
