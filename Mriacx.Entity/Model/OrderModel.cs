using System;
using System.Collections.Generic;
using System.Text;

namespace Mriacx.Entity.Model
{
    public class OrderModel
    {
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
        /// 公司电话
        /// </summary>
        public string CoTelPhone { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 订单品类
        /// </summary>
        public int OrderType { get; set; }

        public string OrderTypeText { get; set; }

    }
}
