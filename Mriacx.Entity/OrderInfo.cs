using System;
using System.Net.Sockets;

namespace Mriacx.Entity
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderInfo
    {
        
        public long Id { get;set; }

        /// <summary>
        /// 关联的订单号
        /// </summary>
        public string HQID { get; set; } 

        /// <summary>
        /// 字体
        /// </summary>
        public string FontType { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string SignName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CoName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CoAddr { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string CoTelPhone { get; set; }

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
