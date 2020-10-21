using Mriacx.Dao;
using Mriacx.Entity;
using Mriacx.Entity.Model;
using System;
using System.Collections;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderModel> GetOrderQueueAndInfoList()
        {
            return orderDao.GetOrderQueueAndInfoList();
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public BaseMessage MoveOrderToHistory(string orderNum)
        {
            return orderDao.MoveOrderToHistory(orderNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public int UpdateOrder(OrderQueue queue)
        {
            return orderDao.UpdateOrder(queue);
        }
    }
}
