using Dapper;
using Mriacx.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mriacx.Dao
{
    public class OrderDao: BaseDao<OrderQueue>
    {
        #region OrderQueue
        /// <summary>
        /// 获取所有订单列表
        /// </summary>
        public List<OrderQueue> GetList()
        {
            var list = DbContext.GetList<OrderQueue>().ToList();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oQueue"></param>
        /// <returns></returns>
        public int UpdateOrder(OrderQueue oQueue)
        {
           return  DbContext.Update(oQueue);
        } 

        #endregion
    }
}
