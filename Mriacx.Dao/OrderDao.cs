using Dapper;
using Mriacx.Entity;
using Mriacx.Entity.Model;
using Mriacx.Utility.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Mriacx.Dao
{
    public class OrderDao: BaseDao<OrderQueue>
    {
        ConfigInfoDao configDao = new ConfigInfoDao();

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
        /// 根据订单号获取orderNum
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public OrderQueue GetOrderQueueByOrderNum(string orderNum)
        {
            var sql = "select * from OrderQueue where OrderNum=@OrderNum order by CreateTime desc;";
            var item = DbContext.QuerySingle<OrderQueue>(sql, new { OrderNum = orderNum });
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfoByOrderNum(string orderNum) 
        {
            var sql = "select  * from OrderInfo where HQID=@OrderNum;";
            var item = DbContext.QuerySingle<OrderInfo>(sql, new { OrderNum = orderNum });
            return item;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderModel> GetOrderQueueAndInfoList(int type)
        {
            var wheresql = string.Empty;
            if (type == 0)
            {
                wheresql = " and Status>0 ";
            }
            var sql = "select oq.OrderNum,oq.CreateTime,oq.Status,oq.Num,oi.HQID,oi.FontType,oq.OrderType," +
                "oi.Content,oi.SignName,oi.CoName,oi.CoAddr,oi.CoTelPhone,oc.Text as  OrderTypeText " +
                "from OrderQueue oq inner join OrderInfo oi on oq.OrderNum = oi.HQID " +
                  " left join ConfigInfo oc  on oq.OrderType  = oc.Name " +
                    "where oc.Type ='OrderType' ";
            sql = sql + wheresql;
            var list = DbContext.Query<OrderModel>(sql).ToList();
            return list;
        }

        /// <summary>
        /// 完成订单并挪到OrderHistory
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public BaseMessage MoveOrderToHistory(string orderNum)
        {
            BaseMessage response = new BaseMessage() { IsSuccess = true };
            if (string.IsNullOrWhiteSpace(orderNum))
            {
                response.IsSuccess = false;
                response.Msg = "参数不能为空";
                return response;
            }
            var item = DbContext.GetList<OrderQueue>("where OrderNum = @OrderNum", new { OrderNum = orderNum }).FirstOrDefault();
            if (item == null || item.Id <= 0)
            {
                response.IsSuccess = false;
                response.Msg = $"未找到订单：{orderNum}";
                return response;
            }

            using (var trans = DbContext.BeginTransaction())
            {
                var delQueueSql = "delete from OrderQueue where OrderNum = @OrderNum"  ;
                var intQueueResult = DbContext.Execute(delQueueSql, new { OrderNum = orderNum }, trans);
                var delInfoSql = "delete from OrderInfo where HQID =@OrderNum";
                var intInfoResult = DbContext.Execute(delInfoSql, new { OrderNum = orderNum }, trans);
                if (intQueueResult > 0 && intInfoResult > 0)
                {
                    var insertSql = "insert into OrderHistory (OrderNum,CreateTime,Status,Num) values (@OrderNum,@CurrentTime,@Status,@Num);";
                    var insResult = DbContext.Execute(insertSql, new { OrderNum = orderNum, CurrentTime = DateTime.Now, Status = item.Status, Num = item.Num }, trans);
                    trans.Commit();
                }
                else {
                    trans.Rollback();
                }
            }
            response.Msg = "执行成功";
            return response;
        }

        /// <summary>
        /// 根据OrderNum获取订单实体
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public OrderModel GetOrderModel(string orderNum)
        {
            var sql = "select oq.OrderNum,oq.CreateTime,oq.Status,oq.Num,oq.OrderType,oi.HQID,oi.FontType," +
               "oi.Content,oi.SignName,oi.CoName,oi.CoAddr,oi.CoTelPhone,oc.Text as  OrderTypeText " +
               "from OrderQueue oq inner join OrderInfo oi on oq.OrderNum = oi.HQID " +
               "left join ConfigInfo oc  on oq.OrderType  = oc.Name "+
               "where oq.OrderNum = @OrderNum and oc.Type ='OrderType';";
            var item = DbContext.Query<OrderModel>(sql, new { OrderNum = orderNum }).FirstOrDefault();
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseMessage CreateOrderAndInfo(OrderModel model)
        {
            BaseMessage respose = new BaseMessage();
            var stock = configDao.GetOrderStock();
            if (stock.Remain - model.Num < 0)
            {
                respose.Msg = "剩余库存不足";
                return respose;
            }

            var createTime = DateTime.Now;
            OrderQueue orderQueue = new OrderQueue()
            {
                CreateTime = createTime,
                Num = model.Num,
                Status = 0,
                OrderNum = GuidCreator.GetUniqueKey("INK"),
                OrderType =model.OrderType
            };

            OrderInfo info = new OrderInfo()
            {
                CoAddr = model.CoAddr,
                CoName = model.CoName,
                Content = model.Content??"",
                CoTelPhone = model.CoTelPhone,
                FontType = model.FontType,
                HQID = orderQueue.OrderNum,
                SignName = model.SignName
            };

            using (var trans = DbContext.BeginTransaction())
            {
                try
                {
                    stock.Remain -= model.Num;
                    DbContext.Update<OrderStock>(stock,trans);
                    DbContext.Insert(orderQueue, trans);
                    DbContext.Insert(info, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    respose.Msg = "异常" + ex.ToString();
                }
            }
            respose.IsSuccess = true;
            respose.Msg = "创单成功";
            return respose;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <returns></returns>
        public BaseMessage DeleteOrder(string orderNum)
        {
            BaseMessage response = new BaseMessage();
            var orderqueue = this.GetOrderQueueByOrderNum(orderNum);
            var orderinfo = this.GetOrderInfoByOrderNum(orderNum);
            var stock = configDao.GetOrderStock();
            using (var trans = DbContext.BeginTransaction())
            {
                try
                {
                    stock.Remain += orderqueue.Num;
                    DbContext.Update<OrderStock>(stock, trans);
                    DbContext.Delete(orderqueue, trans);
                    DbContext.Delete(orderinfo, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    response.Msg = "异常" + ex.ToString();
                }
            }
            response.IsSuccess = true;
            response.Msg = "删除成功";
            return response;

        }

        #endregion
    }
}
