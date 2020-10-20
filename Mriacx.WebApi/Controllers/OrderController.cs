﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mriacx.Entity;
using Mriacx.Service;

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
        public JsonResult GetOrderQueueList()
        {
           var list = service.GetList();
            return Json(list);
        }

        /// <summary>
        /// 
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
