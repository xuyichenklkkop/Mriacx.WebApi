using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mriacx.Entity;
using Mriacx.Entity.Model;
using Mriacx.Service;
using Newtonsoft.Json;

namespace Mriacx.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        OrderService service = new OrderService();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<OrderQueue> GetOrderQueueList()
        {
            var list = service.GetList();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>getorderqueueandinfolist
        [HttpGet]
        public List<OrderModel> GetOrderQueueAndInfoList()
        {
            return service.GetOrderQueueAndInfoList();
        }

        /// <summary>
        /// 根据OrderNum获取订单
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        [HttpGet]
        public OrderModel GetOrderModel(string orderNum)
        {
            return service.GetOrderModel(orderNum);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseMessage CreateOrderAndInfo([FromBody]OrderModel model)
        {
            return service.CreateOrderAndInfo(model);
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public BaseMessage MoveOrderToHistory(string orderNum)
        {
            return service.MoveOrderToHistory(orderNum);
        }

        /// <summary>
        /// 完成订单并挪到OrderHistory
        /// </summary>
        /// <param name="orderQueue"></param>
        /// <returns></returns>
        public JsonResult UpdateOrder(OrderQueue orderQueue)
        {
            var result = service.UpdateOrder(orderQueue);
            return Json(result);
        }

        

    }
}
