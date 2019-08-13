using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Hafuhafu.AccountingSystem.Domain.Dao
{
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(ISqlSugarClient context) : base(context)
        {
        }
    }
}
