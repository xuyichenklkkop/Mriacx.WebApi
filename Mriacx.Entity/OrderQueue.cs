using System;
using System.Net.Sockets;

namespace Mriacx.Entity
{
    /// <summary>
    /// 订单队列
    /// </summary>
    public class OrderQueue
    {
        public long Id { get;set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderNum { get; set; } 

        public DateTime CreateTime { get; set; }

        /// <summary>
        ///  0:默认 1审核确认中 2：生产中
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
    }
}
