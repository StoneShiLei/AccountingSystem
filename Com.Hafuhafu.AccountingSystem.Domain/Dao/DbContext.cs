using Com.Hafuhafu.AccountingSystem.Domain.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Hafuhafu.AccountingSystem.Domain.Dao
{
    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["AccountingSystem"].ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });
        }
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public DbSet<Detail> DetailDb { get { return new DbSet<Detail>(Db); } }//用来处理Detail表的常用操作
        public DbSet<Account> AccountDb { get { return new DbSet<Account>(Db); } }//用来处理Account表的常用操作
        public DbSet<Tag> TagDb { get { return new DbSet<Tag>(Db); } }//用来处理Tag表的常用操作
    }
}
