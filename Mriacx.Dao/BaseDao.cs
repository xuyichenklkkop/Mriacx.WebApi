using Dapper;
using Mriacx.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Mriacx.Dao
{
    public class BaseDao
    {
        public IDbConnection DbContext { get; }

        public BaseDao()
        {
            DbContext = DbContextFactory.GetDbContext();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDao<T> : BaseDao where T : class, new()
    {
        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllList()
        {
            return DbContext.GetList<T>().ToList();
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public T Get(long pk)
        {
            return DbContext.Get<T>(pk);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Update<T>(T t)
        {
            return DbContext.Update(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Delete<T>(T t) 
        {
            return DbContext.Delete(t);
        }
    }
}
