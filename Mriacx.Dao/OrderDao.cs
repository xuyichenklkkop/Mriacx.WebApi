using Dapper;
using Mriacx.Entity;
using Mriacx.Entity.Model;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderModel> GetOrderQueueAndInfoList()
        {
            var sql = "select oq.OrderNum,oq.CreateTime,oq.Status,oq.Num,oi.HQID,oi.FontType," +
                "oi.Content,oi.SignName,oi.CoName,oi.CoAddr,oi.CoTelPhone " +
                "from OrderQueue oq inner join OrderInfo oi on oq.OrderNum = oi.HQID";
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
            var item = DbContext.GetList<OrderQueue>("where OrderNum = @OrderNum", new { OrderNum = orderNum }).FirstOrDefault();
            if (item == null || item.Id <= 0)
            {
                response.IsSuccess = false;
                response.Msg = $"未找到订单：{orderNum}";
                return response;
            }
            using (DbContext)
            {
                DbContext.Open();
                var trans = DbContext.BeginTransaction();
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
        #endregion
    }
}
