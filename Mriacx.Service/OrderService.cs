using Mriacx.Dao;
using Mriacx.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mriacx.Service
{
    public class OrderService
    {
        OrderDao orderDao = new OrderDao();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderQueue> GetList()
        {
          return  orderDao.GetList();
        }
    }
}
