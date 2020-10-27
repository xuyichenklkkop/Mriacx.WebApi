using Dapper;
using Mriacx.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mriacx.Dao
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigInfoDao:BaseDao<ConfigInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OrderStock GetOrderStock()
        {
            return DbContext.GetList<OrderStock>().FirstOrDefault();
        }
    }
}
