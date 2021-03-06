﻿using Mriacx.Dao;
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
        ConfigInfoDao configDao = new ConfigInfoDao();


        /// <summary>
        /// 获取所有OrderQueue
        /// </summary>
        /// <returns></returns>
        public List<OrderQueue> GetList()
        {
          return  orderDao.GetList();
        }

        /// <summary>
        /// 根据订单号获取orderNum
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public OrderQueue GetOrderQueueByOrderNum(string orderNum)
        {
            return orderDao.GetOrderQueueByOrderNum(orderNum);
        }

        /// <summary>
        /// 审核功能
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public BaseMessage AuditOrderStatus(string orderNum,int status)
        {
            BaseMessage response = new BaseMessage()
            {
                IsSuccess = false
            };
            var order = orderDao.GetOrderQueueByOrderNum(orderNum);
            if (order == null || order.Id <= 0)
            {
                response.Msg = "未找到订单";
                return response;
            }
            if (order.Status >= 2)
            {
                response.Msg = "该订单无法通过审核变更";
                return response;
            }
            order.Status = status;
            var intResult = orderDao.UpdateOrder(order);
            if (intResult > 0)
            {
                response.IsSuccess = true;
                response.Msg = "更新成功";
            }
            else
            {
                response.Msg = "更新失败";
            }
            return response;
        }


        #region OrderModel(两表联查)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderModel> GetOrderQueueAndInfoList(int type)
        {
            return orderDao.GetOrderQueueAndInfoList(type);
        }

        /// <summary>
        /// 根据OrderNum获取订单
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public OrderModel GetOrderModel(string orderNum)
        {
            return orderDao.GetOrderModel(orderNum);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseMessage CreateOrderAndInfo(OrderModel model)
        {
            BaseMessage response = new BaseMessage();
            response = orderDao.CreateOrderAndInfo(model);
            return response;
        } 
        #endregion

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
        /// 修改订单状态
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public int UpdateOrder(string orderNum, int status)
        {
            var order = orderDao.GetOrderQueueByOrderNum(orderNum);
            if (order == null || order.Id <= 0)
            {
                return 0;
            }
            order.Status = status;
            return orderDao.UpdateOrder(order);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public BaseMessage DeleteOrder(string orderNum)
        {
            return orderDao.DeleteOrder(orderNum);
        }

    }
}
